using System;
using System.Collections.Generic;
using System.Drawing;
using LSPD_First_Response.Mod.API;
using Rage;

namespace LeroyCalloutsV2
{
	
	internal class Common
	{
		
		public static int PickOutcome(int NumberOfPossibleOutcomes, int ViolentOutcome = -1)
		{
			bool flag = ViolentOutcome == -1;
			int outcome;
			if (flag)
			{
				outcome = Common.num.Next(1, NumberOfPossibleOutcomes + 1);
			}
			else
			{
				int violent = Common.num.Next(1, 101);
				Game.LogTrivial("violent: " + violent.ToString());
				bool flag2 = violent >= Main.probViolent;
				if (flag2)
				{
					for (outcome = ViolentOutcome; outcome == ViolentOutcome; outcome = Common.num.Next(1, NumberOfPossibleOutcomes + 1))
					{
						GameFiber.Yield();
					}
				}
				else
				{
					outcome = ViolentOutcome;
				}
			}
			return outcome;
		}

		
		public static string PickPed(bool suspect)
		{
			string modelString;
			if (suspect)
			{
				int index = Common.num.Next(0, Common.suspectModel.Count);
				modelString = Common.suspectModel[index];
			}
			else
			{
				int index2 = Common.num.Next(0, Common.witnessVictimModel.Count);
				modelString = Common.witnessVictimModel[index2];
			}
			return modelString;
		}

		
		public static string PickVehicle()
		{
			int index = Common.num.Next(0, Common.vehiclesSet.Count);
			return Common.vehiclesSet[index];
		}

		
		public static void WriteToLog(string text)
		{
			Game.LogTrivial("Leroy Callouts | " + Common.activeCall + ": " + text);
		}

		
		public static void WriteErrorToLog(string error)
		{
			Game.LogTrivial("Leroy Callouts (Error) | " + Common.activeCall + ": " + error);
		}

		
		public static Vector3 FindSpawnPoint(int SpawnList, bool beginCall = false, string scannerAudio = "", int responseCode = 2, int audioWaitTime = 0)
		{
			Vector3 result;
			try
			{
				Common.WriteToLog("MIN: " + Main.minSpawnDistance.ToString());
				Common.WriteToLog("MAX: " + Main.maxSpawnDistance.ToString());
				Vector3 spawnPoint = Vector3.Zero;
				int index = 0;
				bool spawnFound = false;
				int max = 1;
				List<Vector3> possibleSpawns = new List<Vector3>();
				bool flag = SpawnList == 0;
				if (flag)
				{
					possibleSpawns = Common.intersectionSpawns;
				}
				else
				{
					bool flag2 = SpawnList == 1;
					if (flag2)
					{
						possibleSpawns = Common.streetSpawns;
					}
				}
				while (!spawnFound && max <= 5)
				{
					GameFiber.Yield();
					index = Common.num.Next(0, possibleSpawns.Count);
					Game.LogTrivial("Spawn index to start: " + index.ToString());
					while (!spawnFound && index < possibleSpawns.Count)
					{
						GameFiber.Yield();
						spawnPoint = possibleSpawns[index];
						bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) < (float)Main.maxSpawnDistance && Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) > (float)Main.minSpawnDistance;
						if (flag3)
						{
							spawnFound = true;
						}
						index++;
					}
					max++;
				}
				bool flag4 = !spawnFound;
				if (flag4)
				{
					index = 0;
				}
				while (!spawnFound && index < possibleSpawns.Count)
				{
					GameFiber.Yield();
					spawnPoint = possibleSpawns[index];
					bool flag5 = Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) < (float)Main.maxSpawnDistance && Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) > (float)Main.minSpawnDistance;
					if (flag5)
					{
						spawnFound = true;
					}
					index++;
				}
				bool flag6 = !spawnFound;
				if (flag6)
				{
					spawnPoint = Vector3.Zero;
				}
				bool flag7 = spawnPoint != Vector3.Zero && beginCall;
				if (flag7)
				{
					Common.WriteToLog("Spawn Point: " + spawnPoint.ToString());
					Functions.PlayScannerAudioUsingPosition(scannerAudio, spawnPoint);
					GameFiber.StartNew(delegate()
					{
						GameFiber.Wait(audioWaitTime);
						Functions.PlayScannerAudio("UNITS_RESPOND_CODE_0" + responseCode.ToString());
						GameFiber.Hibernate();
					});
				}
				result = spawnPoint;
			}
			catch (Exception ex)
			{
				Common.WriteErrorToLog("Error encountered when piucking spawn point: " + ex.ToString());
				result = Vector3.Zero;
			}
			return result;
		}

		
		public static int FindSpawnPointIndex(List<Vector3> spawns, bool beginCall = false, string scannerAudio = "", int responseCode = 2, int audioWaitTime = 0)
		{
			Vector3 spawnPoint = Vector3.Zero;
			int index = 0;
			bool spawnFound = false;
			int max = 1;
			while (!spawnFound && max <= 5)
			{
				GameFiber.Yield();
				index = Common.num.Next(0, spawns.Count);
				Game.LogTrivial("Spawn index to start: " + index.ToString());
				while (!spawnFound && index < spawns.Count)
				{
					GameFiber.Yield();
					spawnPoint = spawns[index];
					bool flag = Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) < (float)Main.maxSpawnDistance && Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) > (float)Main.minSpawnDistance;
					if (flag)
					{
						spawnFound = true;
					}
					index++;
				}
				max++;
			}
			bool flag2 = !spawnFound;
			if (flag2)
			{
				index = 0;
			}
			while (!spawnFound && index < spawns.Count)
			{
				GameFiber.Yield();
				spawnPoint = spawns[index];
				bool flag3 = Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) < (float)Main.maxSpawnDistance && Vector3.Distance(Game.LocalPlayer.Character.Position, spawnPoint) > (float)Main.minSpawnDistance;
				if (flag3)
				{
					spawnFound = true;
				}
				index++;
			}
			bool flag4 = !spawnFound;
			if (flag4)
			{
				spawnPoint = Vector3.Zero;
			}
			bool flag5 = spawnPoint != Vector3.Zero && beginCall;
			if (flag5)
			{
				Common.WriteToLog("Spawn Point: " + spawnPoint.ToString());
				Functions.PlayScannerAudioUsingPosition(scannerAudio, spawnPoint);
				GameFiber.StartNew(delegate()
				{
					GameFiber.Wait(audioWaitTime);
					Functions.PlayScannerAudio("UNITS_RESPOND_CODE_0" + responseCode.ToString());
					GameFiber.Hibernate();
				});
			}
			else
			{
				index = -1;
			}
			return index - 1;
		}

		
		public static float GetHeading(Vector3 SpawnPoint, int SpawnList)
		{
			Common.WriteToLog("inside get heading");
			List<Vector3> possibleSpawns = new List<Vector3>();
			List<float> possibleHeadings = new List<float>();
			bool flag = SpawnList == 1;
			if (flag)
			{
				possibleSpawns = Common.streetSpawns;
				possibleHeadings = Common.streetHeadings;
			}
			Common.WriteToLog("getting index");
			int index = possibleSpawns.IndexOf(SpawnPoint);
			Common.WriteToLog("index: " + index.ToString());
			return possibleHeadings[index];
		}

		
		public static float GetHeading(Vector3 SpawnPoint, List<Vector3> SpawnList, List<float> HeadingList)
		{
			int index = SpawnList.IndexOf(SpawnPoint);
			return HeadingList[index];
		}

		
		public static Ped CreatePed(bool suspect, Vector3 location, bool persistent = true, bool blockEvents = true)
		{
			Random indexPicker = new Random();
			string modelString;
			if (suspect)
			{
				int index = indexPicker.Next(0, Common.suspectModel.Count);
				modelString = Common.suspectModel[index];
			}
			else
			{
				int index2 = indexPicker.Next(0, Common.witnessVictimModel.Count);
				modelString = Common.witnessVictimModel[index2];
			}
			return new Ped(modelString, location, 0f)
			{
				IsPersistent = persistent,
				BlockPermanentEvents = blockEvents
			};
		}

		
		public static Ped CreatePed(List<string> model, Vector3 location, bool persistent = true, bool blockEvents = true)
		{
			Random indexPicker = new Random();
			int index = indexPicker.Next(0, model.Count);
			string modelString = model[index];
			return new Ped(modelString, location, 0f)
			{
				IsPersistent = persistent,
				BlockPermanentEvents = blockEvents
			};
		}

		
		public static Blip CreateBlip(Entity entity, Color blipColor)
		{
			Blip blip = entity.AttachBlip();
			blip.Color = blipColor;
			return blip;
		}

		
		public static Vehicle CreateVehicle(Vector3 location, float heading = 0f, bool persistent = true)
		{
			Common.WriteToLog("inside create vehicle");
			Random indexPicker = new Random();
			int index = indexPicker.Next(0, Common.vehiclesSet.Count);
			string modelString = Common.vehiclesSet[index];
			return new Vehicle(modelString, location, heading)
			{
				IsPersistent = persistent
			};
		}

		
		public static Blip CreateSearchArea(Vector3 location, float radius = 100f)
		{
			return new Blip(location, radius)
			{
				Alpha = 0.5f,
				Color = Color.Yellow
			};
		}

		
		public static void Dismiss(Entity entity)
		{
			bool flag = EntityExtensions.Exists(entity);
			if (flag)
			{
				entity.Dismiss();
			}
		}

		
		public static void Dismiss(Blip blip)
		{
			bool flag = EntityExtensions.Exists(blip);
			if (flag)
			{
				blip.DisableRoute();
				blip.Delete();
			}
		}

		
		internal static Random num = new Random();

		
		internal static string activeCall;

		
		internal static readonly List<string> vehiclesSet = new List<string>
		{
			"felon",
			"sentinel",
			"dominator",
			"dukes",
			"gauntlet",
			"moonbeam",
			"blista"
		};

		
		internal static readonly List<string> suspectModel = new List<string>
		{
			"a_m_m_afriamer_01",
			"s_m_m_autoshop_01",
			"s_m_m_cntrybar_01",
			"a_m_m_tramp_01",
			"a_m_o_tramp_01",
			"a_m_m_tourist_01",
			"a_m_m_rurmeth_01"
		};

		
		internal static readonly List<string> witnessVictimModel = new List<string>
		{
			"a_f_o_ktown_01",
			"a_m_y_ktown_02",
			"a_m_y_vindouche_01",
			"csb_reporter",
			"csb_ramp_hipster",
			"cs_mrk",
			"u_m_m_markfost"
		};

		
		internal static readonly List<Vector3> streetSpawns = new List<Vector3>
		{
			new Vector3(-1000.928f, -2455.307f, 13.179f),
			new Vector3(1000.963f, 1715.509f, 163.1835f),
			new Vector3(1050.63f, -3070.016f, 5.420553f),
			new Vector3(-1061.324f, -2069.028f, 12.78395f),
			new Vector3(-1154.628f, -812.2039f, 14.29103f),
			new Vector3(1154.705f, -2948.636f, 5.415216f),
			new Vector3(-1302.813f, 463.6642f, 97.34183f),
			new Vector3(-1445.33f, 5040.437f, 61.24554f),
			new Vector3(1520.917f, 6454.964f, 22.43879f),
			new Vector3(-1544.867f, -406.6702f, 41.50912f),
			new Vector3(163.8865f, -1884.881f, 23.17613f),
			new Vector3(1667.6f, 4795.15f, 41.4133f),
			new Vector3(1703.975f, 4654.279f, 42.992f),
			new Vector3(1730.58f, 3598.678f, 34.62954f),
			new Vector3(178.7809f, -2994.456f, 5.291381f),
			new Vector3(1801.767f, 3777.018f, 33.10534f),
			new Vector3(-189.4946f, 6442.94f, 30.62612f),
			new Vector3(-1923.799f, 4606.467f, 56.57164f),
			new Vector3(1933.174f, 4584.796f, 38.39237f),
			new Vector3(1975.851f, 2583.741f, 53.6609f),
			new Vector3(2013.874f, 3745.713f, 31.82044f),
			new Vector3(2073.056f, 4704.323f, 40.55347f),
			new Vector3(2330.454f, 5060.937f, 45.16925f),
			new Vector3(236.7543f, -827.7445f, 29.47931f),
			new Vector3(2447.003f, 5606.9f, 44.42184f),
			new Vector3(2677.628f, 4991.98f, 44.28059f),
			new Vector3(269.0828f, 2100.905f, 109.2476f),
			new Vector3(279.3811f, -3274.807f, 5.30244f),
			new Vector3(-2798.789f, 2196.19f, 27.41297f),
			new Vector3(-289.2914f, -1208.71f, 24.19289f),
			new Vector3(299.1783f, 145.2535f, 103.3142f),
			new Vector3(-299.4868f, -628.0153f, 33.00266f),
			new Vector3(-3072.223f, 376.5182f, 6.428005f),
			new Vector3(-316.9319f, 6282.227f, 31.01233f),
			new Vector3(-339.8184f, -2678.489f, 5.562042f),
			new Vector3(34.20783f, -1106.248f, 28.8128f),
			new Vector3(35.63648f, 6580.458f, 30.86032f),
			new Vector3(381.2554f, -1348.166f, 31.21599f),
			new Vector3(-41.11774f, -710.766f, 32.18413f),
			new Vector3(417.1515f, -1527.896f, 28.80315f),
			new Vector3(451.4757f, 118.7918f, 98.65913f),
			new Vector3(-468.0027f, -1092.04f, 26.74269f),
			new Vector3(50.23553f, -237.8287f, 48.8968f),
			new Vector3(-591.756f, 504.2236f, 105.7013f),
			new Vector3(-6.626517f, 253.0064f, 108.2498f),
			new Vector3(-647.0429f, -297.7898f, 34.71304f),
			new Vector3(-668.5414f, -1235.216f, 9.985917f),
			new Vector3(-699.8693f, -647.6351f, 30.40603f),
			new Vector3(-752.6276f, -2303.126f, 12.37782f),
			new Vector3(753.0614f, -42.66079f, 80.99971f),
			new Vector3(-800.5866f, -1103.878f, 10.05277f),
			new Vector3(-809.0464f, -2435.809f, 14.08851f),
			new Vector3(-815.4012f, -10.00509f, 39.34498f),
			new Vector3(818.2114f, 944.5239f, 236.2711f),
			new Vector3(824.2974f, -3045.965f, 5.260636f),
			new Vector3(842.4502f, 6481.917f, 21.85966f),
			new Vector3(-858.5735f, 808.9385f, 192.829f),
			new Vector3(-877.2254f, -2288.619f, 6.228143f),
			new Vector3(-895.339f, -2698.143f, 13.15766f),
			new Vector3(314.9911f, -1161.767f, 28.80944f)
		};

		
		internal static readonly List<float> streetHeadings = new List<float>
		{
			153.0895f,
			260.8667f,
			268.6332f,
			223.7559f,
			130.6456f,
			88.87317f,
			92.89157f,
			306.6843f,
			68.90896f,
			141.0585f,
			65.39176f,
			184.1613f,
			18.01074f,
			120.7593f,
			180.35f,
			301.2817f,
			324.2602f,
			133.7644f,
			282.9901f,
			135.1347f,
			302.251f,
			135.3483f,
			313.348f,
			69.64305f,
			203.8008f,
			13.65027f,
			79.99559f,
			178.7504f,
			298.7212f,
			181.6464f,
			249.8432f,
			327.2701f,
			165.5293f,
			314.384f,
			134.5359f,
			152.7693f,
			135.0762f,
			49.31024f,
			1.601683f,
			210.2088f,
			72.90297f,
			71.89424f,
			341.2902f,
			278.9659f,
			267.1977f,
			29.92377f,
			30.37068f,
			90.06958f,
			226.2381f,
			238.93f,
			208.9359f,
			25.03617f,
			32.05265f,
			328.5115f,
			266.9547f,
			268.6854f,
			115.6189f,
			151.5488f,
			332.9562f,
			179.7231f
		};

		
		internal static readonly List<Vector3> intersectionSpawns = new List<Vector3>
		{
			new Vector3(1042.063f, -535.9856f, 61.06068f),
			new Vector3(-1059.66f, -1262.599f, 5.394829f),
			new Vector3(1087.307f, -3124.522f, 5.182301f),
			new Vector3(-1094.751f, 285.577f, 63.8633f),
			new Vector3(1178.555f, -771.6702f, 57.25331f),
			new Vector3(-1180.59f, 715.7101f, 150.6966f),
			new Vector3(1193.047f, -526.7989f, 64.24577f),
			new Vector3(-1219.825f, -1353.686f, 4.12768f),
			new Vector3(-1447.177f, 518.4992f, 117.7538f),
			new Vector3(148.4758f, 6551.541f, 31.03571f),
			new Vector3(-1651.449f, -549.8773f, 33.0401f),
			new Vector3(1659.273f, 3553.688f, 34.85352f),
			new Vector3(1721.078f, 4586.773f, 39.79048f),
			new Vector3(1725.454f, 4961.294f, 44.87141f),
			new Vector3(176.0557f, -2538.92f, 5.283868f),
			new Vector3(1792.169f, 3852.48f, 33.52646f),
			new Vector3(-1949.736f, 321.8828f, 88.93603f),
			new Vector3(1962.325f, 5119.923f, 42.47208f),
			new Vector3(1970.565f, 3898.153f, 31.48477f),
			new Vector3(-198.2075f, -866.2673f, 29.30798f),
			new Vector3(2066.518f, 3737.772f, 32.21827f),
			new Vector3(2174.044f, 4763.218f, 40.36083f),
			new Vector3(2224.329f, 3000.752f, 44.70772f),
			new Vector3(-243.4944f, -1800.513f, 29.59206f),
			new Vector3(-2452.002f, 3680.441f, 14.08397f),
			new Vector3(2560.96f, 4700.959f, 33.10579f),
			new Vector3(-257.9369f, -1465.241f, 31.10954f),
			new Vector3(2764.166f, 4384.1f, 48.48698f),
			new Vector3(283.8645f, 1690.565f, 238.0174f),
			new Vector3(305.3618f, 2561.316f, 43.16267f),
			new Vector3(-3111.304f, 1305.989f, 19.49105f),
			new Vector3(-321.4915f, 6229.827f, 30.79462f),
			new Vector3(-374.2693f, 2862.492f, 41.83407f),
			new Vector3(-384.568f, 6028.378f, 30.92303f),
			new Vector3(-435.8486f, -1406.11f, 29.27161f),
			new Vector3(-46.67176f, -559.8431f, 39.72552f),
			new Vector3(465.7649f, -1848.978f, 27.11371f),
			new Vector3(-515.3367f, -2136.054f, 8.466986f),
			new Vector3(-521.1123f, -819.5397f, 30.34321f),
			new Vector3(-551.9079f, -274.8358f, 35.19574f),
			new Vector3(591.0282f, 257.8021f, 102.3838f),
			new Vector3(606.2101f, 2166.803f, 67.72757f),
			new Vector3(61.13534f, -1887.656f, 20.94802f),
			new Vector3(-704.4505f, 491.8455f, 109.1851f),
			new Vector3(-707.3085f, -2392.609f, 14.04515f),
			new Vector3(-745.991f, -1120.876f, 9.914954f),
			new Vector3(77.03256f, -186.0745f, 55.03331f),
			new Vector3(77.501f, 6623.213f, 30.91093f),
			new Vector3(809.0839f, -1722.324f, 29.29169f),
			new Vector3(-816.0627f, -284.2921f, 37.50465f),
			new Vector3(818.28f, -1421.905f, 27.22743f),
			new Vector3(-868.7728f, -844.9095f, 19.35919f),
			new Vector3(875.6277f, -3074.934f, 5.185405f),
			new Vector3(88.38743f, -1508.683f, 29.33753f),
			new Vector3(896.0969f, -2468.812f, 28.58652f),
			new Vector3(-91.64217f, 6427.238f, 30.7369f),
			new Vector3(938.174f, 3547.422f, 33.26143f),
			new Vector3(942.5643f, -2098.207f, 30.62117f),
			new Vector3(-959.9331f, -2133.474f, 8.769274f),
			new Vector3(993.6952f, -673.63f, 57.32045f)
		};
	}
}
