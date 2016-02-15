using System;
using System.Collections.Generic;
using System.Text;
using Server.Items;
using Server.Misc;
using Server.Network;
using Server.Factions;

namespace Server.Ethics.Evil
{
	public sealed class UnholyItem : Power
	{
		public UnholyItem()
		{
			m_Definition = new PowerDefinition(
					10,
					"Unholy Item",
					"Vidda K'balc",
					""
				);
		}

		public override void BeginInvoke( Player from )
		{
			from.Mobile.BeginTarget( 12, false, Targeting.TargetFlags.None, new TargetStateCallback( Power_OnTarget ), from );
			from.Mobile.SendMessage( "Which item do you wish to imbue?" );
		}

		private void Power_OnTarget( Mobile fromMobile, object obj, object state )
		{
			Player from = state as Player;

			Item item = obj as Item;

			if ( item == null || item.Deleted )
				return;

			if ( item.Parent != fromMobile )
				fromMobile.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You may only imbue items you are wearing." );
			//else if ( EthicsItem.Find( item ) != null )
			//	fromMobile.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "That has already been imbued." );
			else if ( item is IFactionItem && ((IFactionItem)item).FactionItemState != null )
				fromMobile.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "The magic surrounding this item is too chaotic to imbue." );
			//else if ( fromMobile.Map == Map.Felucca && !fromMobile.InRange( new Point3D( 1458, 844, 5 ), 6 ) )
			//	fromMobile.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You require the power of an evil shrine to imbue this item." );
			else if ( !fromMobile.Backpack.ConsumeTotal( typeof( Gold ), TestCenter.Enabled ? 100 : 5000 ) )
				fromMobile.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, String.Format( "You must sacrifice {0} gold pieces to imbue this item.", TestCenter.Enabled ? "100" : "5,000" ) );
			else if ( ( item is BaseQuiver || item is Spellbook || item is BaseClothing || item is BaseArmor || item is BaseWeapon ) )
			{
				if ( CheckInvoke( from ) )
				{
					EthicsItem ethicItem = EthicsItem.Find( item );
					if ( ethicItem != null )
						ethicItem.StartExpiration();
					else
						EthicsItem.Imbue( item, Ethic.Evil, true, Ethic.Evil.Definition.PrimaryHue );

					fromMobile.FixedEffect( 0x375A, 10, 20 );
					fromMobile.PlaySound( 0x209 );

					FinishInvoke( from );
				}
			}
			else
				fromMobile.LocalOverheadMessage( MessageType.Regular, 0x3B2, false, "You may not imbue that." );
		}
	}
}