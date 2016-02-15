using System;
using System.Collections.Generic;
using Server;
using Server.Items;
using Server.Multis;

namespace Server.Mobiles
{
	public class Barber : BaseVendor
	{
		private List<SBInfo> m_SBInfos = new List<SBInfo>();
		protected override List<SBInfo> SBInfos{ get { return m_SBInfos; } }

		[Constructable]
		public Barber() : base("the barber")
		{
		}

		public override void InitSBInfo()
		{
			m_SBInfos.Add( new SBBarber() );
		}

		public Barber( Serial serial ) : base( serial )
		{
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

	public class SBBarber : SBInfo
	{
		private List<IBuyItemInfo> m_BuyInfo = new InternalBuyInfo();
		private IShopSellInfo m_SellInfo = new InternalSellInfo();

		public SBBarber()
		{
		}

		public override IShopSellInfo SellInfo { get { return m_SellInfo; } }
		public override List<IBuyItemInfo> BuyInfo { get { return m_BuyInfo; } }

		public class InternalBuyInfo : List<IBuyItemInfo>
		{
			public InternalBuyInfo()
			{
				//Add( new GenericBuyInfo( "Special Hair Dye", typeof( SpecialHairDye ), 40, 999, 0xE26, 0 ) );
				//Add( new GenericBuyInfo( "Special Beard Dye", typeof( SpecialBeardDye ), 40, 999, 0xE26, 0 ) );

				Add( new GenericBuyInfo( "Hair Deed", typeof( HairDeed ), 25000, 100, 0x14f0, 0 ) );
				Add( new GenericBuyInfo( "Facial Hair Deed", typeof( FacialHairDeed ), 25000, 100, 0x14f0, 0 ) );

				Add( new GenericBuyInfo( "Hair Shears", typeof( HairShears ), 5000, 100, 0xF9F, 916 ) );
				Add( new GenericBuyInfo( "Facial Hair Shears", typeof( FacialHairShears ), 5000, 100, 0xF9F, 921 ) );
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