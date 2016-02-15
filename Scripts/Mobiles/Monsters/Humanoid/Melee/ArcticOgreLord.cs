using System;
using System.Collections;
using Server.Items;
using Server.Targeting;

namespace Server.Mobiles
{
	[CorpseName( "a frozen ogre lord corpse" )]
	[TypeAlias( "Server.Mobiles.ArticOgreLord" )]
	public class ArcticOgreLord : BaseCreature
	{
		public override string DefaultName{ get{ return "an arctic ogre lord"; } }

		[Constructable]
		public ArcticOgreLord() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 135;
			BaseSoundID = 427;

			SetStr( 858, 914 );
			SetDex( 67, 75 );
			SetInt( 52, 59 );

			SetDamage( 25, 30 );

			SetDamageType( ResistanceType.Physical, 30 );
			SetDamageType( ResistanceType.Cold, 70 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Cold, 60, 70 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 40, 50 );

			SetSkill( SkillName.MagicResist, 125.1, 135.4 );
			SetSkill( SkillName.Tactics, 93.1, 97.0 );
			SetSkill( SkillName.Wrestling, 93.1, 98.2 );

			Fame = 15000;
			Karma = -15000;

			VirtualArmor = 50;

			if ( Utility.Random( 150 ) == 0 )
				PackItem( new SpecialBeardDye() );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
		}

		public override Poison PoisonImmune{ get{ return Poison.Deadly; } }
		public override int TreasureMapLevel{ get{ return 3; } }

		public ArcticOgreLord( Serial serial ) : base( serial )
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