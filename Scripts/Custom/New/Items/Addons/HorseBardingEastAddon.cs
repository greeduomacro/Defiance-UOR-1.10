using System;
using Server;

namespace Server.Items
{
	public class HorseBardingEastAddon : BaseAddon
	{
		public override BaseAddonDeed Deed{ get{ return new HorseBardingEastDeed(); } }

		[Constructable]
		public HorseBardingEastAddon()
		{
			AddComponent( new AddonComponent( 0x1379 ), 0, 0, 0 );
			AddComponent( new AddonComponent( 0x1378 ), 0, 1, 0 );
		}

		public HorseBardingEastAddon( Serial serial ) : base( serial )
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

	public class HorseBardingEastDeed : BaseAddonDeed
	{
		public override BaseAddon Addon{ get{ return new HorseBardingEastAddon(); } }
		//public override int LabelNumber{ get{ return 1044324; } } // horse barding (east)

		[Constructable]
		public HorseBardingEastDeed()
		{

                Name = "horse barding (east)";

		}

		public HorseBardingEastDeed( Serial serial ) : base( serial )
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