using System;
using Server;

namespace Server.Items
{
	public class DirtyPot1 : Item
	{
		[Constructable]
		public DirtyPot1() : base( 0x9DC )
		{
		}

		public DirtyPot1( Serial serial ) : base( serial )
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

	public class DirtyPot2 : Item
	{
		[Constructable]
		public DirtyPot2() : base( 0x9DD )
		{
		}

		public DirtyPot2( Serial serial ) : base( serial )
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

	public class DirtyPot3 : Item
	{
		[Constructable]
		public DirtyPot3() : base( 0x9DF )
		{
		}

		public DirtyPot3( Serial serial ) : base( serial )
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

	public class DirtyPot4 : Item
	{
		[Constructable]
		public DirtyPot4() : base( 0x9E6 )
		{
		}

		public DirtyPot4( Serial serial ) : base( serial )
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

	public class DirtyPot5 : Item
	{
		[Constructable]
		public DirtyPot5() : base( 0x9E7 )
		{
		}

		public DirtyPot5( Serial serial ) : base( serial )
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

	public class DirtyPot6 : Item
	{
		[Constructable]
		public DirtyPot6() : base( 0x9E8 )
		{
		}

		public DirtyPot6( Serial serial ) : base( serial )
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