using System;
using Server;
using Server.Targeting;

namespace Server.Items
{
	public abstract class BaseTinkerTrap : Item
	{
		public abstract double RequiredSkill{ get; }

		public BaseTinkerTrap() : base( 7194 )
		{
			Weight = 4;
		}

		public BaseTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void OnDoubleClick( Mobile from )
		{
			if ( from == null && !from.Deleted && from.Backpack != null )
				return;

			if ( !IsChildOf(from.Backpack) )
				from.SendLocalizedMessage( 1042001 ); // That must be in your pack for you to use it.
			else if ( from.Skills.Tinkering.Base < RequiredSkill )
				from.SendMessage( String.Format( "You need to have {0} tinkering to use this item.", RequiredSkill ) );
			else
			{
				from.BeginTarget( 1, false, TargetFlags.None, new TargetCallback( OnTrap_OnTarget ) );
				from.SendLocalizedMessage( 502921 ); // What would you like to set a trap on?
			}
		}

		public virtual void OnTrap_OnTarget( Mobile from, object obj )
		{
			if ( Deleted )
				return;

			if ( obj is LockableContainer )
			{
				LockableContainer cont = (LockableContainer)obj;

				if ( cont.TrapType != TrapType.None )
					from.SendMessage( "That container is already trapped." );
				else if ( !cont.Locked )
					from.SendMessage( "The container must be locked to trap it." );
				else
				{
					SetContainerTrap( from, cont );

					from.SendMessage( "You successfully trapped the container." );
					from.PlaySound( from.Female ? 794 : 1066 );

					this.Consume();
				}
			}
			else
				from.SendMessage( "You cannot trap this." );
		}

		public abstract void SetContainerTrap( Mobile from, LockableContainer cont );

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );
			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );
			int version = reader.ReadInt();
		}
	}

	public class LesserExplosionTinkerTrap : BaseTinkerTrap
	{
		public override string DefaultName{ get{ return "a lesser explosion tinker trap"; } }
		public override double RequiredSkill{ get{ return 50.0; } }

		[Constructable]
		public LesserExplosionTinkerTrap() : base()
		{
			Hue = 34;
		}

		public LesserExplosionTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void SetContainerTrap( Mobile from, LockableContainer cont )
		{
			cont.TrapType = TrapType.ExplosionTrap;
			cont.TrapPower = 12 + Utility.Random( 5 );

			if ( ( from.Skills.Tinkering.Base / 100.0 ) >= Utility.RandomDouble() )
				cont.TrapPower += 10;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class ExplosionTinkerTrap : BaseTinkerTrap
	{
		public override string DefaultName{ get{ return "an explosion tinker trap"; } }
		public override double RequiredSkill{ get{ return 70.0; } }

		[Constructable]
		public ExplosionTinkerTrap() : base()
		{
			Hue = 34;
		}

		public ExplosionTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void SetContainerTrap( Mobile from, LockableContainer cont )
		{
			cont.TrapType = TrapType.ExplosionTrap;
			cont.TrapPower = 15 + Utility.Random( 10 );

			if ( ( from.Skills.Tinkering.Base / 250.0 ) >= Utility.RandomDouble() )
				cont.TrapPower += 15;
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class GreaterExplosionTinkerTrap : BaseTinkerTrap
	{
		public override string DefaultName{ get{ return "a greater explosion tinker trap"; } }
		public override double RequiredSkill{ get{ return 100.0; } }

		[Constructable]
		public GreaterExplosionTinkerTrap() : base()
		{
			Hue = 34;
		}

		public GreaterExplosionTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void SetContainerTrap( Mobile from, LockableContainer cont )
		{
			cont.TrapType = TrapType.ExplosionTrap;
			cont.TrapPower = 35 + Utility.Random( 15 );

			if ( ( from.Skills.Tinkering.Base / 175.0 ) >= Utility.RandomDouble() )
				cont.TrapPower += 25 + Utility.Random( 20 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class LesserPoisonTinkerTrap : BaseTinkerTrap
	{
		public override string DefaultName{ get{ return "a lesser poison tinker trap"; } }
		public override double RequiredSkill{ get{ return 50.0; } }

		[Constructable]
		public LesserPoisonTinkerTrap() : base()
		{
			Hue = 1370;
		}

		public LesserPoisonTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void SetContainerTrap( Mobile from, LockableContainer cont )
		{
			cont.TrapType = TrapType.PoisonTrap;
			cont.TrapLevel = 1; //Lesser Poison
			cont.TrapPower = 10;

			if ( ( from.Skills.Tinkering.Base / 100.0 ) >= Utility.RandomDouble() )
			{
				cont.TrapLevel += 1;
				cont.TrapPower += 10 + Utility.Random( 5 );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class PoisonTinkerTrap : BaseTinkerTrap
	{
		public override string DefaultName{ get{ return "a poison tinker trap"; } }
		public override double RequiredSkill{ get{ return 60.0; } }

		[Constructable]
		public PoisonTinkerTrap() : base()
		{
			Hue = 1370;
		}

		public PoisonTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void SetContainerTrap( Mobile from, LockableContainer cont )
		{
			cont.TrapType = TrapType.PoisonTrap;
			cont.TrapLevel = 2; //Regular Poison
			cont.TrapPower = 25;

			if ( ( from.Skills.Tinkering.Base / 175.0 ) >= Utility.RandomDouble() )
			{
				cont.TrapLevel += 1;
				cont.TrapPower += 10 + Utility.Random( 5 );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class GreaterPoisonTinkerTrap : BaseTinkerTrap
	{
		public override string DefaultName{ get{ return "a greater poison tinker trap"; } }
		public override double RequiredSkill{ get{ return 80.0; } }

		[Constructable]
		public GreaterPoisonTinkerTrap() : base()
		{
			Hue = 1370;
		}

		public GreaterPoisonTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void SetContainerTrap( Mobile from, LockableContainer cont )
		{
			cont.TrapType = TrapType.PoisonTrap;
			cont.TrapLevel = 3; //Greater Poison
			cont.TrapPower = 40;

			if ( ( from.Skills.Tinkering.Base / 250.0 ) >= Utility.RandomDouble() )
			{
				cont.TrapLevel += 1;
				cont.TrapPower += 20 + Utility.Random( 5 );
			}
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}

	public class DeadlyPoisonTinkerTrap : BaseTinkerTrap
	{
		public override string DefaultName{ get{ return "a deadly poison tinker trap"; } }
		public override double RequiredSkill{ get{ return 100.0; } }

		[Constructable]
		public DeadlyPoisonTinkerTrap() : base()
		{
			Hue = 1370;
		}

		public DeadlyPoisonTinkerTrap( Serial serial ) : base( serial )
		{
		}

		public override void SetContainerTrap( Mobile from, LockableContainer cont )
		{
			cont.TrapType = TrapType.PoisonTrap;
			cont.TrapLevel = 4; //Deadly Poison
			cont.TrapPower = 50;

			if ( 0.3333 >= Utility.RandomDouble() )
				cont.TrapPower += 20 + Utility.Random( 5 );
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int)0 ); // version
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();
		}
	}
}