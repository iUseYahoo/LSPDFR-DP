using System;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Store Robbery City", 2)]
	public class StoreCity : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 29))
				{
				case 1:
					this.Rob = new Vector3(-1285.921f, -1118.228f, 6.990119f);
					this.Own = new Vector3(-1283.76f, -1115.668f, 6.990118f);
					this.Headingown = 93;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = true;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 75;
					break;
				case 2:
					this.Rob = new Vector3(1161.945f, -322.976f, 69.20507f);
					this.Own = new Vector3(1164.984f, -322.854f, 69.20513f);
					this.Headingown = 113;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				case 3:
					this.Rob = new Vector3(324.5471f, 180.6007f, 103.5865f);
					this.Own = new Vector3(323.0688f, 181.8199f, 103.5865f);
					this.Headingown = 147;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = true;
					this.h7 = false;
					this.Headinghostage = 119;
					break;
				case 4:
					this.Rob = new Vector3(-710.2136f, -153.5768f, 37.41514f);
					this.Own = new Vector3(-708.0946f, -152.9056f, 37.41514f);
					this.Door = new Vector3(-716.318f, -156.6457f, 37.18574f);
					this.Headingown = 109;
					this.h1 = false;
					this.h2 = true;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 110;
					break;
				case 5:
					this.Rob = new Vector3(-48.6024f, -1755.72f, 29.42101f);
					this.Own = new Vector3(-46.03804f, -1757.796f, 29.42101f);
					this.Headingown = 50;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				case 6:
					this.Rob = new Vector3(-708.2062f, -913.2461f, 19.21559f);
					this.Own = new Vector3(-705.7925f, -913.3139f, 19.21559f);
					this.Headingown = 85;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				case 7:
					this.Rob = new Vector3(-162.4862f, -302.9768f, 39.73328f);
					this.Own = new Vector3(-165.146f, -302.6105f, 39.73328f);
					this.Headingown = 256;
					this.h1 = false;
					this.h2 = true;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 244;
					break;
				case 8:
					this.Rob = new Vector3(-1194.578f, -769.6265f, 17.31933f);
					this.Own = new Vector3(-1194.192f, -766.5731f, 17.3155f);
					this.Headingown = 214;
					this.h1 = false;
					this.h2 = false;
					this.h3 = true;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 136;
					break;
				case 9:
					this.Rob = new Vector3(-1822.149f, 792.1907f, 138.1447f);
					this.Own = new Vector3(-1819.911f, 795.2961f, 138.0739f);
					this.Headingown = 127;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				case 10:
					this.Rob = new Vector3(1210.301f, -472.7464f, 66.208f);
					this.Own = new Vector3(1211.76f, -471.203f, 66.208f);
					this.Headingown = 67;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = true;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 68;
					break;
				case 11:
					this.Rob = new Vector3(-3041.455f, 586.1425f, 7.908929f);
					this.Own = new Vector3(-3040.658f, 583.5352f, 7.908929f);
					this.Headingown = 15;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 305;
					break;
				case 12:
					this.Rob = new Vector3(26.91059f, -1345.359f, 29.49702f);
					this.Own = new Vector3(24.28757f, -1344.816f, 29.49702f);
					this.Headingown = 259;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 201;
					break;
				case 13:
					this.Rob = new Vector3(-1451.608f, -237.2331f, 49.80844f);
					this.Own = new Vector3(-1449.869f, -239.0791f, 49.81335f);
					this.Headingown = 41;
					this.h1 = false;
					this.h2 = true;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 43;
					break;
				case 14:
					this.Rob = new Vector3(-1155.294f, -1427.67f, 4.954465f);
					this.Own = new Vector3(-1152.43f, -1426.593f, 4.954461f);
					this.Headingown = 25;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = true;
					this.h7 = false;
					this.Headinghostage = 352;
					break;
				case 15:
					this.Rob = new Vector3(375.2445f, 327.5486f, 103.5664f);
					this.Own = new Vector3(372.9799f, 328.3381f, 103.5664f);
					this.Headingown = 256;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 195;
					break;
				case 16:
					this.Rob = new Vector3(-3243.206f, 1002.39f, 12.83071f);
					this.Own = new Vector3(-3244.138f, 999.789f, 12.83071f);
					this.Headingown = 355;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 291;
					break;
				case 17:
					this.Rob = new Vector3(-819.6025f, -186.434f, 37.56893f);
					this.Own = new Vector3(-823.6136f, -183.7751f, 37.56893f);
					this.Headingown = 207;
					this.Own2 = new Vector3(-814.8403f, -183.4227f, 37.56893f);
					this.Headingown2 = 111;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = true;
					this.Headinghostage = 106;
					break;
				case 18:
					this.Rob = new Vector3(-2969.97f, 389.8903f, 15.04331f);
					this.Own = new Vector3(-2965.379f, 391.2633f, 15.04331f);
					this.Headingown = 89;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				case 19:
					this.Rob = new Vector3(424.2729f, -807.819f, 29.49114f);
					this.Own = new Vector3(427.2458f, -806.7103f, 29.49114f);
					this.Headingown = 87;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = true;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 87;
					break;
				case 20:
					this.Rob = new Vector3(76.6454f, -1390.995f, 29.37615f);
					this.Own = new Vector3(73.69182f, -1392.544f, 29.37615f);
					this.Headingown = 274;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = true;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 269;
					break;
				case 21:
					this.Rob = new Vector3(-1487.951f, -380.607f, 40.16343f);
					this.Own = new Vector3(-1485.683f, -377.3025f, 40.16343f);
					this.Headingown = 134;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				case 22:
					this.Rob = new Vector3(1321.616f, -1654.178f, 52.2754f);
					this.Own = new Vector3(1324.46f, -1652.933f, 52.27564f);
					this.Headingown = 37;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = true;
					this.h7 = false;
					this.Headinghostage = 350;
					break;
				case 23:
					this.Rob = new Vector3(-820.1385f, -1073.88f, 11.32811f);
					this.Own = new Vector3(-823.1353f, -1071.98f, 11.32811f);
					this.Headingown = 210;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = true;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 212;
					break;
				case 24:
					this.Rob = new Vector3(125.9417f, -221.5112f, 54.55783f);
					this.Own = new Vector3(127.5301f, -223.8947f, 54.55783f);
					this.Headingown = 64;
					this.h1 = false;
					this.h2 = false;
					this.h3 = true;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 0;
					break;
				case 25:
					this.Rob = new Vector3(136.1369f, -1709.963f, 29.29162f);
					this.Own = new Vector3(134.9309f, -1708.168f, 29.29162f);
					this.Headingown = 118;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = true;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 129;
					break;
				case 26:
					this.Rob = new Vector3(-32.96206f, -149.7691f, 57.08606f);
					this.Own = new Vector3(-30.9424f, -152.0736f, 57.0765f);
					this.Headingown = 327;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = true;
					this.h6 = false;
					this.h7 = false;
					this.Headinghostage = 328;
					break;
				case 27:
					this.Rob = new Vector3(1137.193f, -981.2866f, 46.41584f);
					this.Own = new Vector3(1133.754f, -982.6366f, 46.41584f);
					this.Headingown = 274;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				case 28:
					this.Rob = new Vector3(-1224.681f, -906.4672f, 12.32636f);
					this.Own = new Vector3(-1220.782f, -908.2158f, 12.32635f);
					this.Headingown = 51;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.h7 = false;
					break;
				}
				bool flag = this.Rob.DistanceTo(Game.LocalPlayer.Character.Position) > 200f && this.Rob.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
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
			base.ShowCalloutAreaBlipBeforeAccepting(this.Rob, 30f);
			base.CalloutMessage = "Robbery In Progress";
			base.CalloutPosition = this.Rob;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Rob);
			int num = new Random().Next(1, 3);
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					this.Hostage();
				}
			}
			else
			{
				this.ShootandFight();
			}
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Robbery In Progress", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 3");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_CRIME_ROBBERY_03 MC_RESPOND_CODE3");
			GameFiber.Wait(1000);
			this._Searcharea = this.Rob.Around2D(1f, 2f);
			this._Blip = new Blip(this._Searcharea, 40f);
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			this.Robber = new Ped(this.Robberylist[new Random().Next(this.Robberylist.Length)], this.Rob, 0f);
			this.Robber.IsPersistent = true;
			this.Robber.BlockPermanentEvents = true;
			this.Robber.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 500, true);
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			base.OnCalloutNotAccepted();
		}

		
		public override void Process()
		{
			bool flag = StopThePedFunctions.IsPedStoppedWithSTP(this.Robber).Value && !this.Stoppedped;
			if (flag)
			{
				GameFiber.Wait(500);
				this.Owner.Tasks.Flee(this.Rob, 500f, -1);
				this.Robdeath = true;
				this.Stoppedped = true;
			}
			bool flag2 = this.Robber.IsDead && EntityExtensions.Exists(this.Robber);
			if (flag2)
			{
				GameFiber.Wait(500);
				this.Owner.Tasks.Flee(this.Rob, 500f, -1);
				this.Robdeath = true;
			}
			bool flag3 = Functions.IsPedArrested(this.Robber) || this.Robber.IsCuffed;
			if (flag3)
			{
				GameFiber.Wait(500);
				this.Robdeath = true;
				this.Owner.Tasks.Flee(this.Rob, 500f, -1);
			}
			bool flag4 = Functions.IsPedInPursuit(this.Robber) && !this.PursuitCreated;
			if (flag4)
			{
				this.PursuitCreated = true;
			}
			bool flag5 = Game.IsKeyDown(Settings.EndCall);
			if (flag5)
			{
				this.End();
			}
			bool isDead = Game.LocalPlayer.Character.IsDead;
			if (isDead)
			{
				this.End();
			}
			base.Process();
		}

		
		private void ShootandFight()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Own) <= 24f;
						if (flag)
						{
							bool flag2 = !this.h1 && !this.h2 && !this.h3 && !this.h4 && !this.h5 && !this.h6 && !this.h7;
							if (flag2)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag3 = this.h1;
							if (flag3)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag4 = this.h2;
							if (flag4)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_m_shop_high", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag5 = this.h3;
							if (flag5)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_y_shop_mid", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag6 = this.h4;
							if (flag6)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_y_shop_low", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag7 = this.h5;
							if (flag7)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_m_fembarber", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag8 = this.h6;
							if (flag8)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("u_m_y_tattoo_01", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag9 = this.h7;
							if (flag9)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								Extensions.ClearAreaOfPeds(this.Own2, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								Extensions.ClearAreaOfPeds(this.Own2, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								Extensions.ClearAreaOfPeds(this.Own2, 2f);
								this.Owner = new Ped("a_f_y_business_01", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Owner2 = new Ped("s_m_m_hairdress_01", this.Own2, (float)this.Headingown2);
								this.Owner2.IsPersistent = true;
								this.Owner2.MaxHealth = 175;
								this.Owner2.Health = 100;
								this.Owner2.BlockPermanentEvents = true;
								this.Owner2.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag10 = !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 18f;
						if (flag10)
						{
							bool flag11 = !this.h7;
							if (flag11)
							{
								new RelationshipGroup("BAD");
								this.Robber.RelationshipGroup = "BAD";
								Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
								this.Robber.Tasks.FightAgainst(this.Owner, -1);
								this.Ownerdeath();
								break;
							}
							bool flag12 = this.h7;
							if (flag12)
							{
								new RelationshipGroup("BAD");
								this.Robber.RelationshipGroup = "BAD";
								Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
								this.Robber.Tasks.FireWeaponAt(this.Owner2, -1, -687903391);
								this.Ownerdeath2();
								break;
							}
						}
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void Hostage()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Own) <= 24f;
						if (flag)
						{
							bool flag2 = !this.h1 && !this.h2 && !this.h3 && !this.h4 && !this.h5 && !this.h6 && !this.h7;
							if (flag2)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.SetPositionWithSnap(this.Own);
								this.Robber.Heading = (float)this.Headingown;
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag3 = this.h1;
							if (flag3)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag4 = this.h2;
							if (flag4)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_m_shop_high", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag5 = this.h3;
							if (flag5)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_y_shop_mid", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag6 = this.h4;
							if (flag6)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_y_shop_low", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag7 = this.h5;
							if (flag7)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_m_fembarber", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag8 = this.h6;
							if (flag8)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("u_m_y_tattoo_01", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag9 = this.h7;
							if (flag9)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								Extensions.ClearAreaOfPeds(this.Own2, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own2, 2f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								Extensions.ClearAreaOfPeds(this.Own2, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("a_f_y_business_01", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner2 = new Ped("s_m_m_hairdress_01", this.Own2, (float)this.Headingown2);
								this.Owner2.IsPersistent = true;
								this.Owner2.MaxHealth = 175;
								this.Owner2.Health = 100;
								this.Owner2.BlockPermanentEvents = true;
								this.Owner2.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_malehostage01", 6f, 1);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag10 = !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
						if (flag10)
						{
							Game.DisplaySubtitle("~r~Suspect: ~w~Dont get closer!!!!");
							break;
						}
						bool isShooting = Game.LocalPlayer.Character.IsShooting;
						if (isShooting)
						{
							GameFiber.Sleep(200);
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag11 = !this.Robdeath && !this.Close && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
						if (flag11)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to try talk to the suspect!", Settings.Interact), false);
							bool flag12 = Game.IsKeyDown(Settings.Interact);
							if (flag12)
							{
								int num = new Random().Next(1, 3);
								int num2 = num;
								if (num2 != 1)
								{
									if (num2 == 2)
									{
										this.Giveup = false;
									}
								}
								else
								{
									this.Giveup = true;
								}
								Game.DisplaySubtitle("~b~Player: ~w~Put down your weapon and release the hostage!!");
								break;
							}
						}
						bool flag13 = !this.Giveup && !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 6f;
						if (flag13)
						{
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
						bool isShooting2 = Game.LocalPlayer.Character.IsShooting;
						if (isShooting2)
						{
							GameFiber.Sleep(200);
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag14 = !this.Giveup && !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 6f;
						if (flag14)
						{
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
						bool isShooting3 = Game.LocalPlayer.Character.IsShooting;
						if (isShooting3)
						{
							GameFiber.Sleep(200);
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
						bool flag15 = this.Giveup && !this.Robdeath && !this.Close;
						if (flag15)
						{
							bool flag16 = !this.Robdeath && !this.Close && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
							if (flag16)
							{
								Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
								bool flag17 = Game.IsKeyDown(Settings.Interact);
								if (flag17)
								{
									switch (new Random().Next(1, 5))
									{
									case 1:
										this.Robbertalk = "~r~Suspect: ~w~Sorry, it was not meant to end like this...";
										break;
									case 2:
										this.Robbertalk = "~r~Suspect: ~w~Ok dont shoot, i will surrender...";
										break;
									case 3:
										this.Robbertalk = "~r~Suspect: ~w~I only wanted some money for food, dont shoot...";
										break;
									case 4:
										this.Robbertalk = "~r~Suspect: ~w~I'm not a killer, dont shoot...";
										break;
									}
									Game.DisplaySubtitle(this.Robbertalk);
									GameFiber.Sleep(2800);
									this.Robber.Inventory.EquippedWeapon.DropToGround();
									this.Robber.Tasks.PutHandsUp(-1, Game.LocalPlayer.Character);
									this.Owner.Tasks.Flee(this.Rob, 500f, -1);
									bool flag18 = this.h7;
									if (flag18)
									{
										this.Owner2.Tasks.Flee(this.Rob, 500f, -1);
										break;
									}
									break;
								}
							}
						}
						bool flag19 = !this.Giveup && !this.Robdeath && !this.Close;
						if (flag19)
						{
							bool flag20 = !this.Robdeath && !this.Close && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
							if (flag20)
							{
								Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
								bool flag21 = Game.IsKeyDown(Settings.Interact);
								if (flag21)
								{
									switch (new Random().Next(1, 4))
									{
									case 1:
										this.Robbertalk = "~r~Suspect: ~w~Do you think I want to end up in jail!!";
										break;
									case 2:
										this.Robbertalk = "~r~Suspect: ~w~You are funny!!";
										break;
									case 3:
										this.Robbertalk = "~r~Suspect: ~w~Shut up!!";
										break;
									}
									Game.DisplaySubtitle(this.Robbertalk);
									GameFiber.Sleep(2000);
									this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
									this.Ownerdeath();
									this.counter++;
									break;
								}
							}
						}
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void Ownerdeath()
		{
			while (this.Scenariorunning)
			{
				GameFiber.Yield();
				bool flag = EntityExtensions.Exists(this.Owner) && this.Owner.IsDead;
				if (flag)
				{
					this.Robber.Tasks.Clear();
					GameFiber.Wait(100);
					this.Robber.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
					bool flag2 = EntityExtensions.Exists(this.Owner2);
					if (flag2)
					{
						this.Owner2.Tasks.Flee(this.Rob, 500f, -1);
						break;
					}
					break;
				}
			}
		}

		
		private void Ownerdeath2()
		{
			while (this.Scenariorunning)
			{
				GameFiber.Yield();
				bool flag = EntityExtensions.Exists(this.Owner2) && this.Owner2.IsDead;
				if (flag)
				{
					GameFiber.Wait(100);
					this.Robber.Tasks.FightAgainst(this.Owner, -1);
					this.Ownerdeath();
					break;
				}
			}
		}

		
		private void Playershooting()
		{
		}

		
		public override void End()
		{
			this.Scenariorunning = false;
			Game.LogTrivial("ManiacCallouts - Suspicious Activity Cleaned.");
			bool flag = EntityExtensions.Exists(this.Robber);
			if (flag)
			{
				bool isDead = this.Robber.IsDead;
				if (isDead)
				{
					Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_NEUTRALIZED MC_WE_ARE_CODE_4");
					Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units Suspect Is Neutralized");
				}
				else
				{
					bool flag2 = Functions.IsPedArrested(this.Robber);
					if (flag2)
					{
						Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_ARRESTED MC_WE_ARE_CODE_4");
						Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units Suspect Is Arrested");
					}
					else
					{
						bool isAlive = this.Robber.IsAlive;
						if (isAlive)
						{
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units ~g~Code 4");
						}
					}
				}
			}
			bool flag3 = !EntityExtensions.Exists(this.Robber);
			if (flag3)
			{
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units ~g~Code 4");
			}
			bool flag4 = EntityExtensions.Exists(this._Blip);
			if (flag4)
			{
				this._Blip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this._Blip2);
			if (flag5)
			{
				this._Blip2.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.Owner);
			if (flag6)
			{
				this.Owner.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Owner2);
			if (flag7)
			{
				this.Owner2.Dismiss();
			}
			this.h1 = false;
			bool flag8 = EntityExtensions.Exists(this.Robber);
			if (flag8)
			{
				this.Robber.Dismiss();
			}
			base.End();
		}

		
		private Ped Robber;

		
		private Ped Owner;

		
		private Ped Owner2;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private const string GrabHostage = "misssagrab_inoffice";

		
		private const string SuspectGrab = "hostage_loop";

		
		private const string HostageGrabbed = "hostage_loop_mrk";

		
		private const string Hostagefloor = "missprologueig_2";

		
		private const string Hostagefloor1 = "idle_on_floor_malehostage01";

		
		private Vector3 _Searcharea;

		
		private Vector3 Rob;

		
		private Vector3 Own;

		
		private Vector3 Own2;

		
		private Vector3 Door;

		
		private string[] Storelist = new string[]
		{
			"mp_m_shopkeep_01",
			"s_f_m_sweatshop_01"
		};

		
		private string[] Robberylist = new string[]
		{
			"g_m_m_chicold_01",
			"a_f_y_rurmeth_01",
			"a_m_m_tramp_01"
		};

		
		private string[] Weaponlist = new string[]
		{
			"weapon_doubleaction",
			"weapon_dbshotgun",
			"weapon_microsmg"
		};

		
		private bool PursuitCreated = false;

		
		private bool Blip = false;

		
		private bool Scenariorunning = false;

		
		private bool Robdeath = false;

		
		private bool Stoppedped = false;

		
		private bool Close = false;

		
		private bool h1;

		
		private bool h2;

		
		private bool h3;

		
		private bool h4;

		
		private bool h5;

		
		private bool h6;

		
		private bool h7;

		
		private bool Giveup = false;

		
		private string Robbertalk;

		
		private int Headingown;

		
		private int Headingown2;

		
		private int Headinghostage;

		
		private int counter = 0;
	}
}
