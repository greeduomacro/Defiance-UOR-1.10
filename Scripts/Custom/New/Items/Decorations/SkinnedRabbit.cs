using System;
using Server;

namespace Server.Items
{
	public class SkinnedRabbitLeft : Item
	{
		[Constructable]
		public SkinnedRabbitLeft() : base( 0x1E92 )
		{
		}

		public SkinnedRabbitLeft( Serial serial ) : base( serial )
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

	public class SkinnedRabbitRight : Item
	{
		[Constructable]
		public SkinnedRabbitRight() : base( 0x1E93 )
		{
		}

		public SkinnedRabbitRight( Serial serial ) : base( serial )
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