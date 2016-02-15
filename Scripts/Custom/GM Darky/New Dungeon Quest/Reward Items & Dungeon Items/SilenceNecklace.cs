/////////////////////////////////////////////
//Created by LostSinner & Modified by Darky//
/////////////////////////////////////////////
using System;

namespace Server.Items
{
	public class SilenceNecklace : Necklace
	{
		public override string DefaultName{ get{ return "Choker of Silence"; } }

		[Constructable]
		public SilenceNecklace()
		{
			ItemID = 7941;
			Weight = 1.0;
			Hue = 2125;
			LootType = LootType.Newbied;
		}

		public SilenceNecklace( Serial serial ) : base( serial )
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
					case 7941: ItemID = 7944; break;
					case 7944: ItemID = 4232; break;
					case 4232: ItemID = 4233; break;
					case 4233: ItemID = 7941; break;
				}
			}
			else
				from.SendMessage( "You must have the item in your pack to morph it." );
		}
	}
}