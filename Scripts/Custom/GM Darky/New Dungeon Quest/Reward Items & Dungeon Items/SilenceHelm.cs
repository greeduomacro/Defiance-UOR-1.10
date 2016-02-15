/////////////////////////////////////////////
//Created by LostSinner & Modified by Darky//
/////////////////////////////////////////////
using System;

namespace Server.Items
{
	public class SilenceHelm : Bandana
	{
		public override string DefaultName{ get{ return "Halo of Silence"; } }

		[Constructable]
		public SilenceHelm()
		{
			ItemID = 5440;
			Weight = 5.0;
			Hue = 2125;
		}

		public SilenceHelm( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int) 0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();

			if ( Weight == 1.0 )
				Weight = 5.0;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				switch ( ItemID )
				{
					case 5440: ItemID = 5910; break;
					case 5910: ItemID = 5915; break;
					case 5915: ItemID = 5908; break;
					case 5908: ItemID = 5916; break;
					case 5916: ItemID = 5912; break;
					case 5912: ItemID = 5944; break;
					case 5944: ItemID = 5911; break;
					case 5911: ItemID = 5907; break;
					case 5907: ItemID = 5913; break;
					case 5913: ItemID = 5909; break;
					case 5914: ItemID = 5440; break;
				}
			}
			else
				from.SendMessage( "You must have the item in your pack to morph it." );
		}
	}
}