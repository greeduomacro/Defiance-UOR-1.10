using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an efreet corpse" )]
	public class Efreet : BaseCreature
	{
		public override string DefaultName{ get{ return "an efreet"; } }

		[Constructable]
		public Efreet () : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Body = 131;
			BaseSoundID = 768;

			SetStr( 326, 354 );
			SetDex( 271, 278 );
			SetInt( 177, 195 );

			SetDamage( 12, 15 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 50, 60 );
			SetResistance( ResistanceType.Fire, 60, 70 );
			SetResistance( ResistanceType.Poison, 30, 40 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.EvalInt, 60.1, 75.0 );
			SetSkill( SkillName.Magery, 67.1, 77.0 );
			SetSkill( SkillName.MagicResist, 61.1, 74.0 );
			SetSkill( SkillName.Tactics, 61.1, 79.0 );
			SetSkill( SkillName.Wrestling, 64.1, 79.0 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 56;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
			AddLoot( LootPack.Gems );

			if ( Utility.Random( 500 ) == 0 )
			{
				switch ( Utility.Random( 5 ) )
				{
					case 0:	PackItem( new DaemonArms() );	break;
					case 1:	PackItem( new DaemonChest() );	break;
					case 2:	PackItem( new DaemonGloves() );	break;
					case 3:	PackItem( new DaemonLegs() );	break;
					case 4:	PackItem( new DaemonHelm() );	break;
				}
			}
		}

		public override int TreasureMapLevel{ get{ return 3; } }

		public Efreet( Serial serial ) : base( serial )
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