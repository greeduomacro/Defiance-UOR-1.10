using System;
using Server;
using Server.Items;
using Server.Mobiles;
using Server.Misc;

namespace Xanthos.Evo
{
	[CorpseName( "a spider corpse" )]
	public class EvoSpider : BaseEvoMount, IEvoCreature
	{
		public override BaseEvoSpec GetEvoSpec()
		{
			return EvoSpiderSpec.Instance;
		}

		public override BaseEvoEgg GetEvoEgg()
		{
			return new EvoSpiderEvoEgg();
		}

		public override Poison PoisonImmune{ get{ return Poison.Lethal; } }
		public override bool AddPointsOnDamage { get { return true; } }
		public override bool AddPointsOnMelee { get { return false; } }
		public override Type GetEvoDustType() { return typeof( EvoSpiderEvoDust ); }

		public override bool HasBreath{ get{ return false; } }
		public override int BreathRange{ get{ return RangePerception / 2; } }
		public override string DefaultName{ get{ return "a spider hatchling"; } }

		public EvoSpider() : base( 793, 0x3EBB )
		{
		}

		public EvoSpider( Serial serial ) : base( serial )
		{
		}

		public override WeaponAbility GetWeaponAbility()
		{
			return WeaponAbility.Dismount;
		}

		public override bool SubdueBeforeTame{ get{ return true; } } // Must be beaten into submission

		public override int GetAngerSound()
		{
			return 0x27D;
		}

		public override int GetIdleSound()
		{
			return 0x493;
		}

		public override int GetAttackSound()
		{
			return 0x25A;
		}

		public override int GetHurtSound()
		{
			return 0x288;
		}

		public override int GetDeathSound()
		{
			return 0x284;
		}

		public override void OnDoubleClick( Mobile from )
		{
			return;
		}

		public override void Serialize(GenericWriter writer)
		{
			base.Serialize(writer);

			writer.Write( (int)0 );
		}

		public override void Deserialize(GenericReader reader)
		{
			base.Deserialize(reader);

			int version = reader.ReadInt();
		}
	}
}