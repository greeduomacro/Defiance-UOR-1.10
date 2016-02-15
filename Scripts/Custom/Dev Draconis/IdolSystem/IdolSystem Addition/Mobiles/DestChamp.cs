using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Engines.IdolSystem;

namespace Server.Mobiles
{
	public class DestChamp : BaseDungeonChampion
	{
		[Constructable]
		public DestChamp() : base( AIType.AI_Melee )
		{
			Title = "of Destard";
			Body = 104;
			Hue = 1109;
			BaseSoundID = 0x488;

			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 250.0 );
			SetSkill( SkillName.Wrestling, 250.0 );
			SetSkill( SkillName.DetectHidden, 200.0 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
		}

		public DestChamp( Serial serial ) : base( serial )
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