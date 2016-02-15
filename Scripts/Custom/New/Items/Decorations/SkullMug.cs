using System;
using Server;

namespace Server.Items
{
	public class SkullMugEast1 : Item
	{
		[Constructable]
		public SkullMugEast1() : base( 0xFFB )
		{
		}

		public SkullMugEast1( Serial serial ) : base( serial )
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

	public class SkullMugSouth1 : Item
	{
		[Constructable]
		public SkullMugSouth1() : base( 0xFFC )
		{
		}

		public SkullMugSouth1( Serial serial ) : base( serial )
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

	public class SkullMugEast2 : Item
	{
		[Constructable]
		public SkullMugEast2() : base( 0xFFD )
		{
		}

		public SkullMugEast2( Serial serial ) : base( serial )
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

	public class SkullMugSouth2 : Item
	{
		[Constructable]
		public SkullMugSouth2() : base( 0xFFD )
		{
		}

		public SkullMugSouth2( Serial serial ) : base( serial )
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