using System;

namespace Server.Items
{
	public class DriedFlowers : Item
	{
		public override string DefaultName{ get{ return "dried flowers"; } }

		[Constructable]
		public DriedFlowers() : base( Utility.Random( 0xC3B, 8 ) )
		{
			Weight = 9.0;
		}

		public DriedFlowers( Serial serial ) : base( serial )
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