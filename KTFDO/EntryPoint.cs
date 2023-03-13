using System;
using Rage;

namespace KTFDO
{
	public static class EntryPoint
	{
		public static void Main()
		{
			Game.DisplayNotification("~b~KTFDO ~o~v0.1~w~ by ~b~khorio~w~ loaded");
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					GameFiber.Yield();
					try
					{
						if (Game.LocalPlayer.Character.IsInAnyPoliceVehicle && Game.IsControlPressed(2, 75))
						{
							GameFiber.Sleep(150);
							if (Game.LocalPlayer.Character.IsInAnyPoliceVehicle && Game.IsControlPressed(2, 75))
							{
								Game.LocalPlayer.Character.Tasks.LeaveVehicle(256);
							}
						}
					}
					catch (Exception ex)
					{
						Game.LogTrivial("KTFDO Encountered an error: " + ex.Message);
						Game.LogTrivial("Stack: " + ex.StackTrace);
						Game.LogTrivial("KTFDO: Error handled");
					}
				}
			});
		}
	}
}
