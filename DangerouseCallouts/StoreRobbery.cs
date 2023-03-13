using System;
using System.Collections.Generic;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Store Robbery", 3)]
	internal class StoreRobbery : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Location1 = new Vector3(29.557f, -1340.144f, 29.497f);
			this.Location2 = new Vector3(-708.502f, -904.459f, 19.216f);
			Random random = new Random();
			List<string> list = new List<string>
			{
				"Store1",
				"Store2"
			};
			int index = random.Next(0, 2);
			bool flag = list[index] == "Store1";
			if (flag)
			{
				this.Spawnpoint = this.Location1;
			}
			bool flag2 = list[index] == "Store2";
			if (flag2)
			{
				this.Spawnpoint = this.Location2;
			}
			this.Suspect = new Ped(this.Spawnpoint);
			base.CalloutMessage = "Store Robbery Code 3";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("CRIME_ROBBERY_03 WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", 50, true);
			GameFiber.Sleep(10000);
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			bool isMale = this.Suspect.IsMale;
			if (isMale)
			{
				this.maleFemale = "Sir";
			}
			else
			{
				this.maleFemale = "Mam";
			}
			this.counter = 0;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = this.Suspect.IsCuffed || this.Suspect.IsDead || Game.LocalPlayer.Character.IsDead || !EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				this.End();
				Game.DisplayNotification("Officer: Show me Code 4");
			}
		}

		
		public override void End()
		{
			base.End();
			bool flag = EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				this.Suspect.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag2)
			{
				this.SuspectBlip.Delete();
			}
			Game.LogTrivial("Intoxicated Person: Intoxicated Person has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Blip SuspectBlip;

		
		private Vector3 Spawnpoint;

		
		private int counter;

		
		private string maleFemale;

		
		private Vector3 Location1;

		
		private Vector3 Location2;
	}
}
