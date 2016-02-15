using System;
using System.Collections;
using Server;
using Server.Items;
using Server.Engines.IdolSystem;
using Server.EventPrizeSystem;

namespace Server.Mobiles
{
	[CorpseName( "a dungeon champion corpse" )]
	public abstract class BaseDungeonChampion : BaseBoss
	{
		private void setup()
        	{
            		Name = "Dungeon Champion";

			SetStr( 500 );
			SetDex( 200 );
			SetInt( 500 );

			SetHits( 10000 );
			SetMana( 1000 );

			SetDamage( 20, 30 );

			SetSkill( SkillName.DetectHidden, 120.0 );

			Kills = 5;

			Fame = 12000;
			Karma = -12000;

			VirtualArmor = 50;
        	}

		public BaseDungeonChampion() : base( AIType.AI_Mage )
		{
			setup();
		}

		public BaseDungeonChampion(AIType aiType) : this(aiType, FightMode.Closest)
		{
			setup();
		}

		public BaseDungeonChampion(AIType aiType, FightMode mode) : base(aiType, mode)
		{
			setup();
		}

		public BaseDungeonChampion( Serial serial ) : base( serial )
		{
		}

		public override bool DoDetectHidden { get { return true; } }
		public override bool DoDistributeTokens { get { return true; } }

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

		private Item m_Idol;

		public override void OnDeath( Container c )
		{
			if ( Utility.Random( 2 ) < 1 )
			{
				int randomise = Utility.Random( 7 );

				switch ( randomise )
				{
					case 0: m_Idol = new Idol( IdolType.Shame ); break;
					case 1: m_Idol = new Idol( IdolType.Hythloth ); break;
					case 2: m_Idol = new Idol( IdolType.Destard ); break;
					case 3: m_Idol = new Idol( IdolType.Deceit ); break;
					case 4: m_Idol = new Idol( IdolType.Despise ); break;
					case 5: m_Idol = new Idol( IdolType.Wrong); break;
					case 6: m_Idol = new Idol( IdolType.Covetous); break;
				}

				c.DropItem( m_Idol );
			}

            		if ( Utility.Random( 50 ) < 1 )
            		{
               			UncutCloth cloth = new UncutCloth();
                		cloth.Hue = 1364;
                		c.DropItem( cloth );
           		}

			base.OnDeath( c );
		}

		public override void GenerateLoot()
		{
			AddLoot( LootPack.FilthyRich, 5 );
		}

		public override bool OnBeforeDeath()
		{
			if ( !NoKillAwards )
			{
				Token();

				Map map = this.Map;

				if ( map != null )
				{
					for ( int x = -5; x <= 5; ++x )
					{
						for ( int y = -8; y <= 8; ++y )
						{
							double dist = Math.Sqrt(x*x+y*y);

							if ( dist <= 5 )
								new GoldTimer( map, X + x, Y + y ).Start();
						}
					}
				}
			}
			return base.OnBeforeDeath();
		}

		private class GoldTimer : Timer
		{
			private Map m_Map;
			private int m_X, m_Y;

			public GoldTimer( Map map, int x, int y ) : base( TimeSpan.FromSeconds( Utility.RandomDouble() * 10.0 ) )
			{
				m_Map = map;
				m_X = x;
				m_Y = y;
			}

			protected override void OnTick()
			{
				int z = m_Map.GetAverageZ( m_X, m_Y );
				bool canFit = m_Map.CanFit( m_X, m_Y, z, 6, false, false );

				for ( int i = -3; !canFit && i <= 3; ++i )
				{
					canFit = m_Map.CanFit( m_X, m_Y, z + i, 6, false, false );

					if ( canFit )
						z += i;
				}

				if ( !canFit )
					return;

				Gold g = new Gold( 100, 200 );

				g.MoveToWorld( new Point3D( m_X, m_Y, z ), m_Map );

				if ( 0.5 >= Utility.RandomDouble() )
				{
					switch ( Utility.Random( 3 ) )
					{
						case 0: // Fire column
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x3709, 10, 30, 5052 );
							Effects.PlaySound( g, g.Map, 0x208 );

							break;
						}
						case 1: // Explosion
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36BD, 20, 10, 5044 );
							Effects.PlaySound( g, g.Map, 0x307 );

							break;
						}
						case 2: // Ball of fire
						{
							Effects.SendLocationParticles( EffectItem.Create( g.Location, g.Map, EffectItem.DefaultDuration ), 0x36FE, 10, 10, 5052 );

							break;
						}
					}
				}
			}
		}
	}
}