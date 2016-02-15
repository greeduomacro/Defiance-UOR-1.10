using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a blood elemental corpse" )]
	public class BloodElemental : BaseCreature
	{
		public override string DefaultName{ get{ return "a blood elemental"; } }

		[Constructable]
		public BloodElemental () : base( AIType.AI_Mage, FightMode.Weakest, 10, 1, 0.2, 0.4 )
		{
			Body = 159;
			BaseSoundID = 278;

			SetStr( 529, 615 );
			SetDex( 68, 85 );
			SetInt( 226, 344 );

			SetDamage( 17, 27 );

			SetDamageType( ResistanceType.Physical, 0 );
			SetDamageType( ResistanceType.Poison, 50 );
			SetDamageType( ResistanceType.Energy, 50 );

			SetResistance( ResistanceType.Physical, 55, 65 );
			SetResistance( ResistanceType.Fire, 20, 30 );
			SetResistance( ResistanceType.Cold, 40, 50 );
			SetResistance( ResistanceType.Poison, 50, 60 );
			SetResistance( ResistanceType.Energy, 30, 40 );

			SetSkill( SkillName.EvalInt, 58.0, 80.6 );
			SetSkill( SkillName.Magery, 87.0, 99.4 );
			SetSkill( SkillName.Meditation, 22.1, 61.7 );
			SetSkill( SkillName.MagicResist, 80.9, 94.7 );
			SetSkill( SkillName.Tactics, 86.5, 99.4 );
			SetSkill( SkillName.Wrestling, 82.2, 99.4 );

			Fame = 12500;
			Karma = -12500;

			VirtualArmor = 60;

			if ( Utility.Random( 500 ) == 0 )
				PackItem( new RuinedDrawers() );

			if ( 0.01 > Utility.RandomDouble() )
				PackItem( new BloodPentagramPart( Utility.RandomMinMax(14,18) ) );
		}

		public override void OnDeath( Container c )
	  	{
			if ( Utility.Random( 75 ) <  1 )
				c.DropItem( new BasicBlueCarpet( PieceType.SWCorner ) );

			base.OnDeath( c );
	  	}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich );
			AddLoot( LootPack.Rich );
		}

		public override int TreasureMapLevel{ get{ return 5; } }

		public BloodElemental( Serial serial ) : base( serial )
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