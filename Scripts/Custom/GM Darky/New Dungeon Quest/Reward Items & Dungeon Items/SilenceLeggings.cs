/////////////////////////////////////////////
//Created by LostSinner & Modified by Darky//
/////////////////////////////////////////////
using System;

namespace Server.Items
{
	public class SilenceLeggings : Skirt
	{
		public override string DefaultName{ get{ return "Leggings of Silence"; } }

		[Constructable]
		public SilenceLeggings()
		{
			ItemID = 5398;
			Weight = 5.0;
			Hue = 2125;
		}

		public SilenceLeggings( Serial serial ) : base( serial )
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
					case 5398: ItemID = 5422; break;
					case 5422: ItemID = 5433; break;
					case 5433: ItemID = 5431; break;
					case 5431: ItemID = 5398; break;
				}
			}
			else
				from.SendMessage( "You must have the item in your pack to morph it." );
		}
	}
}