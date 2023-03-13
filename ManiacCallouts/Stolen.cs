using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using LSPD_First_Response.Mod.API;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal class Stolen : EventBaseTraffic
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

		
		
		
		private static int nextEventTimer { get; set; }

		
		public override bool Create()
		{
			bool result;
			try
			{
				Ped[] possiblePedsNearInWorld = (from x in World.GetAllPeds()
				where x.IsInAnyVehicle(false) && x.IsAlive && x.IsHuman && !x.IsInAnyPoliceVehicle && x.CurrentVehicle.IsCar && !x.IsPassenger && !x.CurrentVehicle.Model.IsBus && !x.CurrentVehicle.IsConvertible && !x.CurrentVehicle.Model.IsBigVehicle && !x.IsPersistent && x.DistanceTo(Game.LocalPlayer.Character) > 30f && Vector3.Distance(x.Position, Game.LocalPlayer.Character.Position) < 120f
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
					Game.LogTrivial("[ManiacEvents]Stolen Car Event Started");
					new RelationshipGroup("Driver");
					this.car = this.Ped.CurrentVehicle;
					this.car.IsPersistent = true;
					this.car.AlarmTimeLeft = new TimeSpan(0, 15, 0);
					this.car.Doors[0].Remove();
					this.Larm = true;
					this.car.IsStolen = true;
					this.car.MustBeHotwired = true;
					bool hasPassengers = this.car.HasPassengers;
					if (hasPassengers)
					{
						this.Ped2 = this.car.Passengers[0];
						this.Ped2.Accuracy = 5;
						this.Ped2.RelationshipGroup = "Driver";
					}
					this.Ped.Accuracy = 5;
					Extensions.SetPedWanted(this.Ped, true);
					StopThePedFunctions.SetVehicleInsurance(this.car, StopThePedFunctions.StopThePedVehicleStatus.Valid);
					StopThePedFunctions.SetVehicleRegistration(this.car, StopThePedFunctions.StopThePedVehicleStatus.Valid);
					this.Pull = true;
					Stolen.nextEventTimer = Stolen.Rnd.Next(3, 10);
					this.Ped.RelationshipGroup = "Driver";
					this.Pullover();
				}
			}
			catch (ThreadAbortException)
			{
			}
		}

		
		private void Events_OnPulloverDriverStopped(LHandle handle)
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Pedstop)
					{
						GameFiber.Yield();
						bool flag = !EntityExtensions.Exists(this.Ped) || this.Ped.IsDead;
						if (flag)
						{
							this.Pedstop = false;
							Game.LogTrivial("[ManiacEvents]Stolen Car Ped Killed During Pullover");
							break;
						}
						bool flag2 = Functions.IsPlayerPerformingPullover() && Functions.GetPulloverSuspect(Functions.GetCurrentPullover()) == this.Ped;
						if (flag2)
						{
							Stolen.nextEventTimer *= 1000;
							this.Ped.Inventory.GiveNewWeapon(new WeaponAsset(this.Pistollist[new Random().Next(this.Pistollist.Length)]), 500, true);
							GameFiber.Sleep(Stolen.nextEventTimer);
							this.Ped.Tasks.LeaveVehicle(this.car, 256);
							bool flag3 = EntityExtensions.Exists(this.Ped2);
							if (flag3)
							{
								Extensions.SetPedWanted(this.Ped2, true);
								this.Ped2.Inventory.GiveNewWeapon(new WeaponAsset(this.Pistollist[new Random().Next(this.Pistollist.Length)]), 500, true);
								GameFiber.Sleep(500);
								this.Ped2.Tasks.LeaveVehicle(this.car, 256);
							}
							GameFiber.Sleep(1000);
							this.Ped.Tasks.FightAgainst(Game.LocalPlayer.Character);
							bool flag4 = EntityExtensions.Exists(this.Ped2);
							if (flag4)
							{
								GameFiber.Sleep(500);
								this.Ped2.Tasks.FightAgainst(Game.LocalPlayer.Character);
							}
							this.Pedstop = false;
							Game.LogTrivial("[ManiacEvents]Stolen Car Pullover Shooting");
							break;
						}
					}
				}
				catch (ThreadAbortException)
				{
				}
			});
		}

		
		private void Pullover()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Pull)
					{
						GameFiber.Yield();
						bool flag = EntityExtensions.Exists(this.Ped) && this.Ped.IsAlive;
						if (flag)
						{
							bool flag2 = Functions.IsPlayerPerformingPullover() && Functions.GetPulloverSuspect(Functions.GetCurrentPullover()) == this.Ped;
							if (flag2)
							{
								int num = new Random().Next(1, 3);
								int num2 = num;
								if (num2 != 1)
								{
									if (num2 == 2)
									{
										Game.LogTrivial("[ManiacEvents]Stolen Car Pullover");
										this.Pull = false;
									}
								}
								else
								{
									Events.OnPulloverDriverStopped += new Events.SingleLHandleEventHandler(this.Events_OnPulloverDriverStopped);
									Game.LogTrivial("[ManiacEvents]Stolen Car Pullover Shooting");
									this.Pull = false;
									this.Pedstop = true;
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
		}

		
		public override void CleanUp()
		{
			bool flag = EntityExtensions.Exists(this.car);
			if (flag)
			{
				this.car.Dismiss();
			}
			this.Pedstop = false;
			this.Larm = false;
			base.CleanUp();
		}

		
		public Ped Ped;

		
		public Ped Ped2;

		
		public Vehicle car;

		
		private string[] Pistollist = new string[]
		{
			"weapon_combatpistol",
			"weapon_heavypistol",
			"weapon_pistol",
			"weapon_pistol50",
			"weapon_revolver"
		};

		
		private bool Pedstop = false;

		
		private bool Pull = false;

		
		private bool Larm = false;

		
		private static Random Rnd = new Random();
	}
}
