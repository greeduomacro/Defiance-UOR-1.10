using System;
using Server;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x170d, 0x170e )]
	public class BlackSandals : BaseShoes
	{
		[Constructable]
		public BlackSandals() : base( 0x170D, 1 )
		{
			Weight = 1.0;
			Name = "Quest Sandals";
		}

		public BlackSandals( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 )
				Dyable = false;
		}
	}
}