using System;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Suspect Brandishing Firearm", 2)]
	public class ShowingFirearm : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 12))
				{
				case 1:
					this.Callout = new Vector3(-841.6805f, -79.18355f, 37.86926f);
					this.Outside = false;
					this.s1 = true;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 2:
					this.Callout = new Vector3(-149.0222f, 253.8412f, 94.81785f);
					this.Outside = false;
					this.s1 = false;
					this.s2 = true;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 3:
					this.Callout = new Vector3(-1307.051f, -675.433f, 26.26923f);
					this.Outside = false;
					this.s1 = false;
					this.s2 = false;
					this.s3 = true;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 4:
					this.Callout = new Vector3(128.7883f, -798.9146f, 31.08624f);
					this.Outside = false;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = true;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 5:
					this.Callout = new Vector3(-1669.396f, -576.0272f, 33.7172f);
					this.Outside = false;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = true;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 6:
					this.Callout = new Vector3(1185.998f, -472.6074f, 65.86492f);
					this.Outside = false;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = true;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 7:
					this.Callout = new Vector3(1403.729f, 3587.316f, 34.96089f);
					this.Outside = true;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = true;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 8:
					this.Callout = new Vector3(1537.616f, 3771.408f, 34.05016f);
					this.Outside = true;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = true;
					this.s9 = false;
					this.s10 = false;
					this.s11 = false;
					break;
				case 9:
					this.Callout = new Vector3(1668.682f, 4855.289f, 42.06072f);
					this.Outside = true;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = true;
					this.s10 = false;
					this.s11 = false;
					break;
				case 10:
					this.Callout = new Vector3(-290.9059f, 6242.394f, 31.44697f);
					this.Outside = true;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = true;
					this.s11 = false;
					break;
				case 11:
					this.Callout = new Vector3(-11.38823f, 6524.28f, 31.44099f);
					this.Outside = true;
					this.s1 = false;
					this.s2 = false;
					this.s3 = false;
					this.s4 = false;
					this.s5 = false;
					this.s6 = false;
					this.s7 = false;
					this.s8 = false;
					this.s9 = false;
					this.s10 = false;
					this.s11 = true;
					break;
				}
				bool flag = this.Callout.DistanceTo(Game.LocalPlayer.Character.Position) > 200f && this.Callout.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
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
			base.ShowCalloutAreaBlipBeforeAccepting(this.Callout, 30f);
			base.CalloutMessage = "Suspect Brandishing Firearm";
			base.CalloutPosition = this.Callout;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Callout);
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			bool outside = this.Outside;
			if (outside)
			{
				this.Girllist = new string[]
				{
					"a_f_y_hipster_02",
					"a_f_y_hipster_03",
					"a_f_y_indian_01",
					"a_f_y_hippie_01"
				};
				this.Boylist = new string[]
				{
					"a_m_y_hipster_02",
					"a_m_y_hipster_01",
					"a_m_y_indian_01"
				};
				this.Drunklist = new string[]
				{
					"a_m_y_hippy_01",
					"a_m_y_acult_02",
					"a_f_y_hipster_02",
					"a_m_m_tranvest_01",
					"a_f_y_hipster_04",
					"a_f_y_juggalo_01"
				};
				this.Shootlist = new string[]
				{
					"a_m_y_hippy_01"
				};
			}
			else
			{
				this.Girllist = new string[]
				{
					"a_f_y_clubcust_01",
					"a_f_y_bevhills_01",
					"a_f_y_clubcust_02",
					"a_f_y_bevhills_02",
					"a_f_y_clubcust_03"
				};
				this.Boylist = new string[]
				{
					"a_m_m_bevhills_01",
					"a_m_y_bevhills_01",
					"a_m_m_bevhills_02",
					"a_m_y_busicas_01"
				};
				this.Drunklist = new string[]
				{
					"a_f_y_eastsa_03",
					"a_m_m_farmer_01",
					"a_f_y_gencaspat_01",
					"a_m_m_mexlabor_01",
					"a_m_m_salton_03",
					"a_m_m_soucent_03",
					"a_f_y_tourist_02",
					"a_m_m_prolhost_01"
				};
				this.Shootlist = new string[]
				{
					"mp_m_securoguard_01"
				};
			}
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Suspect Brandishing Firearm", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 2");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_CITIZENS_REPORT MC_CRIME_BRANDISHING_WEAPON_01 MC_RESPOND_CODE_02_02");
			this.Searcharea = this.Callout.Around2D(1f, 2f);
			this._Blip = new Blip(this.Searcharea, 50f);
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			int num = new Random().Next(1, 3);
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					this.Drunkgun();
				}
			}
			else
			{
				this.MilitaryShoot();
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
				this.End();
			}
			bool isDead = Game.LocalPlayer.Character.IsDead;
			if (isDead)
			{
				this.End();
			}
			base.Process();
		}

		
		private void MilitaryShoot()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = this.s1;
						if (flag)
						{
							int num = new Random().Next(1, 3);
							int num2 = num;
							if (num2 != 1)
							{
								if (num2 == 2)
								{
									this.Spawnpoint = new Vector3(-845.6304f, -69.04608f, 37.87817f);
									this.Spawnpointhed = 20;
									this.Spawnpoint2 = new Vector3(-847.3168f, -70.22613f, 37.87795f);
									this.Spawnpoint2hed = 20;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(-845.6304f, -69.04608f, 37.87817f);
								this.Spawnpoint2hed = 20;
								this.Spawnpoint = new Vector3(-847.3168f, -70.22613f, 37.87795f);
								this.Spawnpointhed = 20;
							}
							int num3 = new Random().Next(1, 3);
							int num4 = num3;
							if (num4 != 1)
							{
								if (num4 == 2)
								{
									this.Spawnpoint3 = new Vector3(-871.2985f, -92.8826f, 37.87843f);
								}
							}
							else
							{
								this.Spawnpoint3 = new Vector3(-812.8298f, -61.26815f, 37.7115f);
							}
							int num5 = new Random().Next(1, 3);
							int num6 = num5;
							if (num6 != 1)
							{
								if (num6 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag2 = this.s2;
						if (flag2)
						{
							int num7 = new Random().Next(1, 3);
							int num8 = num7;
							if (num8 != 1)
							{
								if (num8 == 2)
								{
									this.Spawnpoint = new Vector3(-154.0879f, 229.5383f, 94.92014f);
									this.Spawnpointhed = 3;
									this.Spawnpoint2 = new Vector3(-154.2206f, 231.0127f, 94.93211f);
									this.Spawnpoint2hed = 179;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(-154.0879f, 229.5383f, 94.92014f);
								this.Spawnpoint2hed = 3;
								this.Spawnpoint = new Vector3(-154.2206f, 231.0127f, 94.93211f);
								this.Spawnpointhed = 179;
							}
							int num9 = new Random().Next(1, 3);
							int num10 = num9;
							if (num10 != 1)
							{
								if (num10 == 2)
								{
									this.Spawnpoint3 = new Vector3(-126.5428f, 231.9627f, 94.93241f);
								}
							}
							else
							{
								this.Spawnpoint3 = new Vector3(-125.3006f, 239.9901f, 96.47318f);
							}
							int num11 = new Random().Next(1, 3);
							int num12 = num11;
							if (num12 != 1)
							{
								if (num12 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag3 = this.s3;
						if (flag3)
						{
							int num13 = new Random().Next(1, 3);
							int num14 = num13;
							if (num14 != 1)
							{
								if (num14 == 2)
								{
									this.Spawnpoint = new Vector3(-1300.113f, -703.9023f, 24.56631f);
									this.Spawnpointhed = 29;
									this.Spawnpoint2 = new Vector3(-1299.779f, -702.4059f, 24.64633f);
									this.Spawnpoint2hed = 127;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(-1300.113f, -703.9023f, 24.56631f);
								this.Spawnpoint2hed = 29;
								this.Spawnpoint = new Vector3(-1299.779f, -702.4059f, 24.64633f);
								this.Spawnpointhed = 127;
							}
							int num15 = new Random().Next(1, 3);
							int num16 = num15;
							if (num16 != 1)
							{
								if (num16 == 2)
								{
									this.Spawnpoint3 = new Vector3(-1284.87f, -687.2207f, 24.6886f);
								}
							}
							else
							{
								this.Spawnpoint3 = new Vector3(-1311.172f, -677.8314f, 26.20206f);
							}
							int num17 = new Random().Next(1, 3);
							int num18 = num17;
							if (num18 != 1)
							{
								if (num18 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag4 = this.s4;
						if (flag4)
						{
							int num19 = new Random().Next(1, 3);
							int num20 = num19;
							if (num20 != 1)
							{
								if (num20 == 2)
								{
									this.Spawnpoint = new Vector3(99.69308f, -816.0703f, 31.40029f);
									this.Spawnpointhed = 138;
									this.Spawnpoint2 = new Vector3(98.67557f, -817.3869f, 31.35833f);
									this.Spawnpoint2hed = 317;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(99.69308f, -816.0703f, 31.40029f);
								this.Spawnpoint2hed = 138;
								this.Spawnpoint = new Vector3(98.67557f, -817.3869f, 31.35833f);
								this.Spawnpointhed = 317;
							}
							int num21 = new Random().Next(1, 3);
							int num22 = num21;
							if (num22 != 1)
							{
								if (num22 == 2)
								{
									this.Spawnpoint3 = new Vector3(123.7132f, -819.5626f, 31.26778f);
								}
							}
							else
							{
								this.Spawnpoint3 = new Vector3(74.00387f, -800.9303f, 31.53923f);
							}
							int num23 = new Random().Next(1, 3);
							int num24 = num23;
							if (num24 != 1)
							{
								if (num24 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag5 = this.s5;
						if (flag5)
						{
							int num25 = new Random().Next(1, 3);
							int num26 = num25;
							if (num26 != 1)
							{
								if (num26 == 2)
								{
									this.Spawnpoint = new Vector3(-1698.994f, -609.1233f, 32.9295f);
									this.Spawnpointhed = 150;
									this.Spawnpoint2 = new Vector3(-1699.618f, -608.4951f, 32.98687f);
									this.Spawnpoint2hed = 147;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(-1698.994f, -609.1233f, 32.9295f);
								this.Spawnpoint2hed = 150;
								this.Spawnpoint = new Vector3(-1699.618f, -608.4951f, 32.98687f);
								this.Spawnpointhed = 147;
							}
							int num27 = new Random().Next(1, 3);
							int num28 = num27;
							if (num28 != 1)
							{
								if (num28 == 2)
								{
									this.Spawnpoint3 = new Vector3(-1702.494f, -568.4861f, 35.96252f);
								}
							}
							else
							{
								this.Spawnpoint3 = new Vector3(-1648.244f, -606.6656f, 33.5378f);
							}
							int num29 = new Random().Next(1, 3);
							int num30 = num29;
							if (num30 != 1)
							{
								if (num30 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag6 = this.s6;
						if (flag6)
						{
							int num31 = new Random().Next(1, 3);
							int num32 = num31;
							if (num32 != 1)
							{
								if (num32 == 2)
								{
									this.Spawnpoint = new Vector3(1173.735f, -453.4399f, 66.49795f);
									this.Spawnpointhed = 70;
									this.Spawnpoint2 = new Vector3(1173.804f, -454.6507f, 66.46783f);
									this.Spawnpoint2hed = 69;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(1173.735f, -453.4399f, 66.49795f);
								this.Spawnpoint2hed = 70;
								this.Spawnpoint = new Vector3(1173.804f, -454.6507f, 66.46783f);
								this.Spawnpointhed = 69;
							}
							int num33 = new Random().Next(1, 3);
							int num34 = num33;
							if (num34 != 1)
							{
								if (num34 == 2)
								{
									this.Spawnpoint3 = new Vector3(1184.958f, -424.7345f, 67.28158f);
								}
							}
							else
							{
								this.Spawnpoint3 = new Vector3(1166.745f, -487.3476f, 65.5695f);
							}
							int num35 = new Random().Next(1, 3);
							int num36 = num35;
							if (num36 != 1)
							{
								if (num36 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag7 = this.s7;
						if (flag7)
						{
							int num37 = new Random().Next(1, 3);
							int num38 = num37;
							if (num38 != 1)
							{
								if (num38 == 2)
								{
									this.Spawnpoint = new Vector3(1401.335f, 3601.273f, 35.07557f);
									this.Spawnpointhed = 19;
									this.Spawnpoint2 = new Vector3(1400.333f, 3600.079f, 35.02758f);
									this.Spawnpoint2hed = 335;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(1401.335f, 3601.273f, 35.07557f);
								this.Spawnpoint2hed = 19;
								this.Spawnpoint = new Vector3(1400.333f, 3600.079f, 35.02758f);
								this.Spawnpointhed = 335;
							}
							int num39 = new Random().Next(1, 1);
							int num40 = num39;
							if (num40 == 1)
							{
								this.Spawnpoint3 = new Vector3(1362.446f, 3586.529f, 34.96661f);
							}
							int num41 = new Random().Next(1, 3);
							int num42 = num41;
							if (num42 != 1)
							{
								if (num42 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag8 = this.s8;
						if (flag8)
						{
							int num43 = new Random().Next(1, 3);
							int num44 = num43;
							if (num44 != 1)
							{
								if (num44 == 2)
								{
									this.Spawnpoint = new Vector3(1528.885f, 3776.63f, 34.51152f);
									this.Spawnpointhed = 303;
									this.Spawnpoint2 = new Vector3(1530.691f, 3777.825f, 34.51165f);
									this.Spawnpoint2hed = 127;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(1528.885f, 3776.63f, 34.51152f);
								this.Spawnpoint2hed = 303;
								this.Spawnpoint = new Vector3(1530.691f, 3777.825f, 34.51165f);
								this.Spawnpointhed = 127;
							}
							int num45 = new Random().Next(1, 3);
							int num46 = num45;
							if (num46 != 1)
							{
								if (num46 == 2)
								{
									this.Spawnpoint3 = new Vector3(1502.161f, 3763.529f, 33.99243f);
								}
							}
							else
							{
								this.Spawnpoint3 = new Vector3(1561.394f, 3794.513f, 34.11485f);
							}
							int num47 = new Random().Next(1, 3);
							int num48 = num47;
							if (num48 != 1)
							{
								if (num48 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag9 = this.s9;
						if (flag9)
						{
							int num49 = new Random().Next(1, 3);
							int num50 = num49;
							if (num50 != 1)
							{
								if (num50 == 2)
								{
									this.Spawnpoint = new Vector3(1682.027f, 4855.042f, 42.06903f);
									this.Spawnpointhed = 186;
									this.Spawnpoint2 = new Vector3(1682.224f, 4853.769f, 42.06608f);
									this.Spawnpoint2hed = 10;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(1682.027f, 4855.042f, 42.06903f);
								this.Spawnpoint2hed = 186;
								this.Spawnpoint = new Vector3(1682.224f, 4853.769f, 42.06608f);
								this.Spawnpointhed = 10;
							}
							int num51 = new Random().Next(1, 1);
							int num52 = num51;
							if (num52 == 1)
							{
								this.Spawnpoint3 = new Vector3(1673.972f, 4879.388f, 42.04236f);
							}
							int num53 = new Random().Next(1, 3);
							int num54 = num53;
							if (num54 != 1)
							{
								if (num54 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag10 = this.s10;
						if (flag10)
						{
							int num55 = new Random().Next(1, 3);
							int num56 = num55;
							if (num56 != 1)
							{
								if (num56 == 2)
								{
									this.Spawnpoint = new Vector3(-297.956f, 6250.698f, 31.48004f);
									this.Spawnpointhed = 310;
									this.Spawnpoint2 = new Vector3(-296.8658f, 6251.654f, 31.45284f);
									this.Spawnpoint2hed = 135;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(-297.956f, 6250.698f, 31.48004f);
								this.Spawnpoint2hed = 310;
								this.Spawnpoint = new Vector3(-296.8658f, 6251.654f, 31.45284f);
								this.Spawnpointhed = 135;
							}
							int num57 = new Random().Next(1, 1);
							int num58 = num57;
							if (num58 == 1)
							{
								this.Spawnpoint3 = new Vector3(-278.5841f, 6271.637f, 31.45768f);
							}
							int num59 = new Random().Next(1, 3);
							int num60 = num59;
							if (num60 != 1)
							{
								if (num60 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
						bool flag11 = this.s11;
						if (flag11)
						{
							int num61 = new Random().Next(1, 3);
							int num62 = num61;
							if (num62 != 1)
							{
								if (num62 == 2)
								{
									this.Spawnpoint = new Vector3(0.8632202f, 6518.812f, 31.48806f);
									this.Spawnpoint2hed = 47;
									this.Spawnpoint2 = new Vector3(0.3708484f, 6519.34f, 31.48455f);
									this.Spawnpoint2hed = 224;
								}
							}
							else
							{
								this.Spawnpoint2 = new Vector3(0.8632202f, 6518.812f, 31.48806f);
								this.Spawnpoint2hed = 47;
								this.Spawnpoint = new Vector3(0.3708484f, 6519.34f, 31.48455f);
								this.Spawnpointhed = 224;
							}
							int num63 = new Random().Next(1, 1);
							int num64 = num63;
							if (num64 == 1)
							{
								this.Spawnpoint3 = new Vector3(-17.5294f, 6502.106f, 31.5253f);
							}
							int num65 = new Random().Next(1, 3);
							int num66 = num65;
							if (num66 != 1)
							{
								if (num66 == 2)
								{
									this.h1 = false;
								}
							}
							else
							{
								this.h1 = true;
							}
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.Girl = new Ped(this.Girllist[new Random().Next(this.Girllist.Length)], this.Spawnpoint, (float)this.Spawnpointhed);
							this.Girl.IsPersistent = true;
							this.Girl.MaxHealth = 175;
							this.Girl.Health = 100;
							this.Girl.BlockPermanentEvents = true;
							this.Boy = new Ped(this.Boylist[new Random().Next(this.Boylist.Length)], this.Spawnpoint2, (float)this.Spawnpoint2hed);
							this.Boy.IsPersistent = true;
							this.Boy.MaxHealth = 200;
							this.Boy.Health = 135;
							this.Boy.BlockPermanentEvents = true;
							this.Suspect = new Ped(this.Shootlist[new Random().Next(this.Shootlist.Length)], this.Spawnpoint3, 0f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.IsPersistent = true;
							this.Suspect.Accuracy = 90;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 500, true);
							Functions.SetPedCantBeArrestedByPlayer(this.Suspect, false);
							Functions.SetPedArrestIgnoreGroup(this.Suspect, true);
							Persona.FromExistingPed(this.Suspect).Wanted = false;
							Persona.FromExistingPed(this.Boy).Wanted = false;
							Persona.FromExistingPed(this.Girl).Wanted = false;
							this.SuspectAction();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag12 = Game.LocalPlayer.Character.DistanceTo(this.Spawnpoint) <= 100f;
						if (flag12)
						{
							this.Suspect.Tasks.FollowNavigationMeshToPosition(this.Spawnpoint, (float)this.Spawnpointhed, 1.5f, 1f);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag13 = this.Suspect.DistanceTo(this.Spawnpoint) <= 13f;
						if (flag13)
						{
							this.Girl.BlockPermanentEvents = false;
							this.Boy.BlockPermanentEvents = false;
							this.Suspect.Tasks.FireWeaponAt(this.Boy, -1, -957453492);
							this._Blip.Delete();
							this._Blip2 = this.Suspect.AttachBlip();
							this._Blip2.Color = Color.Red;
							this._Blip2.Alpha = 0.5f;
							this.Boydeath();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag14 = this.h1 && !this.Sdeath && EntityExtensions.Exists(this.Girl) && this.Girl.IsDead && this.Bdeath && Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 22f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag14)
						{
							GameFiber.Sleep(1000);
							Game.DisplaySubtitle("~r~Suspect: ~w~DONT SHOOT!");
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 2);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 2);
							this.Suspect.Tasks.Clear();
							GameFiber.Sleep(500);
							this.Suspect.Inventory.EquippedWeapon.DropToGround();
							this.Suspect.Tasks.PutHandsUp(-1, Game.LocalPlayer.Character);
							this.Suspect.BlockPermanentEvents = false;
							GameFiber.Sleep(4000);
							break;
						}
						bool flag15 = !this.h1;
						if (flag15)
						{
							new RelationshipGroup("BAD");
							this.Suspect.RelationshipGroup = "BAD";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "PLAYER", 5);
							GameFiber.Sleep(500);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag16 = this.h1 && !this.Sdeath && EntityExtensions.Exists(this.Girl) && this.Girl.IsDead && this.Bdeath && Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 7f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag16)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								Game.DisplaySubtitle("~r~Suspect: ~w~She cheated on me, I had to do this...");
								break;
							case 2:
								Game.DisplaySubtitle("~r~Suspect: ~w~She was a cheating bitch, they deserved this!");
								break;
							case 3:
								Game.DisplaySubtitle("~r~Suspect: ~w~She was my everything and was cheating on me...");
								break;
							}
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

		
		private void Drunkgun()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = this.s1;
						if (flag)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.Spawnpoint = new Vector3(-812.5207f, -97.31676f, 37.60243f);
								break;
							case 2:
								this.Spawnpoint = new Vector3(-829.052f, -116.8291f, 37.59494f);
								break;
							case 3:
								this.Spawnpoint = new Vector3(-837.7221f, -72.12761f, 37.83072f);
								break;
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag2 = this.s2;
						if (flag2)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.Spawnpoint = new Vector3(-157.5005f, 276.099f, 93.71047f);
								break;
							case 2:
								this.Spawnpoint = new Vector3(-146.0305f, 239.4449f, 94.82146f);
								break;
							case 3:
								this.Spawnpoint = new Vector3(-158.6452f, 232.1967f, 94.92196f);
								break;
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag3 = this.s3;
						if (flag3)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.Spawnpoint = new Vector3(-1316.865f, -681.3683f, 26.34476f);
								break;
							case 2:
								this.Spawnpoint = new Vector3(-1297.362f, -668.3841f, 26.14157f);
								break;
							case 3:
								this.Spawnpoint = new Vector3(-1327.679f, -656.6505f, 26.51784f);
								break;
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag4 = this.s4;
						if (flag4)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.Spawnpoint = new Vector3(88.23241f, -812.1892f, 31.38033f);
								break;
							case 2:
								this.Spawnpoint = new Vector3(111.3195f, -778.4791f, 31.42438f);
								break;
							case 3:
								this.Spawnpoint = new Vector3(104.4529f, -809.6984f, 31.40516f);
								break;
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag5 = this.s5;
						if (flag5)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.Spawnpoint = new Vector3(-1679.069f, -585.8216f, 33.73526f);
								break;
							case 2:
								this.Spawnpoint = new Vector3(-1696.989f, -607.8849f, 32.84404f);
								break;
							case 3:
								this.Spawnpoint = new Vector3(-1661.61f, -593.1473f, 33.67564f);
								break;
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag6 = this.s6;
						if (flag6)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.Spawnpoint = new Vector3(1180.247f, -429.5707f, 67.16107f);
								break;
							case 2:
								this.Spawnpoint = new Vector3(1206.374f, -449.2376f, 66.94154f);
								break;
							case 3:
								this.Spawnpoint = new Vector3(1172.776f, -459.015f, 66.3342f);
								break;
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag7 = this.s7;
						if (flag7)
						{
							int num = new Random().Next(1, 1);
							int num2 = num;
							if (num2 == 1)
							{
								this.Spawnpoint = new Vector3(1397.349f, 3592.646f, 34.88308f);
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag8 = this.s8;
						if (flag8)
						{
							int num3 = new Random().Next(1, 1);
							int num4 = num3;
							if (num4 == 1)
							{
								this.Spawnpoint = new Vector3(1545.918f, 3787.173f, 34.22439f);
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag9 = this.s9;
						if (flag9)
						{
							int num5 = new Random().Next(1, 3);
							int num6 = num5;
							if (num6 != 1)
							{
								if (num6 == 2)
								{
									this.Spawnpoint = new Vector3(1659.262f, 4853.022f, 41.86396f);
								}
							}
							else
							{
								this.Spawnpoint = new Vector3(1676.111f, 4860.118f, 42.08125f);
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag10 = this.s10;
						if (flag10)
						{
							int num7 = new Random().Next(1, 3);
							int num8 = num7;
							if (num8 != 1)
							{
								if (num8 == 2)
								{
									this.Spawnpoint = new Vector3(-276.7206f, 6244.22f, 31.42788f);
								}
							}
							else
							{
								this.Spawnpoint = new Vector3(-288.1684f, 6262.191f, 31.46955f);
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
						bool flag11 = this.s11;
						if (flag11)
						{
							int num9 = new Random().Next(1, 1);
							int num10 = num9;
							if (num10 == 1)
							{
								this.Spawnpoint = new Vector3(-22.46579f, 6498.323f, 31.50924f);
							}
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.h1 = true;
								this.h3 = false;
								break;
							case 2:
								this.h1 = false;
								this.h3 = false;
								break;
							case 3:
								this.h1 = false;
								this.h3 = true;
								break;
							}
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool scenariorunning = this.Scenariorunning;
						if (scenariorunning)
						{
							this.Suspect = new Ped(this.Drunklist[new Random().Next(this.Drunklist.Length)], this.Spawnpoint, 0f);
							this.Suspect.BlockPermanentEvents = true;
							this.Suspect.IsPersistent = true;
							this.Suspect.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist2[new Random().Next(this.Weaponlist2.Length)]), 500, true);
							StopThePedFunctions.SetPedAlcoholOverLimit(this.Suspect, true);
							this.Suspect.MovementAnimationSet = new AnimationSet?("move_m@drunk@moderatedrunk");
							this.Suspect.Accuracy = 15;
							this.Suspect.Tasks.Wander();
							this.SuspectAction();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag12 = Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 20f;
						if (flag12)
						{
							this._Blip.Delete();
							this._Blip2 = this.Suspect.AttachBlip();
							this._Blip2.Color = Color.Yellow;
							this._Blip2.Alpha = 0.5f;
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag13 = Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 7f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag13)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to talk to the suspect!", Settings.Interact), false);
							bool flag14 = Game.IsKeyDown(Settings.Interact);
							if (flag14)
							{
								switch (new Random().Next(1, 4))
								{
								case 1:
									Game.DisplaySubtitle("~b~Player: ~w~Excuse me, I want to talk to you!");
									break;
								case 2:
									Game.DisplaySubtitle("~b~Player: ~w~Excuse me, You can not walk around with a weapon like that");
									break;
								case 3:
									Game.DisplaySubtitle("~b~Player: ~w~Excuse me, Are you okay?");
									break;
								}
								this.Suspect.Tasks.Clear();
								this._Blip2.Delete();
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag15 = Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 7f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag15)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag16 = !this.h3 && Game.IsKeyDown(Settings.Interact);
							if (flag16)
							{
								switch (new Random().Next(1, 4))
								{
								case 1:
									Game.DisplaySubtitle("~y~Suspect: ~w~Leavv meee aloone!");
									break;
								case 2:
									Game.DisplaySubtitle("~y~Suspect: ~w~Gooo aawayyy!");
									break;
								case 3:
									Game.DisplaySubtitle("~y~Suspect: ~w~Miiiind youuuuur owwn busssiness!");
									break;
								}
								this.Suspect.Tasks.AimWeaponAt(Game.LocalPlayer.Character, -1);
								break;
							}
							bool flag17 = this.h3 && Game.IsKeyDown(Settings.Interact);
							if (flag17)
							{
								Game.DisplaySubtitle("~y~Suspect: ~w~Itss nooot myyy weappoon, I fouund ittt outside..");
								this.Suspect.Inventory.EquippedWeapon.DropToGround();
								GameFiber.Sleep(1000);
								Functions.SetPedAsStopped(this.Suspect, true);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag18 = !this.h3 && Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 7f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag18)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag19 = !this.h3 && Game.IsKeyDown(Settings.Interact);
							if (flag19)
							{
								Game.DisplaySubtitle("~b~Player: ~w~DROP THE WEAPON!");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag20 = Game.LocalPlayer.Character.DistanceTo(this.Suspect) <= 7f && Game.LocalPlayer.Character.IsOnFoot;
						if (flag20)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
							bool flag21 = Game.IsKeyDown(Settings.Interact);
							if (flag21)
							{
								bool flag22 = !this.h1;
								if (flag22)
								{
									switch (new Random().Next(1, 4))
									{
									case 1:
										Game.DisplaySubtitle("~y~Suspect: ~w~Takke itt theen.....");
										break;
									case 2:
										Game.DisplaySubtitle("~y~Suspect: ~w~Doont shuuut mee...");
										break;
									case 3:
										Game.DisplaySubtitle("~y~Suspect: ~w~.........");
										break;
									}
									GameFiber.Sleep(2000);
									this.Suspect.Inventory.EquippedWeapon.DropToGround();
									break;
								}
								bool flag23 = this.h1;
								if (flag23)
								{
									switch (new Random().Next(1, 4))
									{
									case 1:
										Game.DisplaySubtitle("~y~Suspect: ~w~Fuuuckk offff!");
										break;
									case 2:
										Game.DisplaySubtitle("~y~Suspect: ~w~Yooooou will loooook bettttter wiiith buullet huoles...");
										break;
									case 3:
										Game.DisplaySubtitle("~y~Suspect: ~w~Gooo toooo halll");
										break;
									}
									GameFiber.Sleep(1500);
									this.Suspect.Tasks.FireWeaponAt(Game.LocalPlayer.Character, -1, 1566631136);
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

		
		private void SuspectAction()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = this.Suspect.IsDead || Functions.IsPedArrested(this.Suspect) || Functions.IsPedGettingArrested(this.Suspect);
						if (flag)
						{
							GameFiber.Wait(500);
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

		
		private void Boydeath()
		{
			while (this.Scenariorunning)
			{
				GameFiber.Yield();
				bool flag = EntityExtensions.Exists(this.Boy) && this.Boy.IsDead;
				if (flag)
				{
					GameFiber.Wait(100);
					this.Bdeath = true;
					this.Suspect.Tasks.FireWeaponAt(this.Girl, -1, -957453492);
					this.Girldeath();
					break;
				}
			}
		}

		
		private void Girldeath()
		{
			while (this.Scenariorunning)
			{
				GameFiber.Yield();
				bool flag = EntityExtensions.Exists(this.Girl) && this.Girl.IsDead && this.Bdeath;
				if (flag)
				{
					GameFiber.Wait(1000);
					this.Suspect.Tasks.Clear();
					break;
				}
			}
		}

		
		public override void End()
		{
			this.Scenariorunning = false;
			Game.LogTrivial("ManiacCallouts - Suspect Brandishing Firearm Cleaned.");
			bool flag = EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				bool isDead = this.Suspect.IsDead;
				if (isDead)
				{
					Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_NEUTRALIZED MC_WE_ARE_CODE_4");
					Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspect Brandishing Firearm", "~b~Dispatch: ~w~All Units Suspect Is Neutralized");
				}
				else
				{
					bool flag2 = Functions.IsPedArrested(this.Suspect);
					if (flag2)
					{
						Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_ARRESTED MC_WE_ARE_CODE_4");
						Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspect Brandishing Firearm", "~b~Dispatch: ~w~All Units Suspect Is Arrested");
					}
					else
					{
						bool isAlive = this.Suspect.IsAlive;
						if (isAlive)
						{
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspect Brandishing Firearm", "~b~Dispatch: ~w~All Units ~g~Code 4");
						}
					}
				}
			}
			bool flag3 = !EntityExtensions.Exists(this.Suspect);
			if (flag3)
			{
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Suspect Brandishing Firearm", "~b~Dispatch: ~w~All Units ~g~Code 4");
			}
			bool flag4 = EntityExtensions.Exists(this.Suspect);
			if (flag4)
			{
				this.Suspect.Dismiss();
			}
			bool flag5 = EntityExtensions.Exists(this.Girl);
			if (flag5)
			{
				this.Girl.Dismiss();
			}
			bool flag6 = EntityExtensions.Exists(this.Boy);
			if (flag6)
			{
				this.Boy.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this._Blip);
			if (flag7)
			{
				this._Blip.Delete();
			}
			bool flag8 = EntityExtensions.Exists(this._Blip2);
			if (flag8)
			{
				this._Blip2.Delete();
			}
			base.End();
		}

		
		private Ped Girl;

		
		private Ped Boy;

		
		private Ped Suspect;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private static string[] drunkAnimation = new string[]
		{
			"move_m@drunk@verydrunk",
			"move_m@drunk@moderatedrunk_head_up",
			"move_m@drunk@moderatedrunk"
		};

		
		private const string Hostagefloor = "missprologueig_2";

		
		private const string Hostagefloor1 = "idle_on_floor_malehostage01";

		
		private const string Hostagefloor2 = "idle_on_floor_malehostage02";

		
		private Vector3 Callout;

		
		private Vector3 Searcharea;

		
		private Vector3 Spawnpoint;

		
		private Vector3 Spawnpoint2;

		
		private Vector3 Spawnpoint3;

		
		private string[] Weaponlist = new string[]
		{
			"weapon_assaultrifle",
			"weapon_carbinerifle"
		};

		
		private string[] Weaponlist2 = new string[]
		{
			"weapon_combatpistol",
			"weapon_heavypistol",
			"weapon_pistol",
			"weapon_pistol50",
			"weapon_revolver"
		};

		
		private string[] Shootlist;

		
		private string[] Girllist;

		
		private string[] Boylist;

		
		private string[] Drunklist;

		
		private string Suspecttalk;

		
		private bool PursuitCreated = false;

		
		private bool Scenariorunning = false;

		
		private bool Bdeath = false;

		
		private bool Sdeath = false;

		
		private bool Outside = false;

		
		private bool h1 = false;

		
		private bool h2 = false;

		
		private bool h3 = false;

		
		private bool s1 = false;

		
		private bool s2 = false;

		
		private bool s3 = false;

		
		private bool s4 = false;

		
		private bool s5 = false;

		
		private bool s6 = false;

		
		private bool s7 = false;

		
		private bool s8 = false;

		
		private bool s9 = false;

		
		private bool s10 = false;

		
		private bool s11 = false;

		
		private int Spawnpointhed;

		
		private int Spawnpoint2hed;

		
		private int counter = 0;
	}
}
