using System;
using Server;

namespace Server.Items
{
	public class SkinnedDeerLeft : Item
	{
		[Constructable]
		public SkinnedDeerLeft() : base( 0x1E90 )
		{
		}

		public SkinnedDeerLeft( Serial serial ) : base( serial )
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

	public class SkinnedDeerRight : Item
	{
		[Constructable]
		public SkinnedDeerRight() : base( 0x1E91 )
		{
		}

		public SkinnedDeerRight( Serial serial ) : base( serial )
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