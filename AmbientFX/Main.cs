using System;
using System.Collections.Generic;
using GTA;
using GTA.Math;
using GTA.Native;

namespace Ambient_FX
{
	public class Main : Script
	{
		public Main()
		{
			base.Tick += this.GetVehiclePool_Tick;
			base.Interval = 2000;
		}

		private void GetVehiclePool_Tick(object sender, EventArgs e)
		{
			Main.VehiclePool = World.GetNearbyVehicles(Main.Me.Player.Position, 300f);
		}

		public static readonly float Scale = 1f;

		public static readonly float LowScale = 0.5f;

		public static readonly float CrazyScale = 5f;

		public static readonly Vector3 MainOffset = new Vector3(0f, 0f, 0f);

		public static readonly Vector3 LeftOffset = new Vector3(-0.5f, 0f, 0f);

		public static readonly Vector3 RightOffset = new Vector3(0.5f, 0f, 0f);

		public static readonly Vector3 BehindOffset = new Vector3(0f, 0.5f, 0f);

		public static readonly Vector3 FrontOffset = new Vector3(0f, 0.5f, 0f);

		public static readonly Vector3 FogBallLeftOffset = new Vector3(-5f, 0f, 2f);

		public static readonly Vector3 FogBallRightOffset = new Vector3(5f, 0f, 2f);

		public static readonly Vector3 FogBallFrontOffset = new Vector3(0f, 5f, 2f);

		public static readonly Vector3 FogBallBehindOffset = new Vector3(0f, -5f, 2f);

		public static readonly Vector3 FlippedRotation = new Vector3(-20f, 50f, 150f);

		public static Vehicle[] VehiclePool = new Vehicle[0];

		public static Dictionary<int, VehicleEffects> OnFireList = new Dictionary<int, VehicleEffects>();

		public static class Me
		{
			public static Ped Player
			{
				get
				{
					return Game.Player.Character;
				}
			}
		}

		public class StartLoopedFX : Script
		{
			public StartLoopedFX()
			{
				base.Tick += this.StartLoopedFX_Tick;
			}

			private void StartLoopedFX_Tick(object sender, EventArgs e)
			{
				for (int i = Main.VehiclePool.Length - 1; i >= 0; i--)
				{
					Vehicle vehicle = Main.VehiclePool[i];
					bool flag = vehicle.IsOnFire && !Main.OnFireList.ContainsKey(vehicle.Handle);
					if (flag)
					{
						VehicleEffects vehicleEffects = new VehicleEffects(vehicle);
						vehicleEffects.InitializeFire_SmokeEffect();
						Main.OnFireList.Add(vehicle.Handle, vehicleEffects);
					}
				}
				foreach (KeyValuePair<int, VehicleEffects> keyValuePair in Main.OnFireList)
				{
					Vehicle vehicle2 = new Vehicle(keyValuePair.Key);
					bool flag2 = !vehicle2.IsOnFire;
					if (flag2)
					{
						keyValuePair.Value.RemoveFire_SmokeEffects();
						this.deleteQueue.Enqueue(keyValuePair.Key);
					}
				}
				while (this.deleteQueue.Count > 0)
				{
					int key = this.deleteQueue.Dequeue();
					Main.OnFireList.Remove(key);
				}
			}

			private Queue<int> deleteQueue = new Queue<int>();
		}

		public class StartNonLoopedFX : Script
		{
			public StartNonLoopedFX()
			{
				base.Interval = 250;
				base.Tick += this.StartNonLoopedFX_Tick;
			}

			private void StartNonLoopedFX_Tick(object sender, EventArgs e)
			{
				foreach (KeyValuePair<int, VehicleEffects> keyValuePair in Main.OnFireList)
				{
					Vehicle vehicle = new Vehicle(keyValuePair.Key);
					Function.Call(-5184338789570016586L, new InputArgument[]
					{
						"core"
					});
					bool flag = Function.Call<bool>(-8718333986571631532L, new InputArgument[]
					{
						"core"
					});
					if (flag)
					{
						Function.Call(7798175403732277905L, new InputArgument[]
						{
							"core"
						});
						int num = Function.Call<int>(-328455959687549766L, new InputArgument[]
						{
							vehicle,
							"engine"
						});
						Vector3 boneCoord = vehicle.GetBoneCoord(num);
						Function.Call<int>(2671361570822135507L, new InputArgument[]
						{
							"exp_grd_vehicle_post",
							boneCoord.X,
							boneCoord.Y - 0.3f,
							boneCoord.Z,
							0f,
							0f,
							0f,
							0.7f,
							0,
							1,
							0
						});
					}
				}
			}
		}
	}
}
