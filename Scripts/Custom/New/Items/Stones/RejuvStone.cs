using System;

namespace Server.Items
{
	[TypeAlias( "Server.Items.HealStone" )]
	public class RejuvStone : Item
	{
		public override string DefaultName{ get{ return "a stone of rejuvenation"; } }

		[Constructable]
		public RejuvStone() : base( 0xED4 )
		{
			Movable = false;
			Hue = 1278;
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from.InRange( this.GetWorldLocation(), 1 ) )
			{
				from.Hits = from.HitsMax;
				from.Mana = from.ManaMax;
				from.Stam = from.StamMax;
				from.SendMessage( "You feel rejuvenated." );
			}
			else
				from.SendLocalizedMessage( 500446 ); // That is too far away.
		}

		public RejuvStone( Serial serial ) : base( serial )
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