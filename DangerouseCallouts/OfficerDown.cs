using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("OfficerDown", 2)]
	internal class OfficerDown : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(3500f));
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Officer Down Respond Code 3";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 CRIME_ASSAULT_PEACE_OFFICER_03 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.Cop = new Ped("s_m_y_cop_01", this.Spawnpoint, this.heading);
			this.Cop.Kill();
			this.Suspect = new Ped(this.Spawnpoint, 3f);
			this.heading = 66.64632f;
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.Suspect.Tasks.FightAgainst(this.Cop);
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", 50, true);
			AnimationSet value;
			value..ctor("dead");
			value.LoadAndWait();
			this.Cop.MovementAnimationSet = new AnimationSet?(value);
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			bool flag = this.Suspect.IsDead || Game.LocalPlayer.Character.IsDead || this.Suspect.IsCuffed;
			if (flag)
			{
				this.End();
				Game.DisplayNotification("Officer: Show me Code 4");
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
			bool flag3 = EntityExtensions.Exists(this.SuspectVehicle);
			if (flag3)
			{
				this.SuspectVehicle.Dismiss();
			}
			Game.LogTrivial("Dangerouse Callouts: Officer Down has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Ped Cop;

		
		private Vehicle SuspectVehicle;

		
		private Blip SuspectBlip;

		
		private Vector3 Spawnpoint;

		
		private float heading;
	}
}
