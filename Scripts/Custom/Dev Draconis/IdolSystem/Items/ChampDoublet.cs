using System;
using Server.Items;

namespace Server.Items
{
    public class ChampionDoublet : Doublet, ILowerRegCost
    {
		public override string DefaultName{ get{ return "Tunic of the Champion - imbued with arcanum level 5"; } }

        [Constructable]
        public ChampionDoublet() : base((int)1150)
        {
            Weight = 1.0;
        }

		public int LowerRegCost{ get{ return 5; } }

        public ChampionDoublet(Serial serial) : base(serial) { }

        public override void Serialize(GenericWriter writer)
        {
            base.Serialize(writer);
            writer.Write((int)0); // version
        }

        public override void Deserialize(GenericReader reader)
        {
            base.Deserialize(reader);
            int version = reader.ReadInt();

			Name = null;
			Weight = 1.0;
        }
    }
}