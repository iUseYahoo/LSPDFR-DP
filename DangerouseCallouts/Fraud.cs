using System;
using System.Drawing;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Fraud", 1)]
	internal class Fraud : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(4000f));
			this.Suspect = new Ped(this.Spawnpoint);
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Person reporting Fraud";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT_03 WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			this.Suspect.IsPersistent = true;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 10f;
			if (flag)
			{
				Game.DisplayHelp("Press Y to talk to the Suspect!", false);
				bool flag2 = Game.IsKeyDown(Keys.Y);
				if (flag2)
				{
					this.counter++;
					bool flag3 = this.counter == 2;
					if (flag3)
					{
						Game.DisplaySubtitle("Officer: Hello" + this.maleFemale + "Get on the ground!");
					}
					bool flag4 = this.counter == 3;
					if (flag4)
					{
						Game.DisplaySubtitle("Suspect: No officer stay back");
					}
					bool flag5 = this.counter == 4;
					if (flag5)
					{
						Game.DisplaySubtitle("Officer:" + this.maleFemale + "DO NOT REACH FOR ANYTHING!");
					}
					bool flag6 = this.counter == 5;
					if (flag6)
					{
						Game.DisplaySubtitle("Suspect: NO");
					}
					bool flag7 = this.counter == 6;
					if (flag7)
					{
						Game.DisplaySubtitle("Officer:" + this.maleFemale + "stop Walking away!");
						this.Suspect.Tasks.Wander();
						this.Suspect.Tasks.ReactAndFlee(this.Suspect);
						this.Suspect.Inventory.GiveNewWeapon("KNIFE", 50, true);
						GameFiber.Sleep(1000);
						this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
						Game.DisplaySubtitle("No more Text to display");
					}
				}
			}
			bool flag8 = this.Suspect.IsCuffed || this.Suspect.IsDead || Game.LocalPlayer.Character.IsDead || !EntityExtensions.Exists(this.Suspect);
			if (flag8)
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

		
		private Vector3 Spawnpoint;

		
		private Blip SuspectBlip;

		
		private Ped Suspect;

		
		private int counter;

		
		private string maleFemale;
	}
}
