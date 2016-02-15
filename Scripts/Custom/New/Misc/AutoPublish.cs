using System;
using System.IO;
using System.Diagnostics;
using System.Threading;
using System.CodeDom;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.Reflection;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Commands;
using Microsoft.CSharp;
using Microsoft.VisualBasic;

namespace Server.Misc
{
	public class AutoPublish
	{

		private static TimeSpan m_MaxBackup = TimeSpan.FromDays( 7.0 ); //No more than a week.
		private static TimeSpan m_Delay = TimeSpan.FromMinutes( 60.0 );
		private static TimeSpan m_Warning = TestCenter.Enabled ? TimeSpan.Zero : TimeSpan.FromSeconds( 60.0 );

		public static void Configure()
		{
			if ( File.Exists( PublishCFG ) )
			{
				using ( StreamReader ip = new StreamReader( PublishCFG ) )
				{
					string line;

					if ( (line = ip.ReadLine()) != null )
						PublishRevision = Utility.ToInt32( line );
				}
			}
//			PublishRevision = 1;
		}

		public static void Initialize()
		{
			CommandSystem.Register( "UpdateSVN", AccessLevel.Administrator, new CommandEventHandler( UpdateSVN_OnCommand ) );
			CommandSystem.Register( "PushPublish", AccessLevel.Administrator, new CommandEventHandler( PushPublish_OnCommand ) );
		}

		[Usage( "UpdateSVN" )]
		[Description( "Updates the SVN status." )]
		private static void UpdateSVN_OnCommand( CommandEventArgs e )
		{
			if ( Enabled )
			{
				e.Mobile.SendMessage( "Preparing SVN update..." );
				new SVNCommand( e.Mobile );
			}
			else
				e.Mobile.SendMessage( "AutoPublish is not available.  Please verify that Subversion is installed." );
		}

		[Usage( "PushPublish" )]
		[Description( "Forces a push of a new publish, and restarts the server." )]
		private static void PushPublish_OnCommand( CommandEventArgs e )
		{
			if ( Enabled )
			{
				e.Mobile.SendMessage( "Preparing to forcefully publish the current code..." );
				new PublishCheck( true );
			}
			else
				e.Mobile.SendMessage( "AutoPublish is not available.  Please verify that Subversion is installed." );
		}

		public static readonly string PublishCFG = @"Data\Publish.cfg";
		public static int PublishRevision = 0;
		public static string SVNPath = @"C:\Program Files\Subversion\svn.exe";
		public static bool Enabled{ get{ return File.Exists( SVNPath ); } }
	}

	public class PublishCheck
	{
		private Thread m_Thread;
		private bool m_Force;

		public PublishCheck() : this( false )
		{
		}

		public PublishCheck( bool force )
		{
			m_Force = force;
			m_Thread = new Thread( new ThreadStart( Check ) );
			m_Thread.Name = "Server.PublishCheck";
			m_Thread.Priority = ThreadPriority.BelowNormal;
			m_Thread.Start();
		}

		public void Check()
		{
			Console.Write( "AutoPublish: Updating SVN..." );
			new SVNCommand( null, false );
			Console.WriteLine( "done." );

			int pubrev = AutoPublish.PublishRevision;

			if ( File.Exists( AutoPublish.PublishCFG ) )
			{
				using ( StreamReader ip = new StreamReader( AutoPublish.PublishCFG ) )
				{
					string line;

					if ( (line = ip.ReadLine()) != null )
						pubrev = Utility.ToInt32( line );
				}

				if ( m_Force || pubrev > AutoPublish.PublishRevision ) //New code to push!
				{
					if ( pubrev > AutoPublish.PublishRevision )
						Console.Write( "AutoPublish: Publish {0} found, creating compiler...", pubrev );
					else
						Console.Write( "AutoPublish: Publish forced, creating compiler..." );

					string serverpath = "NextPublish.Server.exe";
					bool failed = false;

					if ( File.Exists( serverpath ) )
						File.Delete( serverpath );

		#if Framework_3_5
					using ( CSharpCodeProvider provider = new CSharpCodeProvider( new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } } ) )
		#else
					using ( CSharpCodeProvider provider = new CSharpCodeProvider() )
		#endif
					{
						CompilerParameters parms = new CompilerParameters( new string[0], serverpath, Core.Debug );

						string defines = ScriptCompiler.GetDefines();

						if ( !String.IsNullOrEmpty( defines ) )
							parms.CompilerOptions = defines;

						if( Core.HaltOnWarning )
							parms.WarningLevel = 4;

						parms.GenerateExecutable = true; //Make an exe!
						parms.Win32Resource = "Server\runuo.ico";

		#if !MONO
						CompilerResults results = provider.CompileAssemblyFromFile( parms, ScriptCompiler.GetScripts( "Server", "*.cs" ) );
		#else
						parms.CompilerOptions = String.Format( "{0} /nowarn:169,219,414 /recurse:Server/*.cs", parms.CompilerOptions );
						CompilerResults results = provider.CompileAssemblyFromFile( parms, String.Empty );
		#endif
						//ScriptCompiler.Display( results );

		#if !MONO
						if ( results.Errors.Count > 0 )
							failed = true;
		#else
						if( results.Errors.Count > 0 )
						{
							foreach( CompilerError err in results.Errors )
							{
								if ( !err.IsWarning )
								{
									failed = true;
									break;
								}
							}
						}
		#endif
					}

					if ( !failed )
					{
						Console.WriteLine( "done." );
						Console.Write( "AutoPublish: Compiling scripts..." );
						ProcessStartInfo publishinfo = new ProcessStartInfo( serverpath );
						publishinfo.Arguments = "-publish";
						publishinfo.WindowStyle = ProcessWindowStyle.Hidden;
						publishinfo.CreateNoWindow = true;
						publishinfo.UseShellExecute = false;
						publishinfo.WorkingDirectory = Core.BaseDirectory;

						Process publishproc = new Process();
						publishproc.StartInfo = publishinfo;
						publishproc.Start();

						publishproc.WaitForExit( 600000 );

						if ( publishproc != null && !publishproc.HasExited )
						{
							Console.WriteLine( "failed. (timed out at 600s)" );
							publishproc.Kill();
						}
						else
							Console.WriteLine( "done." );

						if ( File.Exists( @"Scripts\Output\NextPublish.Scripts.CS.dll" ) && File.Exists( @"Scripts\Output\NextPublish.Scripts.CS.hash" ) )
						{
							if ( TestCenter.Enabled )
								SchedulePublish( TimeSpan.FromMinutes( 10.0 ) );
							else
								SchedulePublish( TimeSpan.FromHours( 48.0 ) );

							AutoPublish.PublishRevision = pubrev;
						}
					}
					else
						Console.WriteLine( "failed." );
				}
			}
		}

		public static void SchedulePublish( TimeSpan restartdelay )
		{
			if ( AutoRestart.Enabled )
				Console.WriteLine( "AutoPublish: Restart already scheduled for {0}.", AutoRestart.RestartTime.ToString() );
			else
			{
				while ( AutoRestart.WarningCount < AutoRestart.WarningIntervals.Length && TimeSpan.FromSeconds( AutoRestart.WarningIntervals[AutoRestart.WarningCount] ) > restartdelay )
					AutoRestart.WarningCount++;
				AutoRestart.Enabled = true;
				AutoRestart.RestartTime = DateTime.Now + restartdelay;
				Console.WriteLine( "AutoPublish: Publish scheduled for {0}.", AutoRestart.RestartTime.ToString() );
			}
		}
	}

	public class SVNCommand
	{
		private Mobile m_Mobile;
		private Thread m_Thread;

		public SVNCommand( Mobile m ) : this( m, true )
		{
		}

		public SVNCommand( Mobile m, bool threaded )
		{
			m_Mobile = m;

			if ( threaded )
			{
				m_Thread = new Thread( new ThreadStart( UpdateSVN ) );
				m_Thread.Name = "Server.SVNCommand";
				m_Thread.Priority = ThreadPriority.BelowNormal;
				m_Thread.Start();
			}
			else
				UpdateSVN();
		}

		public void UpdateSVN()
		{
			ProcessStartInfo startInfo = new ProcessStartInfo( AutoPublish.SVNPath );
			//startInfo.Arguments = String.Format( "update \"{0}\" --username \"dfib\" --password serversvn", Core.BaseDirectory );
			startInfo.Arguments = String.Format( "update \"{0}\"", Core.BaseDirectory );
			startInfo.RedirectStandardOutput = true;
			startInfo.WindowStyle = ProcessWindowStyle.Hidden;
			startInfo.CreateNoWindow = true;
			startInfo.UseShellExecute = false;
			if ( m_Mobile != null )
				m_Mobile.SendMessage( "Process Started" );

			Process readProcess = new Process();
			readProcess.StartInfo = startInfo;
			readProcess.Start();

			StreamReader reader = readProcess.StandardOutput;
			if ( m_Mobile != null )
			{
				string line;

				while ( (line = reader.ReadLine()) != null )
				{
					m_Mobile.SendMessage( "SVN: {0}", line );
				}

				m_Mobile.SendMessage( "Process Ended" );
			}
		}
	}
}