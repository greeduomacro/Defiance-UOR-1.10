using System;

namespace Server.Items
{
	public class TarotCard : Item
	{
		public override string DefaultName{ get{ return "tarot cards"; } }

		[Constructable]
		public TarotCard() : base( Utility.Random( 0x12A6, 7 ) )
		{
			Weight = 9.0;
		}

		public TarotCard(Serial serial) : base(serial)
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize(writer);

			writer.WriteEncodedInt( (int) 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}