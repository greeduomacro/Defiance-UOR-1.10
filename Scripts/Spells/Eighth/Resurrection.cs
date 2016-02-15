using System;
using System.Collections.Generic;
using Server.Targeting;
using Server.Network;
using Server.Gumps;
using Server.Factions;
using Server.Mobiles;

namespace Server.Spells.Eighth
{
	public class ResurrectionSpell : MagerySpell
	{
		private static SpellInfo m_Info = new SpellInfo(
				"Resurrection", "An Corp",
				245,
				9062,
				Reagent.Bloodmoss,
				Reagent.Garlic,
				Reagent.Ginseng
			);

		public override SpellCircle Circle { get { return SpellCircle.Eighth; } }

		public ResurrectionSpell( Mobile caster, Item scroll ) : base( caster, scroll, m_Info )
		{
		}

		public override void OnCast()
		{
			Caster.Target = new InternalTarget( this );
		}

		public void Target( Mobile m )
		{
			if ( !Caster.CanSee( m ) )
			{
				Caster.SendLocalizedMessage( 500237 ); // Target can not be seen.
			}
			else if ( m == Caster )
			{
				Caster.SendLocalizedMessage( 501039 ); // Thou can not resurrect thyself.
			}
			else if ( !Caster.Alive )
			{
				Caster.SendLocalizedMessage( 501040 ); // The resurrecter must be alive.
			}
			else if ( !Caster.InRange( m, 1 ) )
			{
				Caster.SendLocalizedMessage( 501042 ); // Target is not close enough.
			}
			// jakob, added this to allow resurrection of war horses
			else if ( m is FactionWarHorse )
			{
				PlayerState ps = PlayerState.Find( Caster );

				if ( ps != null && ps.KillPoints >= 15 )
				{
					if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
					{
						Caster.SendLocalizedMessage( 501042 ); // Target can not be resurrected at that location.
					}
					else if ( m.Region != null && m.Region.IsPartOf( "Khaldun" ) )
					{
						Caster.SendLocalizedMessage( 1010395 ); // The veil of death in this area is too strong and resists thy efforts to restore life.
					}
					else
					{
						int healerNumber = 500965; // You are able to resurrect your patient.

						BaseCreature petPatient = (BaseCreature)m;

						if ( petPatient.IsDeadPet )
						{
							Mobile master = petPatient.ControlMaster;

							if ( master != null && master.InRange( petPatient, 3 ) )
							{
								healerNumber = 503255; // You are able to resurrect the creature.

								master.CloseGump( typeof( PetResurrectGump ) );
								master.SendGump( new PetResurrectGump( Caster, petPatient ) );
							}
							else
							{
								bool found = false;

								List<Mobile> friends = petPatient.Friends;

								for ( int i = 0; friends != null && i < friends.Count; ++i )
								{
									Mobile friend = friends[i];

									if ( friend.InRange( petPatient, 3 ) )
									{
										healerNumber = 503255; // You are able to resurrect the creature.

										friend.CloseGump( typeof( PetResurrectGump ) );
										friend.SendGump( new PetResurrectGump( Caster, petPatient ) );

										found = true;
										break;
									}
								}

								if ( !found )
									healerNumber = 1049670; // The pet's owner must be nearby to attempt resurrection.
							}
						}

						Caster.SendLocalizedMessage( healerNumber );
					}
				}
				else
				{
					Caster.SendMessage( "You need at least 15 kill points to resurrect that." );
				}
			}
			else if ( m.Alive )
			{
				Caster.SendLocalizedMessage( 501041 ); // Target is not dead.
			}
			else if ( !Caster.InRange( m, 3 ) )
			{
				Caster.SendLocalizedMessage( 501042 ); // Target is not close enough.
			}
			else if ( !m.Player )
			{
				Caster.SendLocalizedMessage( 501043 ); // Target is not a being.
			}
			else if ( m.Map == null || !m.Map.CanFit( m.Location, 16, false, false ) )
			{
				Caster.SendLocalizedMessage( 501042 ); // Target can not be resurrected at that location.
				m.SendLocalizedMessage( 502391 ); // Thou can not be resurrected there!
			}
			else if ( m.Region != null && m.Region.IsPartOf( "Khaldun" ) )
			{
				Caster.SendLocalizedMessage( 1010395 ); // The veil of death in this area is too strong and resists thy efforts to restore life.
			}
			else if ( CheckBSequence( m, true ) )
			{
				SpellHelper.Turn( Caster, m );

				m.PlaySound( 0x214 );
				m.FixedEffect( 0x376A, 10, 16 );

				m.CloseGump( typeof( ResurrectGump ) );
				m.SendGump( new ResurrectGump( m, Caster ) );
			}

			FinishSequence();
		}

		private class InternalTarget : Target
		{
			private ResurrectionSpell m_Owner;

			public InternalTarget( ResurrectionSpell owner ) : base( 1, false, TargetFlags.Beneficial )
			{
				m_Owner = owner;
			}

			protected override void OnTarget( Mobile from, object o )
			{
				if ( o is Mobile )
				{
					m_Owner.Target( (Mobile)o );
				}
			}

			protected override void OnTargetFinish( Mobile from )
			{
				m_Owner.FinishSequence();
			}
		}
	}
}