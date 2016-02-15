using System;
using Server;

namespace Server.Items
{
	public class EagleHelm : BaseArmor
	{
		private bool m_IsDonationItem;

		[CommandProperty( AccessLevel.GameMaster, AccessLevel.Administrator )]
		public bool IsDonationItem{ get{ return m_IsDonationItem; } set{ m_IsDonationItem = value; } }

		public override int InitMinHits{ get{ return 0; } }
		public override int InitMaxHits{ get{ return 0; } }

		public override int AosStrReq{ get{ return 40; } }
		public override int OldStrReq{ get{ return 10; } }

		public override int ArmorBase{ get{ return 10; } }

		public override ArmorMaterialType MaterialType{ get{ return ArmorMaterialType.Leather; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return ArmorMeditationAllowance.All; } }

		[Constructable]
		public EagleHelm() : base( 11121 )
		{
			Weight = 1.0;
		}

		public EagleHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 1 ); // version

			writer.Write( m_IsDonationItem );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();

			if ( version > 0 )
				m_IsDonationItem = reader.ReadBool();
			else if ( Name == "Eagles Crest of Lore" )
				m_IsDonationItem = true;
		}
	}
}