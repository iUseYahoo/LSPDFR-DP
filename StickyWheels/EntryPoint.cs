using System;
using Rage;

namespace StickyWheels
{
	
	public static class EntryPoint
	{
		
		public static void Main()
		{
			Game.DisplayNotification("~b~StickyWheels ~o~v0.2~w~ by ~b~khorio~w~ loaded");
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					GameFiber.Yield();
					try
					{
						if (Game.LocalPlayer.Character.LastVehicle && Game.LocalPlayer.Character.LastVehicle.Speed == 0f)
						{
							if (Game.LocalPlayer.Character.LastVehicle.SteeringAngle > 20f)
							{
								EntryPoint.steeringAngle = 40f;
							}
							else if (Game.LocalPlayer.Character.LastVehicle.SteeringAngle < -20f)
							{
								EntryPoint.steeringAngle = -40f;
							}
							else if (Game.LocalPlayer.Character.LastVehicle.SteeringAngle > 5f || Game.LocalPlayer.Character.LastVehicle.SteeringAngle < -5f)
							{
								EntryPoint.steeringAngle = Game.LocalPlayer.Character.LastVehicle.SteeringAngle;
							}
							Game.LocalPlayer.Character.LastVehicle.SteeringAngle = EntryPoint.steeringAngle;
						}
					}
					catch (Exception ex)
					{
						Game.LogTrivial("StickyWheels Encountered an error: " + ex.Message);
						Game.LogTrivial("Stack: " + ex.StackTrace);
						Game.LogTrivial("StickyWheels: Error handled");
					}
				}
			});
		}

		
		private static float steeringAngle;
	}
}
