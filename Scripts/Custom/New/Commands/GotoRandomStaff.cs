using System;
using System.Collections.Generic;
using Server;
using Server.Network;

namespace Server.Commands
{
	public class GRSCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "grs", AccessLevel.GameMaster, new CommandEventHandler( GRS_OnCommand ) );
		}

		[Usage( "grs" )]
		[Description( "Go to a random staff member." )]
		private static void GRS_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			List<NetState> states = NetState.Instances;
			List<Mobile> mobiles = new List<Mobile>();

	    	if ( states.Count > 0 )
			{
				for ( int i = 0; i < states.Count; i++ )
				{
					Mobile m = states[i].Mobile;

					if ( m != null && m.AccessLevel > AccessLevel.Player && from.AccessLevel > m.AccessLevel )
						mobiles.Add( m );
				}

				if ( mobiles.Count > 0 )
				{
					Mobile target = mobiles[Utility.Random( mobiles.Count )];

					if ( target != null && target.Map != null && target.Map != Map.Internal )
						from.MoveToWorld( target.Location, target.Map );
	    		}
				else
					from.SendMessage("There are no staff members online which you can go to.");
			}
    	}
	}
}