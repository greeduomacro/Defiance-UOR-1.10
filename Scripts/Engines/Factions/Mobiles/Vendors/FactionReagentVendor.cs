using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Mobiles;

namespace Server.Factions
{
	public class FactionReagentVendor : BaseFactionVendor
	{
		public FactionReagentVendor( Town town, Faction faction ) : base( town, faction, "the Reagent Man" )
		{
			SetSkill( SkillName.EvalInt, 65.0, 88.0 );
			SetSkill( SkillName.Inscribe, 60.0, 83.0 );
			SetSkill( SkillName.Magery, 64.0, 100.0 );
			SetSkill( SkillName.Meditation, 60.0, 83.0 );
			SetSkill( SkillName.MagicResist, 65.0, 88.0 );
			SetSkill( SkillName.Wrestling, 36.0, 68.0 );
		}

		public override void InitSBInfo()
		{
			SBInfos.Add( new SBFactionReagent(Faction) );
		}

		public override VendorShoeType ShoeType
		{
			get{ return Utility.RandomBool() ? VendorShoeType.Shoes : VendorShoeType.Sandals; }
		}

		public override void InitOutfit()
		{
			base.InitOutfit();

			AddItem( new Robe( Utility.RandomBlueHue() ) );
			AddItem( new GnarledStaff() );
		}

		public FactionReagentVendor( Serial serial ) : base( serial )
		{
		}

		public override void VendorBuy(Mobile from)
		{
			PlayerState ps = PlayerState.Find( from );
			if ( ps == null || ps.Faction != this.Faction )
				Say( "I will never deal with your kind!" );
			else
			{
				base.VendorBuy( from );
			}
		}

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
	}

	public class SBFactionReagent : SBInfo
	{
		private List<IBuyItemInfo> m_BuyInfo;
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBFactionReagent( Faction owner )
		{
			m_BuyInfo = new InternalBuyInfo( owner );
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<IBuyItemInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<IBuyItemInfo>
		{
			public InternalBuyInfo( Faction owner )
			{
				for ( int i = 0; i < 2; ++i )
				{
					Add( new InternalBuyInfoItem( owner, typeof( BlackPearl ), 5, 20, 0xF7A, 0 ) );
					Add( new InternalBuyInfoItem( owner, typeof( Bloodmoss ), 5, 20, 0xF7B, 0 ) );
					Add( new InternalBuyInfoItem( owner, typeof( MandrakeRoot ), 3, 20, 0xF86, 0 ) );
					Add( new InternalBuyInfoItem( owner, typeof( Garlic ), 3, 20, 0xF84, 0 ) );
					Add( new InternalBuyInfoItem( owner, typeof( Ginseng ), 3, 20, 0xF85, 0 ) );
					Add( new InternalBuyInfoItem( owner, typeof( Nightshade ), 3, 20, 0xF88, 0 ) );
					Add( new InternalBuyInfoItem( owner, typeof( SpidersSilk ), 3, 20, 0xF8D, 0 ) );
					Add( new InternalBuyInfoItem( owner, typeof( SulfurousAsh ), 3, 20, 0xF8C, 0 ) );
				}
			}
		}

		public class InternalBuyInfoItem : GenericBuyInfo
		{
			private Faction m_Owner;
			public InternalBuyInfoItem( Faction owner, Type type, int price, int amount, int itemID, int hue ) : base( type, price, amount, itemID, hue, null )
			{
				m_Owner = owner;
			}

			public override void OnRestock()
			{

				int ownedTowns = m_Owner == null ? 50 : m_Owner.OwnedTowns, amount;

				if (ownedTowns == 0)
					amount = 50;
				else
					amount = ownedTowns * 50;

				Amount = amount;
			}
		}

		public class InternalSellInfo : GenericSellInfo
		{
			public InternalSellInfo()
			{
			}
		}
	}
}