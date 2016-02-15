using System;
using Server;

namespace Server.Items
{
	public class SpiderCarvingEast1 : Item
	{
		public SpiderCarvingEast1() : base( 0x123A )
		{
		}

		public SpiderCarvingEast1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SpiderCarvingEast2 : Item
	{
		public SpiderCarvingEast2() : base( 0x123B )
		{
		}

		public SpiderCarvingEast2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SpiderCarvingEast3 : Item
	{
		public SpiderCarvingEast3() : base( 0x123C )
		{
		}

		public SpiderCarvingEast3( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SpiderCarvingSouth1 : Item
	{
		public SpiderCarvingSouth1() : base( 0x1237 )
		{
		}

		public SpiderCarvingSouth1( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SpiderCarvingSouth2 : Item
	{
		public SpiderCarvingSouth2() : base( 0x1238 )
		{
		}

		public SpiderCarvingSouth2( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SpiderCarvingSouth3 : Item
	{
		public SpiderCarvingSouth3() : base( 0x1239 )
		{
		}

		public SpiderCarvingSouth3( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SpiderCarvingSingleSouth : Item
	{
		public SpiderCarvingSingleSouth() : base( 0x1244 )
		{
		}

		public SpiderCarvingSingleSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SpiderCarvingSingleEast : Item
	{
		public SpiderCarvingSingleEast() : base( 0x1243 )
		{
		}

		public SpiderCarvingSingleEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}