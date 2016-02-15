using System;

namespace Server.Items
{
	public class GorgonRug : Item
	{
		public override string DefaultName{ get{ return "gorgon rug"; } }

		[Constructable]
		public GorgonRug() : base( Utility.Random( 0x1DEF, 14 ) )
		{
			Weight = 9.0;
		}

		public GorgonRug(Serial serial) : base(serial)
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