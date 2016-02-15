/////////////////////////////////////////////
//Created by LostSinner & Modified by Darky//
/////////////////////////////////////////////
using System;

namespace Server.Items
{
	public class SilenceShoes : Shoes
	{
		public override string DefaultName{ get{ return "Boots of Silence"; } }

		[Constructable]
		public SilenceShoes()
		{
			ItemID = 5903;
			Weight = 5.0;
			Hue = 2125;
		}

		public SilenceShoes( Serial serial ) : base( serial )
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
					case 5903: ItemID = 5899; break;
					case 5899: ItemID = 5905; break;
					case 5905: ItemID = 5903; break;
				}
			}
			else
				from.SendMessage( "You must have the item in your pack to morph it." );
		}
	}
}