using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a terathan matriarch corpse" )]
	public class TerathanMatriarch : BaseCreature
	{
		public override string DefaultName{ get{ return "a terathan matriarch"; } }

		[Constructable]
		public TerathanMatriarch() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 72;
			BaseSoundID = 599;

			SetStr( 316, 405 );
			SetDex( 96, 115 );
			SetInt( 366, 455 );

			SetHits( 190, 243 );

			SetDamage( 11, 14 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 30, 40 );
			SetResistance( ResistanceType.Cold, 35, 45 );
			SetResistance( ResistanceType.Poison, 40, 50 );
			SetResistance( ResistanceType.Energy, 35, 45 );

			SetSkill( SkillName.EvalInt, 90.1, 150.0 );
			SetSkill( SkillName.Magery, 90.1, 100.0 );
			SetSkill( SkillName.MagicResist, 90.1, 100.0 );
			SetSkill( SkillName.Tactics, 50.1, 70.0 );
			SetSkill( SkillName.Wrestling, 60.1, 80.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 40;

			if ( Utility.Random( 1000 ) == 0 )
			{
				switch ( Utility.Random( 6 ) )
				{
					case 0: PackItem( new SpiderCarvingEast1() ); break;
					case 1: PackItem( new SpiderCarvingEast2() ); break;
					case 2: PackItem( new SpiderCarvingEast3() ); break;
					case 3: PackItem( new SpiderCarvingSouth1() ); break;
					case 4: PackItem( new SpiderCarvingSouth2() ); break;
					case 5: PackItem( new SpiderCarvingSouth3() ); break;
				}
			}

			if ( 0.10 > Utility.RandomDouble() )
				PackItem( new Bola() );

			PackItem( new SpidersSilk( 5 ) );
			PackNecroReg( Utility.RandomMinMax( 4, 10 ) );

			//if ( 0.01 > Utility.RandomDouble() )
			//	PackItem( new InvisHat() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average, 2 );
			AddLoot( LootPack.MedScrolls, 2 );
			AddLoot( LootPack.Potions );
		}

		public override int TreasureMapLevel{ get{ return 4; } }

		public override OppositionGroup OppositionGroup
		{
			get{ return OppositionGroup.TerathansAndOphidians; }
		}

		public TerathanMatriarch( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}