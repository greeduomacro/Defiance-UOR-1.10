using System;
using Server;

namespace Server.Items
{
	public class ElvenRobe : BaseOuterTorso
	{
		[Constructable]
		public ElvenRobe() : base( 0x2FB9 )
		{
			Hue = 0x0;
			Weight = 1;
			Name = "Elven Robe";
		}

		public ElvenRobe( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 1 ); //version
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
			if ( version < 1 )
				Dyable = false;
		}
	}
}