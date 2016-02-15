using System;
using Server;

namespace Server.Items
{
	public enum PromotionalTokenType { Soulstone, SoulstoneFragment }

	public class PromotionalToken : Item, IConvertableItem
	{
		private PromotionalTokenType m_BoundItem; //Kept for conversion

		public PromotionalToken() : base()
		{
			LootType = LootType.Blessed;
			Light = LightType.Circle300;
			Weight = 5.0;
		}

        public PromotionalToken( Serial serial ) : base( serial )
        {
        }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);

            writer.WriteEncodedInt( (int)1 ); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);

            int version = reader.ReadEncodedInt();

            /*m_NeedAccountDonation =*/ reader.ReadBool();
            /*m_BoundItem = (PromotionalTokenType)*/reader.ReadInt();

			if ( version < 1 )
				ObjectConversion.ItemConversionList.Add( this );
        }

		public Item Convert()
		{
			if ( m_BoundItem == PromotionalTokenType.Soulstone )
				return new SoulstoneToken();
			else
				return new SoulstoneFragmentToken();
		}

		public override int LabelNumber { get { return 1070997; } } // A promotional token
	}
}