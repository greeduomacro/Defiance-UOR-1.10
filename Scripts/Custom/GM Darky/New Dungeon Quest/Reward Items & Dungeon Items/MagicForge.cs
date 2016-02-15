using System;

namespace Server.Items
{
	public class MagicForge : Item
	{
		public override string DefaultName{ get{ return "a magic forge"; } }

		[Constructable]
		public MagicForge() : base( 0xFB1 )
		{
			Weight = 50.0;
		}

		public MagicForge(Serial serial) : base(serial)
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