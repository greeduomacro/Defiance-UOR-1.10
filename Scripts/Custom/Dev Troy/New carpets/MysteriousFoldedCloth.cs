using System;
using System.Collections;
using Server.Items;

namespace Server.Items
{
	public class MysteriousCloth : Item
	{
		public override string DefaultName{ get{ return "a piece of mysterious cloth"; } }

		[Constructable]
		public MysteriousCloth() : base( 5981 )
		{
			Weight = 9.0;
			Hue = 1175;
		}

		public MysteriousCloth(Serial serial) : base(serial)
		{
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write((int) 0);
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();

		}
	}

}