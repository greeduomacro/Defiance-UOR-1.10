/////////////////////////////////////////////
//Created by LostSinner & Modified by Darky//
/////////////////////////////////////////////
using System;

namespace Server.Items
{
	public class SilenceRobe : Robe
	{
		public override string DefaultName{ get{ return "Robes of Silence"; } }

		[Constructable]
		public SilenceRobe()
		{
			ItemID = 7939;
			Weight = 5.0;
			Hue = 2125;
		}

		public SilenceRobe( Serial serial ) : base( serial )
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
					case 7939: ItemID = 7937; break;
					case 7937: ItemID = 7936; break;
					case 7936: ItemID = 7939; break;
				}
			}
			else
				from.SendMessage( "You must have the item in your pack to morph it." );
		}
	}
}