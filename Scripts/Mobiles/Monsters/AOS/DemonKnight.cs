using System;
using System.Collections;
using Server;
using Server.Items;
using System.Collections.Generic;

namespace Server.Mobiles
{
	[CorpseName( "a demon knight corpse" )]
	public class DemonKnight : BaseCreature
	{
		public override bool IgnoreYoungProtection { get { return Core.ML; } }

		public static Type[] ArtifactRarity10 { get { return m_ArtifactRarity10; } }
		public static Type[] ArtifactRarity11 { get { return m_ArtifactRarity11; } }
		private static Type[] m_ArtifactRarity10 = new Type[]
			{
				typeof( LegacyOfTheDreadLord ),
				typeof( TheTaskmaster )
			};

		private static Type[] m_ArtifactRarity11 = new Type[]
			{
				typeof( TheDragonSlayer ),
				typeof( ArmorOfFortune ),
				typeof( GauntletsOfNobility ),
				typeof( HelmOfInsight ),
				typeof( HolyKnightsBreastplate ),
				typeof( JackalsCollar ),
				typeof( LeggingsOfBane ),
				typeof( MidnightBracers ),
				typeof( OrnateCrownOfTheHarrower ),
				typeof( ShadowDancerLeggings ),
				typeof( TunicOfFire ),
				typeof( VoiceOfTheFallenKing ),
				typeof( BraceletOfHealth ),
				typeof( OrnamentOfTheMagician ),
				typeof( RingOfTheElements ),
				typeof( RingOfTheVile ),
				typeof( Aegis ),
				typeof( ArcaneShield ),
				typeof( AxeOfTheHeavens ),
				typeof( BladeOfInsanity ),
				typeof( BoneCrusher ),
				typeof( BreathOfTheDead ),
				typeof( Frostbringer ),
				typeof( SerpentsFang ),
				typeof( StaffOfTheMagi ),
				typeof( TheBerserkersMaul ),
				typeof( TheDryadBow ),
				typeof( DivineCountenance ),
				typeof( HatOfTheMagi ),
				typeof( HuntersHeaddress ),
				typeof( SpiritOfTheTotem )
			};

		public static Item CreateRandomArtifact()
		{
			if ( !Core.AOS )
				return null;

			int count = ( m_ArtifactRarity10.Length * 5 ) + ( m_ArtifactRarity11.Length * 4 );
			int random = Utility.Random( count );
			Type type;

			if ( random < ( m_ArtifactRarity10.Length * 5 ) )
			{
				type = m_ArtifactRarity10[random / 5];
			}
			else
			{
				random -= m_ArtifactRarity10.Length * 5;
				type = m_ArtifactRarity11[random / 4];
			}

			return Loot.Construct( type );
		}

		public static Mobile FindRandomPlayer( BaseCreature creature )
		{
			List<DamageStore> rights = BaseCreature.GetLootingRights( creature.DamageEntries, creature.HitsMax );

			for ( int i = rights.Count - 1; i >= 0; --i )
			{
				DamageStore ds = rights[i];

				if ( !ds.m_HasRight )
					rights.RemoveAt( i );
			}

			if ( rights.Count > 0 )
				return rights[Utility.Random( rights.Count )].m_Mobile;

			return null;
		}

		public static void DistributeArtifact( BaseCreature creature )
		{
			DistributeArtifact( creature, CreateRandomArtifact() );
		}

		public static void DistributeArtifact( BaseCreature creature, Item artifact )
		{
			DistributeArtifact( FindRandomPlayer( creature ), artifact );
		}

		public static void DistributeArtifact( Mobile to )
		{
			DistributeArtifact( to, CreateRandomArtifact() );
		}

		public static void DistributeArtifact( Mobile to, Item artifact )
		{
			if ( to == null || artifact == null )
				return;

			Container pack = to.Backpack;

			if ( pack == null || !pack.TryDropItem( to, artifact, false ) )
				to.BankBox.DropItem( artifact );

			to.SendLocalizedMessage( 1062317 ); // For your valor in combating the fallen beast, a special artifact has been bestowed on you.
		}

		public static int GetArtifactChance( Mobile boss )
		{
			if ( !Core.AOS )
				return 0;

			int luck = LootPack.GetLuckChanceForKiller( boss );
			int chance;

			if ( boss is DemonKnight )
				chance = 1500 + (luck / 5);
			else
				chance = 750 + (luck / 10);

			return chance;
		}

		public static bool CheckArtifactChance( Mobile boss )
		{
			return GetArtifactChance( boss ) > Utility.Random( 100000 );
		}

		public override WeaponAbility GetWeaponAbility()
		{
			switch ( Utility.Random( 3 ) )
			{
				default:
				case 0: return WeaponAbility.DoubleStrike;
				case 1: return WeaponAbility.WhirlwindAttack;
				case 2: return WeaponAbility.CrushingBlow;
			}
		}

		[Constructable]
		public DemonKnight() : base( AIType.AI_Mage, FightMode.Closest, 10, 1, 0.2, 0.4 )
		{
			Name = NameList.RandomName( "demon knight" );
			Title = "the dark father";
			Body = 318;
			BaseSoundID = 0x165;

			SetStr( 2000 );
			SetDex( 150 );
			SetInt( 1500 );

			SetHits( 20000 );
			SetMana( 50000 );

			SetDamage( 18, 30 );

			SetDamageType( ResistanceType.Physical, 20 );
			SetDamageType( ResistanceType.Fire, 20 );
			SetDamageType( ResistanceType.Cold, 20 );
			SetDamageType( ResistanceType.Poison, 20 );
			SetDamageType( ResistanceType.Energy, 20 );

			SetResistance( ResistanceType.Physical, 30 );
			SetResistance( ResistanceType.Fire, 30 );
			SetResistance( ResistanceType.Cold, 30 );
			SetResistance( ResistanceType.Poison, 30 );
			SetResistance( ResistanceType.Energy, 30 );

			SetSkill( SkillName.EvalInt, 125.0 );
			SetSkill( SkillName.Magery, 150.0 );
			SetSkill( SkillName.Meditation, 125.0 );
			SetSkill( SkillName.MagicResist, 150.0 );
			SetSkill( SkillName.Tactics, 125.0 );
			SetSkill( SkillName.Wrestling, 125.0 );

			Fame = 18500;
			Karma = -18500;

			VirtualArmor = 60;
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.SuperBoss );
			AddLoot( LootPack.UltraRich );
			AddLoot( LootPack.FilthyRich, 2 );
			AddLoot( LootPack.HighScrolls, Utility.RandomMinMax( 6, 25 ) );
		}

		public override bool BardImmune{ get{ return !Core.SE; } }
		public override bool Unprovokable{ get{ return Core.SE; } }
		public override bool AreaPeaceImmune { get { return Core.SE; } }
		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool AutoDispel{ get{ return true; } }
		public override int TreasureMapLevel{ get{ return 1; } }
		public override bool AlwaysMurderer{ get{ return true; } }

		private static bool m_InHere;

		public override void OnDamage( int amount, Mobile from, bool willKill )
		{
			if ( from != null && from != this && !m_InHere )
			{
				m_InHere = true;
				AOS.Damage( from, this, Utility.RandomMinMax( 8, 20 ), 100, 0, 0, 0, 0 );

				MovingEffect( from, 0xECA, 10, 0, false, false, 0, 0 );
				PlaySound( 0x491 );

				if ( 0.05 > Utility.RandomDouble() )
					Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerStateCallback( CreateBones_Callback ), from );

				m_InHere = false;
			}
		}

		public virtual void CreateBones_Callback( object state )
		{
			Mobile from = (Mobile)state;
			Map map = from.Map;

			if ( map == null )
				return;

			int count = Utility.RandomMinMax( 1, 3 );

			for ( int i = 0; i < count; ++i )
			{
				int x = from.X + Utility.RandomMinMax( -1, 1 );
				int y = from.Y + Utility.RandomMinMax( -1, 1 );
				int z = from.Z;

				if ( !map.CanFit( x, y, z, 16, false, true ) )
				{
					z = map.GetAverageZ( x, y );

					if ( z == from.Z || !map.CanFit( x, y, z, 16, false, true ) )
						continue;
				}

				UnholyBone bone = new UnholyBone();

				bone.Hue = 0;
				bone.Name = "unholy bones";
				bone.ItemID = Utility.Random( 0xECA, 9 );

				bone.MoveToWorld( new Point3D( x, y, z ), map );
			}
		}

		public DemonKnight( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void AlterMeleeDamageTo( Mobile to, ref int damage )
		{
			if ( to is BaseCreature )
				damage *= 3;
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );

			if ( !Summoned && !NoKillAwards && DemonKnight.CheckArtifactChance( this ) )
				DemonKnight.DistributeArtifact( this );

			if ( 0.75 > Utility.RandomDouble() )
			{
				int r = Utility.Random( 100 );
				Item drop = null;

				if ( r < 1 ) drop = new LayerSashDeed();			
				else if	( r < 4 ) drop = new SpecialQuestSandals();
				else if	( r < 6 ) drop = new RareCreamCarpet( PieceType.Centre );
				else if	( r < 8 ) drop = new RareBlueCarpet( PieceType.Centre );
				else if	( r < 10 ) drop = new RareBloodCarpet( PieceType.Centre );
				else if	( r < 12 ) drop = new BasicBlueCarpet( PieceType.Centre );
				else if	( r < 14 ) drop = new BasicPinkCarpet( PieceType.Centre );
				else if	( r < 29 ) drop = new BloodPentagramPart( Utility.Random( 5 ) );
				else if	( r < 30 ) drop = new ClothingBlessDeed();
				else if	( r < 35 ) drop = new MysteriousCloth();
				else if	( r < 40 ) drop = new SpecialHairDye();
				else if	( r < 45 ) drop = new SpecialBeardDye();
				else if	( r < 50 ) drop = new NameChangeDeed();
				else if	( r < 65 ) drop = new SkillTunic();
				else if	( r < 80 ) drop = new TamersCrook();
				else if	( r < 85 ) drop = new HeroShield();
				else if	( r < 88 ) drop = new EvilShield();
				else if	( r < 91 ) drop = new MondainHat();
				else if	( r < 94 ) drop = new PlatinGloves();
				else if	( r < 96 ) drop = new AncientSamuraiHelm();
				else if	( r < 98 ) drop = Utility.RandomBool() ? (Item)(new MirrorEast()) : (Item)(new MirrorNorth());
				else
					drop = Utility.RandomBool() ? (Item)(new BoneBenchEastPart()) : (Item)(new BoneBenchWestPart());

				c.DropItem( drop );
			}
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}
}