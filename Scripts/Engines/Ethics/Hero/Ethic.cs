using System;
using System.Collections.Generic;
using System.Text;
using Server.Factions;

namespace Server.Ethics.Hero
{
	public sealed class HeroEthic : Ethic
	{
		public HeroEthic()
		{
			m_Definition = new EthicDefinition(
					1150,
					"Hero", "(Hero)",
					"I will defend the virtues",
					new Power[]
					{
						new HolySense(),
						new HolyItem(),
						new SummonFamiliar(),
						//new HolyBlade(),
						//new Bless(),
						//new HolyShield(),
						new HolySteedPower(),
						//new HolyWord()
					}
				);
		}

		public override bool IsEligible( Mobile mob )
		{
			Faction fac = Faction.Find( mob );

			return mob.AccessLevel == AccessLevel.Player && !( fac is Minax || fac is Shadowlords );
		}
	}
}