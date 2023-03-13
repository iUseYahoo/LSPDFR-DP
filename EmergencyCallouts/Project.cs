using System;
using System.Reflection;

namespace EmergencyCallouts.Essential
{
	
	internal static class Project
	{
		
		
		internal static string Name
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Name;
			}
		}

		
		
		internal static string LocalVersion
		{
			get
			{
				return Assembly.GetExecutingAssembly().GetName().Version.ToString().Substring(0, 5);
			}
		}

		
		
		internal static string SettingsPath
		{
			get
			{
				return "Plugins/LSPDFR/Emergency Callouts.ini";
			}
		}
	}
}
