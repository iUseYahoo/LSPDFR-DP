using System;
using Rage;

namespace EmergencyCallouts.Essential
{
	
	internal static class Inventory
	{
		
		internal static void GiveRandomMeleeWeapon(this Ped ped, short ammoCount, bool equipNow)
		{
			string[] array = new string[]
			{
				"WEAPON_DAGGER",
				"WEAPON_BAT",
				"WEAPON_BOTTLE",
				"WEAPON_CROWBAR",
				"WEAPON_UNARMED",
				"WEAPON_FLASHLIGHT",
				"WEAPON_HAMMER",
				"WEAPON_HATCHET",
				"WEAPON_KNUCKLE",
				"WEAPON_KNIFE",
				"WEAPON_MACHETE",
				"WEAPON_SWITCHBLADE",
				"WEAPON_NIGHTSTICK",
				"WEAPON_WRENCH",
				"WEAPON_BATTLEAXE"
			};
			int num = Helper.random.Next(array.Length);
			if (EntityExtensions.Exists(ped))
			{
				ped.Inventory.GiveNewWeapon(array[num], ammoCount, equipNow);
			}
		}

		
		internal static void GiveRandomHandgun(this Ped ped, short ammoCount, bool equipNow)
		{
			string[] array = new string[]
			{
				"WEAPON_PISTOL",
				"WEAPON_COMBATPISTOL",
				"WEAPON_PISTOL50",
				"WEAPON_VINTAGEPISTOL",
				"WEAPON_SNSPISTOL",
				"WEAPON_HEAVYPISTOL",
				"WEAPON_CERAMICPISTOL"
			};
			int num = Helper.random.Next(array.Length);
			if (EntityExtensions.Exists(ped))
			{
				ped.Inventory.GiveNewWeapon(array[num], ammoCount, equipNow);
			}
		}

		
		internal static void GiveRandomSubmachineGun(this Ped ped, short ammoCount, bool equipNow)
		{
			string[] array = new string[]
			{
				"WEAPON_MICROSMG",
				"WEAPON_SMG",
				"WEAPON_ASSAULTSMG",
				"WEAPON_MINISMG"
			};
			int num = Helper.random.Next(array.Length);
			if (EntityExtensions.Exists(ped))
			{
				ped.Inventory.GiveNewWeapon(array[num], ammoCount, equipNow);
			}
		}

		
		internal static void GiveRandomAssaultRifle(this Ped ped, short ammoCount, bool equipNow)
		{
			string[] array = new string[]
			{
				"WEAPON_CARBINERIFLE",
				"WEAPON_ASSAULTRIFLE",
				"WEAPON_ADVANCEDRIFLE",
				"WEAPON_SPECIALCARBINE",
				"WEAPON_BULLPUPRIFLE"
			};
			int num = Helper.random.Next(array.Length);
			if (EntityExtensions.Exists(ped))
			{
				ped.Inventory.GiveNewWeapon(array[num], ammoCount, equipNow);
			}
		}

		
		internal static void GiveRandomShotgun(this Ped ped, short ammoCount, bool equipNow)
		{
			string[] array = new string[]
			{
				"WEAPON_PUMPSHOTGUN",
				"WEAPON_SAWNOFFSHOTGUN",
				"WEAPON_BULLPUPSHOTGUN",
				"WEAPON_HEAVYSHOTGUN",
				"WEAPON_DBSHOTGUN",
				"WEAPON_COMBATSHOTGUN"
			};
			int num = Helper.random.Next(array.Length);
			if (EntityExtensions.Exists(ped))
			{
				ped.Inventory.GiveNewWeapon(array[num], ammoCount, equipNow);
			}
		}

		
		internal static void GiveRandomMachineGun(this Ped ped, short ammoCount, bool equipNow)
		{
			string[] array = new string[]
			{
				"WEAPON_MG",
				"WEAPON_COMBATMG",
				"WEAPON_GUSENBERG"
			};
			int num = Helper.random.Next(array.Length);
			if (EntityExtensions.Exists(ped))
			{
				ped.Inventory.GiveNewWeapon(array[num], ammoCount, equipNow);
			}
		}

		
		internal static void GiveRandomSniperRifle(this Ped ped, short ammoCount, bool equipNow)
		{
			string[] array = new string[]
			{
				"WEAPON_SNIPERRIFLE",
				"WEAPON_HEAVYSNIPER",
				"WEAPON_MARKSMANRIFLE"
			};
			int num = Helper.random.Next(array.Length);
			if (EntityExtensions.Exists(ped))
			{
				ped.Inventory.GiveNewWeapon(array[num], ammoCount, equipNow);
			}
		}
	}
}
