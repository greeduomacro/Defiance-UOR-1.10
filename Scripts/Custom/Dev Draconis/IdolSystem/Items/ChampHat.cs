using System;

namespace Server.Items
{
    public class ChampionHat : WizardsHat, ILowerRegCost
    {
		public override string DefaultName{ get{ return "Headdress of the Champion - imbued with arcanum level 7"; } }

        [Constructable]
        public ChampionHat() : base(1157)
        {
            Weight = 1.0;
        }

		public int LowerRegCost{ get{ return 7; } }

        public ChampionHat(Serial serial) : base(serial) { }


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