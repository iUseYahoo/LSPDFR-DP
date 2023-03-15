using System;
using System.Runtime.InteropServices;
using PersistentWeaponFlashlight.Memory;
using Rage;

namespace PersistentWeaponFlashlight
{
	
	internal static class EntryPoint
	{
		
		private static void Main()
		{
			while (Game.IsLoading)
			{
				GameFiber.Sleep(1000);
			}
			EntryPoint.PluginInit();
			for (;;)
			{
				GameFiber.Yield();
				EntryPoint.PluginUpdate();
			}
		}

		
		private static void OnUnload(bool isTerminating)
		{
			EntryPoint.PluginShutdown();
		}

		
		private static void PluginInit()
		{
			EntryPoint.nopper1.Nop();
			EntryPoint.nopper2.Nop();
			EntryPoint.nopper3.Nop();
			EntryPoint.nopper4.Nop();
		}

		
		private static void PluginUpdate()
		{
			IntPtr playerPedAddress = Util.GetPlayerPedAddress();
			if (playerPedAddress == IntPtr.Zero)
			{
				return;
			}
			long pedWeaponManager = MemoryFunctions.GetPedWeaponManager(playerPedAddress);
			if (pedWeaponManager == 0L)
			{
				return;
			}
			long weaponManagerCurrentWeaponObject = MemoryFunctions.GetWeaponManagerCurrentWeaponObject(pedWeaponManager);
			if (weaponManagerCurrentWeaponObject == 0L)
			{
				return;
			}
			long objectWeapon = MemoryFunctions.GetObjectWeapon(weaponManagerCurrentWeaponObject);
			if (objectWeapon == 0L)
			{
				return;
			}
			long weaponComponentFlashlight = MemoryFunctions.GetWeaponComponentFlashlight(objectWeapon);
			if (weaponComponentFlashlight == 0L)
			{
				return;
			}
			if (Util.IsWeaponSpecialTwoJustPressed() || EntryPoint.lastOn != MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashlight))
			{
				EntryPoint.toggleWeaponFlashlight(weaponComponentFlashlight, playerPedAddress, 0L);
				EntryPoint.lastOn = MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashlight);
			}
		}

		
		private static void PluginShutdown()
		{
			if (EntryPoint.nopper1.IsNopped)
			{
				EntryPoint.nopper1.Restore();
			}
			if (EntryPoint.nopper2.IsNopped)
			{
				EntryPoint.nopper2.Restore();
			}
			if (EntryPoint.nopper3.IsNopped)
			{
				EntryPoint.nopper3.Restore();
			}
			if (EntryPoint.nopper4.IsNopped)
			{
				EntryPoint.nopper4.Restore();
			}
		}

		
		private static readonly Nopper nopper1 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 536, 4);

		
		private static readonly Nopper nopper2 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 390, 5);

		
		private static readonly Nopper nopper3 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 323, 5);

		
		private static readonly Nopper nopper4 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 510, 5);

		
		private static readonly EntryPoint.ToggleWeaponFlashlightDelegate toggleWeaponFlashlight = Marshal.GetDelegateForFunctionPointer<EntryPoint.ToggleWeaponFlashlightDelegate>(MemoryFunctions.ToggleWeaponFlashlightFunctionAddr);

		
		private static bool lastOn = false;

		
		
		private delegate void ToggleWeaponFlashlightDelegate(long compFlashlight, IntPtr ped, long unk_0);
	}
}
