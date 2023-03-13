using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using ManiacCallouts.API;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace ManiacCallouts.Event
{
	
	internal class LightDriver : EventBaseTraffic
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
					this.car = this.Ped.CurrentVehicle;
					this.car.IsPersistent = true;
					this.car.Mods.ApplyAllMods();
					bool flag2 = World.TimeOfDay.Hours <= 5 || World.TimeOfDay.Hours >= 21;
					if (flag2)
					{
						Game.LogTrivial("[ManiacEvents]No Lights Event Started");
						if (LightDriver.<>o__18.<>p__0 == null)
						{
							LightDriver.<>o__18.<>p__0 = CallSite<Action<CallSite, object, Vehicle, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_VEHICLE_LIGHTS", null, typeof(LightDriver), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						LightDriver.<>o__18.<>p__0.Target(LightDriver.<>o__18.<>p__0, NativeFunction.Natives, this.car, 1);
					}
					else
					{
						Game.LogTrivial("[ManiacEvents]Talking In Phone Event Started");
						if (LightDriver.<>o__18.<>p__1 == null)
						{
							LightDriver.<>o__18.<>p__1 = CallSite<Action<CallSite, object, Ped, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_USE_MOBILE_PHONE_TIMED", null, typeof(LightDriver), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						LightDriver.<>o__18.<>p__1.Target(LightDriver.<>o__18.<>p__1, NativeFunction.Natives, this.Ped, 100000);
					}
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
	}
}
