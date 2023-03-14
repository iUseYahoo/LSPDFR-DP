using System;
using Rage;

namespace YobbinCallouts.Utilities
{
	
	public static class VehicleExtensions
	{
		
		public static void OpenDoor(this Vehicle vehicle, VehicleExtensions.Doors door, bool openInstantly = false)
		{
			bool flag = vehicle.Doors[(int)door].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[(int)door]);
			if (flag)
			{
				vehicle.Doors[(int)door].Open(false);
			}
		}

		
		public static void OpenDoors(this Vehicle vehicle, VehicleExtensions.Doors[] doors, bool openInstantly = false)
		{
			foreach (VehicleExtensions.Doors door in doors)
			{
				int index = (int)door;
				bool flag = vehicle.Doors[index].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[index]);
				if (flag)
				{
					vehicle.Doors[index].Open(false);
				}
			}
		}

		
		public static void OpenAllDoors(this Vehicle vehicle, bool openInstantly = false)
		{
			for (int doorIndex = 0; doorIndex < 6; doorIndex++)
			{
				bool flag = vehicle.Doors[doorIndex].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[doorIndex]);
				if (flag)
				{
					vehicle.Doors[doorIndex].Open(false);
				}
			}
		}

		
		public static void OpenDoorsForSearch(this Vehicle vehicle, bool openInstantly = false)
		{
			for (int doorIndex = 0; doorIndex < 6; doorIndex++)
			{
				bool flag = doorIndex == 4;
				if (!flag)
				{
					bool flag2 = vehicle.Doors[doorIndex].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[doorIndex]);
					if (flag2)
					{
						vehicle.Doors[doorIndex].Open(false);
					}
				}
			}
		}

		
		public static void CloseDoor(this Vehicle vehicle, VehicleExtensions.Doors door, bool closeInstantly = false)
		{
			bool flag = vehicle.Doors[(int)door].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[(int)door]);
			if (flag)
			{
				vehicle.Doors[(int)door].Close(false);
			}
		}

		
		public static void CloseDoors(this Vehicle vehicle, VehicleExtensions.Doors[] doors, bool closeInstantly = false)
		{
			foreach (VehicleExtensions.Doors door in doors)
			{
				int index = (int)door;
				bool flag = vehicle.Doors[index].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[index]);
				if (flag)
				{
					vehicle.Doors[index].Close(false);
				}
			}
		}

		
		public static void CloseAllDoors(this Vehicle vehicle, bool closeInstantly = false)
		{
			for (int doorIndex = 0; doorIndex < 6; doorIndex++)
			{
				bool flag = vehicle.Doors[doorIndex].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[doorIndex]);
				if (flag)
				{
					vehicle.Doors[doorIndex].Close(false);
				}
			}
		}

		
		public static void CloseDoorsForSearch(this Vehicle vehicle, bool closeInstantly = false)
		{
			for (int doorIndex = 0; doorIndex < 6; doorIndex++)
			{
				bool flag = doorIndex == 4;
				if (!flag)
				{
					bool flag2 = vehicle.Doors[doorIndex].IsValid() && vehicle.HasBone(VehicleExtensions.DoorMappings[doorIndex]);
					if (flag2)
					{
						vehicle.Doors[doorIndex].Close(false);
					}
				}
			}
		}

		
		private static readonly string[] DoorMappings = new string[]
		{
			"door_dside_f",
			"door_pside_f",
			"door_dside_r",
			"door_pside_r",
			"bonnet",
			"boot"
		};

		
		public enum Doors
		{
			
			FrontLeft,
			
			FrontRight,
			
			BackLeft,
			
			BackRight,
			
			Hood,
			
			Trunk
		}
	}
}
