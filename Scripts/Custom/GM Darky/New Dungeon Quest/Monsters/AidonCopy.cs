using System;
using Server.Misc;
using Server.Items;
using Server.Spells;
using Server.Spells.Seventh;
using Server.Spells.Fifth;
using Server.Engines.CannedEvil;
using Server.Engines.Quests.Doom;

namespace Server.Mobiles
{
	public class AidonCopy : BaseCreature
	{
		public override string DefaultName{ get{ return "Aidon the Archwizard"; } }

		[Constructable]
		public AidonCopy():base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.15, 0.2 )
		{
			Body = 400;
			Hue = 0x3F6;

			SetStr( 356, 396 );
			SetDex( 125, 135 );
			SetInt( 830, 953 );

			SetDamage( 15, 20 );

			SetSkill( SkillName.Wrestling, 91.3, 97.8 );
			SetSkill( SkillName.Tactics, 91.5, 97.0 );
			SetSkill( SkillName.MagicResist, 140.6, 156.8);
			SetSkill( SkillName.Magery, 96.7, 99.8 );
			SetSkill( SkillName.EvalInt, 75.1, 80.1 );
			SetSkill( SkillName.Meditation, 61.1, 68.1 );

			Fame = 17500;
			Karma = -17500;

			VirtualArmor = 15;

			AddItem( Rehued( new Robe(), 2112 ) );
			AddItem( Immovable( Rehued( new SavageMask(), 1175 ) ) );
			AddItem( Immovable( Rehued( new Sandals(), 1175 ) ) );
			AddItem( Immovable( Rehued( new GoldRing(), 1360 ) ) );
			AddItem( Immovable( Rehued( new GoldRing(), 1360 ) ) );

			HairItemID = 0x203B;
			HairHue = 1072;

			FacialHairItemID = 0x203E;
			FacialHairHue = 1072;

			switch ( Utility.Random( 10 ) )
			{
				case 0: PackItem( new GreaterAgilityPotion() ); break;
				case 1: PackItem( new GreaterExplosionPotion() ); break;
				case 2: PackItem( new GreaterCurePotion() ); break;
				case 3: PackItem( new GreaterHealPotion() ); break;
				case 4: PackItem( new NightSightPotion() ); break;
				case 5: PackItem( new GreaterPoisonPotion() ); break;
				case 6: PackItem( new TotalRefreshPotion() ); break;
				case 7: PackItem( new GreaterStrengthPotion() ); break;
			}

			switch ( Utility.Random( 20 ) )
			{
				case 0: PackWeapon( 2, 5 ); break;
				case 1: PackArmor( 2, 5 ); break;
			}

			switch ( Utility.Random( 10 ) )
			{
				case 0: PackWeapon( 0, 5 ); break;
				case 1: PackArmor( 0, 5 ); break;
			}

			switch ( Utility.Random( 5 ) )
			{
				case 0: PackWeapon( 0, 5 ); break;
				case 1: PackArmor( 0, 5 ); break;
			}

			switch ( Utility.Random( 2 ) )
			{
				case 0: PackWeapon( 0, 5 ); break;
				case 1: PackArmor( 0, 5 ); break;
			}

			switch ( Utility.Random( 5 ) )
			{
				case 0: PackReg( 11 ); break;
				case 1: PackScroll( 4, 8 ); break;
				case 2: PackScroll( 6, 7 ); break;
				case 3: PackReg( 12 ); break;
				case 4: PackReg( 10 ); break;
			}

			PackGold( 500, 1000 );

		}

		public override bool AlwaysMurderer{ get{ return true; } }
		public override bool CanRummageCorpses{ get{ return true; } }
		public override bool ShowFameTitle{ get{ return false; } }

		public AidonCopy( Serial serial ) : base( serial )
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

		public override void OnDeath( Container c )
		{
			switch( Utility.Random(10) )
			{
				case 0: c.DropItem( new FameIounStone() ); break;
				case 1: c.DropItem( new KarmaIounStone() ); break;
			}

			base.OnDeath( c );
		}
	}
}