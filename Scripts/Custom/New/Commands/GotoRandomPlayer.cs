using System;
using System.Collections.Generic;
using Server;
using Server.Network;

namespace Server.Commands
{
	public class GRPCommand
	{
		public static void Initialize()
		{
			CommandSystem.Register( "grp", AccessLevel.Seer, new CommandEventHandler( GRP_OnCommand ) );
		}

		[Usage( "grp" )]
		[Description( "Go to a random staff player." )]
		private static void GRP_OnCommand( CommandEventArgs e )
		{
			Mobile from = e.Mobile;

			List<NetState> states = NetState.Instances;
			List<Mobile> mobiles = new List<Mobile>();

	    	if ( states.Count > 0 )
			{
				for ( int i = 0; i < states.Count; i++ )
				{
					Mobile m = states[i].Mobile;

					if ( m != null && m.AccessLevel == AccessLevel.Player )
						mobiles.Add( m );
				}

				if ( mobiles.Count > 0 )
				{
					Mobile target = mobiles[Utility.Random( mobiles.Count )];

					if ( target != null && target.Map != null && target.Map != Map.Internal )
						from.MoveToWorld( target.Location, target.Map );
	    		}
				else
					from.SendMessage("There are no players online which you can go to.");
			}
    	}
	}
}