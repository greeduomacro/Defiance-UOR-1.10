using System;
using Server.Commands;
using System.Collections.Generic;

namespace Server.Factions
{
	public class Generator
	{
		public static void Initialize()
		{
			CommandSystem.Register( "GenerateFactions", AccessLevel.Administrator, new CommandEventHandler( GenerateFactions_OnCommand ) );
			// jakob, added this command
			CommandSystem.Register( "ResetFactionTowns", AccessLevel.Administrator, new CommandEventHandler( ResetFactionTowns_OnCommand ) );
			// end
		}

		// jakob, added this command
		public static void ResetFactionTowns_OnCommand( CommandEventArgs e )
		{
			List<Sigil> sigils = Sigil.Sigils;
			foreach ( Sigil sigil in sigils )
			{
				Town town = sigil.Town;
				TownMonolith m = town.Monolith;
				m.Sigil = sigil;
				sigil.LastMonolith = null;
				sigil.Corrupting = null;
				sigil.CorruptionStart = DateTime.MinValue;
				town.Capture( null );
				sigil.Corrupted = null;
				sigil.PurificationStart = DateTime.MinValue;
			}

			e.Mobile.SendMessage( "Faction towns are now reset. ;-)" );
		}
		// end

		public static void GenerateFactions_OnCommand( CommandEventArgs e )
		{
			new FactionPersistence();

			List<Faction> factions = Faction.Factions;

			foreach ( Faction faction in factions )
				Generate( faction );

			List<Town> towns = Town.Towns;

			foreach ( Town town in towns )
				Generate( town );
		}

		public static void Generate( Town town )
		{
			Map facet = Faction.Facet;

			TownDefinition def = town.Definition;

			if ( !CheckExistance( def.Monolith, facet, typeof( TownMonolith ) ) )
			{
				TownMonolith mono = new TownMonolith( town );
				mono.MoveToWorld( def.Monolith, facet );
				mono.Sigil = new Sigil( town );
			}

			if ( !CheckExistance( def.TownStone, facet, typeof( TownStone ) ) )
				new TownStone( town ).MoveToWorld( def.TownStone, facet );
		}

		public static void Generate( Faction faction )
		{
			Map facet = Faction.Facet;

			List<Town> towns = Town.Towns;

			StrongholdDefinition stronghold = faction.Definition.Stronghold;

			if ( !CheckExistance( stronghold.JoinStone, facet, typeof( JoinStone ) ) )
				new JoinStone( faction ).MoveToWorld( stronghold.JoinStone, facet );

			if ( !CheckExistance( stronghold.FactionStone, facet, typeof( FactionStone ) ) )
				new FactionStone( faction ).MoveToWorld( stronghold.FactionStone, facet );

			for ( int i = 0; i < stronghold.Monoliths.Length; ++i )
			{
				Point3D monolith = stronghold.Monoliths[i];

				if ( !CheckExistance( monolith, facet, typeof( StrongholdMonolith ) ) )
					new StrongholdMonolith( towns[i], faction ).MoveToWorld( monolith, facet );
			}
		}

		private static bool CheckExistance( Point3D loc, Map facet, Type type )
		{
			foreach ( Item item in facet.GetItemsInRange( loc, 0 ) )
			{
				if ( type.IsAssignableFrom( item.GetType() ) )
					return true;
			}

			return false;
		}
	}
}