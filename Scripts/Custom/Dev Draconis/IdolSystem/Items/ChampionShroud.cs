using System;

namespace Server.Items
{
    public class ChampionShroud : HoodedShroudOfShadows, ILowerRegCost
    {
		public override string DefaultName{ get{ return "Shroud of the Champion - imbued with arcanum level 10"; } }

        [Constructable]
        public ChampionShroud() : base(1157)
        {
            Weight = 1.0;
        }

		public int LowerRegCost{ get{ return 10; } }

        public ChampionShroud(Serial serial) : base(serial) { }


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