using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Fist Fight on Street", 2)]
	internal class FistFight : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(4000f));
			this.Suspect = new Ped(this.Spawnpoint);
			this.Suspect2 = new Ped(this.Spawnpoint);
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Random Fist Fight on the street";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("CITIZENS_REPORT_03 WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.Suspect.Tasks.FightAgainst(this.Suspect2);
			this.Suspect2.Tasks.FightAgainst(this.Suspect);
			this.Suspect.IsPersistent = true;
			this.Suspect2.IsPersistent = true;
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			bool flag = Game.LocalPlayer.Character.IsDead || this.Suspect.IsDead || this.Suspect.IsCuffed || this.Suspect2.IsDead || this.Suspect2.IsCuffed;
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
				GameFiber.Sleep(3000);
				this.Suspect.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.SuspectBlip);
			if (flag2)
			{
				this.SuspectBlip.Delete();
			}
			bool flag3 = EntityExtensions.Exists(this.Suspect2);
			if (flag3)
			{
				GameFiber.Sleep(3000);
				this.Suspect2.Dismiss();
			}
			Game.LogTrivial("UK Callouts: Stabbing has been cleaned up!");
		}

		
		private Vector3 Spawnpoint;

		
		private Blip SuspectBlip;

		
		private Ped Suspect;

		
		private Ped Suspect2;
	}
}
