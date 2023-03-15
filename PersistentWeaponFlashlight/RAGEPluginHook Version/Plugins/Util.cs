using System;
using Rage;

namespace PersistentWeaponFlashlight
{
	
	internal static class Util
	{
		
		public static void Log(string text)
		{
			Game.LogTrivial(text);
		}

		
		public static bool IsWeaponSpecialTwoJustPressed()
		{
			return Game.IsControlJustPressed(0, 54);
		}

		
		public static IntPtr GetPlayerPedAddress()
		{
			Ped character = Game.LocalPlayer.Character;
			if (!character)
			{
				return IntPtr.Zero;
			}
			return character.MemoryAddress;
		}

		
		public static void UnloadActivePlugin()
		{
			Game.UnloadActivePlugin();
		}
	}
}
