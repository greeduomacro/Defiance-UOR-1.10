using System;
using Server;

namespace Server.Items
{
	public class SilverwareEast : Item
	{
		[Constructable]
		public SilverwareEast() : base( 0x9BD )
		{
		}

		public SilverwareEast( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SilverwareSouth : Item
	{
		[Constructable]
		public SilverwareSouth() : base( 0x9BE )
		{
		}

		public SilverwareSouth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SilverwareNorth : Item
	{
		[Constructable]
		public SilverwareNorth() : base( 0x9D4 )
		{
		}

		public SilverwareNorth( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}

	public class SilverwareWest : Item
	{
		[Constructable]
		public SilverwareWest() : base( 0x9D5 )
		{
		}

		public SilverwareWest( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( 0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}