using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Engines.IdolSystem;

namespace Server.Mobiles
{
	public class WrongChamp : BaseDungeonChampion
	{
		[Constructable]
		public WrongChamp() : base( AIType.AI_Melee )
		{
			Title = "of Wrong";
			Body = 400;
			Hue = 0;

			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 250.0 );
			SetSkill( SkillName.Wrestling, 250.0 );
			SetSkill( SkillName.DetectHidden, 200.0 );

			Robe robe = new Robe();
			robe.Hue = 1109;
			robe.Name = "Robe of the Idol Keeper";
			robe.LootType = LootType.Blessed;
			AddItem( robe );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
		}

		public WrongChamp( Serial serial ) : base( serial )
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