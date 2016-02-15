using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Engines.IdolSystem;

namespace Server.Mobiles
{
	public class HythChamp : BaseDungeonChampion
	{
		[Constructable]
		public HythChamp() : base( AIType.AI_Melee )
		{
			Title = "of Hythloth";
			Body = 149;
			Hue = 1154;
			BaseSoundID = 0x4B0;

			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 250.0 );
			SetSkill( SkillName.Wrestling, 250.0 );
			SetSkill( SkillName.DetectHidden, 200.0 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
		}

		public HythChamp( Serial serial ) : base( serial )
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