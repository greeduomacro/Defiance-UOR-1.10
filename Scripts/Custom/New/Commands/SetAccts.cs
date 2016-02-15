using System;
using System.Collections.Generic;
using Server;
using Server.Network;
using Server.Mobiles;
using Server.Accounting;

namespace Server.Commands
{
	public class SetAccts
	{
		public static void Initialize()
		{
			CommandSystem.Register( "SetAccts", AccessLevel.Lead, new CommandEventHandler( SetAccts_OnCommand ) );
		}

		[Usage( "SetAccts" )]
		[Description( "Completely sets all accounts." )]
		private static void SetAccts_OnCommand( CommandEventArgs e )
		{
			List<Account> dellist = new List<Account>();

			List<Account> adminacctlist = new List<Account>();

			List<Mobile> gmlist = new List<Mobile>();
			List<Mobile> adminlist = new List<Mobile>();
			List<Mobile> seerlist = new List<Mobile>();

			foreach ( Account a in Accounts.GetAccounts() )
			{
				//if ( a.Length == 0 )
				//	dellist.Add( a );
				if ( a.AccessLevel >= AccessLevel.Lead )
					adminacctlist.Add( a );

				for ( int i = 0;i < a.Length; i++ )
				{
					if ( a[i] != null )
					{
						if ( a[i].AccessLevel >= AccessLevel.Lead )
							adminlist.Add( a[i] );
						else if ( a[i].AccessLevel >= AccessLevel.GameMaster )
							seerlist.Add( a[i] );
						else if ( a[i].AccessLevel >= AccessLevel.Seer )
							gmlist.Add( a[i] );
					}
				}
			}

			//for ( int i = dellist.Count - 1; i >= 0; i-- )
			//	dellist[i].Delete();

			for ( int i = 0; i < adminacctlist.Count; i++ )
				adminacctlist[i].AccessLevel = AccessLevel.Owner;

			for ( int i = 0; i < adminlist.Count; i++ )
				adminlist[i].AccessLevel = AccessLevel.Owner;

			for ( int i = 0; i < seerlist.Count; i++ )
				seerlist[i].AccessLevel = AccessLevel.Lead;

			for ( int i = 0; i < gmlist.Count; i++ )
				gmlist[i].AccessLevel = AccessLevel.GameMaster;

			e.Mobile.SendMessage( "Completed." );
		}
	}
}