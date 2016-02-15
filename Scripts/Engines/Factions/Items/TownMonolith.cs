using System;
using System.Collections.Generic;

namespace Server.Factions
{
	public class TownMonolith : BaseMonolith
	{
		public override int DefaultLabelNumber{ get{ return 1041403; } } // A Faction Town Sigil Monolith

		private List<ControlPoint> m_ControlPoints = new List<ControlPoint>();

		public void AddControlPoint( ControlPoint controlPoint )
		{
			m_ControlPoints.Add( controlPoint );
			controlPoint.Owner = this.Faction;
		}

		public void RemoveControlPoint( ControlPoint controlPoint )
		{
			m_ControlPoints.Remove( controlPoint );
			controlPoint.Owner = null;
		}

		public void CaptureControlPoints( Faction f )
		{
			foreach ( ControlPoint controlPoint in m_ControlPoints )
				controlPoint.Owner = f;
		}

		public bool HasAllControlPoints( Faction faction )
		{
			foreach ( ControlPoint controlPoint in m_ControlPoints )
			{
				if ( controlPoint.Owner != faction )
					return false;
			}

			return true;
		}

		public override void OnTownChanged()
		{
			AssignName( Town == null ? null : Town.Definition.TownMonolithName );
		}

		public TownMonolith() : this( null )
		{
		}

		public TownMonolith( Town town ) : base( town, null )
		{
		}

		public TownMonolith( Serial serial ) : base( serial )
		{
		}

		public override void Serialize( GenericWriter writer )
		{
			base.Serialize( writer );

			writer.Write( (int) 2 ); // version

			writer.WriteItemList<ControlPoint>( m_ControlPoints );
		}

		public override void Deserialize( GenericReader reader )
		{
			base.Deserialize( reader );

			int version = reader.ReadInt();

			switch ( version )
			{
				case 2:
				{
					m_ControlPoints = reader.ReadStrongItemList<ControlPoint>();

					break;
				}
				case 1:
				{
					int count = reader.ReadInt();
					for ( int i = 0; i < count; i++ )
						m_ControlPoints.Add( reader.ReadItem<ControlPoint>() );

					break;
				}
			}
		}
	}
}