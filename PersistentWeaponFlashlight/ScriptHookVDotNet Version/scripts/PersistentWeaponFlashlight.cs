using System;
using System.Runtime.InteropServices;
using GTA;
using PersistentWeaponFlashlight.Memory;

namespace PersistentWeaponFlashlight
{
	
	internal class PersistantWeaponFlashlight : Script
	{
		
		public PersistantWeaponFlashlight()
		{
			base.Tick += PersistantWeaponFlashlight.OnTick;
			base.Aborted += PersistantWeaponFlashlight.OnAborted;
			PersistantWeaponFlashlight.PluginInit();
		}

		
		private static void OnTick(object sender, EventArgs e)
		{
			PersistantWeaponFlashlight.PluginUpdate();
		}

		
		private static void OnAborted(object sender, EventArgs e)
		{
			PersistantWeaponFlashlight.PluginShutdown();
		}

		
		private static void PluginInit()
		{
			PersistantWeaponFlashlight.nopper1.Nop();
			PersistantWeaponFlashlight.nopper2.Nop();
			PersistantWeaponFlashlight.nopper3.Nop();
			PersistantWeaponFlashlight.nopper4.Nop();
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
			if (Util.IsWeaponSpecialTwoJustPressed() || PersistantWeaponFlashlight.lastOn != MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashlight))
			{
				PersistantWeaponFlashlight.toggleWeaponFlashlight(weaponComponentFlashlight, playerPedAddress, 0L);
				PersistantWeaponFlashlight.lastOn = MemoryFunctions.GetComponentFlashlightIsOn(weaponComponentFlashlight);
			}
		}

		
		private static void PluginShutdown()
		{
			if (PersistantWeaponFlashlight.nopper1.IsNopped)
			{
				PersistantWeaponFlashlight.nopper1.Restore();
			}
			if (PersistantWeaponFlashlight.nopper2.IsNopped)
			{
				PersistantWeaponFlashlight.nopper2.Restore();
			}
			if (PersistantWeaponFlashlight.nopper3.IsNopped)
			{
				PersistantWeaponFlashlight.nopper3.Restore();
			}
			if (PersistantWeaponFlashlight.nopper4.IsNopped)
			{
				PersistantWeaponFlashlight.nopper4.Restore();
			}
		}

		
		private static readonly Nopper nopper1 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 536, 4);

		
		private static readonly Nopper nopper2 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 390, 5);

		
		private static readonly Nopper nopper3 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 323, 5);

		
		private static readonly Nopper nopper4 = new Nopper(MemoryFunctions.UpdateWeaponComponentFlashlightFunctionAddr + 510, 5);

		
		private static readonly PersistantWeaponFlashlight.ToggleWeaponFlashlightDelegate toggleWeaponFlashlight = Marshal.GetDelegateForFunctionPointer<PersistantWeaponFlashlight.ToggleWeaponFlashlightDelegate>(MemoryFunctions.ToggleWeaponFlashlightFunctionAddr);

		
		private static bool lastOn = false;

		
		
		private delegate void ToggleWeaponFlashlightDelegate(long compFlashlight, IntPtr ped, long unk_0);
	}
}
