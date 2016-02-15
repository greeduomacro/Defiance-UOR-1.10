using System;
using Server.Mobiles;
using Server.Items;
using Server.Multis.Deeds;
using Xanthos.Evo;

namespace Server.Items
{
	[DynamicFliping]
	[Flipable( 0x9A8, 0xE80 )]
	public class TamerDonationBox : MetalBox
	{
		[Constructable]
		public TamerDonationBox()
		{
			Weight = 1.0;
			Hue = 1278;
			Name = "Defiance Beast Handler Box";

			PlaceItemIn( 16, 50, new PetSkillBall( 10 ) );
			PlaceItemIn( 16, 65, new PetSkillBall( 20 ) );
			PlaceItemIn( 16, 80, new PetSkillBall( 30 ) );
			PlaceItemIn( 16, 95, new PetSkillBall( 40 ) );
			//PlaceItemIn( 16, 110, (item = new PetSkillBall( 50 )) );

			PlaceItemIn( 160, 50, new PetSkillBall( 10 ) );
			PlaceItemIn( 160, 65, new PetSkillBall( 20 ) );
			PlaceItemIn( 160, 80, new PetSkillBall( 30 ) );
			PlaceItemIn( 160, 95, new PetSkillBall( 40 ) );
			//PlaceItemIn( 160, 110, (item = new PetSkillBall( 50 )) );

			Item item = null;

			switch ( Utility.Random( 3 ) )
			{
				case 0: item = new VultureHelm(); item.Weight = 1.0; item.Name = "Vultures Serrated Beak"; ((VultureHelm)item).IsDonationItem = true; break;
				case 1: item = new EagleHelm(); item.Weight = 1.0; item.Name = "Eagles Crest of Lore"; ((EagleHelm)item).IsDonationItem = true; break;
				case 2: item = new RavenHelm(); item.Weight = 1.0; item.Name = "Raven Headdress of Spirituality"; ((RavenHelm)item).IsDonationItem = true; break;
			}

			PlaceItemIn( 91, 57, item );

			item.Hue = Utility.RandomList(1150, 1151, 1154, 1153, 1281);
			item.LootType = LootType.Blessed;
			//item.Name = "Beast Handler's Helm";

			PlaceItemIn( 66, 56, (item = new GracedPetSummonBall()) );
			PlaceItemIn( 28, 60, (item = new RaelisDragonEgg()) );
			PlaceItemIn( 114, 60, (item = new HiryuEvoEgg()) );

			PlaceItemIn( 34, 83, (item = new PetBondDeed()) );
			item.Hue = 1158;

			PlaceItemIn( 48, 83, (item = new EvoPointsDeed()) );
			//item.Hue = 2401;

			PlaceItemIn( 64, 83, (item = new EvoPointsDeed()) );
			item.Hue = 1158;

			PlaceItemIn( 80, 83, (item = new EvoPointsDeed()) );
			//item.Hue = 2401;

			PlaceItemIn( 98, 83, (item = new EvoPointsDeed()) );
			item.Hue = 1158;

			PlaceItemIn(114, 83, (item = new MembershipTicket()));
			item.Hue = 1278;

			((MembershipTicket)item).MemberShipTime = TimeSpan.FromDays(730);
		}

		public TamerDonationBox( Serial serial ) : base( serial )
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
}