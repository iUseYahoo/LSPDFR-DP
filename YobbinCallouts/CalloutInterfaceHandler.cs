using System;
using CalloutInterface.API;
using LSPD_First_Response.Mod.Callouts;

namespace YobbinCallouts
{
	
	internal static class CalloutInterfaceHandler
	{
		
		public static void SendCalloutDetails(Callout sender, string priority, string agency = "")
		{
			try
			{
				Functions.SendCalloutDetails(sender, priority, agency);
			}
			catch (Exception ex)
			{
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
			}
		}
	}
}
