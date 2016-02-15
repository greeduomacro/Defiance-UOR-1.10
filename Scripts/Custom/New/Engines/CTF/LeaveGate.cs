using System;
using System.Collections;
using System.Collections.Generic;
using Server;
using Server.Mobiles;

namespace Server.Items
{
	public class LeaveGameGate : ConfirmationMoongate
	{
		[Constructable]
		public LeaveGameGate()
		{
			this.GumpWidth = 420;
			this.GumpHeight = 280;
			this.Dispellable = false;
			this.TitleNumber = 1060635;
			this.TitleColor = 30720;
			this.MessageColor = 0xFFC000;
			this.MessageString = "If you leave the game, you may not be able to rejoin.  Are you sure you wish to leave the game?";
		}

		public LeaveGameGate( Serial ser ) : base( ser )
		{
		}

		public override void UseGate( Mobile m )
		{
			CTFTeam team = CTFGame.FindTeamFor( m );
			if ( team != null )
				team.Game.LeaveGame( m );

			base.UseGate( m );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}

		public static void Strip( Mobile from )
		{
			if ( from.Holding != null )
				from.Holding.Delete();
			if ( from.Backpack != null )
			{
				List<Item> items = from.Backpack.Items;

				for ( int i = items.Count - 1; i >= 0; i-- )
					items[i].Delete();
			}

			Item layers = from.FindItemOnLayer( Layer.TwoHanded );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.OneHanded );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Shoes );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Pants );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Shirt );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Helm );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Gloves );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Ring );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Neck );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.InnerTorso );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Bracelet );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Earrings );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Arms );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Cloak );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.OuterTorso );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.OuterLegs );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Waist );
			if (layers != null)
				layers.Delete();

			layers = from.FindItemOnLayer( Layer.Mount );
			if (layers != null)
				layers.Delete();
		}
	}
}