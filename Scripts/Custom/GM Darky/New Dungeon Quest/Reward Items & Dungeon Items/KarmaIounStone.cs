using System;

namespace Server.Items
{
	public class KarmaIounStone : Item
	{
		public override string DefaultName{ get{ return "dreaded ioun stone"; } }

		[Constructable]
		public KarmaIounStone() : base( 0x2809 )
		{
			Weight = 1.0;
			Hue = 2106;
			LootType = LootType.Cursed;
		}

		public KarmaIounStone( Serial serial ) : base( serial )
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

		public override void OnDoubleClick( Mobile from )
		{
			if ( !IsChildOf( from.Backpack ) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else
			{
				from.Karma -= 10000;
				from.SendMessage( 0x5, "Using the ioun stone lowered your karma greatly!" );
				this.Delete();
			}
		}
	}
}