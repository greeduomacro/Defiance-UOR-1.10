using System;

namespace Server.Items
{
	public class PlayingCards : Item
	{
		public override string DefaultName{ get{ return "playing cards"; } }

		[Constructable]
		public PlayingCards() : base( Utility.RandomList( 0xFA2, 0xFA3, 0xE15, 0xE16, 0xE18, 0xE19 ) )
		{
			Weight = 9.0;
		}

		public PlayingCards(Serial serial) : base(serial)
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