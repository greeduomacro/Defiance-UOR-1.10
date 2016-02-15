using System;
using Server;

namespace Server.Items
{
	public class RedStocking : Item, IConvertableItem
	{
		//[Constructable]
		public RedStocking() : this( Utility.RandomList( 0x2bdb, 0x2bdc ) )
		{
		}

		//[Constructable]
		public RedStocking( int itemID ) : base( itemID )
		{
			Movable = true;
			Weight = 1;
			Name = "a red stocking";
		}

		public RedStocking( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 1 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			if ( version < 1 )
				ObjectConversion.ItemConversionList.Add( this );
		}

		public Item Convert()
		{
			return new RedStockingOSI();
		}
	}
}