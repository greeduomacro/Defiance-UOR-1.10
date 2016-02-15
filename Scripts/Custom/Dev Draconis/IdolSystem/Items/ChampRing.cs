using System;
using Server.Items;

namespace Server.Items
{
    public class ChampionRing : BaseRing, ILowerRegCost
    {
		public override string DefaultName{ get{ return "Band of the Champion - imbued with arcanum level 3"; } }

        [Constructable]
        public ChampionRing() : base( 0x108A )
        {
            Weight = 1.0;
            Hue = 1150;

            LootType = LootType.Blessed;
        }

		public int LowerRegCost{ get{ return 3; } }

        public ChampionRing(Serial serial) : base(serial) { }

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