using System;
using System.Collections;
using Server.Multis;
using Server.Mobiles;

namespace Server.Items
{
	public class SummoningChest : Container
	{
		public override int DefaultMaxWeight{ get{ return 0; } }

		public override int DefaultGumpID{ get{ return 0x3D; } }
		public override int DefaultDropSound{ get{ return 0x48; } }

		public override Rectangle2D Bounds
		{
			get{ return new Rectangle2D( 29, 34, 108, 94 ); }
		}


		[Constructable]
		public SummoningChest() : base( 0xE77 )
		{
			Name = "Summoning Chest";
			Hue = 0x3B2;
			Movable = false;
		}

		public SummoningChest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public override bool OnDragDrop( Mobile from, Item dropped )
		{
			if ( !base.OnDragDrop( from, dropped ) )
				return false;

			if ( dropped is Idol )
			{
				if ( TotalItems == 6 )
				{
					Summon();
				}
				else
				{
					dropped.Movable = false;
				}

				return true;
			}
			else
			{
				from.SendMessage( "You cannot sacrifice that item!" );

				return false;
			}
		}

		public override bool OnDragDropInto( Mobile from, Item item, Point3D p )
		{
			if ( !base.OnDragDropInto( from, item, p ) )
				return false;

			if ( item is Idol )
			{
				if ( TotalItems == 6 )
				{
					Summon();
				}
				else
				{
					item.Movable = false;
				}

				return true;
			}
			else
			{
				from.SendMessage( "You cannot sacrifice that item!" );

				return false;
			}
		}

		private Mobile m_Champ;

		public void Summon()
		{
			int randomise = Utility.Random( 7 );

			switch ( randomise )
			{
				case 0: m_Champ = new ShameChamp(); break;
				case 1: m_Champ = new DecChamp(); break;
				case 2: m_Champ = new DestChamp(); break;
				case 3: m_Champ = new HythChamp(); break;
				case 4: m_Champ = new DespChamp(); break;
				case 5: m_Champ = new CoveChamp(); break;
				case 6: m_Champ = new WrongChamp(); break;
			}

			m_Champ.MoveToWorld( this.Location, this.Map );

			ArrayList items = new ArrayList();

			if ( items.Count > 0 )
			{
				PublicOverheadMessage( Network.MessageType.Emote, 1161, true, String.Format( "*Consumes the Idols and summons a dungeon champion*" ) );

				for ( int i = items.Count - 1; i >= 0; --i )
				{
					if ( i >= items.Count )
						continue;

					((Item)items[i]).Delete();
				}
			}
		}
	}
}