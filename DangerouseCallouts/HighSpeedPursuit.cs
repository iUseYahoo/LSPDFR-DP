using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("HighSpeed Pursuit", 2)]
	internal class HighSpeedPursuit : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(5500f));
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "HighSpeed Pursuit in Progress";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.SuspectVehicle = new Vehicle(this.Spawnpoint);
			this.SuspectVehicle.IsPersistent = true;
			this.Suspect = new Ped(this.SuspectVehicle.GetOffsetPositionFront(5f));
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			this.PursuitCreated = false;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = !this.PursuitCreated && Game.LocalPlayer.Character.DistanceTo(this.SuspectVehicle) <= 25f;
			if (flag)
			{
				this.Pursuit = Functions.CreatePursuit();
				Functions.AddPedToPursuit(this.Pursuit, this.Suspect);
				Functions.SetPursuitIsActiveForPlayer(this.Pursuit, true);
				this.PursuitCreated = true;
			}
			bool flag2 = this.PursuitCreated && !Functions.IsPursuitStillRunning(this.Pursuit);
			if (flag2)
			{
				this.End();
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
			bool flag3 = EntityExtensions.Exists(this.SuspectVehicle);
			if (flag3)
			{
				this.SuspectVehicle.Dismiss();
			}
			Game.LogTrivial("Dangerouse Callouts: HighSpeed Pursuit has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Vehicle SuspectVehicle;

		
		private Blip SuspectBlip;

		
		private LHandle Pursuit;

		
		private Vector3 Spawnpoint;

		
		private bool PursuitCreated;
	}
}
