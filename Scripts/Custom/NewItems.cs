using System;
using Server.Gumps;
using Server.Network;
using Server.Items;
using Server.Mobiles;

namespace Server.Items
{
	public class PaladinSword : BaseSword
	{
		public override WeaponAbility PrimaryAbility{ get{ return WeaponAbility.CrushingBlow; } }
		public override WeaponAbility SecondaryAbility{ get{ return WeaponAbility.ParalyzingBlow; } }

		public override int AosStrengthReq{ get{ return 55; } }
		public override int AosMinDamage{ get{ return 22; } }
		public override int AosMaxDamage{ get{ return 31; } }
		public override int AosSpeed{ get{ return 18; } }

		public override int OldStrengthReq{ get{ return 40; } }
		public override int OldMinDamage{ get{ return 6; } }
		public override int OldMaxDamage{ get{ return 34; } }
		public override int OldSpeed{ get{ return 30; } }

		public override int DefHitSound{ get{ return 0x237; } }
		public override int DefMissSound{ get{ return 0x23A; } }

		[Constructable]
		public PaladinSword() : base( 0x26CE )
		{
			Weight = 15.0;
		}

		public PaladinSword( Serial serial ) : base( serial )
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
	}

	public class EtherealRideGump : Gump
	{
		private EtherealDeed m_Deed;

		public EtherealRideGump ( EtherealDeed deed ) : base ( 220, 182 )
		{
			m_Deed = deed;
			int x = 30;
			int x2 = 65;
			int y = 35;
			int id = 1;

			AddPage ( 0 );

			AddImage( 0, 0, 0x820 );
			AddImage( 18, 30, 0x821);
			AddImage( 18, 85, 0x822 );
			AddImage( 18, 135, 0x823 );

			AddHtml( 60, 5, 275, 20, "<b>Choose your ethereal mount:</b>", false, false);

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
			AddLabel( x2, y, 0x480, "Horse" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
			AddLabel( x2, y, 0x480, "Ostard" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
			AddLabel( x2, y, 0x480, "Llama" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
			AddLabel( x2, y, 0x480, "Kirin" );
			id=id+1;

			if (!m_Deed.IsDonation)
			{
				y = 35;
				x = 150;
				x2 = 185;

				AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
				AddLabel( x2, y, 0x480, "Unicorn" );
				id=id+1;
				y=y+25;

				AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
				AddLabel( x2, y, 0x480, "Ridgeback" );
				id=id+1;
				y=y+25;

				AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
				AddLabel( x2, y, 0x480, "Beetle" );
				id=id+1;
				y=y+25;

				AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 );
				AddLabel( x2, y, 0x480, "Swamp Dragon" );
			}
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			Container bp = from.Backpack;

			if (m_Deed == null || m_Deed.Deleted)
				return;

			switch (info.ButtonID)
			{
				case 1:
				{
					bp.DropItem( new EtherealHorse() );
					break;
				}
				case 2:
				{
					bp.DropItem( new EtherealOstard() );
					break;
				}
				case 3:
				{
					bp.DropItem( new EtherealLlama() );
					break;
				}
				case 4:
				{
					bp.DropItem( new EtherealKirin() );
					break;
				}
				case 5:
				{
					bp.DropItem( new EtherealUnicorn() );
					break;
				}
				case 6:
				{
					bp.DropItem( new EtherealRidgeback() );
					break;
				}
				case 7:
				{
					bp.DropItem( new EtherealBeetle() );
					break;
				}
				case 8:
				{
					bp.DropItem( new EtherealSwampDragon() );
					break;
				}
			}
			m_Deed.Delete();
		}
	}

	public class EtherealDeed : Item
	{
		private bool m_IsDonation;

		public bool IsDonation
		{
			get{ return m_IsDonation; } set{ m_IsDonation = value; }
		}

		[Constructable]
		public EtherealDeed() : this(false)
		{
		}

		[Constructable]
		public EtherealDeed(bool donation) : base(0x14ef)
		{
			Weight = .5;
			Name = "ethereal deed";
			m_IsDonation = donation;
		}

		public EtherealDeed(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from ) && !from.HasGump( typeof(EtherealRideGump) ) )
				from.SendGump( new EtherealRideGump( this ) );
		}
	}

	[TypeAlias( "Server.Items.HairCuttingScissors" )]
	public class HairShears : Item
	{
		public override string DefaultName{ get{ return "a pair of hair shears"; } }

		[Constructable]
		public HairShears() : base( 0xF9F )
		{
			Weight = 1.0;
			Hue = 916;
		}

		public HairShears( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) && from.HairItemID > 0 )
			{
				from.HairItemID = 0;
				from.PlaySound( 0x248 );
			}
		}
	}

	[TypeAlias( "Server.Items.FacialHairCuttingScissors" )]
	public class FacialHairShears : Item
	{
		public override string DefaultName{ get{ return "a pair of facial hair shears"; } }

		[Constructable]
		public FacialHairShears() : base( 0xF9F )
		{
			Weight = 1.0;
			Hue = 921;
		}

		public FacialHairShears( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) && from.FacialHairItemID > 0 )
			{
				from.FacialHairItemID = 0;
				from.PlaySound( 0x248 );
			}
		}
	}

	public class HairGump : Gump
	{
		private Item m_Deed;

		public HairGump( Mobile from, Item deed ) : base( 120, 152 )
		{
			m_Deed = deed;
			int x = 30;
			int x2 = 65;
			int y = 35;
			int id = 1;

			from.CloseGump( typeof( HairGump ) );

			AddPage ( 0 );

			AddImage( 0, 0, 0x820 );
			AddImage( 18, 35, 0x821);
			AddImage( 18, 95, 0x822 );
			AddImage( 18, 155, 0x823 );

			AddHtml( 70, 5, 275, 20, "<b>Choose your hair style:</b>", false, false);

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Short Hair" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Long Hair" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Pony Tail" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Mohawk" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Pageboy" );
			id=id+1;

			y = 35;
			x = 150;
			x2 = 185;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Buns Hair" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Afro" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Receeding Hair" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "2 Pig-Tails" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Krisna Hair" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			Container bp = from.Backpack;

			switch ( info.ButtonID )
			{
				default: return;
				case 1: from.HairItemID = 0x203B; break;
				case 2: from.HairItemID = 0x203C; break;
				case 3: from.HairItemID = 0x203D; break;
				case 4: from.HairItemID = 0x2044; break;
				case 5: from.HairItemID = 0x2045; break;
				case 6: from.HairItemID = 0x2046; break;
				case 7: from.HairItemID = 0x2047; break;
				case 8: from.HairItemID = 0x2048; break;
				case 9: from.HairItemID = 0x2049; break;
				case 10: from.HairItemID = 0x204A; break;
			}

			from.PlaySound( 0x248 );
			m_Deed.Delete();
		}
	}

	public class HairDeed : Item
	{
		public override string DefaultName{ get{ return "a hair deed"; } }

		[Constructable]
		public HairDeed() : base( 0x14f0 )
		{
			Weight = .5;
			Hue = 921;
		}

		public HairDeed( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from ) )
			{
				from.SendGump( new HairGump( from, this ) );
			}
		}
	}

	public class FacialHairGump : Gump
	{
		private Item m_Deed;

		public FacialHairGump( Mobile from, Item deed ) : base( 120, 152 )
		{
			m_Deed = deed;
			int x = 25;
			int x2 = 60;
			int y = 35;
			int id = 1;

			from.CloseGump( typeof( FacialHairGump ) );

			AddPage ( 0 );

			AddImage( 0, 0, 0x820 );
			AddImage( 18, 30, 0x821);
			AddImage( 18, 85, 0x822 );
			AddImage( 18, 135, 0x823 );

			AddHtml( 50, 5, 275, 20, "<b>Choose your facial hair style:</b>", false, false);

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Long Beard" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Short Beard" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Goatee" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Mustache" );
			id=id+1;
			y=y+25;

			y = 35;
			x = 135;
			x2 = 170;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Med-Short Beard" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Med-Long Beard" );
			id=id+1;
			y=y+25;

			AddButton( x, y, 4023, 4025, id, GumpButtonType.Reply, 0 ); // height: 20; width: 34;
			AddLabel( x2, y, 0x480, "Vandyke" );
		}

		public override void OnResponse( NetState state, RelayInfo info )
		{
			Mobile from = state.Mobile;
			Container bp = from.Backpack;

			switch ( info.ButtonID )
			{
				default: return;
				case 1: from.FacialHairItemID = 0x203E; break;
				case 2: from.FacialHairItemID = 0x203F; break;
				case 3: from.FacialHairItemID = 0x2040; break;
				case 4: from.FacialHairItemID = 0x2041; break;
				case 5: from.FacialHairItemID = 0x204B; break;
				case 6: from.FacialHairItemID = 0x204C; break;
				case 7: from.FacialHairItemID = 0x204D; break;
			}

			from.PlaySound( 0x248 );
			m_Deed.Delete();
		}
	}

	public class FacialHairDeed : Item
	{
		public override string DefaultName{ get{ return "a facial hair deed"; } }

		[Constructable]
		public FacialHairDeed() : base( 0x14f0 )
		{
			Weight = .5;
			Hue = 921;
		}

		public FacialHairDeed( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from ) )
			{
				if ( from.Female )
					from.SendMessage( "You are a woman, and women do not grow facial hair!" );
				else
					from.SendGump( new FacialHairGump( from, this ) );
			}
		}
	}
}