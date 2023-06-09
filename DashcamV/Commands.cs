using System;
using Rage;
using Rage.Attributes;
using Rage.Native;

namespace DashcamV
{
	
	public static class Commands
	{
		
		[ConsoleCommand(Description = "Reloads Dashcam V's configuration file without having to reload the plugin.")]
		public static void ReloadDashcamConfig()
		{
			Configs.RunConfigCheck();
			for (;;)
			{
				IL_05:
				uint num = 1482019632U;
				for (;;)
				{
					uint num2;
					switch ((num2 = (num ^ 912028018U)) % 3U)
					{
					case 0U:
						goto IL_05;
					case 2U:
						Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(2215428565U));
						num = (num2 * 3576133968U ^ 3501917711U);
						continue;
					}
					return;
				}
			}
		}

		
		[ConsoleCommand(Description = "Set Dashcam V to use the metric measurement system.")]
		public static void UseMetric()
		{
			DashHelper.units = 0;
			for (;;)
			{
				IL_06:
				uint num = 2175556503U;
				for (;;)
				{
					uint num2;
					switch ((num2 = (num ^ 3352751092U)) % 3U)
					{
					case 0U:
						goto IL_06;
					case 2U:
						Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u206D\u200C\u206D\u202B\u200E\u206C\u200B\u202A\u200B\u202B\u200B\u200D\u202C\u202B\u206D\u200E\u200E\u206E\u206B\u202B\u206E\u202A\u202B\u202A\u202D\u206C\u206B\u202C\u200C\u206C\u206D\u202B\u206A\u200E\u206D\u202A\u202B\u200C\u202A\u202E<string>(231698949U));
						num = (num2 * 3753403842U ^ 741323069U);
						continue;
					}
					return;
				}
			}
		}

		
		[ConsoleCommand(Description = "Set Dashcam V to use the imperial measurement system.")]
		public static void UseImperial()
		{
			DashHelper.units = 1;
			Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(2104410801U));
		}

		
		[ConsoleCommand(Description = "Enable or disable Dashcam V's black and white filter.")]
		public static void UseFilter(bool enable)
		{
			if (enable)
			{
				goto IL_03;
			}
			goto IL_64;
			uint num2;
			for (;;)
			{
				IL_08:
				uint num;
				switch ((num = (num2 ^ 155026170U)) % 6U)
				{
				case 0U:
					goto IL_03;
				case 1U:
					goto IL_64;
				case 2U:
					Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u206D\u200E\u202D\u206B\u202A\u206A\u200B\u202E\u202D\u200C\u202C\u206D\u206E\u206D\u200E\u202B\u200E\u206B\u202C\u202E\u206F\u200C\u202E\u206C\u200E\u202A\u206B\u202B\u200E\u200D\u200C\u200B\u206B\u206A\u206B\u200D\u202C\u202B\u200B\u202E<string>(3001528618U));
					num2 = (num * 2651413490U ^ 1863799931U);
					continue;
				case 3U:
					return;
				case 5U:
					DashHelper.filter = true;
					num2 = (num * 3107103187U ^ 1246751675U);
					continue;
				}
				break;
			}
			NativeFunction.CallByHash<uint>(1083088722320385809UL, Array.Empty<NativeArgument>());
			Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(1078608466U));
			return;
			IL_03:
			num2 = 2102967095U;
			goto IL_08;
			IL_64:
			DashHelper.filter = false;
			num2 = 231660374U;
			goto IL_08;
		}

		
		[ConsoleCommand(Description = "Set the unit name to use on the dashcam. Type %20 to indicate a space.")]
		public static void SetUnitName(string name)
		{
			DashHelper.configUnit = Commands.\u202B\u206D\u206B\u200B\u202B\u206F\u200B\u206D\u206A\u206C\u200D\u200C\u200D\u200E\u206D\u202C\u206B\u200D\u206C\u206D\u200E\u206F\u206C\u206E\u200B\u200B\u202B\u200F\u206F\u206C\u206F\u200C\u206F\u202B\u200D\u206D\u200F\u206B\u206B\u202E(name, \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u202B\u206A\u206C\u202E\u202E\u202B\u202A\u200E\u206A\u206E\u200E\u200E\u202E\u200C\u206E\u200D\u202E\u202D\u206A\u202A\u206D\u200E\u200E\u202A\u206E\u206D\u202B\u202E\u206A\u202E\u202A\u202C\u200B\u206E\u206A\u206A\u206E\u200D\u202A\u202E<string>(2617490683U), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u202B\u206A\u206C\u202E\u202E\u202B\u202A\u200E\u206A\u206E\u200E\u200E\u202E\u200C\u206E\u200D\u202E\u202D\u206A\u202A\u206D\u200E\u200E\u202A\u206E\u206D\u202B\u202E\u206A\u202E\u202A\u202C\u200B\u206E\u206A\u206A\u206E\u200D\u202A\u202E<string>(3071144221U));
			Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), Commands.\u206D\u202D\u206D\u200D\u202A\u206F\u202C\u202A\u200D\u200D\u200C\u202B\u200F\u206D\u206F\u202C\u202A\u206E\u200C\u206C\u202C\u202A\u200C\u206A\u206F\u206A\u202C\u202A\u202A\u202A\u206A\u206C\u202B\u202A\u202E\u202B\u206E\u202D\u206C\u202E(\u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u202B\u202D\u200C\u206B\u202A\u200E\u206F\u206C\u206F\u200F\u202B\u200F\u200E\u206C\u202E\u202B\u200D\u200D\u200E\u206A\u200C\u202E\u202D\u206C\u206F\u206D\u202B\u200E\u206C\u206C\u202B\u202D\u200C\u202B\u200F\u200B\u206E\u200C\u202E<string>(477502498U), Commands.\u202B\u206D\u206B\u200B\u202B\u206F\u200B\u206D\u206A\u206C\u200D\u200C\u200D\u200E\u206D\u202C\u206B\u200D\u206C\u206D\u200E\u206F\u206C\u206E\u200B\u200B\u202B\u200F\u206F\u206C\u206F\u200C\u206F\u202B\u200D\u206D\u200F\u206B\u206B\u202E(name, \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u202B\u202D\u200C\u206B\u202A\u200E\u206F\u206C\u206F\u200F\u202B\u200F\u200E\u206C\u202E\u202B\u200D\u200D\u200E\u206A\u200C\u202E\u202D\u206C\u206F\u206D\u202B\u200E\u206C\u206C\u202B\u202D\u200C\u202B\u200F\u200B\u206E\u200C\u202E<string>(872736358U), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(3110028088U)), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u206D\u200C\u206D\u202B\u200E\u206C\u200B\u202A\u200B\u202B\u200B\u200D\u202C\u202B\u206D\u200E\u200E\u206E\u206B\u202B\u206E\u202A\u202B\u202A\u202D\u206C\u206B\u202C\u200C\u206C\u206D\u202B\u206A\u200E\u206D\u202A\u202B\u200C\u202A\u202E<string>(846757699U)));
		}

		
		[ConsoleCommand(Description = "Resets the unit name to the default vehicle-unique name.")]
		public static void ResetUnitName()
		{
			DashHelper.configUnit = "";
			Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u202B\u206A\u206C\u202E\u202E\u202B\u202A\u200E\u206A\u206E\u200E\u200E\u202E\u200C\u206E\u200D\u202E\u202D\u206A\u202A\u206D\u200E\u200E\u202A\u206E\u206D\u202B\u202E\u206A\u202E\u202A\u202C\u200B\u206E\u206A\u206A\u206E\u200D\u202A\u202E<string>(2178578460U));
		}

		
		[ConsoleCommand(Description = "Enable or disable dashcam text on all vehicle view modes.")]
		public static void EnableDashcamOnAllViews(bool enable)
		{
			if (enable)
			{
				goto IL_03;
			}
			goto IL_31;
			uint num2;
			for (;;)
			{
				IL_08:
				uint num;
				switch ((num = (num2 ^ 3454992537U)) % 6U)
				{
				case 0U:
					return;
				case 1U:
					goto IL_31;
				case 2U:
					goto IL_03;
				case 3U:
					Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(4115645375U));
					num2 = (num * 181966309U ^ 2717311945U);
					continue;
				case 4U:
					DashHelper.dashAllViews = true;
					Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u202B\u206A\u206C\u202E\u202E\u202B\u202A\u200E\u206A\u206E\u200E\u200E\u202E\u200C\u206E\u200D\u202E\u202D\u206A\u202A\u206D\u200E\u200E\u202A\u206E\u206D\u202B\u202E\u206A\u202E\u202A\u202C\u200B\u206E\u206A\u206A\u206E\u200D\u202A\u202E<string>(832359161U));
					num2 = (num * 3632092733U ^ 2045796205U);
					continue;
				}
				break;
			}
			return;
			IL_03:
			num2 = 2915546771U;
			goto IL_08;
			IL_31:
			DashHelper.dashAllViews = false;
			num2 = 2823941272U;
			goto IL_08;
		}

		
		[ConsoleCommand(Description = "Set which layout Dashcam V should use (0 - regular, 1 - IVDashCam layout).")]
		public static void SetLayout(int layout)
		{
			if (DashHelper.eventOn)
			{
				goto IL_07;
			}
			goto IL_7D;
			uint num2;
			for (;;)
			{
				IL_0C:
				uint num;
				switch ((num = (num2 ^ 1203189608U)) % 23U)
				{
				case 0U:
					Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(2558574381U));
					num2 = 107542149U;
					continue;
				case 1U:
					Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(2578759429U));
					num2 = (num * 1556387878U ^ 111014156U);
					continue;
				case 2U:
					goto IL_07;
				case 3U:
					num2 = ((DashHelper.layout == 1) ? 1060240305U : 81806070U);
					continue;
				case 5U:
					DashHelper.InitializeDashcam(1);
					DashHelper.eventOn = true;
					num2 = (num * 3841741614U ^ 2206249885U);
					continue;
				case 6U:
					return;
				case 7U:
					num2 = ((DashHelper.isDashOn ? 2055445896U : 1967688375U) ^ num * 2145096983U);
					continue;
				case 8U:
					num2 = (((!DashHelper.eventOn) ? 961605469U : 1945787346U) ^ num * 4020337604U);
					continue;
				case 9U:
					num2 = (((DashHelper.layout == 1) ? 477975173U : 832355733U) ^ num * 2859453480U);
					continue;
				case 10U:
					goto IL_7D;
				case 11U:
					num2 = (((!DashHelper.isDashOn) ? 1583044713U : 1010491556U) ^ num * 2348949679U);
					continue;
				case 12U:
					num2 = (((!DashHelper.isDashOn) ? 3383141789U : 2259109630U) ^ num * 3531835056U);
					continue;
				case 13U:
					DashHelper.eventOn = false;
					num2 = 1827168829U;
					continue;
				case 14U:
					DashHelper.InitializeDashcam(0);
					DashHelper.eventOn = true;
					num2 = (num * 843318476U ^ 1065138069U);
					continue;
				case 15U:
					Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u200C\u206D\u202D\u202A\u200E\u202D\u202E\u200F\u206D\u200F\u200F\u202C\u200B\u202E\u200F\u206E\u202E\u200F\u206F\u202A\u202C\u202E\u202B\u206E\u200E\u206D\u206E\u202C\u200C\u202D\u206A\u202B\u202C\u206D\u200E\u202C\u202C\u200C\u202E\u202E<string>(927220606U));
					num2 = (num * 266272674U ^ 775921018U);
					continue;
				case 16U:
					Commands.\u200D\u206B\u206B\u202A\u200F\u206C\u202D\u202B\u202D\u202B\u202E\u200E\u200C\u202A\u202D\u200D\u206A\u202B\u202B\u206E\u206E\u200C\u202E\u200E\u202C\u200D\u202D\u200D\u206C\u200C\u200C\u206B\u202A\u206B\u200F\u200B\u202A\u202D\u202A\u202E(new EventHandler<GraphicsEventArgs>(DashHelper.OnFrameRenderLayout2));
					num2 = (num * 4215403187U ^ 2848245439U);
					continue;
				case 17U:
					num2 = (num * 2252850902U ^ 184192638U);
					continue;
				case 18U:
					DashHelper.layout = 1;
					Commands.\u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(Commands.\u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E(), \u206B\u200D\u200B\u202E\u206A\u206C\u200F\u206D\u200B\u202B\u202C\u206B\u206E\u206E\u206D\u206E\u202D\u202E\u202E\u202D\u206A\u202A\u206B\u206F\u202D\u202C\u202D\u200E\u200F\u206E\u200C\u206D\u206C\u202E\u202C\u206E\u202B\u200B\u200D\u202E.\u206D\u200C\u206D\u202B\u200E\u206C\u200B\u202A\u200B\u202B\u200B\u200D\u202C\u202B\u206D\u200E\u200E\u206E\u206B\u202B\u206E\u202A\u202B\u202A\u202D\u206C\u206B\u202C\u200C\u206C\u206D\u202B\u206A\u200E\u206D\u202A\u202B\u200C\u202A\u202E<string>(2307213717U));
					num2 = (num * 481992340U ^ 1272848674U);
					continue;
				case 19U:
					num2 = ((!DashHelper.eventOn) ? 988143296U : 970995229U);
					continue;
				case 20U:
					num2 = ((layout != 1) ? 932848792U : 1702803832U);
					continue;
				case 21U:
					Commands.\u200D\u206B\u206B\u202A\u200F\u206C\u202D\u202B\u202D\u202B\u202E\u200E\u200C\u202A\u202D\u200D\u206A\u202B\u202B\u206E\u206E\u200C\u202E\u200E\u202C\u200D\u202D\u200D\u206C\u200C\u200C\u206B\u202A\u206B\u200F\u200B\u202A\u202D\u202A\u202E(new EventHandler<GraphicsEventArgs>(DashHelper.OnFrameRenderLayout1));
					num2 = 1125609176U;
					continue;
				case 22U:
					DashHelper.layout = 0;
					num2 = (num * 726907258U ^ 1103985584U);
					continue;
				}
				break;
			}
			return;
			IL_07:
			num2 = 1526796654U;
			goto IL_0C;
			IL_7D:
			num2 = ((layout == 0) ? 460618058U : 1097055175U);
			goto IL_0C;
		}

		
		static GameConsole \u202B\u206E\u206B\u200C\u202C\u206C\u200B\u206A\u200D\u202E\u202B\u206B\u206E\u202D\u200D\u206A\u202D\u200E\u200D\u202A\u200E\u200D\u200D\u206A\u202D\u200E\u200B\u200B\u202A\u200F\u206A\u206E\u202C\u202D\u200E\u200C\u206B\u202B\u206A\u202E()
		{
			return Game.Console;
		}

		
		static void \u200B\u202C\u202E\u206F\u202B\u202B\u206F\u200D\u206F\u206D\u206C\u202A\u206A\u200C\u200C\u206E\u202C\u202B\u206B\u206B\u202C\u202E\u206C\u206E\u202B\u206F\u206C\u200F\u200F\u202D\u200E\u206A\u206D\u206C\u200E\u206D\u200B\u202D\u206D\u202E(GameConsole A_0, string A_1)
		{
			A_0.Print(A_1);
		}

		
		static string \u202B\u206D\u206B\u200B\u202B\u206F\u200B\u206D\u206A\u206C\u200D\u200C\u200D\u200E\u206D\u202C\u206B\u200D\u206C\u206D\u200E\u206F\u206C\u206E\u200B\u200B\u202B\u200F\u206F\u206C\u206F\u200C\u206F\u202B\u200D\u206D\u200F\u206B\u206B\u202E(string A_0, string A_1, string A_2)
		{
			return A_0.Replace(A_1, A_2);
		}

		
		static string \u206D\u202D\u206D\u200D\u202A\u206F\u202C\u202A\u200D\u200D\u200C\u202B\u200F\u206D\u206F\u202C\u202A\u206E\u200C\u206C\u202C\u202A\u200C\u206A\u206F\u206A\u202C\u202A\u202A\u202A\u206A\u206C\u202B\u202A\u202E\u202B\u206E\u202D\u206C\u202E(string A_0, string A_1, string A_2)
		{
			return A_0 + A_1 + A_2;
		}

		
		static void \u200D\u206B\u206B\u202A\u200F\u206C\u202D\u202B\u202D\u202B\u202E\u200E\u200C\u202A\u202D\u200D\u206A\u202B\u202B\u206E\u206E\u200C\u202E\u200E\u202C\u200D\u202D\u200D\u206C\u200C\u200C\u206B\u202A\u206B\u200F\u200B\u202A\u202D\u202A\u202E(EventHandler<GraphicsEventArgs> A_0)
		{
			Game.FrameRender -= A_0;
		}
	}
}
