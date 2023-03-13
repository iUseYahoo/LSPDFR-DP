using System;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using Rage;
using StopThePed.API;

namespace DangerouseCallouts.Callouts
{
	
	[CalloutInfo("Drunk Driver", 2)]
	internal class DrunkDriver : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			this.Spawnpoint = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(4200f));
			base.ShowCalloutAreaBlipBeforeAccepting(this.Spawnpoint, 30f);
			base.AddMinimumDistanceCheck(30f, this.Spawnpoint);
			base.CalloutMessage = "Drunk Driver Reported By Citizens Respond ~r~CODE-2";
			base.CalloutPosition = this.Spawnpoint;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Spawnpoint);
			return base.OnBeforeCalloutDisplayed();
		}

		
		public override bool OnCalloutAccepted()
		{
			this.SuspectVehicle = new Vehicle(this.TruckList[new Random().Next(this.TruckList.Length)], this.Spawnpoint);
			this.SuspectVehicle.IsPersistent = true;
			this.Suspect = new Ped(this.SuspectVehicle.GetOffsetPositionFront(5f));
			this.Suspect.IsPersistent = true;
			this.Suspect.BlockPermanentEvents = true;
			this.Suspect.WarpIntoVehicle(this.SuspectVehicle, -1);
			this.Suspect.Tasks.CruiseWithVehicle(8f);
			Functions.injectPedSearchItems(this.Suspect);
			Functions.setPedAlcoholOverLimit(this.Suspect, true);
			this.SuspectBlip = this.Suspect.AttachBlip();
			this.SuspectBlip.Color = Color.Blue;
			this.SuspectBlip.IsRouteEnabled = true;
			AnimationSet value;
			value..ctor("move_m@drunk@verydrunk");
			value.LoadAndWait();
			this.Suspect.MovementAnimationSet = new AnimationSet?(value);
			DrunkDriver.done = false;
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
			bool flag2 = !DrunkDriver.done && Game.LocalPlayer.Character.DistanceTo(this.SuspectVehicle) <= 25f;
			if (flag2)
			{
				switch (new Random().Next(0, 3))
				{
				case 0:
					this.Pursuit = Functions.CreatePursuit();
					Functions.AddPedToPursuit(this.Pursuit, this.Suspect);
					Functions.SetPursuitIsActiveForPlayer(this.Pursuit, true);
					this.PursuitCreated = true;
					DrunkDriver.done = true;
					break;
				case 1:
					DrunkDriver.done = true;
					break;
				case 2:
					this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
					DrunkDriver.done = true;
					break;
				}
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
			Game.LogTrivial("Dangerouse Callouts: Drunk Driver has been cleaned up!");
		}

		
		private Ped Suspect;

		
		private Vehicle SuspectVehicle;

		
		private Blip SuspectBlip;

		
		private Vector3 Spawnpoint;

		
		private bool PursuitCreated;

		
		private LHandle Pursuit;

		
		private string[] TruckList = new string[]
		{
			"DUKES",
			"BALLER",
			"BALLER2",
			"BISON",
			"BISON2",
			"BJXL",
			"CAVALCADE",
			"CHEETAH",
			"COGCABRIO",
			"ASEA",
			"ADDER",
			"FELON",
			"FELON2",
			"ZENTORNO",
			"WARRENER",
			"RAPIDGT",
			"INTRUDER",
			"FELTZER2",
			"FQ2",
			"RANCHERXL",
			"REBEL",
			"SCHWARZER",
			"COQUETTE",
			"CARBONIZZARE",
			"EMPEROR",
			"SULTAN",
			"EXEMPLAR",
			"MASSACRO",
			"DOMINATOR",
			"ASTEROPE",
			"PRAIRIE",
			"NINEF",
			"WASHINGTON",
			"CHINO",
			"CASCO",
			"INFERNUS",
			"ZTYPE",
			"DILETTANTE",
			"VIRGO",
			"F620",
			"PRIMO",
			"SULTAN",
			"EXEMPLAR",
			"F620",
			"FELON2",
			"FELON",
			"SENTINEL",
			"WINDSOR",
			"DOMINATOR",
			"DUKES",
			"GAUNTLET",
			"VIRGO",
			"ADDER",
			"BUFFALO",
			"ZENTORNO",
			"MASSACRO"
		};

		
		public static bool done;

		
		public enum Actions
		{
			
			Flee,
			
			Nothing,
			
			Fight
		}
	}
}
