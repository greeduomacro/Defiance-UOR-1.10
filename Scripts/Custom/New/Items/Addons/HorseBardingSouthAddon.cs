using System;
using Server;

namespace Server.Items
{
	public class HorseBardingSouthAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HorseBardingSouthDeed(); } }

		[Constructable]
		public HorseBardingSouthAddon()
		{
			AddComponent( new AddonComponent( 0x1376 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x1377 ), 1, 0, 0 );
		}

		public HorseBardingSouthAddon( Serial serial ) : base( serial )
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

	public class HorseBardingSouthDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HorseBardingSouthAddon(); } }
		//public override int LabelNumber{ get{ return 1044324; } } // horse barding (east)

		[Constructable]
		public HorseBardingSouthDeed()
		{

                Name = "horse barding (south)";

		}

		public HorseBardingSouthDeed( Serial serial ) : base( serial )
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