using System;

namespace Server.Items
{
	public class TalismanOfDefiance : BaseEarrings
	{
		public override string DefaultName{ get{ return "Talisman of Defiance"; } }

		[Constructable]
		public TalismanOfDefiance() : base( 0x2F5B )
		{
			Weight = 1;
			Layer = Layer.Talisman;
			LootType = LootType.Blessed;
		}

		public TalismanOfDefiance( Serial serial ) : base( serial )
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
}