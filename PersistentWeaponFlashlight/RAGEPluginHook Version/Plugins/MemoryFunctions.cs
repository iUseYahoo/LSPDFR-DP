using System;
using Rage;

namespace PersistentWeaponFlashlight.Memory
{
	
	internal static class MemoryFunctions
	{
		
		unsafe static MemoryFunctions()
		{
			IntPtr intPtr = MemoryFunctions.FindPattern("48 89 BB ?? ?? ?? ?? EB 16 48 89 BB");
			IntPtr intPtr2 = MemoryFunctions.FindPattern("48 89 BB ?? ?? ?? ?? 48 85 FF 74 18 B8");
			bool flag = false;
			if (MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr == IntPtr.Zero)
			{
				Util.Log("ERROR: could not find UpdateWeaponComponentFlashlightFunctionAddr");
				flag = true;
			}
			if (MemoryFunctions.ToggleWeaponFlashlightFunctionAddr == IntPtr.Zero)
			{
				Util.Log("ERROR: could not find ToggleWeaponFlashlightFunctionAddr");
				flag = true;
			}
			if (intPtr == IntPtr.Zero)
			{
				Util.Log("ERROR: could not find weaponComponentFlashlightOffsetAddr");
				flag = true;
			}
			if (intPtr2 == IntPtr.Zero)
			{
				Util.Log("ERROR: could not find objectWeaponOffsetAddr");
				flag = true;
			}
			if (flag)
			{
				Util.Log("Unloading...");
				Util.UnloadActivePlugin();
				return;
			}
			MemoryFunctions.CWeaponManager_CurrentWeaponObjectOffset = 120;
			MemoryFunctions.CObject_WeaponOffset = *(int*)((void*)(intPtr2 + 3));
			MemoryFunctions.CWeaponComponentFlashLight_StateOffset = 73;
			MemoryFunctions.CPed_WeaponManagerOffset = *(int*)((void*)(MemoryFunctions.ToggleWeaponFlashlightFunctionAddr + 104));
			MemoryFunctions.CWeapon_WeaponComponentFlashlightOffset = *(int*)((void*)(intPtr + 3));
		}

		
		public unsafe static long GetPedWeaponManager(IntPtr pedPtr)
		{
			return *(long*)((void*)(pedPtr + MemoryFunctions.CPed_WeaponManagerOffset));
		}

		
		public unsafe static long GetWeaponManagerCurrentWeaponObject(long weaponMgr)
		{
			return *(UIntPtr)(weaponMgr + (long)MemoryFunctions.CWeaponManager_CurrentWeaponObjectOffset);
		}

		
		public unsafe static long GetObjectWeapon(long obj)
		{
			return *(UIntPtr)(obj + (long)MemoryFunctions.CObject_WeaponOffset);
		}

		
		public unsafe static long GetWeaponComponentFlashlight(long weapon)
		{
			return *(UIntPtr)(weapon + (long)MemoryFunctions.CWeapon_WeaponComponentFlashlightOffset);
		}

		
		public unsafe static bool GetComponentFlashlightIsOn(long component)
		{
			return (*(UIntPtr)(component + (long)MemoryFunctions.CWeaponComponentFlashLight_StateOffset) & 1) > 0;
		}

		
		private static IntPtr FindPattern(string pattern)
		{
			return Game.FindPattern(pattern);
		}

		
		private const string UpdateWeaponComponentFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC 40 48 8B FA 48 8B D9 48 85 D2 0F 84 ?? ?? ?? ?? 80 7A 28 04 0F 85";

		
		private const string ToggleWeaponFlashlightFunctionPattern = "48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC 20 8A 41 49 48 8B FA 48 8B 51 10 44 8A C0 24 FE 4C 8B F9 41 F6 D0 41 80 E0 01 44 0A C0 41 80 C8 02 44 88 41 49 48 8D 0D";

		
		public static readonly IntPtr UpdateWeaponComponentFlashlightFunctionAddr = MemoryFunctions.FindPattern("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 7C 24 ?? 41 54 41 56 41 57 48 83 EC 40 48 8B FA 48 8B D9 48 85 D2 0F 84 ?? ?? ?? ?? 80 7A 28 04 0F 85");

		
		public static readonly IntPtr ToggleWeaponFlashlightFunctionAddr = MemoryFunctions.FindPattern("48 89 5C 24 ?? 48 89 6C 24 ?? 48 89 74 24 ?? 57 41 56 41 57 48 83 EC 20 8A 41 49 48 8B FA 48 8B 51 10 44 8A C0 24 FE 4C 8B F9 41 F6 D0 41 80 E0 01 44 0A C0 41 80 C8 02 44 88 41 49 48 8D 0D");

		
		private static readonly int CPed_WeaponManagerOffset;

		
		private static readonly int CWeaponManager_CurrentWeaponObjectOffset;

		
		private static readonly int CObject_WeaponOffset;

		
		private static readonly int CWeapon_WeaponComponentFlashlightOffset;

		
		private static readonly int CWeaponComponentFlashLight_StateOffset;
	}
}
