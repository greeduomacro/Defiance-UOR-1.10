using System;
using Server.Items;

namespace Server.Items
{
    public class ChampionPants : ShortPants, ILowerRegCost
    {
		public override string DefaultName{ get{ return "Leggings of the Champion - imbued with arcanum level 5"; } }

        [Constructable]
        public ChampionPants() : base( 1157 )
        {
            Weight = 1.0;
        }

		public int LowerRegCost{ get{ return 5; } }

        public ChampionPants(Serial serial) : base(serial) { }

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