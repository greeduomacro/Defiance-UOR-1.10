using System;
using Server;
using Server.Items;

namespace Server.Mobiles
{
	[CorpseName( "a darknight creeper corpse" )]
	public class DarknightCreeper : BaseCreature
	{
		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		[Constructable]
		public DarknightCreeper() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "darknight creeper" );
			Body = 313;
			BaseSoundID = 0xE0;

			SetStr( 2301, 2330 );
			SetDex( 101, 110 );
			SetInt( 301, 330 );

			SetHits( 10000 );

			SetDamage( 27, 29 );

			SetDamageType( ResistanceType.Physical, 85 );
			SetDamageType( ResistanceType.Poison, 15 );

			SetResistance( ResistanceType.Physical, 60 );
			SetResistance( ResistanceType.Fire, 60 );
			SetResistance( ResistanceType.Cold, 100 );
			SetResistance( ResistanceType.Poison, 90 );
			SetResistance( ResistanceType.Energy, 75 );

			SetSkill( SkillName.DetectHidden, 80.0 );
			SetSkill( SkillName.EvalInt, 118.1, 120.0 );
			SetSkill( SkillName.Magery, 112.6, 120.0 );
			SetSkill( SkillName.Meditation, 150.0 );
			SetSkill( SkillName.Poisoning, 120.0 );
			SetSkill( SkillName.MagicResist, 90.1, 90.9 );
			SetSkill( SkillName.Tactics, 100.0 );
			SetSkill( SkillName.Wrestling, 90.1, 90.9 );

			Fame = 22000;
			Karma = -22000;

			VirtualArmor = 34;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 7 );
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override bool BleedImmune{ get{ return true; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override Poison HitPoison{ get{ return Poison.Lethal; } }
		public override bool AutoDispel{ get{ return true; } }

		public override int TreasureMapLevel{ get{ return 1; } }

		public DarknightCreeper( Serial serial ) : base( serial )
		{
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