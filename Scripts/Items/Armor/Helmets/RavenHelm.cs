using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2B71, 0x3168 )]
	public class RavenHelm : BaseArmor
	{
		private bool m_IsDonationItem;

		[CommandProperty( AccessLevel.GameMaster, AccessLevel.Administrator )]
		public bool IsDonationItem{ get{ return m_IsDonationItem; } set{ m_IsDonationItem = value; } }

		public override Race RequiredRace { get { return m_IsDonationItem ? null : Race.Elf; } }

		public override int BasePhysicalResistance{ get{ return 5; } }
		public override int BaseFireResistance{ get{ return 1; } }
		public override int BaseColdResistance{ get{ return 2; } }
		public override int BasePoisonResistance{ get{ return 2; } }
		public override int BaseEnergyResistance{ get{ return 5; } }

		public override int InitMinHits{ get{ return m_IsDonationItem ? 0 : 50; } }
		public override int InitMaxHits{ get{ return m_IsDonationItem ? 0 : 65; } }

		public override int AosStrReq{ get{ return 25; } }
		public override int OldStrReq{ get{ return 25; } }

		public override int ArmorBase{ get{ return m_IsDonationItem ? 10 : 40; } }

		public override ArmorMaterialType MaterialType{ get{ return m_IsDonationItem ? ArmorMaterialType.Leather : ArmorMaterialType.Plate; } }
		public override ArmorMeditationAllowance DefMedAllowance{ get{ return m_IsDonationItem ? ArmorMeditationAllowance.All : base.DefMedAllowance; } }

		[Constructable]
		public RavenHelm() : base( 0x2B71 )
		{
			Weight = 5.0;
		}

		public RavenHelm( Serial serial ) : base( serial )
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
			else if ( Name == "Ravens Headdress of Spiritry" )
			{
				ItemID = 0x2B71;
				m_IsDonationItem = true;
				Name = "Raven Headdress of Spirituality";
			}
		}
	}
}