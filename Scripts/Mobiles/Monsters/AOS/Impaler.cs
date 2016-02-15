using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "an impaler corpse" )]
	public class Impaler : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.MortalStrike : WeaponAbility.BleedAttack;
		}

		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		[Constructable]
		public Impaler() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "impaler" );
			Body = 306;
			BaseSoundID = 0x2A7;

			SetStr( 2190 );
			SetDex( 45 );
			SetInt( 190 );

			SetHits( 10000 );

			SetDamage( 31, 35 );

			SetDamageType( ResistanceType.Physical, 100 );

			SetResistance( ResistanceType.Physical, 90 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, 75 );
			SetResistance( ResistanceType.Poison, 60 );
			SetResistance( ResistanceType.Energy, 100 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.Meditation, 120.0 );
			SetSkill( SkillName.Poisoning, 160.0 );
			SetSkill( SkillName.MagicResist, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 80.0 );

			Fame = 24000;
			Karma = -24000;

			VirtualArmor = 49;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 7 );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return (0.8 >= Utility.RandomDouble() ? Poison.Greater : Poison.Deadly); } }
		public override int TreasureMapLevel{ get{ return 1; } }

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
				DemonKnight.DistributeArtifact( this );

			if ( 0.2 > Utility.RandomDouble() )
			{
				int r = Utility.Random( 100 );
				Item drop = null;

				if 		( r > 70 ) drop = new BloodPentagramPart( Utility.Random( 5 ) );
				else if	( r > 60 ) drop = new MetalChest();
				else if	( r > 50 ) drop = new DecorativeAxeNorthDeed();
				else if	( r > 40 ) drop = new BrownBearRugSouthDeed();
				else if	( r > 30 ) drop = new BrownBearRugEastDeed();
				else if	( r > 20 ) drop = new StackedArrows();
				else if	( r > 10 ) drop = new BronzeIngots();
				else if	( r > 5 ) drop = new StackedShafts();
				else if	( r > 1 ) drop = new RareFeathers();
				else
					drop = new ClothingBlessDeed();

				c.DropItem( drop );
			}
		}

		public Impaler( Serial serial ) : base( serial )
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

			if ( BaseSoundID == 1200 )
				BaseSoundID = 0x2A7;
		}
	}
}