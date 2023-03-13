using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LSPD_First_Response.Mod.API;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal class DrugUser : EventBasePed
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
				where !x.IsInAnyVehicle(false) && x.IsAlive && x.IsHuman && !x.IsPersistent && !Functions.IsPedACop(x) && x.DistanceTo(Game.LocalPlayer.Character) > 30f && Vector3.Distance(x.Position, Game.LocalPlayer.Character.Position) < 120f
				select x).ToArray<Ped>();
				possiblePedsNearInWorld.Shuffle<Ped>();
				possiblePedsNearInWorld.Shuffle<Ped>();
				this.Ped = possiblePedsNearInWorld.FirstOrDefault<Ped>();
				bool flag = this.Ped == null || !EntityExtensions.Exists(this.Ped);
				if (flag)
				{
					Game.LogTrivial("[ManiacEvents]Aborting pedestrian event, Couldn't find any near peds.");
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
					Game.LogTrivial("[ManiacEvents]Drug Smoking Event Started");
					this.Ped.Tasks.Clear();
					this.Ped.SetMovementAnimationSet(DrugUser._drunkAnimationSets.GetRandomElement(false));
					Extensions.PlayTaskScen(this.Ped, "WORLD_HUMAN_SMOKING_POT", 0, false);
					StopThePedFunctions.SetPedUnderDrugsInfluence(this.Ped, true);
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
			base.CleanUp();
		}

		
		public Ped Ped;

		
		private static string[] _drunkAnimationSets = new string[]
		{
			"move_m@drunk@moderatedrunk"
		};
	}
}
