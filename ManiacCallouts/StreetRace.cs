using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Illegal Street Race Highway", 2)]
	public class StreetRace : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 7))
				{
				case 1:
					this.Calloutloc = new Vector3(1662.849f, -938.4568f, 65.06882f);
					this.Starthead = 116;
					this.Block1 = new Vector3(1621.484f, -961.1974f, 62.53897f);
					this.Block1head = 244;
					this.Block2 = new Vector3(1624.713f, -969.1682f, 62.38006f);
					this.Block2head = 17;
					this.Racer1 = new Vector3(1654.665f, -946.4666f, 64.16999f);
					this.Racer1head = 300;
					this.Racer2 = new Vector3(1651.829f, -941.906f, 64.13655f);
					this.Racer2head = 301;
					this.Speedslow = new Vector3(1583.038f, -986.1891f, 60.32903f);
					this.Speedblock = new Vector3(1620.318f, -964.4386f, 62.4618f);
					this.Watcher1 = new Vector3(1650.812f, -955.8578f, 63.62216f);
					this.Watch1head = 359;
					this.Watcher2 = new Vector3(1646.29f, -957.9594f, 63.34587f);
					this.Watch2head = 357;
					this.Watcher3 = new Vector3(1642.108f, -960.0703f, 63.10297f);
					this.Watch3head = 357;
					this.Watcher4 = new Vector3(1638.359f, -962.2896f, 62.87571f);
					this.Watch4head = 356;
					this.Watcher5 = new Vector3(1634.5f, -964.4017f, 62.64908f);
					this.Watch5head = 352;
					this.Wped1 = new Vector3(1639.659f, -947.3549f, 63.58964f);
					this.W1head = 288;
					this.Wped2 = new Vector3(1642.523f, -957.1606f, 63.44587f);
					this.W2head = 330;
					this.Wped3 = new Vector3(1644.101f, -956.9259f, 63.52262f);
					this.W3head = 344;
					this.Wped4 = new Vector3(1641.293f, -956.0715f, 63.49781f);
					this.W4head = 301;
					this.Wped5 = new Vector3(1640.21f, -954.5848f, 63.56875f);
					this.W5head = 300;
					this.Wped6 = new Vector3(1639.268f, -952.5228f, 63.60897f);
					this.W6head = 293;
					this.Wped7 = new Vector3(1654.094f, -951.4729f, 64.11922f);
					this.W7head = 19;
					this.Wped8 = new Vector3(1646.742f, -955.1658f, 63.68811f);
					this.W8head = 339;
					break;
				case 2:
					this.Calloutloc = new Vector3(684.3875f, -2639.435f, 49.85097f);
					this.Starthead = 275;
					this.Block1 = new Vector3(743.9763f, -2631.984f, 51.60977f);
					this.Block1head = 29;
					this.Block2 = new Vector3(739.7335f, -2626.822f, 51.73487f);
					this.Block2head = 145;
					this.Racer1 = new Vector3(693.9862f, -2635.103f, 49.9181f);
					this.Racer1head = 100;
					this.Racer2 = new Vector3(695.0929f, -2640.129f, 50.09417f);
					this.Racer2head = 98;
					this.Speedslow = new Vector3(777.9079f, -986.1891f, 60.32903f);
					this.Speedblock = new Vector3(750.3596f, -2622.829f, 52.6123f);
					this.Watcher1 = new Vector3(705.9894f, -2632.796f, 50.36293f);
					this.Watch1head = 133;
					this.Watcher2 = new Vector3(711.3317f, -2632.017f, 50.58161f);
					this.Watch2head = 138;
					this.Watcher3 = new Vector3(715.8927f, -2631.44f, 50.74624f);
					this.Watch3head = 142;
					this.Watcher4 = new Vector3(706.9521f, -2638.896f, 50.35576f);
					this.Watch4head = 65;
					this.Watcher5 = new Vector3(715.7165f, -2637.536f, 50.55078f);
					this.Watch5head = 85;
					this.Wped1 = new Vector3(701.4435f, -2639.353f, 50.6311f);
					this.W1head = 75;
					this.Wped2 = new Vector3(701.0367f, -2637.032f, 50.63107f);
					this.W2head = 96;
					this.Wped3 = new Vector3(700.6565f, -2635.817f, 50.6245f);
					this.W3head = 98;
					this.Wped4 = new Vector3(702.7104f, -2637.599f, 50.69404f);
					this.W4head = 94;
					this.Wped5 = new Vector3(702.8682f, -2641.26f, 50.67457f);
					this.W5head = 84;
					this.Wped6 = new Vector3(706.0306f, -2636.137f, 50.83723f);
					this.W6head = 76;
					this.Wped7 = new Vector3(692.7859f, -2631.664f, 50.50661f);
					this.W7head = 183;
					this.Wped8 = new Vector3(708.2175f, -2634.24f, 50.93768f);
					this.W8head = 93;
					break;
				case 3:
					this.Calloutloc = new Vector3(-413.2717f, -925.2845f, 37.17987f);
					this.Starthead = 355;
					this.Block1 = new Vector3(-408.7338f, -864.6625f, 37.74225f);
					this.Block1head = 115;
					this.Block2 = new Vector3(-415.8159f, -862.6255f, 37.94465f);
					this.Block2head = 228;
					this.Racer1 = new Vector3(-416.3448f, -912.8123f, 36.7069f);
					this.Racer1head = 180;
					this.Racer2 = new Vector3(-410.9453f, -912.7667f, 36.76213f);
					this.Racer2head = 178;
					this.Speedslow = new Vector3(-412.6651f, -826.1636f, 38.8808f);
					this.Speedblock = new Vector3(-412.8201f, -855.3694f, 38.8455f);
					this.Watcher1 = new Vector3(-418.0175f, -900.8579f, 36.66089f);
					this.Watch1head = 231;
					this.Watcher2 = new Vector3(-417.9317f, -894.5791f, 36.66193f);
					this.Watch2head = 229;
					this.Watcher3 = new Vector3(-417.2065f, -890.3109f, 36.68772f);
					this.Watch3head = 233;
					this.Watcher4 = new Vector3(-408.0602f, -901.8649f, 36.67183f);
					this.Watch4head = 127;
					this.Watcher5 = new Vector3(-408.2831f, -895.645f, 36.67669f);
					this.Watch5head = 126;
					this.Wped1 = new Vector3(-408.6106f, -905.6265f, 37.18621f);
					this.W1head = 150;
					this.Wped2 = new Vector3(-411.0065f, -900.6556f, 37.26235f);
					this.W2head = 176;
					this.Wped3 = new Vector3(-412.5717f, -900.8118f, 37.31211f);
					this.W3head = 175;
					this.Wped4 = new Vector3(-408.8439f, -899.1671f, 37.19352f);
					this.W4head = 139;
					this.Wped5 = new Vector3(-414.9191f, -903.8242f, 37.25498f);
					this.W5head = 200;
					this.Wped6 = new Vector3(-411.586f, -895.4924f, 37.28082f);
					this.W6head = 163;
					this.Wped7 = new Vector3(-418.6917f, -911.5504f, 37.13382f);
					this.W7head = 261;
					this.Wped8 = new Vector3(-413.7603f, -892.5721f, 37.2933f);
					this.W8head = 193;
					break;
				case 4:
					this.Calloutloc = new Vector3(1045.333f, 326.154f, 84.12128f);
					this.Starthead = 128;
					this.Block1 = new Vector3(1013.389f, 286.6557f, 82.22215f);
					this.Block1head = 12;
					this.Block2 = new Vector3(1007.848f, 292.1224f, 82.46278f);
					this.Block2head = 270;
					this.Racer1 = new Vector3(1040.068f, 315.9596f, 83.54051f);
					this.Racer1head = 315;
					this.Racer2 = new Vector3(1035.397f, 320.7953f, 83.54828f);
					this.Racer2head = 312;
					this.Speedslow = new Vector3(982.3097f, 259.3268f, 81.0259f);
					this.Speedblock = new Vector3(1004.372f, 283.2929f, 82.5132f);
					this.Watcher1 = new Vector3(1027.789f, 313.8954f, 83.28693f);
					this.Watch1head = 258;
					this.Watcher2 = new Vector3(1024.602f, 310.5267f, 83.16169f);
					this.Watch2head = 256;
					this.Watcher3 = new Vector3(1021.863f, 307.6075f, 83.04991f);
					this.Watch3head = 255;
					this.Watcher4 = new Vector3(1032.652f, 306.3516f, 83.11736f);
					this.Watch4head = 8;
					this.Watcher5 = new Vector3(1028.434f, 302.4431f, 82.9179f);
					this.Watch5head = 3;
					this.Wped1 = new Vector3(1027.973f, 309.5302f, 83.68852f);
					this.W1head = 312;
					this.Wped2 = new Vector3(1029.907f, 307.3322f, 83.65221f);
					this.W2head = 327;
					this.Wped3 = new Vector3(1027.966f, 308.308f, 83.66191f);
					this.W3head = 326;
					this.Wped4 = new Vector3(1029.174f, 305.3127f, 83.56165f);
					this.W4head = 348;
					this.Wped5 = new Vector3(1027.028f, 305.4699f, 83.56446f);
					this.W5head = 344;
					this.Wped6 = new Vector3(1035.338f, 310.6295f, 83.78168f);
					this.W6head = 329;
					this.Wped7 = new Vector3(1029.793f, 319.1165f, 83.93454f);
					this.W7head = 247;
					this.Wped8 = new Vector3(1031.114f, 317.2047f, 83.90826f);
					this.W8head = 303;
					break;
				case 5:
					this.Calloutloc = new Vector3(-1626.705f, -741.3248f, 11.41079f);
					this.Starthead = 60;
					this.Block1 = new Vector3(-1669.977f, -721.5256f, 10.98918f);
					this.Block1head = 302;
					this.Block2 = new Vector3(-1666.293f, -714.9342f, 10.87198f);
					this.Block2head = 199;
					this.Racer1 = new Vector3(-1636.111f, -740.4293f, 10.93014f);
					this.Racer1head = 244;
					this.Racer2 = new Vector3(-1633.384f, -735.1654f, 10.85567f);
					this.Racer2head = 242;
					this.Speedslow = new Vector3(-1697.99f, -699.7684f, 11.51328f);
					this.Speedblock = new Vector3(-1674.523f, -713.5178f, 11.44721f);
					this.Watcher1 = new Vector3(-1647.957f, -734.5181f, 10.94425f);
					this.Watch1head = 292;
					this.Watcher2 = new Vector3(-1651.98f, -732.0944f, 10.94291f);
					this.Watch2head = 297;
					this.Watcher3 = new Vector3(-1655.817f, -729.6908f, 10.94285f);
					this.Watch3head = 296;
					this.Watcher4 = new Vector3(-1643.396f, -727.17f, 10.79316f);
					this.Watch4head = 196;
					this.Watcher5 = new Vector3(-1647.492f, -725.5129f, 10.84802f);
					this.Watch5head = 195;
					this.Wped1 = new Vector3(-1639.676f, -730.3197f, 11.33943f);
					this.W1head = 216;
					this.Wped2 = new Vector3(-1653.228f, -726.9264f, 11.43972f);
					this.W2head = 233;
					this.Wped3 = new Vector3(-1652.671f, -728.8101f, 11.44597f);
					this.W3head = 253;
					this.Wped4 = new Vector3(-1649.023f, -727.6174f, 11.405f);
					this.W4head = 230;
					this.Wped5 = new Vector3(-1640.543f, -727.544f, 11.27745f);
					this.W5head = 221;
					this.Wped6 = new Vector3(-1645.575f, -726.9099f, 11.35137f);
					this.W6head = 195;
					this.Wped7 = new Vector3(-1640.341f, -741.0259f, 11.43946f);
					this.W7head = 285;
					this.Wped8 = new Vector3(-1649.232f, -732.5969f, 11.44133f);
					this.W8head = 280;
					break;
				case 6:
					this.Calloutloc = new Vector3(2430.444f, 2908.95f, 40.36139f);
					this.Starthead = 122;
					this.Block1 = new Vector3(2393.563f, 2883.641f, 39.92015f);
					this.Block1head = 245;
					this.Block2 = new Vector3(2398.486f, 2876.336f, 39.77507f);
					this.Block2head = 0;
					this.Racer1 = new Vector3(2423.166f, 2898.659f, 39.79254f);
					this.Racer1head = 309;
					this.Racer2 = new Vector3(2419.949f, 2903.76f, 39.79531f);
					this.Racer2head = 308;
					this.Speedslow = new Vector3(2350.955f, 2845.669f, 40.72203f);
					this.Speedblock = new Vector3(2389.558f, 2876.45f, 40.32317f);
					this.Watcher1 = new Vector3(2424.375f, 2891.51f, 39.72446f);
					this.Watch1head = 4;
					this.Watcher2 = new Vector3(2420.23f, 2889.202f, 39.72181f);
					this.Watch2head = 6;
					this.Watcher3 = new Vector3(2416.041f, 2886.094f, 39.71693f);
					this.Watch3head = 9;
					this.Watcher4 = new Vector3(2411.799f, 2883.225f, 39.72375f);
					this.Watch4head = 355;
					this.Watcher5 = new Vector3(2404.56f, 2876.292f, 39.7102f);
					this.Watch5head = 322;
					this.Wped1 = new Vector3(2418.136f, 2891.242f, 40.24873f);
					this.W1head = 330;
					this.Wped2 = new Vector3(2412.033f, 2892.281f, 40.27845f);
					this.W2head = 317;
					this.Wped3 = new Vector3(2413.371f, 2891.068f, 40.26474f);
					this.W3head = 323;
					this.Wped4 = new Vector3(2407.812f, 2878.464f, 40.19981f);
					this.W4head = 322;
					this.Wped5 = new Vector3(2411.078f, 2886.9f, 40.25149f);
					this.W5head = 312;
					this.Wped6 = new Vector3(2404.474f, 2880.275f, 40.24638f);
					this.W6head = 330;
					this.Wped7 = new Vector3(2422.537f, 2893.427f, 40.26134f);
					this.W7head = 23;
					this.Wped8 = new Vector3(2422.078f, 2889.854f, 40.20707f);
					this.W8head = 1;
					break;
				}
				bool flag = this.Calloutloc.DistanceTo(Game.LocalPlayer.Character.Position) > 200f && this.Calloutloc.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
				if (flag)
				{
					break;
				}
				GameFiber.Yield();
				WaitCount++;
				bool flag2 = WaitCount > 100;
				if (flag2)
				{
					goto Block_4;
				}
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.Calloutloc, 30f);
			base.CalloutMessage = "Illegal Street Race Highway";
			base.CalloutPosition = this.Calloutloc;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Calloutloc);
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Illegal Street Race", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 2");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_RESPOND_CODE_02_02");
			this._Searcharea = this.Block1.Around2D(1f, 2f);
			this._Blip = new Blip(this._Searcharea, 30f);
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			int num = new Random().Next(1, 1);
			int num2 = num;
			if (num2 == 1)
			{
				this.Racing();
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			base.OnCalloutNotAccepted();
		}

		
		public override void Process()
		{
			bool flag = Game.IsKeyDown(Settings.EndCall);
			if (flag)
			{
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Illegal Street Race", "~b~Dispatch: ~w~All Units ~g~Code 4");
				GameFiber.Sleep(2000);
				this.End();
			}
			bool isDead = Game.LocalPlayer.Character.IsDead;
			if (isDead)
			{
				this.End();
			}
			base.Process();
		}

		
		private void Racing()
		{
			this.Scenariorunning = true;
			this.Blockspeed = true;
			GameFiber.Yield();
			this.SpeedSlowZone();
			this.SpeedBlockZone();
			this.ClearRaceZone();
			this.ClearRaceZone2();
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						Extensions.ClearAreaOfVehicles(this.Block1, 20f);
						this.Block1Vehicle = new Vehicle(this.Blocklist[new Random().Next(this.Blocklist.Length)], this.Block1, (float)this.Block1head);
						this.Block1Vehicle.RandomiseLicencePlate();
						this.Block1Vehicle.IsPersistent = true;
						this.Block1Vehicle.IsStolen = false;
						this.Block1Vehicle.IsEngineOn = true;
						this.Block1Vehicle.IndicatorLightsStatus = 3;
						this.Block1Vehicle.ShouldVehiclesYieldToThisVehicle = true;
						this.Block1Vehicle.CreateRandomDriver();
						this.Block1Vehicle.Driver.BlockPermanentEvents = true;
						GameFiber.Yield();
						this.Block2Vehicle = new Vehicle(this.Blocklist[new Random().Next(this.Blocklist.Length)], this.Block2, (float)this.Block2head);
						this.Block2Vehicle.RandomiseLicencePlate();
						this.Block2Vehicle.IsPersistent = true;
						this.Block2Vehicle.IsStolen = false;
						this.Block2Vehicle.IsEngineOn = true;
						this.Block2Vehicle.IndicatorLightsStatus = 3;
						this.Block2Vehicle.ShouldVehiclesYieldToThisVehicle = true;
						this.Block2Vehicle.CreateRandomDriver();
						this.Block2Vehicle.Driver.BlockPermanentEvents = true;
						GameFiber.Yield();
						this.Racer1Vehicle = new Vehicle(this.Superlist[new Random().Next(this.Superlist.Length)], this.Racer1, (float)this.Racer1head);
						this.Racer1Vehicle.RandomiseLicencePlate();
						this.Racer1Vehicle.IsPersistent = true;
						this.Racer1Vehicle.IsStolen = false;
						this.Racer1Vehicle.IsEngineOn = true;
						this.Racer1Vehicle.Mods.HasTurbo = true;
						GameFiber.Yield();
						this.Racer2Vehicle = new Vehicle(this.Superlist[new Random().Next(this.Superlist.Length)], this.Racer2, (float)this.Racer2head);
						this.Racer2Vehicle.RandomiseLicencePlate();
						this.Racer2Vehicle.IsPersistent = true;
						this.Racer2Vehicle.IsStolen = false;
						this.Racer2Vehicle.IsEngineOn = true;
						this.Racer2Vehicle.Mods.HasTurbo = true;
						GameFiber.Yield();
						this.Startgirl = new Ped(this.Startlist[new Random().Next(this.Startlist.Length)], this.Calloutloc, (float)this.Starthead);
						this.Startgirl.IsPersistent = true;
						this.Startgirl.BlockPermanentEvents = true;
						this.Startgirl.Inventory.GiveNewWeapon(new WeaponAsset("weapon_flare"), -1, true);
						GameFiber.Yield();
						this.Racer1ped = new Ped(this.Racerlist[new Random().Next(this.Racerlist.Length)], this.Racer1, (float)this.Racer1head);
						this.Racer1ped.WarpIntoVehicle(this.Racer1Vehicle, -1);
						this.Racer1ped.IsPersistent = true;
						this.Racer1ped.BlockPermanentEvents = true;
						GameFiber.Yield();
						this.Racer2ped = new Ped(this.Racerlist[new Random().Next(this.Racerlist.Length)], this.Racer2, (float)this.Racer2head);
						this.Racer2ped.WarpIntoVehicle(this.Racer2Vehicle, -1);
						this.Racer2ped.IsPersistent = true;
						this.Racer2ped.BlockPermanentEvents = true;
						GameFiber.Yield();
						this.Watch1Vehicle = new Vehicle(this.Superlist[new Random().Next(this.Superlist.Length)], this.Watcher1, (float)this.Watch1head);
						this.Watch1Vehicle.RandomiseLicencePlate();
						this.Watch1Vehicle.IsPersistent = true;
						this.Watch1Vehicle.IsStolen = false;
						GameFiber.Yield();
						this.Watch2Vehicle = new Vehicle(this.Superlist[new Random().Next(this.Superlist.Length)], this.Watcher2, (float)this.Watch2head);
						this.Watch2Vehicle.RandomiseLicencePlate();
						this.Watch2Vehicle.IsPersistent = true;
						this.Watch2Vehicle.IsStolen = false;
						GameFiber.Yield();
						this.Watch3Vehicle = new Vehicle(this.Superlist[new Random().Next(this.Superlist.Length)], this.Watcher3, (float)this.Watch3head);
						this.Watch3Vehicle.RandomiseLicencePlate();
						this.Watch3Vehicle.IsPersistent = true;
						this.Watch3Vehicle.IsStolen = false;
						GameFiber.Yield();
						this.Watch4Vehicle = new Vehicle(this.Superlist[new Random().Next(this.Superlist.Length)], this.Watcher4, (float)this.Watch4head);
						this.Watch4Vehicle.RandomiseLicencePlate();
						this.Watch4Vehicle.IsPersistent = true;
						this.Watch4Vehicle.IsStolen = false;
						GameFiber.Yield();
						this.Watch5Vehicle = new Vehicle(this.Superlist[new Random().Next(this.Superlist.Length)], this.Watcher5, (float)this.Watch5head);
						this.Watch5Vehicle.RandomiseLicencePlate();
						this.Watch5Vehicle.IsPersistent = true;
						this.Watch5Vehicle.IsStolen = false;
						this.Watcherped1 = new Ped(this.Watcherboylist[new Random().Next(this.Watcherboylist.Length)], this.Wped1, (float)this.W1head);
						this.Watcherped1.IsPersistent = true;
						this.Watcherped1.BlockPermanentEvents = true;
						Extensions.PlayTaskScen(this.Watcherped1, "WORLD_HUMAN_PAPARAZZI", 0, false);
						this.Watcherped2 = new Ped(this.Watcherboylist[new Random().Next(this.Watcherboylist.Length)], this.Wped2, (float)this.W2head);
						this.Watcherped2.IsPersistent = true;
						this.Watcherped2.BlockPermanentEvents = true;
						Extensions.PlayTaskScen(this.Watcherped2, "WORLD_HUMAN_TOURIST_MOBILE", 0, false);
						this.Watcherped3 = new Ped(this.Watchergirllist[new Random().Next(this.Watchergirllist.Length)], this.Wped3, (float)this.W3head);
						this.Watcherped3.IsPersistent = true;
						this.Watcherped3.BlockPermanentEvents = true;
						StopThePedFunctions.IsPedAlcoholOverLimit(this.Watcherped3);
						Extensions.PlayTaskScen(this.Watcherped3, "WORLD_HUMAN_PARTYING", 0, false);
						this.Watcherped4 = new Ped(this.Watchergirllist[new Random().Next(this.Watchergirllist.Length)], this.Wped4, (float)this.W4head);
						this.Watcherped4.IsPersistent = true;
						this.Watcherped4.BlockPermanentEvents = true;
						StopThePedFunctions.IsPedAlcoholOverLimit(this.Watcherped4);
						Extensions.PlayTaskScen(this.Watcherped4, "WORLD_HUMAN_PROSTITUTE_HIGH_CLASS", 0, false);
						this.Watcherped5 = new Ped(this.Watchergirllist[new Random().Next(this.Watchergirllist.Length)], this.Wped5, (float)this.W5head);
						this.Watcherped5.IsPersistent = true;
						this.Watcherped5.BlockPermanentEvents = true;
						Extensions.PlayTaskScen(this.Watcherped5, "WORLD_HUMAN_TOURIST_MOBILE", 0, false);
						this.Watcherped6 = new Ped(this.Watcherboylist[new Random().Next(this.Watcherboylist.Length)], this.Wped6, (float)this.W6head);
						this.Watcherped6.IsPersistent = true;
						this.Watcherped6.BlockPermanentEvents = true;
						StopThePedFunctions.IsPedAlcoholOverLimit(this.Watcherped6);
						Extensions.PlayTaskScen(this.Watcherped6, "WORLD_HUMAN_DRINKING_CASINO_TERRACE", 0, false);
						this.Watcherped7 = new Ped(this.Watcherboylist[new Random().Next(this.Watcherboylist.Length)], this.Wped7, (float)this.W7head);
						this.Watcherped7.IsPersistent = true;
						this.Watcherped7.BlockPermanentEvents = true;
						Extensions.PlayTaskScen(this.Watcherped7, "WORLD_HUMAN_CLIPBOARD", 0, false);
						this.Watcherped8 = new Ped(this.Watchergirllist[new Random().Next(this.Watchergirllist.Length)], this.Wped8, (float)this.W8head);
						this.Watcherped8.IsPersistent = true;
						this.Watcherped8.BlockPermanentEvents = true;
						StopThePedFunctions.IsPedAlcoholOverLimit(this.Watcherped8);
						Extensions.PlayTaskScen(this.Watcherped8, "WORLD_HUMAN_DRINKING_CASINO_TERRACE", 0, false);
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Startgirl) < 35f;
						if (flag)
						{
							this._Blip.Delete();
							this.Racer1ped.Tasks.PerformDrivingManeuver(31);
							this.Racer2ped.Tasks.PerformDrivingManeuver(31);
							GameFiber.Sleep(1000);
							this.Startgirl.Inventory.EquippedWeaponObject.Detach();
							this.Veh1();
							this.Veh2();
							this.Veh3();
							this.Veh4();
							this.Veh5();
							break;
						}
					}
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						GameFiber.Sleep(500);
						this.Racer1ped.Tasks.CruiseWithVehicle(this.Racer1Vehicle, 50f, 262710);
						this.Racer2ped.Tasks.CruiseWithVehicle(this.Racer2Vehicle, 50f, 262710);
						GameFiber.Sleep(1000);
						this.Veh();
						GameFiber.Sleep(1000);
						this.Pursuit = Functions.CreatePursuit();
						Functions.AddPedToPursuit(this.Pursuit, this.Racer1ped);
						Functions.AddPedToPursuit(this.Pursuit, this.Racer2ped);
						Functions.SetPursuitIsActiveForPlayer(this.Pursuit, true);
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Calloutloc) > 200f;
						if (flag2)
						{
							this.Scenariorunning = false;
							break;
						}
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void ClearRaceZone()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Blockspeed)
				{
					GameFiber.Yield();
					foreach (Vehicle veh in World.GetEntities(this.Calloutloc, 30f, 917731L))
					{
						bool flag = veh != null;
						if (flag)
						{
							bool flag2 = EntityExtensions.Exists(veh);
							if (flag2)
							{
								bool flag3 = veh != Game.LocalPlayer.Character.CurrentVehicle;
								if (flag3)
								{
									bool flag4 = !veh.CreatedByTheCallingPlugin;
									if (flag4)
									{
										bool flag5 = !this.StreetRaceEnitys.Contains(veh);
										if (flag5)
										{
											bool flag6 = veh.DistanceTo(this.Calloutloc) < 8f;
											if (flag6)
											{
												veh.Delete();
											}
										}
									}
								}
							}
						}
					}
				}
			});
		}

		
		private void Veh()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Scenariorunning)
				{
					GameFiber.Yield();
					bool flag = EntityExtensions.Exists(this.Startgirl) && EntityExtensions.Exists(this.Watcherped1) && EntityExtensions.Exists(this.Watcherped2) && EntityExtensions.Exists(this.Watcherped3) && EntityExtensions.Exists(this.Watcherped4) && EntityExtensions.Exists(this.Watcherped5) && EntityExtensions.Exists(this.Watcherped6) && EntityExtensions.Exists(this.Watcherped7) && EntityExtensions.Exists(this.Watcherped8);
					if (flag)
					{
						bool flag2 = !this.Startgirl.IsDead || !Functions.IsPedArrested(this.Startgirl);
						if (flag2)
						{
							this.Startgirl.Tasks.GoStraightToPosition(this.Watch1Vehicle.RightPosition, 2f, 0f, 1f, -1);
						}
						bool flag3 = !this.Watcherped7.IsDead || !Functions.IsPedArrested(this.Watcherped7);
						if (flag3)
						{
							this.Watcherped7.Tasks.GoStraightToPosition(this.Watch1Vehicle.LeftPosition, 2f, 0f, 1f, -1);
						}
						bool flag4 = !this.Watcherped1.IsDead || !Functions.IsPedArrested(this.Watcherped1);
						if (flag4)
						{
							this.Watcherped1.Tasks.GoStraightToPosition(this.Watch2Vehicle.LeftPosition, 2f, 0f, 1f, -1);
						}
						bool flag5 = !this.Watcherped8.IsDead || !Functions.IsPedArrested(this.Watcherped8);
						if (flag5)
						{
							this.Watcherped8.Tasks.GoStraightToPosition(this.Watch2Vehicle.RightPosition, 2f, 0f, 1f, -1);
						}
						bool flag6 = !this.Watcherped2.IsDead || !Functions.IsPedArrested(this.Watcherped2);
						if (flag6)
						{
							this.Watcherped2.Tasks.GoStraightToPosition(this.Watch3Vehicle.LeftPosition, 2f, 0f, 1f, -1);
						}
						bool flag7 = !this.Watcherped3.IsDead || !Functions.IsPedArrested(this.Watcherped3);
						if (flag7)
						{
							this.Watcherped3.Tasks.GoStraightToPosition(this.Watch3Vehicle.RightPosition, 2f, 0f, 1f, -1);
						}
						bool flag8 = !this.Watcherped5.IsDead || !Functions.IsPedArrested(this.Watcherped5);
						if (flag8)
						{
							this.Watcherped5.Tasks.GoStraightToPosition(this.Watch4Vehicle.LeftPosition, 2f, 0f, 1f, -1);
						}
						bool flag9 = !this.Watcherped6.IsDead || !Functions.IsPedArrested(this.Watcherped6);
						if (flag9)
						{
							this.Watcherped6.Tasks.GoStraightToPosition(this.Watch5Vehicle.LeftPosition, 2f, 0f, 1f, -1);
						}
						bool flag10 = !this.Watcherped4.IsDead || !Functions.IsPedArrested(this.Watcherped4);
						if (flag10)
						{
							this.Watcherped4.Tasks.GoStraightToPosition(this.Watch5Vehicle.RightPosition, 2f, 0f, 1f, -1);
						}
						this.Blockspeed = false;
						GameFiber.Sleep(3500);
						this.Block1Vehicle.Driver.Dismiss();
						this.Block2Vehicle.Driver.Dismiss();
						break;
					}
				}
				while (this.Scenariorunning)
				{
					GameFiber.Yield();
					bool flag11 = EntityExtensions.Exists(this.Startgirl) && EntityExtensions.Exists(this.Watcherped1) && EntityExtensions.Exists(this.Watcherped2) && EntityExtensions.Exists(this.Watcherped3) && EntityExtensions.Exists(this.Watcherped4) && EntityExtensions.Exists(this.Watcherped5) && EntityExtensions.Exists(this.Watcherped6) && EntityExtensions.Exists(this.Watcherped7) && EntityExtensions.Exists(this.Watcherped8);
					if (flag11)
					{
						bool flag12 = !this.Startgirl.IsDead || !Functions.IsPedArrested(this.Startgirl);
						if (flag12)
						{
							this.Startgirl.Tasks.EnterVehicle(this.Watch1Vehicle, 0);
						}
						bool flag13 = !this.Watcherped7.IsDead || !Functions.IsPedArrested(this.Watcherped7);
						if (flag13)
						{
							this.Watcherped7.Tasks.EnterVehicle(this.Watch1Vehicle, -1);
						}
						bool flag14 = !this.Watcherped1.IsDead || !Functions.IsPedArrested(this.Watcherped1);
						if (flag14)
						{
							this.Watcherped1.Tasks.EnterVehicle(this.Watch2Vehicle, -1);
						}
						bool flag15 = !this.Watcherped8.IsDead || !Functions.IsPedArrested(this.Watcherped8);
						if (flag15)
						{
							this.Watcherped8.Tasks.EnterVehicle(this.Watch2Vehicle, 0);
						}
						bool flag16 = !this.Watcherped2.IsDead || !Functions.IsPedArrested(this.Watcherped2);
						if (flag16)
						{
							this.Watcherped2.Tasks.EnterVehicle(this.Watch3Vehicle, -1);
						}
						bool flag17 = !this.Watcherped3.IsDead || !Functions.IsPedArrested(this.Watcherped3);
						if (flag17)
						{
							this.Watcherped3.Tasks.EnterVehicle(this.Watch3Vehicle, 0);
						}
						bool flag18 = !this.Watcherped5.IsDead || !Functions.IsPedArrested(this.Watcherped5);
						if (flag18)
						{
							this.Watcherped5.Tasks.EnterVehicle(this.Watch4Vehicle, -1);
						}
						bool flag19 = !this.Watcherped6.IsDead || !Functions.IsPedArrested(this.Watcherped6);
						if (flag19)
						{
							this.Watcherped6.Tasks.EnterVehicle(this.Watch5Vehicle, -1);
						}
						bool flag20 = !this.Watcherped4.IsDead || !Functions.IsPedArrested(this.Watcherped4);
						if (flag20)
						{
							this.Watcherped4.Tasks.EnterVehicle(this.Watch5Vehicle, 0);
						}
						break;
					}
				}
			});
		}

		
		private void Veh1()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Scenariorunning)
				{
					GameFiber.Yield();
					bool flag = EntityExtensions.Exists(this.Startgirl) && EntityExtensions.Exists(this.Watcherped7);
					if (flag)
					{
						bool flag2 = this.Startgirl.IsInVehicle(this.Watch1Vehicle, true) || this.Startgirl.IsDead || (Functions.IsPedArrested(this.Startgirl) && this.Watcherped7.IsInVehicle(this.Watch1Vehicle, true)) || this.Watcherped7.IsDead || Functions.IsPedArrested(this.Watcherped7);
						if (flag2)
						{
							bool flag3 = this.Startgirl.IsInVehicle(this.Watch1Vehicle, true) && this.Watcherped7.IsInVehicle(this.Watch1Vehicle, true);
							if (flag3)
							{
								GameFiber.Sleep(1000);
								this.Watcherped7.Tasks.CruiseWithVehicle(this.Watch1Vehicle, 10f, 1);
								break;
							}
							bool flag4 = (this.Startgirl.IsInVehicle(this.Watch1Vehicle, true) && this.Watcherped7.IsDead) || Functions.IsPedArrested(this.Watcherped7);
							if (flag4)
							{
								this.Startgirl.Tasks.Clear();
								Game.DisplayNotification("Running");
								GameFiber.Sleep(2000);
								this.Startgirl.Tasks.ShuffleToAdjacentSeat();
								GameFiber.Sleep(3000);
								this.Startgirl.Tasks.CruiseWithVehicle(this.Watch1Vehicle, 15f, 1);
								break;
							}
							bool flag5 = this.Startgirl.IsDead || (Functions.IsPedArrested(this.Startgirl) && this.Watcherped7.IsInVehicle(this.Watch1Vehicle, true));
							if (flag5)
							{
								this.Watcherped7.Tasks.CruiseWithVehicle(this.Watch1Vehicle, 15f, 1);
								break;
							}
						}
					}
				}
			});
		}

		
		private void Veh2()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Scenariorunning)
				{
					GameFiber.Yield();
					bool flag = EntityExtensions.Exists(this.Watcherped1) && EntityExtensions.Exists(this.Watcherped8);
					if (flag)
					{
						bool flag2 = this.Watcherped1.IsInVehicle(this.Watch2Vehicle, true) || this.Watcherped1.IsDead || (Functions.IsPedArrested(this.Watcherped1) && this.Watcherped8.IsInVehicle(this.Watch2Vehicle, true)) || this.Watcherped8.IsDead || Functions.IsPedArrested(this.Watcherped8);
						if (flag2)
						{
							bool flag3 = this.Watcherped1.IsInVehicle(this.Watch2Vehicle, true) && this.Watcherped8.IsInVehicle(this.Watch2Vehicle, true);
							if (flag3)
							{
								GameFiber.Sleep(1000);
								this.Watcherped1.Tasks.CruiseWithVehicle(this.Watch2Vehicle, 10f, 1);
								break;
							}
							bool flag4 = (this.Watcherped8.IsInVehicle(this.Watch2Vehicle, true) && this.Watcherped1.IsDead) || Functions.IsPedArrested(this.Watcherped1);
							if (flag4)
							{
								this.Watcherped8.Tasks.Clear();
								GameFiber.Sleep(2000);
								this.Watcherped8.Tasks.ShuffleToAdjacentSeat();
								GameFiber.Sleep(3000);
								this.Watcherped8.Tasks.CruiseWithVehicle(this.Watch2Vehicle, 15f, 1);
								break;
							}
							bool flag5 = this.Watcherped8.IsDead || (Functions.IsPedArrested(this.Watcherped8) && this.Watcherped1.IsInVehicle(this.Watch2Vehicle, true));
							if (flag5)
							{
								this.Watcherped1.Tasks.CruiseWithVehicle(this.Watch2Vehicle, 15f, 1);
								break;
							}
						}
					}
				}
			});
		}

		
		private void Veh3()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Scenariorunning)
				{
					GameFiber.Yield();
					bool flag = EntityExtensions.Exists(this.Watcherped2) && EntityExtensions.Exists(this.Watcherped3);
					if (flag)
					{
						bool flag2 = this.Watcherped2.IsInVehicle(this.Watch3Vehicle, true) || this.Watcherped2.IsDead || (Functions.IsPedArrested(this.Watcherped2) && this.Watcherped3.IsInVehicle(this.Watch3Vehicle, true)) || this.Watcherped3.IsDead || Functions.IsPedArrested(this.Watcherped3);
						if (flag2)
						{
							bool flag3 = this.Watcherped2.IsInVehicle(this.Watch3Vehicle, true) && this.Watcherped3.IsInVehicle(this.Watch3Vehicle, true);
							if (flag3)
							{
								GameFiber.Sleep(1000);
								this.Watcherped2.Tasks.CruiseWithVehicle(this.Watch3Vehicle, 10f, 1);
								break;
							}
							bool flag4 = (this.Watcherped3.IsInVehicle(this.Watch3Vehicle, true) && this.Watcherped2.IsDead) || Functions.IsPedArrested(this.Watcherped2);
							if (flag4)
							{
								this.Watcherped3.Tasks.Clear();
								GameFiber.Sleep(2000);
								this.Watcherped3.Tasks.ShuffleToAdjacentSeat();
								GameFiber.Sleep(3000);
								this.Watcherped3.Tasks.CruiseWithVehicle(this.Watch3Vehicle, 15f, 1);
								break;
							}
							bool flag5 = this.Watcherped3.IsDead || (Functions.IsPedArrested(this.Watcherped3) && this.Watcherped2.IsInVehicle(this.Watch3Vehicle, true));
							if (flag5)
							{
								this.Watcherped2.Tasks.CruiseWithVehicle(this.Watch3Vehicle, 15f, 1);
								break;
							}
						}
					}
				}
			});
		}

		
		private void Veh4()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Scenariorunning)
				{
					GameFiber.Yield();
					bool flag = EntityExtensions.Exists(this.Watcherped5);
					if (flag)
					{
						bool flag2 = this.Watcherped5.IsInVehicle(this.Watch4Vehicle, true);
						if (flag2)
						{
							GameFiber.Sleep(1000);
							this.Watcherped5.Tasks.CruiseWithVehicle(this.Watch4Vehicle, 10f, 1);
							break;
						}
					}
				}
			});
		}

		
		private void Veh5()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Scenariorunning)
				{
					GameFiber.Yield();
					bool flag = EntityExtensions.Exists(this.Watcherped6) && EntityExtensions.Exists(this.Watcherped4);
					if (flag)
					{
						bool flag2 = this.Watcherped6.IsInVehicle(this.Watch5Vehicle, true) || this.Watcherped6.IsDead || (Functions.IsPedArrested(this.Watcherped6) && this.Watcherped4.IsInVehicle(this.Watch5Vehicle, true)) || this.Watcherped4.IsDead || Functions.IsPedArrested(this.Watcherped4);
						if (flag2)
						{
							bool flag3 = this.Watcherped6.IsInVehicle(this.Watch5Vehicle, true) && this.Watcherped4.IsInVehicle(this.Watch5Vehicle, true);
							if (flag3)
							{
								GameFiber.Sleep(1000);
								this.Watcherped6.Tasks.CruiseWithVehicle(this.Watch5Vehicle, 10f, 1);
								break;
							}
							bool flag4 = (this.Watcherped4.IsInVehicle(this.Watch5Vehicle, true) && this.Watcherped6.IsDead) || Functions.IsPedArrested(this.Watcherped6);
							if (flag4)
							{
								this.Watcherped4.Tasks.Clear();
								GameFiber.Sleep(2000);
								this.Watcherped4.Tasks.ShuffleToAdjacentSeat();
								GameFiber.Sleep(3000);
								this.Watcherped4.Tasks.CruiseWithVehicle(this.Watch5Vehicle, 15f, 1);
								break;
							}
							bool flag5 = this.Watcherped4.IsDead || (Functions.IsPedArrested(this.Watcherped4) && this.Watcherped6.IsInVehicle(this.Watch5Vehicle, true));
							if (flag5)
							{
								this.Watcherped6.Tasks.CruiseWithVehicle(this.Watch5Vehicle, 15f, 1);
								break;
							}
						}
					}
				}
			});
		}

		
		private void ClearRaceZone2()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Blockspeed)
				{
					GameFiber.Yield();
					foreach (Vehicle veh in World.GetEntities(this.Wped2, 30f, 917731L))
					{
						bool flag = veh != null;
						if (flag)
						{
							bool flag2 = EntityExtensions.Exists(veh);
							if (flag2)
							{
								bool flag3 = veh != Game.LocalPlayer.Character.CurrentVehicle;
								if (flag3)
								{
									bool flag4 = !veh.CreatedByTheCallingPlugin;
									if (flag4)
									{
										bool flag5 = !this.StreetRaceEnitys.Contains(veh);
										if (flag5)
										{
											bool flag6 = veh.DistanceTo(this.Wped2) < 10f;
											if (flag6)
											{
												veh.Delete();
											}
										}
									}
								}
							}
						}
					}
				}
			});
		}

		
		private void SpeedBlockZone()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Blockspeed)
				{
					GameFiber.Yield();
					foreach (Vehicle veh in World.GetEntities(this.Speedblock, 50f, 786659L))
					{
						bool flag = veh != null;
						if (flag)
						{
							bool flag2 = EntityExtensions.Exists(veh);
							if (flag2)
							{
								bool flag3 = veh != Game.LocalPlayer.Character.CurrentVehicle;
								if (flag3)
								{
									bool flag4 = !veh.CreatedByTheCallingPlugin;
									if (flag4)
									{
										bool flag5 = !this.StreetRaceEnitys.Contains(veh);
										if (flag5)
										{
											bool flag6 = veh.DistanceTo(this.Speedblock) < 10f;
											if (flag6)
											{
												bool flag7 = veh.Velocity.Length() > 0f;
												if (flag7)
												{
													Vector3 velocity = veh.Velocity;
													velocity.Normalize();
													velocity *= 0f;
													veh.Velocity = velocity;
													bool hasDriver = veh.HasDriver;
													if (hasDriver)
													{
														veh.Driver.Tasks.PerformDrivingManeuver(1);
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			});
		}

		
		private void SpeedSlowZone()
		{
			GameFiber.StartNew(delegate()
			{
				while (this.Blockspeed)
				{
					GameFiber.Yield();
					foreach (Vehicle veh in World.GetEntities(this.Speedslow, 50f, 786659L))
					{
						bool flag = veh != null;
						if (flag)
						{
							bool flag2 = EntityExtensions.Exists(veh);
							if (flag2)
							{
								bool flag3 = veh != Game.LocalPlayer.Character.CurrentVehicle;
								if (flag3)
								{
									bool flag4 = !veh.CreatedByTheCallingPlugin;
									if (flag4)
									{
										bool flag5 = !this.StreetRaceEnitys.Contains(veh);
										if (flag5)
										{
											bool flag6 = veh.DistanceTo(this.Speedslow) < 10f;
											if (flag6)
											{
												bool hasDriver = veh.HasDriver;
												if (hasDriver)
												{
													veh.Driver.Tasks.CruiseWithVehicle(6f);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			});
		}

		
		public override void End()
		{
			this.Scenariorunning = false;
			Game.LogTrivial("ManiacCallouts - Illegal Street Race Cleaned.");
			bool flag = EntityExtensions.Exists(this.Startgirl);
			if (flag)
			{
				this.Startgirl.Dismiss();
			}
			bool flag2 = EntityExtensions.Exists(this.Racer1Vehicle);
			if (flag2)
			{
				this.Racer1Vehicle.Dismiss();
			}
			bool flag3 = EntityExtensions.Exists(this.Racer2Vehicle);
			if (flag3)
			{
				this.Racer2Vehicle.Dismiss();
			}
			bool flag4 = EntityExtensions.Exists(this.Block1Vehicle);
			if (flag4)
			{
				this.Block1Vehicle.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Block1Vehicle.Driver);
			if (flag5)
			{
				this.Block1Vehicle.Driver.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.Block2Vehicle);
			if (flag6)
			{
				this.Block2Vehicle.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Block2Vehicle.Driver);
			if (flag7)
			{
				this.Block2Vehicle.Driver.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this.Racer1ped);
			if (flag8)
			{
				this.Racer1ped.Dismiss();
			}
			bool flag9 = EntityExtensions.Exists(this.Racer2ped);
			if (flag9)
			{
				this.Racer2ped.Dismiss();
			}
			bool flag10 = EntityExtensions.Exists(this._Blip);
			if (flag10)
			{
				this._Blip.Delete();
			}
			bool flag11 = EntityExtensions.Exists(this.Watch1Vehicle);
			if (flag11)
			{
				this.Watch1Vehicle.Dismiss();
			}
			bool flag12 = EntityExtensions.Exists(this.Watch2Vehicle);
			if (flag12)
			{
				this.Watch2Vehicle.Dismiss();
			}
			bool flag13 = EntityExtensions.Exists(this.Watch3Vehicle);
			if (flag13)
			{
				this.Watch3Vehicle.Dismiss();
			}
			bool flag14 = EntityExtensions.Exists(this.Watch4Vehicle);
			if (flag14)
			{
				this.Watch4Vehicle.Dismiss();
			}
			bool flag15 = EntityExtensions.Exists(this.Watch5Vehicle);
			if (flag15)
			{
				this.Watch5Vehicle.Dismiss();
			}
			bool flag16 = EntityExtensions.Exists(this.Watcherped1);
			if (flag16)
			{
				this.Watcherped1.Dismiss();
			}
			bool flag17 = EntityExtensions.Exists(this.Watcherped2);
			if (flag17)
			{
				this.Watcherped2.Dismiss();
			}
			bool flag18 = EntityExtensions.Exists(this.Watcherped3);
			if (flag18)
			{
				this.Watcherped3.Dismiss();
			}
			bool flag19 = EntityExtensions.Exists(this.Watcherped4);
			if (flag19)
			{
				this.Watcherped4.Dismiss();
			}
			bool flag20 = EntityExtensions.Exists(this.Watcherped5);
			if (flag20)
			{
				this.Watcherped5.Dismiss();
			}
			bool flag21 = EntityExtensions.Exists(this.Watcherped6);
			if (flag21)
			{
				this.Watcherped6.Dismiss();
			}
			bool flag22 = EntityExtensions.Exists(this.Watcherped7);
			if (flag22)
			{
				this.Watcherped7.Dismiss();
			}
			bool flag23 = EntityExtensions.Exists(this.Watcherped8);
			if (flag23)
			{
				this.Watcherped8.Dismiss();
			}
			base.End();
		}

		
		private Ped Cop;

		
		private Ped Cop2;

		
		private Ped Startgirl;

		
		private Ped Racer1ped;

		
		private Ped Racer2ped;

		
		private Ped Watcherped1;

		
		private Ped Watcherped2;

		
		private Ped Watcherped3;

		
		private Ped Watcherped4;

		
		private Ped Watcherped5;

		
		private Ped Watcherped6;

		
		private Ped Watcherped7;

		
		private Ped Watcherped8;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private Vehicle Racer1Vehicle;

		
		private Vehicle Racer2Vehicle;

		
		private Vehicle Block1Vehicle;

		
		private Vehicle Block2Vehicle;

		
		private Vehicle Watch1Vehicle;

		
		private Vehicle Watch2Vehicle;

		
		private Vehicle Watch3Vehicle;

		
		private Vehicle Watch4Vehicle;

		
		private Vehicle Watch5Vehicle;

		
		private Vehicle Moneytruck;

		
		private Vehicle Police;

		
		private Object Moneybag;

		
		private const string Hostagefloor = "missprologueig_2";

		
		private const string Hostagefloor1 = "idle_on_floor_malehostage01";

		
		private const string Hostagefloor2 = "idle_on_floor_malehostage02";

		
		private Vector3 _Searcharea;

		
		private Vector3 Calloutloc;

		
		private Vector3 Block1;

		
		private Vector3 Block2;

		
		private Vector3 Racer1;

		
		private Vector3 Racer2;

		
		private Vector3 Speedslow;

		
		private Vector3 Speedblock;

		
		private Vector3 Watcher1;

		
		private Vector3 Watcher2;

		
		private Vector3 Watcher3;

		
		private Vector3 Watcher4;

		
		private Vector3 Watcher5;

		
		private Vector3 Wped1;

		
		private Vector3 Wped2;

		
		private Vector3 Wped3;

		
		private Vector3 Wped4;

		
		private Vector3 Wped5;

		
		private Vector3 Wped6;

		
		private Vector3 Wped7;

		
		private Vector3 Wped8;

		
		private string[] Startlist = new string[]
		{
			"u_f_y_dancerave_01"
		};

		
		private string[] Racerlist = new string[]
		{
			"a_f_y_bevhills_01",
			"a_m_m_bevhills_01",
			"a_m_y_stlat_01",
			"a_f_y_bevhills_02",
			"a_m_m_bevhills_02",
			"a_m_m_malibu_01",
			"a_f_y_genhot_01",
			"a_m_m_soucent_01",
			"a_m_m_stlat_02"
		};

		
		private string[] Watchergirllist = new string[]
		{
			"a_f_y_bevhills_04",
			"a_f_y_bevhills_03",
			"a_f_y_vinewood_04",
			"a_f_y_scdressy_01"
		};

		
		private string[] Watcherboylist = new string[]
		{
			"a_m_m_bevhills_02",
			"a_m_m_bevhills_01",
			"a_m_m_malibu_01",
			"a_m_m_socenlat_01"
		};

		
		private string[] Weaponlist = new string[]
		{
			"weapon_assaultrifle",
			"weapon_carbinerifle",
			"weapon_gusenberg"
		};

		
		private string[] Blocklist = new string[]
		{
			"Sandking",
			"Bison",
			"Rumpo",
			"Boxville4"
		};

		
		private string[] Superlist = new string[]
		{
			"EntityXF",
			"Infernus",
			"Cheetah",
			"Nero2",
			"Osiris",
			"Zentorno",
			"T20",
			"Italigtb",
			"Vacca",
			"Pfister811"
		};

		
		private string[] Coplist;

		
		private bool PursuitCreated = false;

		
		private bool Scenariorunning = false;

		
		private bool Blockspeed = false;

		
		private List<Ped> Driverlist = new List<Ped>();

		
		private List<Entity> StreetRaceEnitys = new List<Entity>();

		
		private int Starthead;

		
		private int Block1head;

		
		private int Block2head;

		
		private int Racer1head;

		
		private int Racer2head;

		
		private int Watch1head;

		
		private int Watch2head;

		
		private int Watch3head;

		
		private int Watch4head;

		
		private int Watch5head;

		
		private int W1head;

		
		private int W2head;

		
		private int W3head;

		
		private int W4head;

		
		private int W5head;

		
		private int W6head;

		
		private int W7head;

		
		private int W8head;

		
		private int counter = 0;
	}
}
