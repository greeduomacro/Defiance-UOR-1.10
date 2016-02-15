//==============================================//
// Created by Dupre					//
//==============================================//
using System;
using Server;

namespace Server.Items
{
	public class BlackthornsBlade : Katana
	{
		public override int ArtifactRarity{ get{ return 69; } }

		public override int InitMinHits{ get{ return 255; } }
		public override int InitMaxHits{ get{ return 255; } }

		[Constructable]
		public BlackthornsBlade()
		{
			Name = "Blackthorn's Blade";
			Hue = 1;
		}

		public override bool OnEquip(Mobile from) 
	      { 
			UnMorph(from);
		         return base.OnEquip(from); 
		}

	      public override void OnRemoved( object parent) 
	      { 
	        if (parent is Mobile) 
	        { 
	         Mobile from = (Mobile)parent; 
		   UnMorph(from);
		  }

	         base.OnRemoved(parent); 
      	}

		public bool UnMorph(Mobile from)
		{

		if (from.FindItemOnLayer(Layer.OuterTorso) == null || from.FindItemOnLayer(Layer.OuterTorso).Name != "a Shroud of Phasing")
			{
			this.Hue = 1;
			}
			else
			{
			}
		return true;
		}
		public BlackthornsBlade( Serial serial ) : base( serial )
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
		}
	}
}
