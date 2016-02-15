using System;
using System.Collections.Generic;
using System.Text;
using Server.Network;
using Server.Mobiles;
using Server.Factions;
using Server.Engines.CannedEvil;
using Server.Ethics.Evil;

namespace Server.Ethics
{
	public abstract class Ethic
	{
		public static readonly bool Enabled = true;

		public static Ethic Find( Item item )
		{
			if ( item is IEthicsItem )
			{
				EthicsItem ethicsItem = ((IEthicsItem)item).EthicsItemState;
				if ( ethicsItem != null )
					return ethicsItem.Ethic;
			}

			return null;
		}

		public static bool CheckTrade( Mobile from, Mobile to, Mobile newOwner, Item item )
		{
			Ethic itemEthic = Find( item );

			if ( itemEthic == null || Find( newOwner ) == itemEthic )
				return true;

			if ( itemEthic == Hero )
				( from == newOwner ? to : from ).SendMessage( "Only heros may receive this item." );
			else if ( itemEthic == Evil )
				( from == newOwner ? to : from ).SendMessage( "Only the evil may receive this item." );

			return false;
		}

		public static bool CheckEquip( Mobile from, Item item )
		{
			Ethic itemEthic = Find( item );

			if ( itemEthic == null || Find( from ) == itemEthic )
				return true;

			if ( itemEthic == Hero )
				from.SendMessage( "Only heros may wear this item." );
			else if ( itemEthic == Evil )
				from.SendMessage( "Only the evil may wear this item." );

			return false;
		}

		public static bool IsImbued( Item item )
		{
			return IsImbued( item, false );
		}

		public static bool IsImbued( Item item, bool recurse )
		{
			if ( Find( item ) != null )
				return true;

			if ( recurse )
			{
				foreach ( Item child in item.Items )
					if ( IsImbued( child, true ) )
						return true;
			}

			return false;
		}

		public static void Initialize()
		{
			if( Enabled )
				EventSink.Speech += new SpeechEventHandler( EventSink_Speech );
		}

		public static void EventSink_Speech( SpeechEventArgs e )
		{
			if ( e.Blocked || e.Handled )
				return;

			Player pl = Player.Find( e.Mobile );

			if ( pl == null )
			{
				for ( int i = 0; i < Ethics.Length; ++i )
				{
					Ethic ethic = Ethics[i];

					if ( !ethic.IsEligible( e.Mobile ) )
						continue;

					if ( !Insensitive.Equals( ethic.Definition.JoinPhrase.String, e.Speech ) )
						continue;

					bool isNearAnkh = false;

					foreach ( Item item in e.Mobile.GetItemsInRange( 2 ) )
					{
						if ( item is Items.AnkhNorth || item is Items.AnkhWest )
						{
							isNearAnkh = true;
							break;
						}
					}

					if ( isNearAnkh )
					{
						pl = new Player( ethic, e.Mobile );

						pl.Attach();

						if ( ethic is EvilEthic )
						{
							e.Mobile.FixedParticles( 0x375A, 1, 17, 9919, 33, 7, EffectLayer.Waist );
							e.Mobile.FixedParticles( 0x3728, 1, 13, 9502, 33, 7, (EffectLayer)255 );
							e.Mobile.PlaySound( 0x457 );
						}
						else
						{
							e.Mobile.FixedEffect( 0x373A, 10, 30 );
							e.Mobile.PlaySound( 0x209 );
						}

						e.Handled = true;
						break;
					}
				}
			}
			else
			{
				Ethic ethic = pl.Ethic;

				for ( int i = 0; i < ethic.Definition.Powers.Length; ++i )
				{
					Power power = ethic.Definition.Powers[i];

					if ( !Insensitive.Equals( power.Definition.Phrase.String, e.Speech ) )
						continue;

					if ( !power.CheckInvoke( pl ) )
						continue;

					power.BeginInvoke( pl );
					e.Handled = true;

					break;
				}
			}
		}

		public virtual void AddAggressor( Mobile source )
		{
			if ( !m_Aggressors.Contains( source ) )
				m_Aggressors.Add( source );
		}

		public virtual void RemoveAggressor( Mobile source )
		{
			if ( m_Aggressors.Contains( source ) )
				m_Aggressors.Remove( source );
		}

		public virtual bool IsAggressed( Mobile source )
		{
			return m_Aggressors.Contains( source );
		}

		public static void HandleDeath( Mobile mob )
		{
			HandleDeath( mob, null );
		}

		public static void HandleDeath( Mobile victim, Mobile killer )
		{
			if ( killer != null && victim != null && killer != victim && !(killer is BaseCreature) )
			{
				if ( victim is BaseCreature )
				{
					BaseCreature bc = (BaseCreature)victim;
					Region homeregion = Region.Find( bc.Home, victim.Map );

					if ( ( homeregion == null || !homeregion.IsPartOf( typeof( ChampionSpawnRegion ) ) ) && ( killer.Region == null || !killer.Region.IsPartOf( typeof( ChampionSpawnRegion ) ) ) )
					{
						if ( !(victim is BaseVendor) && bc.GetEthicAllegiance( killer ) == BaseCreature.Allegiance.Enemy )
						{
							Player killerEPL = Player.Find( killer );

							if ( killerEPL != null && killerEPL.Power < Player.MaxPower )
							{
								++killerEPL.Power;
								++killerEPL.History;

								killer.SendMessage( "You gain a little life force for slaying a minion of {0}.", killerEPL.Ethic == Ethic.Evil ? "good" : "evil" );
							}
						}
					}
				}
				else if ( victim.Guild == null || killer.Guild == null || killer.Guild != victim.Guild ) //not guild mates
				{
					Ethics.Player killerEPL = Player.Find( killer );
					Ethics.Player victimEPL = Player.Find( victim );

					PlayerState killerState = PlayerState.Find( killer );
					PlayerState victimState = PlayerState.Find( victim );

					bool applyskillloss = false;

					if ( killerState != null && victimState != null && killerState.Faction == victimState.Faction ) //Faction mate penalty
						applyskillloss = true;
					else if ( killerEPL != null ) //Killer is in ethics
					{
						if ( victimEPL != null ) //Victim is in ethics
						{
							if ( killerEPL.Ethic != victimEPL.Ethic )
							{
								if ( ( killerEPL.Ethic == Ethic.Evil || ( !killer.Criminal && killer.Kills < 5 ) ) && victimEPL.Power > 0 )
								{
									int powerTransfer = Math.Max( 1, victimEPL.Power / 5 );

									if ( powerTransfer > ( Player.MaxPower - killerEPL.Power ) )
										powerTransfer = Player.MaxPower - killerEPL.Power;

									if ( powerTransfer > 0 )
									{
										victimEPL.Power -= powerTransfer;
										killerEPL.Power += powerTransfer;

										killerEPL.History += powerTransfer;

										killer.FixedEffect( 0x373A, 10, 30 );
										killer.PlaySound( 0x209 );

										string message = "a little";

										if ( powerTransfer > 20 )
											message = "a lot";
										else if ( powerTransfer > 10 )
											message = "a good amount";
										else if ( powerTransfer > 5 )
											message = "some";

										killer.SendMessage( "You have gained {0} life force for killing {1}.", powerTransfer, victim.Name );
										victim.SendMessage( "You have lost {1} life force for falling victim to {1}.", powerTransfer, killer.Name );
									}
								}
							}
							else if ( victimEPL.Ethic == Ethic.Hero ) //Both are Heros - PENALTY!
								applyskillloss = true;
						}
						else if ( killerEPL.Ethic.IsAggressed( victim ) )
							killerEPL.Ethic.RemoveAggressor( victim );
						else if ( ( victimState == null || killerState == null ) && victim.Kills < 5 ) //Innocent - PENALTY!
						{
							ApplySkillLoss( killer );
							killerEPL.Power -= Math.Min( killerEPL.Power, 20 );
							killer.PlaySound( 0x133 );
							killer.FixedParticles( 0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist );
						}
					}
					else if ( victimEPL != null && !(killer is BaseCreature) ) //Killer is NOT in ethics, but victim is.
					{
						if ( victimEPL.Ethic == Ethic.Evil )
						{
							if ( killer.Kills < 5 && !killer.Criminal )
							{
								killer.FixedEffect( 0x373A, 10, 30 );
								killer.PlaySound( 0x209 );

								killer.SendMessage( "You have slayed an evil soul and embraced the path of heros." );
							}
						}
						else if ( victimEPL.Ethic == Ethic.Hero )
						{
							killer.FixedEffect( 0x373A, 10, 30 );
							killer.PlaySound( 0x209 );

							killer.SendMessage( "You have slayed a heroic soul and embraced the path of the evil." );
						}
					}

					if ( applyskillloss )
					{
						ApplySkillLoss( killer );
						killerEPL.Power /= 3;
						killer.PlaySound( 0x133 );
						killer.FixedParticles( 0x377A, 244, 25, 9950, 31, 0, EffectLayer.Waist );
					}

					if ( killer.Kills >= Mobile.MurderCount && killerEPL != null && killerEPL.Ethic == Ethic.Hero )
					{
						killer.SendMessage( "You have been stripped of your hero status due to your dastardly actions." );
						Ethics.Player killerPLYR = Player.Find( killer, false );
						if ( killerPLYR != null )
							killerPLYR.Detach();
					}
				}
			}
		}

		#region Skill/Stat Loss
		public const double SkillLossFactor = 1.0 / 3.0;
		public const double StatLossFactor = 1.0 / 4.0;
		public static readonly TimeSpan SkillLossPeriod = TimeSpan.FromMinutes( 5.0 );

		private static Dictionary<Mobile, SkillLossContext> m_SkillLoss = new Dictionary<Mobile, SkillLossContext>();

		private class SkillLossContext
		{
			public Timer m_Timer;
			public List<SkillMod> m_SkillMods;
		}

		public static void ApplySkillLoss( Mobile mob )
		{
			SkillLossContext context;
			m_SkillLoss.TryGetValue( mob, out context );

			if ( context == null )
			{
				context = new SkillLossContext();
				m_SkillLoss[mob] = context;

				List<SkillMod> mods = context.m_SkillMods = new List<SkillMod>();

				for ( int i = 0; i < mob.Skills.Length; ++i )
				{
					Skill sk = mob.Skills[i];
					double baseValue = sk.Base;

					if ( baseValue > 0 )
					{
						SkillMod mod = new DefaultSkillMod( sk.SkillName, true, -(baseValue * SkillLossFactor) );

						mods.Add( mod );
						mob.AddSkillMod( mod );
					}
				}

				mob.AddStatMod( new StatMod( StatType.Str, "Ethics Penalty Str", -(int)(mob.RawStr * StatLossFactor), SkillLossPeriod ) );
				mob.AddStatMod( new StatMod( StatType.Dex, "Ethics Penalty Dex", -(int)(mob.RawDex * StatLossFactor), SkillLossPeriod ) );
				mob.AddStatMod( new StatMod( StatType.Int, "Ethics Penalty Int", -(int)(mob.RawInt * StatLossFactor), SkillLossPeriod ) );

				context.m_Timer = Timer.DelayCall( SkillLossPeriod, new TimerStateCallback( ClearSkillLoss_Callback ), mob );
			}
		}

		private static void ClearSkillLoss_Callback( object state )
		{
			ClearSkillLoss( (Mobile) state );
		}

		public static bool ClearSkillLoss( Mobile mob )
		{
			SkillLossContext context;
			m_SkillLoss.TryGetValue( mob, out context );

			if ( context != null )
			{
				m_SkillLoss.Remove( mob );

				List<SkillMod> mods = context.m_SkillMods;

				for ( int i = 0; i < mods.Count; ++i )
					mob.RemoveSkillMod( mods[i] );

				context.m_Timer.Stop();

				return true;
			}

			return false;
		}

		public static bool InSkillLoss( Mobile mob )
		{
			SkillLossContext context;
			m_SkillLoss.TryGetValue( mob, out context );

			return context != null;
		}
		#endregion

		protected EthicDefinition m_Definition;

		protected List<Player> m_Players;
		protected List<EthicsItem> m_EthicItems;
		protected List<Mobile> m_Aggressors;

		public EthicDefinition Definition
		{
			get { return m_Definition; }
		}

		public List<Player> Players
		{
			get { return m_Players; }
		}

		public List<EthicsItem> EthicItems
		{
			get { return m_EthicItems; }
		}

		public List<Mobile> Aggressors
		{
			get { return m_Aggressors; }
		}

		public static Ethic Find( Mobile mob )
		{
			return Find( mob, false );
		}

		public static Ethic Find( Mobile mob, bool inherit )
		{
			return Find( mob, inherit, false );
		}

		public static Ethic Find( Mobile mob, bool inherit, bool allegiance )
		{
			Player pl = Player.Find( mob );

			if ( pl != null )
				return pl.Ethic;

			if ( inherit && mob is BaseCreature )
			{
				BaseCreature bc = (BaseCreature) mob;

				if ( bc.Controlled )
					return Find( bc.ControlMaster, false );
				else if ( bc.Summoned )
					return Find( bc.SummonMaster, false );
				else if ( allegiance )
					return bc.EthicAllegiance;
			}

			return null;
		}

		public Ethic()
		{
			m_Players = new List<Player>();
			m_EthicItems = new List<EthicsItem>();
			m_Aggressors = new List<Mobile>();
		}

		public abstract bool IsEligible( Mobile mob );

		public virtual void Deserialize( GenericReader reader )
		{
			int version = reader.ReadEncodedInt();

			switch ( version )
			{
				case 1:
				{
					int itemCount = reader.ReadEncodedInt();

					for ( int i = 0; i < itemCount; ++i )
					{
						EthicsItem ethicsItem = new EthicsItem( reader, this );

						Timer.DelayCall( TimeSpan.Zero, new TimerCallback( ethicsItem.CheckAttach ) ); // sandbox attachment
					}

					goto case 0;
				}
				case 0:
				{
					int playerCount = reader.ReadEncodedInt();

					for ( int i = 0; i < playerCount; ++i )
					{
						Player pl = new Player( this, reader );

						if ( pl.Mobile != null )
							Timer.DelayCall( TimeSpan.Zero, new TimerCallback( pl.CheckAttach ) );
					}

					break;
				}
			}
		}

		public virtual void Serialize( GenericWriter writer )
		{
			writer.WriteEncodedInt( 1 ); // version

			writer.WriteEncodedInt( m_EthicItems.Count );

			for ( int i = 0; i < m_EthicItems.Count; ++i )
				m_EthicItems[i].Serialize( writer );

			writer.WriteEncodedInt( m_Players.Count );

			for ( int i = 0; i < m_Players.Count; ++i )
				m_Players[i].Serialize( writer );
		}

		public static readonly Ethic Hero = new Hero.HeroEthic();
		public static readonly Ethic Evil = new Evil.EvilEthic();

		public static readonly Ethic[] Ethics = new Ethic[]
			{
				Hero,
				Evil
			};
	}
}