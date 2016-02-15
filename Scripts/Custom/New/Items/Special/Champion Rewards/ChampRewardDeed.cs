using System;
using Server.Network;
using Server.Mobiles;
using Server.Gumps;
using Xanthos.Evo;

namespace Server.Items
{
	public class ChampionRewardDeed : Item
	{
		public override string DefaultName{ get{ return "a champion reward deed"; } }

		[Constructable]
		public ChampionRewardDeed() : base( 0x14F0 )
		{
			Weight = 2.0;
			LootType = LootType.Cursed;
			Hue = 2964;
		}

		public ChampionRewardDeed( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else
				from.SendGump( new InternalGump( from, this ) );
		}

		private class InternalGump : Gump
		{
			private Mobile m_From;
			private ChampionRewardDeed m_Ticket;

			public InternalGump( Mobile from, ChampionRewardDeed ticket ) : base( 50, 50 )
			{
				m_From = from;
				m_Ticket = ticket;

				AddBackground( 0, 0, 400, 385, 0xA28 );

				AddHtml( 30, 45, 340, 70, "You have defeated the vile evil. For your courageousness you may choose one of the following:", true, false );

				AddButton( 46, 128, 0xFA5, 0xFA7, 1, GumpButtonType.Reply, 0 );
				AddHtml( 80,129,240,24,"a 225 stat ball",true,false );

				AddButton( 46, 163, 0xFA5, 0xFA7, 4, GumpButtonType.Reply, 0);
				AddHtml( 80, 164, 240, 24, "a skin tone deed", true, false );

				AddButton( 46, 198, 0xFA5, 0xFA7, 2, GumpButtonType.Reply, 0 );
				AddHtml( 80, 199, 240, 24, "a promotional token for a soulstone fragment", true, false );

				AddButton( 46, 233, 0xFA5, 0xFA7, 3, GumpButtonType.Reply, 0 );
				AddHtml( 80, 234, 240, 24,"an ethereal beetle", true, false );

				AddButton( 46, 269, 0xFA5, 0xFA7, 5, GumpButtonType.Reply, 0 );
				AddHtml( 80, 269, 240, 24,"an unhatched spider egg", true, false );

				AddButton( 120, 310, 0xFA5, 0xFA7, 0, GumpButtonType.Reply, 0 );
				AddHtmlLocalized( 154, 312, 100, 35, 1011012, false, false ); // CANCEL
			}

			public override void OnResponse( NetState sender, RelayInfo info )
			{
				if ( m_Ticket == null || m_Ticket.Deleted || m_From == null || !m_Ticket.IsChildOf( m_From.Backpack ) )
					return;

				Item item = null;

				switch ( info.ButtonID )
				{
					default: case 0: return;
					case 1: item = new StatsBall(); break;
					case 2: item = new PromotionalToken(); break;
					case 3: item = new EtherealBeetle(); break;
					case 4: item = new SkinToneDeed(); break;
					case 5: item = new EvoSpiderEvoEgg(); break;
				}

				if ( item != null )
				{
					m_Ticket.Delete();
					m_From.AddToBackpack( item );
				}
			}
		}
	}
}