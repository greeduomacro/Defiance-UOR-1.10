using System;
using System.Collections;
using Server.Items;
using Server.Targeting;
using Server.Engines.IdolSystem;

namespace Server.Mobiles
{
	public class DecChamp : BaseDungeonChampion
	{
		[Constructable]
		public DecChamp() : base( AIType.AI_Melee )
		{
			Title = "of Deceit";
			Hue = 1250;
			Body = 154;
			BaseSoundID = 471;

			SetSkill( SkillName.MagicResist, 80.0 );
			SetSkill( SkillName.Tactics, 250.0 );
			SetSkill( SkillName.Wrestling, 250.0 );
			SetSkill( SkillName.DetectHidden, 200.0 );
		}

		public override void OnDeath( Container c )
		{
			base.OnDeath( c );
		}

		public DecChamp( Serial serial ) : base( serial )
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