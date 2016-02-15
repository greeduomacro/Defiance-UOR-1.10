using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an acid elemental corpse" )]
	public class ToxicElemental : BaseCreature
	{
		public override double DispelDifficulty{ get{ return 98.5; } }
		public override double DispelFocus{ get{ return 65.0; } }
		public override string DefaultName{ get{ return "a toxic elemental"; } }

		[Constructable]
		public ToxicElemental () : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 0x9E;
			BaseSoundID = 278;

			SetStr( 126, 152 );
			SetDex( 166, 185 );
			SetInt( 102, 121 );

			SetDamageType( ResistanceType.Physical, 25 );
			SetDamageType( ResistanceType.Fire, 50 );
			SetDamageType( ResistanceType.Energy, 25 );

			SetResistance( ResistanceType.Physical, 45, 55 );
			SetResistance( ResistanceType.Fire, 40, 50 );
			SetResistance( ResistanceType.Cold, 20, 30 );
			SetResistance( ResistanceType.Poison, 10, 20 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.Wrestling, 66, 82 );
			SetSkill( SkillName.Tactics, 62, 78 );
			SetSkill( SkillName.MagicResist, 63, 74 );
			SetSkill( SkillName.Magery, 67, 78 );

			Fame = 10000;
			Karma = -10000;

			VirtualArmor = 40;

			if ( Utility.Random( 500 ) == 0 )
				PackItem( new RuinedBooks() );
		}

		public override void OnDeath( Container c )
	  	{
			if ( Utility.Random( 75 ) <  1 )
				c.DropItem( new BasicBlueCarpet( PieceType.SouthEdge ) );

			base.OnDeath( c );
	  	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.Rich );
			AddLoot( LootPack.Average );
		}

		public override bool BleedImmune{ get{ return true; } }
		public override Poison HitPoison{ get{ return Poison.Deadly; } }
		public override double HitPoisonChance{ get{ return 0.6; } }
		public override int TreasureMapLevel{ get{ return Core.AOS ? 2 : 3; } }

		public ToxicElemental( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 263 )
				BaseSoundID = 278;

			if ( Body == 13 )
				Body = 0x9E;

			if ( Hue == 0x4001 )
				Hue = 0;
		}
	}
}