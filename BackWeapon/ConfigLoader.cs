using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Rage;

namespace BackWeapon
{
	
	internal class ConfigLoader
	{
		
		public static Dictionary<string, object> GetIniValues()
		{
			Game.LogTrivial("Loading Configuration...");
			Dictionary<string, object> iniValues = new Dictionary<string, object>();
			InitializationFile ini = ConfigLoader.InitializeFile();
			string[] array = ini.ReadString("Main", "AcceptedWeapons", "WEAPON_CARBINERIFLE_MK2,WEAPON_SMG").Split(new char[]
			{
				','
			});
			List<uint> acceptedWeaponHashes = new List<uint>();
			foreach (string acceptedWeapon in array)
			{
				try
				{
					acceptedWeaponHashes.Add(Game.GetHashKey(acceptedWeapon.Trim(new char[]
					{
						' '
					})));
				}
				catch
				{
					Game.LogTrivial(acceptedWeapon + " is not a valid weapon");
				}
			}
			iniValues.Add("AcceptedWeapons", acceptedWeaponHashes);
			Vector3 offsetPosition = ini.ReadVector3("Main", "OffsetPosition", new Vector3(0f, -0.19f, -0.02f));
			iniValues.Add("OffsetPosition", offsetPosition);
			Rotator Rotation = ini.ReadRotator("Main", "Rotation", new Rotator(0f, 165f, 0f));
			iniValues.Add("Rotation", Rotation);
			bool hideWhileInVehicle = ini.ReadBoolean("Main", "HideWhileInVehicle", true);
			iniValues.Add("HideWhileInVehicle", hideWhileInVehicle);
			bool disableFlashlight = ini.ReadBoolean("Main", "DisableFlashlight", false);
			iniValues.Add("DisableFlashlight", disableFlashlight);
			string deleteWeaponKeyString = ini.ReadString("Main", "DeleteWeaponKey", "Decimal");
			Keys deleteWeaponKey = (Keys)Enum.Parse(typeof(Keys), deleteWeaponKeyString);
			iniValues.Add("DeleteWeaponKey", deleteWeaponKey);
			bool enableAI = ini.ReadBoolean("AI", "EnableAI", false);
			iniValues.Add("EnableAI", enableAI);
			bool copsOnly = ini.ReadBoolean("AI", "CopsOnly", true);
			iniValues.Add("CopsOnly", copsOnly);
			string[] array3 = ini.ReadString("AI", "AcceptedWeapons", "WEAPON_CARBINERIFLE_MK2,WEAPON_SMG").Split(new char[]
			{
				','
			});
			List<uint> aiAcceptedWeaponHashes = new List<uint>();
			foreach (string acceptedWeapon2 in array3)
			{
				try
				{
					aiAcceptedWeaponHashes.Add(Game.GetHashKey(acceptedWeapon2.Trim(new char[]
					{
						' '
					})));
				}
				catch
				{
					Game.LogTrivial(acceptedWeapon2 + " is not a valid weapon");
				}
			}
			iniValues.Add("AIAcceptedWeapons", aiAcceptedWeaponHashes);
			Vector3 aiOffsetPosition = ini.ReadVector3("AI", "OffsetPosition", new Vector3(0f, -0.19f, -0.02f));
			iniValues.Add("AIOffsetPosition", aiOffsetPosition);
			Rotator aiRotation = ini.ReadRotator("AI", "Rotation", new Rotator(0f, 165f, 0f));
			iniValues.Add("AIRotation", aiRotation);
			bool aiHideWhileInVehicle = ini.ReadBoolean("AI", "HideWhileInVehicle", true);
			iniValues.Add("AIHideWhileInVehicle", aiHideWhileInVehicle);
			bool enableBestWeapon = ini.ReadBoolean("AI", "EnableBestWeapon", false);
			iniValues.Add("EnableBestWeapon", enableBestWeapon);
			string config = null;
			foreach (string key in iniValues.Keys.ToArray<string>())
			{
				config += string.Format("{0}: {1}; ", key, iniValues[key]);
			}
			Game.LogTrivial("Loaded configuration: " + config);
			return iniValues;
		}

		
		private static InitializationFile InitializeFile()
		{
			InitializationFile initializationFile = new InitializationFile("Plugins/BackWeapon.ini");
			initializationFile.Create();
			return initializationFile;
		}
	}
}
