using System;
using System.Threading;
using Rage;

namespace BackWeapon
{
	
	public class EntryPoint
	{
		
		private static void Main()
		{
			while (Game.IsLoading)
			{
				GameFiber.Yield();
			}
			Game.LogTrivial("Enabling Player Loop...");
			new GameFiber(new ThreadStart(BackWeapon.PlayerLoop)).Start();
			if (EntryPoint.enableAI)
			{
				Game.LogTrivial("Enabling AI Loop...");
				new GameFiber(new ThreadStart(BackWeapon.AIPedsLoop)).Start();
			}
			Game.LogTrivial("Stow That Weapon (BackWeapon.dll) by willpv23 has been loaded!");
			GameFiber.Hibernate();
		}

		
		private static bool enableAI = (bool)ConfigLoader.GetIniValues()["EnableAI"];
	}
}
