using System;
using System.Collections.Generic;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Heavily Armed man inside bank with Jugernaut suit", 1)]
	internal class Juggernaut : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Location2 = new Vector3(-1212.617f, -336.593f, 37.791f);
			this.Location1 = new Vector3(251.28f, 216.427f, 106.287f);
			this.heading = 66.64632f;
			Random random = new Random();
			List<string> list = new List<string>
			{
				"Location1",
				"Location2"
			};
			int index = random.Next(0, 2);
			bool flag = list[index] == "Location1";
			if (flag)
			{
				this.Spawnpoint = this.Location1;
			}
			bool flag2 = list[index] == "Location2";
			if (flag2)
			{
				this.Spawnpoint = this.Location2;
			}
			this.Suspect = new Ped("u_m_y_juggernaut_01", this.Spawnpoint, this.heading);
			base.CalloutMessage = "Heavily Armed man inside bank with Jugernaut suit";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("UNITS_RESPOND_CODE_99_01", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.Suspect.Health = 600000;
			this.Suspect.CanRagdoll = false;
			this.Suspect.IsRagdoll = false;
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_Combatmg_mk2", 100, true);
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			this.SuspectBlip.IsFriendly = false;
			Functions.RequestBackup(this.Spawnpoint, 1, 3);
			GameFiber.Sleep(3000);
			Functions.RequestBackup(this.Spawnpoint, 1, 3);
			Functions.RequestBackup(this.Spawnpoint, 1, 3);
			GameFiber.Sleep(3000);
			Functions.RequestBackup(this.Spawnpoint, 1, 3);
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 30f;
			if (flag)
			{
				this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			}
			bool flag2 = this.Suspect.IsDead || Game.LocalPlayer.Character.IsDead || this.Suspect.IsCuffed;
			if (flag2)
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
			Game.LogTrivial("Dangerouse Callouts: Juggernaut has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Blip SuspectBlip;

		
		private Vector3 Spawnpoint;

		
		private Vector3 Location1;

		
		private Vector3 Location2;

		
		private float heading;
	}
}
