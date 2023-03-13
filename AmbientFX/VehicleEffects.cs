using System;
using GTA;
using GTA.Math;
using GTA.Native;

namespace Ambient_FX
{
	public class VehicleEffects
	{
		public VehicleEffects(Vehicle vehicle)
		{
			this.smoke = new LoopedPTFX("core", "ent_amb_smoke_scrap");
			this.fog = new LoopedPTFX("core", "ent_amb_foundry_fogball");
			this.fog2 = new LoopedPTFX("core", "ent_amb_foundry_fogball");
			this.fog3 = new LoopedPTFX("core", "ent_amb_foundry_fogball");
			this.fog4 = new LoopedPTFX("core", "ent_amb_foundry_fogball");
			this.fire = new LoopedPTFX("core", "ent_amb_fbi_fire_wall_sm");
			this.fire2 = new LoopedPTFX("core", "ent_amb_fbi_fire_wall_sm");
			this._vehicle = vehicle;
		}

		public void InitializeFire_SmokeEffect()
		{
			bool flag = !this.smoke.IsLoaded;
			if (flag)
			{
				this.smoke.Load();
			}
			this.smoke.Start(this._vehicle, 1f, Main.MainOffset, Vector3.Zero, new Bone?(Function.Call<int>(-328455959687549766L, new InputArgument[]
			{
				this._vehicle,
				"engine"
			})));
			this.fog.Start(this._vehicle, 1f, Main.FogBallBehindOffset, Vector3.Zero, new Bone?(Function.Call<int>(-328455959687549766L, new InputArgument[]
			{
				this._vehicle,
				"engine"
			})));
			this.fog2.Start(this._vehicle, 1f, Main.FogBallFrontOffset, Vector3.Zero, new Bone?(Function.Call<int>(-328455959687549766L, new InputArgument[]
			{
				this._vehicle,
				"engine"
			})));
			this.fog3.Start(this._vehicle, 1f, Main.FogBallLeftOffset, Vector3.Zero, new Bone?(Function.Call<int>(-328455959687549766L, new InputArgument[]
			{
				this._vehicle,
				"engine"
			})));
			this.fog4.Start(this._vehicle, 1f, Main.FogBallRightOffset, Vector3.Zero, new Bone?(Function.Call<int>(-328455959687549766L, new InputArgument[]
			{
				this._vehicle,
				"engine"
			})));
			this.fire.Start(this._vehicle, 1f, Main.LeftOffset, Vector3.Zero, new Bone?(Function.Call<int>(-328455959687549766L, new InputArgument[]
			{
				this._vehicle,
				"engine"
			})));
			this.fire2.Start(this._vehicle, 1f, Main.RightOffset, Vector3.Zero, new Bone?(Function.Call<int>(-328455959687549766L, new InputArgument[]
			{
				this._vehicle,
				"engine"
			})));
			Function.Call(-2544088794899434175L, new InputArgument[]
			{
				this.smoke.Handle,
				3000.0
			});
			Function.Call(-2544088794899434175L, new InputArgument[]
			{
				this.fog.Handle,
				3000.0
			});
			Function.Call(-2544088794899434175L, new InputArgument[]
			{
				this.fog2.Handle,
				3000.0
			});
			Function.Call(-2544088794899434175L, new InputArgument[]
			{
				this.fog3.Handle,
				3000.0
			});
			Function.Call(-2544088794899434175L, new InputArgument[]
			{
				this.fog4.Handle,
				3000.0
			});
			Function.Call(-2544088794899434175L, new InputArgument[]
			{
				this.fire.Handle,
				3000.0
			});
			Function.Call(-2544088794899434175L, new InputArgument[]
			{
				this.fire2.Handle,
				3000.0
			});
			this.fog.SetColor(1f, 1f, 1f, 150f);
			this.fog2.SetColor(1f, 1f, 1f, 150f);
			this.fog3.SetColor(1f, 1f, 1f, 150f);
			this.fog4.SetColor(1f, 1f, 1f, 150f);
		}

		public void RemoveFire_SmokeEffects()
		{
			this.smoke.Remove();
			this.fog.Remove();
			this.fog2.Remove();
			this.fog3.Remove();
			this.fog4.Remove();
			this.fire.Remove();
			this.fire2.Remove();
		}

		public void VehicleFXUpdate()
		{
		}

		private LoopedPTFX smoke;

		private LoopedPTFX fog;

		private LoopedPTFX fog2;

		private LoopedPTFX fog3;

		private LoopedPTFX fog4;

		private LoopedPTFX fire;

		private LoopedPTFX fire2;

		public Vehicle _vehicle;
	}
}
