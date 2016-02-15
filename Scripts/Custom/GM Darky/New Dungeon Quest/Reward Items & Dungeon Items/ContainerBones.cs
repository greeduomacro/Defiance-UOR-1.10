using System;
using Server;

namespace Server.Items
{
	public class ContainerBones : Bag
	{
		public override string DefaultName{ get{ return "skeleton of an unknown bard"; } }

		[Constructable]
		public ContainerBones() : this( 1 )
		{
			GumpID = 9;
			ItemID = 3787;
		}

		[Constructable]
		public ContainerBones( int amount )
		{
			DropItem( new BodySash( 0x159 ) );
			DropItem( new Sandals( 0x0 ) );
			DropItem( new Lute( ) );
			DropItem( new Gold( 260, 350 ) );
			DropItem( new LongPants( 0x45E ) );
			DropItem( new FancyShirt( 0x4D1 ) );
	   }

		public ContainerBones( Serial serial ) : base( serial )
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