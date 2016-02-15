/////////////////////////////////////////////
//Created by LostSinner & Modified by Darky//
/////////////////////////////////////////////
using System;

namespace Server.Items
{
	public class SilenceShirt : Surcoat
	{
		public override string DefaultName{ get{ return "Bindings of Silence"; } }

		[Constructable]
		public SilenceShirt()
		{
			ItemID = 8189;
			Weight = 1.0;
			Hue = 2125;
		}

		public SilenceShirt( Serial serial ) : base( serial )
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
					case 8189: ItemID = 8097; break;
					case 8097: ItemID = 8059; break;
					case 8059: ItemID = 5437; break;
					case 5437: ItemID = 8095; break;
					case 8095: ItemID = 5441; break;
					case 5441: ItemID = 8189; break;
				}
			}
			else
				from.SendMessage( "You must have the item in your pack to morph it." );
		}
	}
}