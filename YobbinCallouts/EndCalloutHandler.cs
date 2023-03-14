using System;
using Rage;

namespace YobbinCallouts
{
	
	internal class EndCalloutHandler
	{
		
		public static void EndCallout()
		{
			bool flag = Config.LeaveCalloutsRunning && !EndCalloutHandler.CalloutForcedEnd;
			if (flag)
			{
				GameFiber.Wait(2000);
				Game.DisplayHelp("Press ~y~" + Config.CalloutEndKey.ToString() + " ~w~to ~b~Finish~w~ the Callout.");
				Game.LogTrivial("YOBBINCALLOUTS: ENDCALLOUTHANDLER - Player Will Manually End the Callout");
				while (!Game.IsKeyDown(Config.CalloutEndKey))
				{
					GameFiber.Wait(0);
				}
			}
			else
			{
				bool flag2 = !EndCalloutHandler.CalloutForcedEnd;
				if (flag2)
				{
					GameFiber.Wait(2000);
				}
				else
				{
					Game.LogTrivial("YOBBINCALLOUTS: Callout Was Ended at Start, May Cause Issues!");
				}
				Game.LogTrivial("YOBBINCALLOUTS: ENDCALLOUTHANDLER - Ending Callout Immediately");
			}
			EndCalloutHandler.CalloutForcedEnd = false;
		}

		
		public static bool CalloutForcedEnd;
	}
}
