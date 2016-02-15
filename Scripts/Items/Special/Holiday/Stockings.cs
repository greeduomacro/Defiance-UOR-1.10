using System;
using Server;

namespace Server.Items
{
	[FlipableAttribute( 0x2bd9, 0x2bda )]
	public class GreenStockingOSI : BaseContainer
	{
		[Constructable]
		public GreenStockingOSI() : base ( 0x2bd9 )
		{
			GumpID = 0x103;
		}

		public GreenStockingOSI( Serial serial ) : base( serial )
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

	[FlipableAttribute( 0x2bdb, 0x2bdc )]
	public class RedStockingOSI : BaseContainer
	{
		[Constructable]
		public RedStockingOSI() : base ( 0x2bdb )
		{
			GumpID = 0x103;
		}

		public RedStockingOSI( Serial serial ) : base( serial )
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
		}
	}
}