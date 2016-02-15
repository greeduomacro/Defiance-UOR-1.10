using System;
using Server.Items;

namespace Server.Items
{
    public class ChampionNecklace : BaseNecklace, ILowerRegCost
    {
		public override string DefaultName{ get{ return "Amulet of the Champion - imbued with arcanum level 3"; } }

        [Constructable]
        public ChampionNecklace() : base(0x1085)
        {
            Weight = 1.0;
            Hue = 1157;
            LootType = LootType.Blessed;
        }

		public int LowerRegCost{ get{ return 3; } }

        public ChampionNecklace(Serial serial) : base(serial) { }

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