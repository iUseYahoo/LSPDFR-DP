using System;
using System.Reflection;
using System.Windows.Forms;
using Rage;

namespace RPH_Delete_Vehicle
{
	
	public static class EntryPoint
	{
		
		
		
		public static Keys DeleteKey { get; set; }

		
		
		
		public static Keys DeleteModifierKey { get; set; }

		
		
		
		public static bool ProtectCurrentVehicle { get; set; }

		
		
		
		public static bool ProtectLastVehicle { get; set; }

		
		
		
		public static bool ProtectEmergencyVehicles { get; set; }

		
		
		
		public static bool ShowDebug { get; set; }

		
		public static void Main()
		{
			Game.LogTrivial("Loading settings...");
			EntryPoint.LoadValuesFromIniFile();
			Game.LogTrivial(Assembly.GetExecutingAssembly().GetName().Version.ToString() + " has been initialised.");
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					GameFiber.Yield();
					bool flag = Game.IsKeyDown(EntryPoint.DeleteKey) && (Game.IsKeyDownRightNow(EntryPoint.DeleteModifierKey) || EntryPoint.DeleteModifierKey == Keys.None);
					if (flag)
					{
						EntryPoint.DoDeleteVehicle();
						GameFiber.Sleep(1000);
					}
				}
			});
		}

		
		private static void DoDeleteVehicle()
		{
			try
			{
				Ped playerPed = Game.LocalPlayer.Character;
				Vehicle GetVehicle = (Vehicle)World.GetClosestEntity(playerPed.Position, 6f, 255L);
				bool flag = GetVehicle != null;
				if (flag)
				{
					EntryPoint.Command_Debug("Found a " + GetVehicle.Model.Name + ", Registration: " + GetVehicle.LicensePlate);
					bool flag2 = EntryPoint.ProtectCurrentVehicle && playerPed.IsInAnyVehicle(true);
					if (flag2)
					{
						Game.DisplayNotification("You cannot delete the vehicle you are sitting in!");
					}
					else
					{
						bool flag3 = EntryPoint.ProtectCurrentVehicle && GetVehicle == playerPed.LastVehicle;
						if (flag3)
						{
							Game.DisplayNotification("You cannot delete the last vehicle you were sitting in!");
						}
						else
						{
							bool flag4 = EntryPoint.ProtectEmergencyVehicles && GetVehicle.Class == 18;
							if (flag4)
							{
								Game.DisplayNotification("You cannot delete emergency vehicles!");
							}
							else
							{
								Ped GetDriver = GetVehicle.Driver;
								bool flag5 = GetDriver != null;
								if (flag5)
								{
									GetDriver.Delete();
								}
								GetVehicle.Delete();
							}
						}
					}
				}
				else
				{
					Game.DisplayNotification("Could not find vehicle to delete! Try getting a little closer.");
				}
			}
			catch (Exception e)
			{
				EntryPoint.ErrorLogger(e, "Activation", "Error during execution");
			}
		}

		
		private static void LoadValuesFromIniFile()
		{
			InitializationFile ini = new InitializationFile(EntryPoint.INIpath);
			ini.Create();
			try
			{
				bool flag = ini.DoesKeyExist("Keyboard", "DeleteKey");
				if (flag)
				{
					EntryPoint.DeleteKey = ini.ReadEnum<Keys>("Keyboard", "DeleteKey", Keys.L);
				}
				else
				{
					ini.Write("Keyboard", "DeleteKey", "L");
					EntryPoint.DeleteKey = Keys.L;
				}
				bool flag2 = ini.DoesKeyExist("Keyboard", "DeleteModifierKey");
				if (flag2)
				{
					EntryPoint.DeleteModifierKey = ini.ReadEnum<Keys>("Keyboard", "DeleteModifierKey", Keys.ShiftKey);
				}
				else
				{
					ini.Write("Keyboard", "DeleteModifierKey", "ShiftKey");
					EntryPoint.DeleteModifierKey = Keys.ShiftKey;
				}
				bool flag3 = ini.DoesKeyExist("Other", "ProtectCurrentVehicle");
				if (flag3)
				{
					EntryPoint.ProtectCurrentVehicle = ini.ReadBoolean("Other", "ProtectCurrentVehicle", true);
				}
				else
				{
					ini.Write("Other", "ProtectCurrentVehicle", "true");
					EntryPoint.ProtectCurrentVehicle = true;
				}
				bool flag4 = ini.DoesKeyExist("Other", "ProtectlastVehicle");
				if (flag4)
				{
					EntryPoint.ProtectLastVehicle = ini.ReadBoolean("Other", "ProtectlastVehicle", true);
				}
				else
				{
					ini.Write("Other", "ProtectlastVehicle", "true");
					EntryPoint.ProtectLastVehicle = true;
				}
				bool flag5 = ini.DoesKeyExist("Other", "ProtectEmergencyVehicles");
				if (flag5)
				{
					EntryPoint.ProtectEmergencyVehicles = ini.ReadBoolean("Other", "ProtectEmergencyVehicles", true);
				}
				else
				{
					ini.Write("Other", "ProtectEmergencyVehicles", "true");
					EntryPoint.ProtectEmergencyVehicles = true;
				}
				bool flag6 = ini.DoesKeyExist("Other", "ShowDebug");
				if (flag6)
				{
					EntryPoint.ShowDebug = ini.ReadBoolean("Other", "ShowDebug", false);
				}
				else
				{
					ini.Write("Other", "ShowDebug", "false");
					EntryPoint.ShowDebug = false;
				}
				Game.LogTrivial("Settings initialisation complete.");
			}
			catch (Exception e)
			{
				EntryPoint.ErrorLogger(e, "Initialisation", "Unable to read INI file.");
			}
		}

		
		public static void Command_Debug(string text)
		{
			bool showDebug = EntryPoint.ShowDebug;
			if (showDebug)
			{
				Game.DisplayNotification("~r~~h~Debug:~h~~s~ " + text);
				Game.LogTrivial("Debug: " + text);
			}
		}

		
		public static void ErrorLogger(Exception Err, string ErrMod, string ErrDesc)
		{
			Game.LogTrivial("--------------------------------------");
			Game.LogTrivial("Error during " + ErrMod);
			Game.LogTrivial("Decription: " + ErrDesc);
			Game.LogTrivial(Err.ToString());
			Game.DisplayNotification("~r~~h~Delete Vehicle:~h~~s~ Error during " + ErrMod + ". Please send logs.");
		}

		
		public static string INIpath = "Plugins\\RPH_Delete_Vehicle.ini";
	}
}
