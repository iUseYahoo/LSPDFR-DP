using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Media;
using System.Threading;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.API;
using Rage;

namespace YobbinCallouts
{
	
	internal class CallHandler
	{
		
		public static void Dialogue(List<string> dialogue, Ped animped = null, string animdict = "missfbi3_party_d", string animname = "stand_talk_loop_a_male1", float animspeed = -1f, AnimationFlags animflag = 1)
		{
			CallHandler.count = 0;
			while (CallHandler.count < dialogue.Count)
			{
				GameFiber.Yield();
				bool flag = Game.IsKeyDown(Config.MainInteractionKey);
				if (flag)
				{
					bool flag2 = animped != null && EntityExtensions.Exists(animped);
					if (flag2)
					{
						try
						{
							animped.Tasks.PlayAnimation(animdict, animname, animspeed, animflag);
						}
						catch (Exception)
						{
						}
					}
					Game.DisplaySubtitle(dialogue[CallHandler.count]);
					CallHandler.count++;
				}
			}
		}

		
		public static void IdleAction(Ped ped, bool iscop)
		{
			bool flag = ped != null && EntityExtensions.Exists(ped);
			if (flag)
			{
				if (iscop)
				{
					bool isFemale = ped.IsFemale;
					if (isFemale)
					{
						int animation = CallHandler.monke.Next(0, CallHandler.FemaleCopAnim.Length / 2);
						ped.Tasks.PlayAnimation(CallHandler.FemaleCopAnim[animation, 0], CallHandler.FemaleCopAnim[animation, 1], -1f, 1);
					}
					else
					{
						int animation2 = CallHandler.monke.Next(0, CallHandler.MaleCopAnim.Length / 2);
						ped.Tasks.PlayAnimation(CallHandler.MaleCopAnim[animation2, 0], CallHandler.MaleCopAnim[animation2, 1], -1f, 1);
					}
				}
				else
				{
					bool isFemale2 = ped.IsFemale;
					if (isFemale2)
					{
						int animation3 = CallHandler.monke.Next(0, CallHandler.FemaleRandoAnim.Length / 2);
						ped.Tasks.PlayAnimation(CallHandler.FemaleRandoAnim[animation3, 0], CallHandler.FemaleRandoAnim[animation3, 1], -1f, 1);
					}
					else
					{
						int animation4 = CallHandler.monke.Next(0, CallHandler.MaleRandoAnim.Length / 2);
						ped.Tasks.PlayAnimation(CallHandler.MaleRandoAnim[animation4, 0], CallHandler.MaleRandoAnim[animation4, 1], -1f, 1);
					}
				}
			}
		}

		
		public static Vehicle SpawnVehicle(Vector3 SpawnPoint, float Heading, bool persistent = true)
		{
			CallHandler.VehicleModels = new string[]
			{
				"asbo",
				"blista",
				"dilettante",
				"panto",
				"prairie",
				"cogcabrio",
				"exemplar",
				"f620",
				"felon",
				"felon2",
				"jackal",
				"oracle",
				"oracle2",
				"sentinel",
				"sentinel2",
				"zion",
				"zion2",
				"baller",
				"baller2",
				"baller3",
				"cavalcade",
				"fq2",
				"granger",
				"gresley",
				"habanero",
				"huntley",
				"mesa",
				"radi",
				"rebla",
				"rocoto",
				"seminole",
				"serrano",
				"xls",
				"asea",
				"asterope",
				"emporor",
				"fugitive",
				"ingot",
				"intruder",
				"premier",
				"primo",
				"primo2",
				"regina",
				"stanier",
				"stratum",
				"surge",
				"tailgater",
				"washington",
				"bestiagts",
				"blista2",
				"buffalo",
				"schafter2",
				"euros",
				"sadler",
				"bison",
				"bison2",
				"bison3",
				"burrito",
				"burrito2",
				"minivan",
				"minivan2",
				"paradise",
				"pony"
			};
			int model = CallHandler.monke.Next(0, CallHandler.VehicleModels.Length);
			Game.LogTrivial("YOBBINCALLOUTS: VEHICLESPAWNER: Vehicle Model is " + CallHandler.VehicleModels[model]);
			Vehicle veh = new Vehicle(CallHandler.VehicleModels[model], SpawnPoint, Heading);
			if (persistent)
			{
				veh.IsPersistent = true;
			}
			return veh;
		}

		
		public static void OpenDoor(Vector3 doorlocation, Ped resident = null, string residentmodel = "")
		{
			Game.DisplayHelp("Press ~y~" + Config.MainInteractionKey.ToString() + "~w~ to ~b~Ring~w~ the Doorbell.");
			while (!Game.IsKeyDown(Config.MainInteractionKey))
			{
				GameFiber.Wait(0);
			}
			CallHandler.Doorbell();
			GameFiber.Wait(2500);
			Game.LocalPlayer.HasControl = false;
			Game.FadeScreenOut(1500, true);
			bool flag = resident != null;
			if (flag)
			{
				bool flag2 = residentmodel != "";
				if (flag2)
				{
					resident = new Ped(residentmodel, doorlocation, Game.LocalPlayer.Character.Heading - 180f);
				}
				else
				{
					resident = new Ped(doorlocation, Game.LocalPlayer.Character.Heading - 180f);
				}
				resident.Heading = Game.LocalPlayer.Character.Heading - 180f;
				CallHandler.IdleAction(resident, false);
			}
			GameFiber.Wait(1500);
			Game.FadeScreenIn(1500, true);
			Game.LocalPlayer.HasControl = true;
		}

		
		public static void PlaySound(string SoundLocation)
		{
			try
			{
				Game.LogTrivial("YOBBINCALLOUTS: PLAYSOUNDHANDLER:" + SoundLocation + " - SOUND PLAY");
				SoundPlayer sound = new SoundPlayer();
				sound.SoundLocation = SoundLocation;
				GameFiber.StartNew(delegate()
				{
					try
					{
						sound.Load();
						sound.Play();
						GameFiber.Wait(4500);
						sound.Stop();
						CallHandler.SoundPlayed = true;
					}
					catch (FileNotFoundException)
					{
						Game.DisplayNotification("The ~b~Audio File~w~ for ~g~YobbinCallouts~w~ is ~r~not Installed Properly.~w~ Please ~b~Reinstall~w~ the Plugin Properly.");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
						Game.LogTrivial("AUDIO FILE FOR YOBBINCALLOUTS NOT INSTALLED. PLEASE REINSTALL THE PLUGIN PROPERLY.");
						Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
						CallHandler.SoundPlayed = false;
					}
				});
			}
			catch (ThreadAbortException)
			{
			}
			catch (FileNotFoundException)
			{
				Game.DisplayNotification("The ~b~Audio File~w~ for ~g~YobbinCallouts~w~ is ~r~not Installed Properly.~w~ Please ~b~Reinstall~w~ the Plugin Properly.");
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
				Game.LogTrivial("AUDIO FILE FOR YOBBINCALLOUTS NOT INSTALLED. PLEASE REINSTALL THE PLUGIN PROPERLY.");
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
				CallHandler.SoundPlayed = false;
			}
			catch (Exception e)
			{
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
				string error = e.ToString();
				Game.LogTrivial("ERROR: " + error);
				Game.LogTrivial("IN - YOBBINCALLOUTS SOUND PLAYER");
				Game.DisplayNotification("There was an ~r~Error~w~ Caught with ~b~YobbinCallouts. ~w~Please Check Your ~g~Log File.~w~ Sorry for the Inconvenience!");
				Game.LogTrivial("If You Believe this is a Bug, Please Report it on my Discord Server. Thanks!");
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT==========");
				CallHandler.SoundPlayed = false;
			}
		}

		
		public static void Doorbell()
		{
			int model = CallHandler.monke.Next(0, 3);
			bool flag = model == 0;
			if (flag)
			{
				CallHandler.PlaySound("lspdfr\\audio\\scanner\\YobbinCallouts Audio\\YC_DOORBELL1.wav");
			}
			else
			{
				bool flag2 = model == 2;
				if (flag2)
				{
					CallHandler.PlaySound("lspdfr\\audio\\scanner\\YobbinCallouts Audio\\YC_DOORBELL2.wav");
				}
				else
				{
					CallHandler.PlaySound("lspdfr\\audio\\scanner\\YobbinCallouts Audio\\YC_RINGDOORBELL.wav");
				}
			}
		}

		
		public static void SuspectWait(Ped Suspect)
		{
			Game.LogTrivial("YOBBINCALLOUTS: Waiting the active GameFiber until the suspect is killed or arrested.");
			while (EntityExtensions.Exists(Suspect))
			{
				GameFiber.Yield();
				bool flag = !EntityExtensions.Exists(Suspect) || Suspect.IsDead || Functions.IsPedArrested(Suspect);
				if (flag)
				{
					break;
				}
			}
			bool isAlive = Suspect.IsAlive;
			if (isAlive)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Suspect is alive and therefore under arrest.");
				Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest.");
			}
			else
			{
				Game.LogTrivial("YOBBINCALLOUTS: Suspect is dead.");
				GameFiber.Wait(1000);
				Game.DisplayNotification("Dispatch, Suspect is ~r~Dead.");
			}
			GameFiber.Wait(2000);
			Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
			GameFiber.Wait(2000);
		}

		
		public static Blip AssignBlip(Entity entity, Color blipcolor, float scale = 1f, string name = "", bool route = false, float intensity = 1f)
		{
			Blip result;
			try
			{
				bool flag = !EntityExtensions.Exists(entity);
				if (flag)
				{
					result = null;
				}
				else
				{
					Blip blip = entity.AttachBlip();
					bool flag2 = blipcolor == Color.Blue;
					if (flag2)
					{
						blip.IsFriendly = true;
					}
					else
					{
						bool flag3 = blipcolor == Color.Red;
						if (flag3)
						{
							blip.IsFriendly = false;
						}
						else
						{
							blip.Color = blipcolor;
						}
					}
					blip.Scale = scale;
					bool flag4 = name != "";
					if (flag4)
					{
						blip.Name = name;
					}
					else
					{
						bool flag5 = blipcolor == Color.Blue;
						if (flag5)
						{
							blip.Name = "Citizen";
						}
						else
						{
							bool flag6 = blipcolor == Color.Red;
							if (flag6)
							{
								blip.Name = "Suspect";
							}
						}
					}
					blip.IsRouteEnabled = route;
					blip.Alpha = intensity;
					result = blip;
				}
			}
			catch (Exception e)
			{
				string str = "YOBBINCALLOUTS: Error assigning blip. Error: ";
				Exception ex = e;
				Game.LogTrivial(str + ((ex != null) ? ex.ToString() : null));
				result = null;
			}
			return result;
		}

		
		public static void locationChooser(ArrayList list, float maxdistance = 600f, float mindistance = 25f)
		{
			ArrayList closeLocations = new ArrayList();
			for (int i = 1; i < list.Count; i++)
			{
				float distance = Vector3.Distance(Game.LocalPlayer.Character.Position, (Vector3)list[i]);
				bool flag = distance <= maxdistance && distance >= mindistance;
				if (flag)
				{
					closeLocations.Add(list[i]);
				}
			}
			bool flag2 = closeLocations.Count == 0;
			if (flag2)
			{
				Game.LogTrivial("YOBBINCALLOUTS: Spawn Point not found.");
				CallHandler.locationReturned = false;
			}
			else
			{
				CallHandler.SpawnPoint = (Vector3)closeLocations[CallHandler.monke.Next(0, closeLocations.Count)];
				CallHandler.locationReturned = true;
				Game.LogTrivial("YOBBINCALLOUTS: Spawn Point found successfully.");
				string str = "YOBBINCALLOUTS: Spawn Point found at ";
				Vector3 spawnPoint = CallHandler.SpawnPoint;
				Game.LogTrivial(str + spawnPoint.ToString() + " in " + Functions.GetZoneAtPosition(CallHandler.SpawnPoint).RealAreaName);
			}
		}

		
		public static bool FiftyFifty()
		{
			int num = CallHandler.monke.Next(0, 2);
			bool flag = num == 0;
			return !flag;
		}

		
		public static void VehicleInfo(Vehicle vehicle, Citizen ped)
		{
			bool flag = EntityExtensions.Exists(vehicle) && EntityExtensions.Exists(ped);
			if (flag)
			{
				Functions.SetVehicleOwnerName(vehicle, ped.FullName);
				string[] personaarray = new string[4];
				personaarray[0] = "~n~~w~Registered to: ~y~" + ped.Forename;
				string[] array = personaarray;
				int num = 1;
				string str = "~n~~w~Ped Info: ~y~";
				WantedInformation wantedInformation = ped.WantedInformation;
				array[num] = str + ((wantedInformation != null) ? wantedInformation.ToString() : null);
				personaarray[2] = "~n~~w~Plate: ~y~" + vehicle.LicensePlate;
				personaarray[3] = "~n~~w~Color: ~y~" + vehicle.PrimaryColor.Name;
				string persona = string.Concat(personaarray);
				Game.DisplayNotification("mpcarhud", "leaderboard_car_colour_icon", "~g~Vehicle Description", "~b~" + vehicle.Model.Name, persona);
			}
		}

		
		public static void CreatePursuit(LHandle pursuit, bool wait = true, bool audio = true, bool backup = false, params Ped[] suspects)
		{
			try
			{
				Functions.ForceEndCurrentPullover();
				pursuit = Functions.CreatePursuit();
				Functions.SetPursuitIsActiveForPlayer(pursuit, true);
				foreach (Ped Suspect in suspects)
				{
					Functions.AddPedToPursuit(pursuit, Suspect);
				}
				Game.LogTrivial("YOBBINCALLOUTS: PURSUITHANDLER: Started Pursuit with " + suspects.Length.ToString() + " Suspects.");
				if (audio)
				{
					GameFiber.Wait(1500);
					Functions.PlayScannerAudio("CRIME_SUSPECT_ON_THE_RUN_01");
					if (backup)
					{
						try
						{
							Functions.RequestBackup(Game.LocalPlayer.Character.Position, 1, 0);
						}
						catch
						{
						}
					}
				}
				bool flag = wait && suspects.Length == 1;
				if (flag)
				{
					Ped suspect = suspects[0];
					while (Functions.IsPursuitStillRunning(pursuit))
					{
						GameFiber.Wait(0);
					}
					while (EntityExtensions.Exists(suspect))
					{
						GameFiber.Yield();
						bool flag2 = !EntityExtensions.Exists(suspect) || suspect.IsDead || Functions.IsPedArrested(suspect);
						if (flag2)
						{
							break;
						}
					}
					bool isAlive = suspect.IsAlive;
					if (isAlive)
					{
						Game.DisplayNotification("Dispatch, a Suspect is Under ~g~Arrest~w~ Following the Pursuit.");
						Game.LogTrivial("YOBBINCALLOUTS: PURSUITHANDLER: Suspect is under arrest.");
						CallHandler.arrested = true;
					}
					else
					{
						GameFiber.Wait(1000);
						Game.DisplayNotification("Dispatch, a Suspect Was ~r~Killed~w~ Following the Pursuit.");
						Game.LogTrivial("YOBBINCALLOUTS: PURSUITHANDLER: Suspect is killed.");
						CallHandler.arrested = false;
					}
					GameFiber.Wait(2000);
					Functions.PlayScannerAudio("REPORT_RESPONSE_COPY_02");
					GameFiber.Wait(1500);
				}
				else
				{
					while (Functions.IsPursuitStillRunning(pursuit))
					{
						GameFiber.Wait(0);
					}
				}
				Game.LogTrivial("YOBBINCALLOUTS: PURSUITHANDLER: Pursuit over.");
				Game.LogTrivial("YOBBINCALLOUTS: PURSUITHANDLER: Suspect is arrested: " + CallHandler.arrested.ToString());
			}
			catch (Exception e)
			{
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - PURSUITHANDLER==========");
				string error = e.ToString();
				Game.LogTrivial("ERROR: " + error);
				Game.LogTrivial("No Need to Report This Error if it Did not Result in an LSPDFR Crash.");
				Game.LogTrivial("==========YOBBINCALLOUTS: ERROR CAUGHT - PURSUITHANDLER==========");
			}
		}

		
		
		static CallHandler()
		{
			string[,] array = new string[6, 2];
			array[0, 0] = "amb@world_human_cop_idles@female@base";
			array[0, 1] = "base";
			array[1, 0] = "amb@world_human_cop_idles@female@idle_a";
			array[1, 1] = "idle_a";
			array[2, 0] = "amb@world_human_cop_idles@female@idle_a";
			array[2, 1] = "idle_b";
			array[3, 0] = "amb@world_human_cop_idles@female@idle_a";
			array[3, 1] = "idle_c";
			array[4, 0] = "amb@world_human_cop_idles@female@idle_b";
			array[4, 1] = "idle_d";
			array[5, 0] = "amb@world_human_cop_idles@female@idle_b";
			array[5, 1] = "idle_e";
			CallHandler.FemaleCopAnim = array;
			string[,] array2 = new string[6, 2];
			array2[0, 0] = "amb@world_human_cop_idles@male@base";
			array2[0, 1] = "base";
			array2[1, 0] = "amb@world_human_cop_idles@male@idle_a";
			array2[1, 1] = "idle_a";
			array2[2, 0] = "amb@world_human_cop_idles@male@idle_a";
			array2[2, 1] = "idle_b";
			array2[3, 0] = "amb@world_human_cop_idles@male@idle_a";
			array2[3, 1] = "idle_c";
			array2[4, 0] = "amb@world_human_cop_idles@male@idle_b";
			array2[4, 1] = "idle_d";
			array2[5, 0] = "amb@world_human_cop_idles@male@idle_b";
			array2[5, 1] = "idle_e";
			CallHandler.MaleCopAnim = array2;
			string[,] array3 = new string[6, 2];
			array3[0, 0] = "amb@world_human_hang_out_street@female_arm_side@idle_a";
			array3[0, 1] = "idle_a";
			array3[1, 0] = "amb@world_human_hang_out_street@female_arm_side@idle_a";
			array3[1, 1] = "idle_b";
			array3[2, 0] = "amb@world_human_hang_out_street@female_arm_side@idle_a";
			array3[2, 1] = "idle_c";
			array3[3, 0] = "amb@world_human_hang_out_street@female_arms_crossed@idle_a";
			array3[3, 1] = "idle_a";
			array3[4, 0] = "amb@world_human_hang_out_street@female_arms_crossed@idle_a";
			array3[4, 1] = "idle_b";
			array3[5, 0] = "amb@world_human_hang_out_street@female_arms_crossed@idle_a";
			array3[5, 1] = "idle_c";
			CallHandler.FemaleRandoAnim = array3;
			string[,] array4 = new string[3, 2];
			array4[0, 0] = "amb@world_human_hang_out_street@male_a@base";
			array4[0, 1] = "base";
			array4[1, 0] = "amb@world_human_hang_out_street@male_b@base";
			array4[1, 1] = "base";
			array4[2, 0] = "amb@world_human_hang_out_street@male_c@base";
			array4[2, 1] = "base";
			CallHandler.MaleRandoAnim = array4;
			CallHandler.HouseList = new ArrayList
			{
				new Vector3(240.7677f, -1687.701f, 29.6996f),
				new Vector3(100.6926f, -1914.058f, 21.03957f),
				new Vector3(288.6435f, -1792.515f, 28.08904f),
				new Vector3(1250.818f, -1734.568f, 52.03207f),
				new Vector3(1354.907f, -1694.046f, 60.49123f),
				new Vector3(1362.024f, -1568.026f, 56.34648f),
				new Vector3(1221.362f, -668.7222f, 63.49313f),
				new Vector3(1010.55f, -418.9665f, 64.95395f),
				new Vector3(-1101.879f, -1536.912f, 4.579572f),
				new Vector3(-977.2473f, -1091.995f, 4.222562f),
				new Vector3(-1064.605f, -1057.521f, 6.411661f),
				new Vector3(-1031.352f, -903.0417f, 3.691091f),
				new Vector3(-1950.582f, -544.1102f, 14.7255f),
				new Vector3(-1901.605f, -585.9387f, 11.86937f),
				new Vector3(-1777.128f, -701.4404f, 10.52536f),
				new Vector3(-817.6935f, 177.9567f, 72.22254f),
				new Vector3(-896.5508f, -5.058554f, 43.79892f),
				new Vector3(-1106.531f, 421.4244f, 75.68616f),
				new Vector3(-933.4481f, 472.059f, 85.12269f),
				new Vector3(-678.85f, 512.1063f, 113.526f),
				new Vector3(-565.5161f, 525.6989f, 110.2012f),
				new Vector3(-972.2734f, 752.2137f, 176.3808f),
				new Vector3(-305.1205f, 431.0618f, 110.4823f),
				new Vector3(260.885f, 22.27959f, 88.12721f),
				new Vector3(1975.007f, 3816.095f, 33.42553f),
				new Vector3(1862.327f, 3853.849f, 36.27155f),
				new Vector3(1808.881f, 3907.963f, 33.73134f),
				new Vector3(1544.591f, 3721.3f, 34.62653f),
				new Vector3(1725.54f, 4642.25f, 43.87547f),
				new Vector3(1966.98f, 4634.148f, 41.1016f),
				new Vector3(-218.4349f, 6453.148f, 31.19829f),
				new Vector3(-365.8479f, 6341.065f, 29.84357f),
				new Vector3(-374.104f, 6190.625f, 31.72954f)
			};
			CallHandler.HospitalList = new ArrayList
			{
				new Vector3(361.0359f, -585.4946f, 28.8267f),
				new Vector3(356.689f, -597.6279f, 28.78184f),
				new Vector3(-449.401f, -347.7617f, 34.50174f),
				new Vector3(-447.8303f, -334.3066f, 34.50184f),
				new Vector3(295.7652f, -1447.524f, 29.966f),
				new Vector3(341.2158f, -1398.245f, 32.50923f),
				new Vector3(1838.992f, 3673.217f, 34.27671f),
				new Vector3(1815.018f, 3679.552f, 34.27674f),
				new Vector3(-247.249f, 6330.457f, 32.42619f),
				new Vector3(1152.5f, -1526.501f, 34.84344f),
				new Vector3(1161.176f, -1536.283f, 39.39494f)
			};
			CallHandler.StoreList = new ArrayList
			{
				new Vector3(-47.29313f, -1758.671f, 29.42101f),
				new Vector3(289f, -1267f, 29.44f),
				new Vector3(818f, -1039f, 26.75f),
				new Vector3(289f, -1267f, 29.44f),
				new Vector3(1211.76f, -1390f, 35.37f),
				new Vector3(1164.94f, -324.3139f, 69.22092f),
				new Vector3(-530f, -1220f, 18.45f),
				new Vector3(-711f, -917f, 19.21f),
				new Vector3(-2073f, -327f, 13.32f),
				new Vector3(527f, -151f, 57.46f),
				new Vector3(643f, 264.4f, 103.3f),
				new Vector3(1959.956f, 3740.31f, 32.34f),
				new Vector3(-1442f, -1993f, 13.164f),
				new Vector3(-93f, 6410.87f, 31.65f),
				new Vector3(1696.867f, 4923.803f, 42.06f),
				new Vector3(2557.269f, 380.7113f, 108.6229f),
				new Vector3(-3038f, 483.778f, 7.91f),
				new Vector3(-2545.63f, 2316.986f, 33.21579f)
			};
		}

		
		public static Vector3 SpawnPoint;

		
		public static bool locationReturned = true;

		
		private static int count;

		
		private static string[] VehicleModels;

		
		private static Random monke = new Random();

		
		public static bool arrested;

		
		public static bool SoundPlayed;

		
		private static string[,] FemaleCopAnim;

		
		private static string[,] MaleCopAnim;

		
		private static string[,] FemaleRandoAnim;

		
		private static string[,] MaleRandoAnim;

		
		public static ArrayList HouseList;

		
		public static ArrayList HospitalList;

		
		public static ArrayList StoreList;
	}
}
