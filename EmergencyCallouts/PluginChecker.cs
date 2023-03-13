using System;
using System.Linq;
using System.Reflection;
using LSPD_First_Response.Mod.API;

namespace EmergencyCallouts.Other
{
	
	internal static class PluginChecker
	{
		
		private static readonly Func<string, Version, bool> IsVersionLoaded = (string plugin, Version version) => Functions.GetAllUserPlugins().Any((Assembly x) => x.GetName().Name.Equals(plugin) && x.GetName().Version.CompareTo(version) >= 0);

		
		public static readonly bool IsCalloutInterfaceRunning = PluginChecker.IsVersionLoaded("CalloutInterface", new Version("1.2"));
	}
}
