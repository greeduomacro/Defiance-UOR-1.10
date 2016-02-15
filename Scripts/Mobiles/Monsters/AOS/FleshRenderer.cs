using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a fleshrenderer corpse" )]
	public class FleshRenderer : BaseCreature
	{
		public override WeaponAbility GetWeaponAbility()
		{
			return Utility.RandomBool() ? WeaponAbility.Dismount : WeaponAbility.ParalyzingBlow;
		}

		public override bool IgnoreYoungProtection { get { return true; } }
		public override string DefaultName{ get{ return "a fleshrenderer"; } }

		[Constructable]
		public FleshRenderer() : base( AIType.AI_Melee, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Body = 315;

			SetStr( 2201, 2260 );
			SetDex( 201, 210 );
			SetInt( 221, 260 );

			SetHits( 10000 );

			SetDamage( 19, 27 );

			SetDamageType( ResistanceType.Physical, 80 );
			SetDamageType( ResistanceType.Poison, 20 );

			SetResistance( ResistanceType.Physical, 80, 90 );
			SetResistance( ResistanceType.Fire, 50, 60 );
			SetResistance( ResistanceType.Cold, 50, 60 );
			SetResistance( ResistanceType.Poison, 100 );
			SetResistance( ResistanceType.Energy, 70, 80 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.MagicResist, 155.1, 160.0 );
			SetSkill( SkillName.Meditation, 100.0 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 100.0 );

			Fame = 23000;
			Karma = -23000;

			VirtualArmor = 24;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 7 );
		}

		public override bool AutoDispel{ get{ return true; } }
		public override bool BardImmune { get { return !Core.SE; } }
		public override bool Unprovokable { get { return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public override int GetAttackSound()
		{
			return 0x34C;
		}

		public override int GetHurtSound()
		{
			return 0x354;
		}

		public override int GetAngerSound()
		{
			return 0x34C;
		}

		public override int GetIdleSound()
		{
			return 0x34C;
		}

		public override int GetDeathSound()
		{
			return 0x354;
		}

		public FleshRenderer( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

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

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( BaseSoundID == 660 )
				BaseSoundID = -1;
		}
	}
}