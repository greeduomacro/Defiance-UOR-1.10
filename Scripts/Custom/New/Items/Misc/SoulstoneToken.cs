using System;
using Server;
using Server.Network;

namespace Server.Items
{
	public class SoulstoneToken : BasePromotionalToken
	{
		public override Item CreateItemFor( Mobile from )
		{
			if( from != null && from.Account != null )
				return new Soulstone( from.Account.ToString() );
			else
				return null;
		}

		public override TextDefinition ItemGumpName{ get{ return 1030903; } }// <center>Soulstone</center>
		public override TextDefinition ItemName { get { return 1030899; } } //soulstone
		public override TextDefinition ItemReceiveMessage{ get{ return 1070743; } } // A Soulstone has been created in your bank box!

		[Constructable]
		public SoulstoneToken() : base()
		{
		}

		public SoulstoneToken( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.WriteEncodedInt( (int)0 );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadEncodedInt();
		}
	}
}