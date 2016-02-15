using System;
using Server.Items;

namespace Server.Items
{
	[FlipableAttribute( 0x141B, 0x141C )]
	public class OrcMask : BaseHat
	{
		[Constructable]
		public OrcMask() : base( 0x141B )
		{
		}

		public OrcMask( Serial serial ) : base ( serial )
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