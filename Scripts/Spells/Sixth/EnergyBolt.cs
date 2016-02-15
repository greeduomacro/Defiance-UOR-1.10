using System;
using Server.Targeting;
using Server.Network;

namespace Server.Spells.Sixth
{
	public class EnergyBoltSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Energy Bolt", "Corp Por",
				230,
				9022,
				Reagent.BlackPearl,
				Reagent.Nightshade
			);

		public override SpellCircle Circle { get { return SpellCircle.Sixth; } }

		public EnergyBoltSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public override bool DelayedDamage{ get{ return true; } }

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( CheckHSequence( m ) )
			{
				Mobile attacker = Caster, source = Caster;

				SpellHelper.Turn( Caster, m );

				attacker.AggressiveAction( m, attacker.IsHarmfulCriminal( m ) );

				SpellHelper.CheckReflect( (int)this.Circle, ref source, ref m );

				InternalTimer t = new InternalTimer( this, attacker, source, m );
				t.Start();
			}

			FinishSequence();
		}

		private class InternalTimer : Timer
		{
			private MagerySpell m_Spell;
			private Mobile m_Source, m_Target, m_Attacker;

			public InternalTimer( MagerySpell spell, Mobile attacker, Mobile source, Mobile target )
				: base( TimeSpan.FromSeconds( 0.7 ) )
			{
				m_Spell = spell;
				m_Attacker = attacker;
				m_Target = target;
				m_Source = source;

				if ( m_Spell != null )
					m_Spell.StartDelayedDamageContext( attacker, this );

				Priority = TimerPriority.FiftyMS;
			}

			protected override void OnTick()
			{
				double damage;

				if ( Core.AOS )
				{
					damage = m_Spell.GetNewAosDamage( 40, 1, 5, m_Target );
				}
				else
				{
					//Default: damage = Utility.Random( 24, 18 );
					damage = Utility.Random( 26, 9 );

					//damage = Utility.Dice( 3, 5, 20 );

					if ( m_Spell.CheckResisted( m_Target ) )
					{
						damage *= 0.75;

						m_Target.SendLocalizedMessage( 501783 ); // You feel yourself resisting magical energy.
					}

					// Scale damage based on evalint and resist
					damage *= m_Spell.GetDamageScalar( m_Target );
				}

				// Do the effects
				m_Source.MovingParticles( m_Target, 0x379F, 7, 0, false, true, 3043, 4043, 0x211 );
				m_Source.PlaySound( 0x20A );

				// Deal the damage
				SpellHelper.Damage( m_Spell, m_Target, damage, 0, 100, 0, 0, 0 );

				if ( m_Spell != null )
					m_Spell.RemoveDelayedDamageContext( m_Attacker );
			}
		}

		private class InternalTarget : Target
		{
			private EnergyBoltSpell m_Owner;

			public InternalTarget( EnergyBoltSpell owner ) : base( Core.ML ? 10 : 12, false, TargetFlags.Harmful )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
					m_Owner.Target( (Mobile)o );
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}