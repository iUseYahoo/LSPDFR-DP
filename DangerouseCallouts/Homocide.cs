using System;
using System.Collections.Generic;
using System.Drawing;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Homicide Suspect Seen", 4)]
	internal class Homicide : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Location1 = new Vector3(-14.38f, -1438.514f, 31.106f);
			this.Location2 = new Vector3(-63.464f, -1526.421f, 34.245f);
			this.Location3 = new Vector3(111.42f, -1814.584f, 25.928f);
			this.Location4 = new Vector3(514.472f, -1989.546f, 24.098f);
			this.Location5 = new Vector3(692.76f, -1747.425f, 9.73f);
			Random random = new Random();
			List<string> list = new List<string>
			{
				"Homicide1",
				"Homicide2",
				"Homicide3",
				"Homicide4",
				"Homicide5"
			};
			int index = random.Next(0, 5);
			bool flag = list[index] == "Homicide1";
			if (flag)
			{
				this.Spawnpoint = this.Location1;
			}
			bool flag2 = list[index] == "Homicide2";
			if (flag2)
			{
				this.Spawnpoint = this.Location2;
			}
			bool flag3 = list[index] == "Homicide3";
			if (flag3)
			{
				this.Spawnpoint = this.Location3;
			}
			bool flag4 = list[index] == "Homicide4";
			if (flag4)
			{
				this.Spawnpoint = this.Location4;
			}
			bool flag5 = list[index] == "Homicide5";
			if (flag5)
			{
				this.Spawnpoint = this.Location5;
			}
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(4000f));
			this.Suspect = new Ped(this.Spawnpoint);
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Possible Homicide Reported";
			base.CalloutPosition = this.Spawnpoint;
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", 50, true);
			this.Suspect.IsPersistent = false;
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			bool flag = Game.LocalPlayer.Character.IsDead || this.Suspect.IsDead || this.Suspect.IsCuffed;
			if (flag)
			{
				this.End();
			}
			base.Process();
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
			Game.LogTrivial("UK Callouts: Stabbing has been cleaned up!");
		}

		
		private Vector3 Spawnpoint;

		
		private Vector3 Location1;

		
		private Vector3 Location2;

		
		private Vector3 Location3;

		
		private Vector3 Location4;

		
		private Vector3 Location5;

		
		private Blip SuspectBlip;

		
		private Ped Suspect;
	}
}
