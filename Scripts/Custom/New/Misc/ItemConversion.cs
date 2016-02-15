using System;
using System.Collections.Generic;
using Server;
using Server.Misc;
using Server.Items;
using Server.Mobiles;
using Server.Regions;
using Server.Multis;

namespace Server
{
	public interface IConvertableItem
	{
		Item Convert();
	}

	public interface IConvertableMobile
	{
		Mobile Convert();
	}

	public class ObjectConversion
	{
		//Conversion List
		public static List<IConvertableItem> ItemConversionList = new List<IConvertableItem>();
		public static List<IConvertableMobile> MobileConversionList = new List<IConvertableMobile>();

		public static void Initialize()
		{
			//World Loading is finished?

			if ( ItemConversionList.Count > 0 ) //Do we have items to convert?
				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( ConvertItems ) );
			//We do not want these to overlap, so if ConvertItems is executed, that will chain to ConvertMobiles.
			else if ( MobileConversionList.Count > 0 ) //Do we have mobiles to convert?
				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( ConvertMobiles ) );
		}

		public static void ConvertItems()
		{
			foreach ( IConvertableItem ci in ItemConversionList )
			{
				Item before = ci as Item; //Always an item
				Item after = ci.Convert();

				if ( after != null )
				{
					if ( before.Parent is Container )
					{
						after.Location = new Point3D( before.X, before.Y, 0 );
						((Container)before.Parent).AddItem( after );
					}
					else if ( before.Parent is Mobile )
						((Mobile)before.Parent).AddItem( after );
					else
						after.MoveToWorld( before.Location, before.Map );

					//Is it in a house?
					BaseHouse house = BaseHouse.FindHouseAt( before );
					if ( house != null && house.Owner != null )
					{
						if ( before is Container && before.IsSecure )
						{
							SecureLevel level = SecureLevel.Owner;

							for ( int i = 0;i < house.Secures.Count; i++ )
							{
								SecureInfo info = house.Secures[i];
								if ( info.Item == before )
								{
									level = info.Level;
									break;
								}
							}

							SecureInfo si = new SecureInfo( (Container)after, level );

							after.IsLockedDown = false;
							after.IsSecure = true;

							house.Secures.Add( si );
							house.LockDowns.Remove( after );
							after.Movable = false;
						}
						else if ( before.IsLockedDown )
						{
							house.LockDowns.Add( after );
							after.IsLockedDown = true;
							after.Movable = false;
						}
					}

					if ( before.Movable )
						after.Movable = true;

					before.Delete();
				}
			}

			ItemConversionList.Clear();

			if ( MobileConversionList.Count > 0 ) //Do we have mobiles to convert?
				Timer.DelayCall( TimeSpan.FromSeconds( 1.0 ), new TimerCallback( ConvertMobiles ) );
		}

		public static void ConvertMobiles()
		{
			foreach ( IConvertableMobile ci in MobileConversionList )
			{
				Mobile before = ci as Mobile; //Always an item
				Mobile after = ci.Convert();

				//Ownership of stuff?  Items bound to this mobile?  Crafter references? etc.
				//DO NOT USE FOR PLAYER CONVERSION!!!

				after.MoveToWorld( before.Location, before.Map );
			}

			MobileConversionList.Clear();
		}
	}
}