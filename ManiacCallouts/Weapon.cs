using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LSPD_First_Response.Mod.API;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal class Weapon : EventBasePed
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
					Game.LogTrivial("[ManiacEvents]Weapon Event Started");
					this.Ped.Tasks.Clear();
					this.Ped.SetMovementAnimationSet(Weapon._drunkAnimationSets.GetRandomElement(false));
					new RelationshipGroup("BAD");
					Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
					Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
					this.Ped.RelationshipGroup = "BAD";
					Game.LocalPlayer.Character.RelationshipGroup = "COP";
					StopThePedFunctions.SetPedUnderDrugsInfluence(this.Ped, true);
					this.Ped.Accuracy = 10;
					this.Flee = true;
					this.Ped.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 500, true);
					this.Ped.Tasks.FightAgainstClosestHatedTarget(15f, -1);
					this.Ped.KeepTasks = true;
					this.Fleeing();
					this.Aim();
					this.Ped.Tasks.Wander();
				}
			}
			catch (ThreadAbortException)
			{
			}
		}

		
		private void Fleeing()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Flee)
					{
						GameFiber.Yield();
						foreach (Ped x2 in from x in World.GetAllPeds()
						where !x.IsInAnyVehicle(false) && x.IsAlive && x.IsHuman && !x.IsLocalPlayer && !x.IsPersistent && !Functions.IsPedACop(x) && x.DistanceTo(this.Ped.Position) < 20f
						select x)
						{
							bool flag = x2 != null;
							if (flag)
							{
								bool flag2 = EntityExtensions.Exists(x2);
								if (flag2)
								{
									x2.MakePersistent();
									x2.Tasks.Flee(this.Ped, 1000f, 20000);
								}
							}
						}
					}
				}
				catch (ThreadAbortException)
				{
				}
			});
		}

		
		private void Aim()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Flee)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Ped) < 13f;
						if (flag)
						{
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Flee = false;
								}
							}
							else
							{
								this.Ped.Tasks.FightAgainst(Game.LocalPlayer.Character);
								this.Flee = false;
							}
						}
					}
				}
				catch (ThreadAbortException)
				{
				}
			});
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
			this.Flee = false;
			base.CleanUp();
		}

		
		public Ped Ped;

		
		private static string[] _drunkAnimationSets = new string[]
		{
			"move_m@drunk@moderatedrunk"
		};

		
		private string[] Weaponlist = new string[]
		{
			"weapon_revolver",
			"weapon_minismg",
			"weapon_doubleaction"
		};

		
		private bool Flee = false;
	}
}
