using System;
using System.IO;
using GTA;

namespace PersistentWeaponFlashlight
{
	
	internal static class Util
	{
		
		public static void Log(string text)
		{
			using (StreamWriter streamWriter = new StreamWriter("persistent-weapon-flashlight.log", true))
			{
				streamWriter.WriteLine(text);
			}
		}

		
		public static bool IsWeaponSpecialTwoJustPressed()
		{
			return Game.IsControlJustPressed(54);
		}

		
		public static IntPtr GetPlayerPedAddress()
		{
			Ped character = Game.Player.Character;
			if (!character.Exists())
			{
				return IntPtr.Zero;
			}
			return character.MemoryAddress;
		}

		
		public static void UnloadActivePlugin()
		{
			throw new Exception("There was an error");
		}
	}
}
