using System;

namespace Server.Items
{
	public class Wheat : Item
	{
		[Constructable]
		public Wheat() : this( 1 )
		{
		}

		[Constructable]
		public Wheat( int amount ) : base( 0x1EBD )
		{
			Stackable = true;
			Weight = 4.0;
			Amount = amount;
		}

		public Wheat( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( IsChildOf( from.Backpack ) )
			{
				if ( Amount >= 4 )
				{
					foreach ( Item m in from.GetItemsInRange( 2 ) )
					{
						if ( m is FlourMillEastAddon || m is FlourMillSouthAddon )
						{
							from.SendMessage( "You turned the wheat sheaves into a sack of flour." );
							from.AddToBackpack( new SackFlour() );
							Consume( 4 );
							return;
						}
					}
				}
				else
					from.SendMessage( "You need more wheat sheaves." );
			}
			else
			{
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			}
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