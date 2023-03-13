using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal class DrugDriver : EventBaseTraffic
	{
		
		
		
		public override bool IsRunning
		{
			get
			{
				return base.IsRunning;
			}
			set
			{
				base.IsRunning = value;
			}
		}

		
		
		public override bool CanBeSpawned
		{
			get
			{
				return true;
			}
		}

		
		
		
		public override List<Entity> SpawnedEntities
		{
			get
			{
				return base.SpawnedEntities;
			}
			set
			{
				base.SpawnedEntities = value;
			}
		}

		
		
		
		public override List<Blip> Blips
		{
			get
			{
				return base.Blips;
			}
			set
			{
				base.Blips = value;
			}
		}

		
		
		
		public override Vector3 SpawnPosition
		{
			get
			{
				return base.SpawnPosition;
			}
			set
			{
				base.SpawnPosition = value;
			}
		}

		
		public override bool Create()
		{
			bool result;
			try
			{
				Ped[] possiblePedsNearInWorld = (from x in World.GetAllPeds()
				where x.IsInAnyVehicle(false) && x.IsAlive && x.IsHuman && !x.IsInAnyPoliceVehicle && x.CurrentVehicle.IsCar && !x.IsPassenger && !x.CurrentVehicle.Model.IsBus && !x.CurrentVehicle.Model.IsBigVehicle && !x.IsPersistent && Vector3.Distance(x.Position, Game.LocalPlayer.Character.Position) < 120f
				select x).ToArray<Ped>();
				possiblePedsNearInWorld.Shuffle<Ped>();
				possiblePedsNearInWorld.Shuffle<Ped>();
				this.Ped = possiblePedsNearInWorld.FirstOrDefault<Ped>();
				bool flag = this.Ped == null || !EntityExtensions.Exists(this.Ped);
				if (flag)
				{
					Game.LogTrivial("[ManiacEvents]Aborting traffic event, Couldn't find any near peds.");
					result = false;
				}
				else
				{
					this.Ped.MakePersistent();
					this.SpawnedEntities.Add(this.Ped);
					result = base.Create();
				}
			}
			catch (Exception e)
			{
				result = false;
			}
			return result;
		}

		
		public override void Action()
		{
			base.Action();
			try
			{
				bool flag = EntityExtensions.Exists(this.Ped);
				if (flag)
				{
					Game.LogTrivial("[ManiacEvents]Drug Driver Event Started");
					this.car = this.Ped.CurrentVehicle;
					this.car.IsPersistent = true;
					this.Ped.SetMovementAnimationSet(DrugDriver._drunkAnimationSets.GetRandomElement(false));
					StopThePedFunctions.SetPedUnderDrugsInfluence(this.Ped, true);
					StopThePedFunctions.SetPedAlcoholOverLimit(this.Ped, false);
					this.speed = this.car.Speed;
					bool flag2 = this.speed >= 6f;
					if (flag2)
					{
						this.newspeed = 7f;
					}
					else
					{
						this.newspeed = 5f;
					}
					this.Ped.Tasks.CruiseWithVehicle(this.car, this.newspeed, 262710);
				}
			}
			catch (ThreadAbortException)
			{
			}
		}

		
		public override void Process()
		{
			try
			{
				bool flag = !EntityExtensions.Exists(this.Ped) || this.Ped.IsDead || Game.LocalPlayer.Character.IsDead || this.Ped.Position.DistanceTo(Game.LocalPlayer.Character.Position) > 350f;
				if (flag)
				{
					this.CleanUp();
				}
				base.Process();
			}
			catch (ThreadAbortException)
			{
			}
			catch (Exception e)
			{
			}
		}

		
		public override void CleanUp()
		{
			bool flag = EntityExtensions.Exists(this.car);
			if (flag)
			{
				this.car.Dismiss();
			}
			base.CleanUp();
		}

		
		public Ped Ped;

		
		public Vehicle car;

		
		protected float speed;

		
		protected float newspeed;

		
		private static string[] _drunkAnimationSets = new string[]
		{
			"move_m@drunk@moderatedrunk"
		};
	}
}
