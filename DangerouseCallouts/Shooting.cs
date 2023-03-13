using System;
using System.Collections.Generic;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Civilian Shooting", 1)]
	internal class Shooting : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(300f));
			this.Suspect = new Ped(this.Spawnpoint, 0f);
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Shots Fired by civilian";
			base.CalloutPosition = this.Spawnpoint;
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			Random random = new Random();
			List<string> list = new List<string>
			{
				"Attack",
				"NotAttack"
			};
			int index = random.Next(0, 2);
			bool flag = list[index] == "Attack";
			if (flag)
			{
				this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			}
			bool flag2 = list[index] == "NotAttack";
			if (flag2)
			{
				this.Suspect.Tasks.PutHandsUp(100, Game.LocalPlayer.Character);
			}
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.Inventory.GiveNewWeapon("WEAPON_CARBINERIFLE", 50, true);
			this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			return base.OnCalloutAccepted();
		}

		
		public override void Process()
		{
			base.Process();
			bool flag = Game.LocalPlayer.Character.IsDead || this.Suspect.IsCuffed || this.Suspect.IsDead;
			if (flag)
			{
				this.End();
			}
		}

		
		public override void End()
		{
			base.End();
			bool isDead = this.Suspect.IsDead;
			if (isDead)
			{
				this.Suspect.Dismiss();
			}
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
			Game.LogTrivial("Dangerouse Callouts: Civilian Shooting has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Blip SuspectBlip;

		
		private Vector3 Spawnpoint;
	}
}
