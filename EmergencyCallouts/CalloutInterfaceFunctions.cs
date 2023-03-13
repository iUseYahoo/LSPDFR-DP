using System;
using CalloutInterface.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace EmergencyCallouts.Other
{
	
	internal static class CalloutInterfaceFunctions
	{
		
		public static void SendCalloutDetails(Callout sender, string priority, string agency = "")
		{
			try
			{
				Functions.SendCalloutDetails(sender, priority, agency);
			}
			catch (Exception ex)
			{
				Game.LogTrivial(ex.Message);
			}
		}

		
		public static void SendMessage(Callout sender, string message)
		{
			try
			{
				Functions.SendMessage(sender, message);
			}
			catch (Exception ex)
			{
				Game.LogTrivial(ex.Message);
			}
		}
	}
}
