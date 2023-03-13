using System;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.API;
using Rage;

namespace EmergencyCallouts.Essential
{
	
	internal static class Helper
	{
		
		
		internal static Ped MainPlayer
		{
			get
			{
				return Game.LocalPlayer.Character;
			}
		}

		
		
		
		internal static string CalloutArea { get; set; }

		
		
		
		internal static int CalloutScenario { get; set; }

		
		internal static Persona PlayerPersona = Functions.GetPersonaForPed(Helper.MainPlayer);

		
		internal static Random random = new Random();

		
		internal enum PedCategory
		{
			
			Suspect,
			
			Suspect2,
			
			Victim,
			
			Bystander,
			
			Guard,
			
			Officer,
			
			Paramedic,
			
			Firefighter
		}

		
		internal static class Entity
		{
			
			internal static string GetRandomMaleModel()
			{
				string[] array = new string[]
				{
					"a_m_m_afriamer_01",
					"ig_barry",
					"u_m_y_baygor",
					"a_m_o_beach_01",
					"a_m_y_beach_01",
					"a_m_y_ktown_02",
					"ig_ortega",
					"a_m_m_salton_04",
					"g_m_y_salvagoon_02",
					"a_m_y_stlat_01",
					"a_m_m_stlat_02",
					"u_m_m_aldinapoli",
					"a_m_y_beach_02",
					"a_m_y_beachvesp_01",
					"a_m_y_beachvesp_02",
					"u_m_m_bikehire_01",
					"a_m_y_ktown_01",
					"csb_oscar",
					"ig_paper",
					"a_m_m_salton_03",
					"g_m_y_salvagoon_03",
					"a_m_y_stbla_02",
					"s_m_y_ammucity_01",
					"a_m_y_bevhills_01",
					"a_m_m_bevhills_02",
					"a_m_y_bevhills_02",
					"ig_brad",
					"a_m_y_busicas_01",
					"ig_manuel",
					"u_m_y_paparazzi",
					"a_m_m_salton_02",
					"u_m_y_sbike",
					"a_m_y_stbla_01",
					"cs_carbuyer",
					"s_m_o_busker_01",
					"ig_car3guy2",
					"g_m_m_chigoon_01",
					"ig_jimmyboston",
					"a_m_m_business_01",
					"g_m_y_lost_01",
					"a_m_m_paparazzi_01",
					"a_m_y_salton_01",
					"a_m_m_skater_01",
					"a_m_m_trampbeac_01",
					"u_m_y_antonb",
					"g_m_m_chigoon_02",
					"csb_chin_goon",
					"u_m_y_chip",
					"ig_claypain",
					"a_m_y_business_01",
					"a_m_m_hillbilly_02",
					"a_m_m_mexcntry_01",
					"s_m_y_robber_01",
					"a_m_y_skater_01",
					"u_m_m_spyactor",
					"g_m_m_armboss_01",
					"s_m_m_cntrybar_01",
					"csb_customer",
					"a_m_y_cyclist_01",
					"a_m_y_business_02",
					"a_m_m_hasjew_01",
					"g_m_y_lost_03",
					"g_m_y_pologoon_01",
					"g_m_y_salvagoon_01",
					"a_m_y_soucent_04",
					"s_m_m_trucker_01",
					"g_m_m_armgoon_01",
					"s_m_y_dealer_01",
					"g_m_m_korboss_01",
					"s_m_m_lathandy_01",
					"a_m_y_business_03",
					"a_m_m_hillbilly_01",
					"g_m_m_mexboss_01",
					"csb_reporter",
					"a_m_y_skater_02",
					"a_m_m_soucent_04",
					"csb_undercover",
					"g_m_y_armgoon_02",
					"a_m_y_downtown_01",
					"a_m_m_eastsa_01",
					"s_m_m_linecook",
					"ig_clay",
					"ig_cletus",
					"a_m_y_hippy_01",
					"g_m_m_mexboss_02",
					"u_m_m_rivalpap",
					"a_m_m_skidrow_01",
					"a_m_y_soucent_03",
					"a_m_y_vinewood_01",
					"g_m_m_armlieut_01",
					"a_m_m_eastsa_02",
					"a_m_y_eastsa_02",
					"u_m_m_edtoh",
					"ig_fabien",
					"u_m_y_cyclist_01",
					"a_m_y_hipster_01",
					"u_m_y_party_01",
					"csb_roccopelosi",
					"a_m_m_socenlat_01",
					"ig_stretch",
					"a_m_y_vinewood_02",
					"s_m_m_autoshop_01",
					"g_m_y_famca_01",
					"g_m_y_famdnf_01",
					"g_m_y_famfor_01",
					"a_m_m_farmer_01",
					"mp_m_exarmy_01",
					"a_m_y_methhead_01",
					"u_m_m_partytarget",
					"g_m_y_salvaboss_01",
					"a_m_o_soucent_03",
					"s_m_y_winclean_01",
					"ig_money",
					"a_m_m_fatlatin_01",
					"ig_lazlow",
					"s_m_m_hairdress_01",
					"a_m_o_ktown_01",
					"s_m_y_dealer_01",
					"u_m_y_hippie_01",
					"a_m_m_og_boss_01",
					"a_m_y_runner_02",
					"ig_solomon",
					"a_m_m_soucent_03",
					"a_m_m_tourist_01",
					"g_m_y_azteca_01",
					"u_m_o_finguru_01",
					"csb_fos_rep",
					"ig_g",
					"a_m_y_cyclist_01",
					"u_m_y_fibmugger_01",
					"a_m_m_mexlabor_01",
					"ig_popov",
					"a_m_o_salton_01",
					"a_m_y_soucent_02",
					"ig_talina",
					"g_m_y_ballaeast_01",
					"csb_g",
					"a_m_m_genfat_01",
					"a_m_m_genfat_02",
					"a_m_y_genstreet_01",
					"u_m_m_filmdirector",
					"g_m_y_lost_02",
					"ig_oneil",
					"u_m_m_promourn_01",
					"a_m_o_soucent_01",
					"a_m_y_sunbathe_01",
					"g_m_y_ballaorig_01",
					"a_m_y_genstreet_02",
					"csb_fos_rep",
					"s_m_m_gentransport",
					"csb_imran",
					"u_m_y_mani",
					"a_m_y_polynesian_01",
					"a_m_m_salton_01",
					"a_m_o_soucent_02",
					"ig_tomepsilon",
					"ig_ballasog",
					"s_m_m_highsec_02",
					"a_m_y_hipster_02",
					"csb_hugh",
					"a_m_m_indian_01",
					"a_m_o_genstreet_01",
					"a_m_m_ktown_01",
					"ig_nigel",
					"a_m_m_prolhost_01",
					"a_m_y_soucent_01",
					"a_m_y_stwhi_02",
					"a_m_m_tramp_01",
					"g_m_y_ballasout_01",
					"u_m_m_jewelsec_01",
					"a_m_m_malibu_01",
					"g_m_y_mexgoon_02",
					"g_m_y_mexgoon_03",
					"a_m_y_hiker_01",
					"ig_mrk",
					"a_m_m_polynesian_01",
					"ig_russiandrunk",
					"a_m_y_stwhi_01",
					"ig_terry",
					"u_m_m_bankman",
					"u_m_m_jewelthief",
					"ig_josh",
					"ig_joeminuteman",
					"g_m_y_mexgoon_01",
					"g_m_y_mexgang_01",
					"g_m_y_korean_02",
					"g_m_y_pologoon_02",
					"a_m_m_rurmeth_01",
					"s_m_y_strvend_01",
					"u_m_y_tattoo_01",
					"ig_bankman",
					"g_m_y_korean_01",
					"g_m_y_korlieut_01",
					"a_m_y_mexthug_01",
					"s_m_m_migrant_01",
					"ig_milton",
					"cs_josef",
					"mp_g_m_pros_01",
					"u_m_y_proldriver_01",
					"s_m_m_strvend_01",
					"u_m_o_taphillbilly",
					"s_m_y_barman_01",
					"a_m_y_ktown_02",
					"a_m_y_latino_01",
					"u_m_y_militarybum",
					"csb_agent",
					"hc_hacker",
					"ig_hao",
					"s_m_m_movprem_01",
					"csb_porndudes",
					"g_m_y_strpunk_01",
					"g_m_y_strpunk_02",
					"u_m_o_tramp_01"
				};
				int num = Helper.random.Next(array.Length);
				return array[num];
			}

			
			internal static string GetRandomFemaleModel()
			{
				string[] array = new string[]
				{
					"ig_abigail",
					"csb_anita",
					"s_f_y_bartender_01",
					"ig_isldj_04_d_01",
					"ig_ashley",
					"a_f_y_bevhills_02",
					"u_f_y_bikerchic",
					"a_f_m_bevhills_01",
					"a_f_y_bevhills_01",
					"a_f_m_bevhills_02",
					"a_f_y_bevhills_03",
					"a_f_y_bevhills_04",
					"mp_f_boatstaff_01",
					"a_f_y_business_02",
					"a_f_m_business_02",
					"a_f_y_business_01",
					"a_f_y_business_04",
					"u_f_y_comjane",
					"a_f_m_eastsa_02",
					"csb_denise_friend",
					"cs_debra",
					"a_f_m_eastsa_01",
					"a_f_y_eastsa_02",
					"a_f_y_business_03",
					"a_f_y_eastsa_01",
					"a_f_y_epsilon_01",
					"a_f_m_fatwhite_01",
					"s_f_m_fembarber",
					"a_f_y_fitness_01",
					"a_f_y_fitness_02",
					"a_f_y_hipster_01",
					"a_f_y_genhot_01",
					"a_f_o_genstreet_01",
					"a_f_y_golfer_01",
					"cs_guadalope",
					"a_f_y_hipster_02",
					"s_f_y_hooker_03",
					"a_f_y_hipster_03",
					"a_f_y_hipster_04",
					"a_f_o_indian_01",
					"ig_janet",
					"u_f_y_jewelass_01",
					"a_f_y_indian_01",
					"ig_jewelass",
					"ig_kerrymcintosh",
					"a_f_o_ktown_01",
					"a_f_m_ktown_02",
					"ig_magenta",
					"u_f_o_moviestar",
					"u_f_y_mistress",
					"ig_molly",
					"ig_mrsphillips",
					"a_f_y_eastsa_03",
					"a_f_y_hiker_01",
					"a_f_m_trampbeac_01",
					"ig_mrs_thornhill",
					"ig_natalia",
					"ig_paige",
					"ig_patricia",
					"u_f_y_princess",
					"a_f_m_skidrow_01",
					"a_f_m_salton_01",
					"a_f_o_salton_01",
					"a_f_y_rurmeth_01",
					"a_f_y_runner_01",
					"a_f_y_scdressy_01",
					"ig_screen_writer",
					"s_f_m_shop_high",
					"s_f_y_shop_low",
					"s_f_y_shop_mid",
					"a_f_y_skater_01",
					"a_f_o_soucent_01",
					"a_f_y_soucent_01",
					"a_f_m_soucent_02",
					"a_f_o_soucent_02",
					"a_f_y_soucent_02",
					"a_f_y_soucent_03",
					"a_f_m_soucentmc_01",
					"u_f_y_spyactress",
					"s_f_m_sweatshop_01",
					"s_f_y_sweatshop_01",
					"a_f_y_tennis_01",
					"a_f_y_tourist_01",
					"a_f_y_tourist_02",
					"a_f_m_soucent_01",
					"g_f_y_vagos_01",
					"a_f_y_vinewood_01",
					"a_f_y_vinewood_02",
					"a_f_y_vinewood_03",
					"a_f_y_vinewood_04",
					"a_f_y_yoga_01",
					"a_f_y_femaleagent",
					"mp_f_chbar_01",
					"mp_f_counterfeit_01",
					"mp_f_execpa_01",
					"mp_f_execpa_02",
					"ig_jackie",
					"s_f_y_beachbarstaff_01",
					"ig_patricia_02"
				};
				int num = Helper.random.Next(array.Length);
				return array[num];
			}
		}

		
		internal static class Display
		{
			
			internal static void AcceptNotification(string details)
			{
				Game.DisplayNotification("dia_police", "dia_police", "Dispatch", "~y~Notification", details);
			}

			
			internal static void AcceptSubtitle(string calloutMessage, string calloutArea)
			{
				Game.DisplaySubtitle(string.Concat(new string[]
				{
					"Go to the ~r~",
					calloutMessage,
					"~s~ at ~y~",
					calloutArea,
					"~s~."
				}), 10000);
			}

			
			internal static void OutdatedReminder()
			{
				if (UpdateChecker.OnlineVersion != Project.LocalVersion && !Settings.EarlyAccess)
				{
					Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~r~v" + Project.LocalVersion + " ~c~by Faya", "Found update ~g~v" + UpdateChecker.OnlineVersion + " ~s~available for you!");
				}
			}

			
			internal static void EndNotification()
			{
				Game.DisplayNotification("dia_police", "dia_police", "Dispatch", "~y~Notification", "Situation is under control.");
			}

			
			internal static void HideSubtitle()
			{
				Game.DisplaySubtitle(string.Empty);
			}

			
			internal static void HintEndCallout()
			{
				Game.DisplayHelp(string.Format("You may end the callout with the ~y~{0}~s~ key.", Settings.EndCalloutKey));
			}
		}

		
		internal class Log
		{
			
			internal static void OnCalloutAccepted(string CalloutMessage)
			{
				Game.LogTrivial("[Emergency Callouts]: Created callout (" + CalloutMessage);
			}

			
			internal static void OnCalloutAccepted(string CalloutMessage, int ScenarioNumber)
			{
				Game.LogTrivial(string.Format("[Emergency Callouts]: Created callout ({0}, Scenario {1})", CalloutMessage, ScenarioNumber));
			}

			
			internal static void OnCalloutEnded(string CalloutMessage, int ScenarioNumber)
			{
				Game.LogTrivial(string.Format("[Emergency Callouts]: Ended callout ({0}, Scenario {1})", CalloutMessage, ScenarioNumber));
			}

			
			internal static void Exception(Exception e, string _class, string method)
			{
				Game.LogTrivial(string.Concat(new string[]
				{
					"[Emergency Callouts v",
					Project.LocalVersion,
					"]: ",
					e.Message,
					" At ",
					_class,
					".",
					method,
					"()"
				}));
				Game.DisplayNotification("commonmenu", "mp_alerttriangle", "Emergency Callouts", "~r~Issue detected!", "Please fill in a ~g~bug report form~s~.\nThat can be found on the ~y~Emergency Callouts Page~s~.");
				if (!Settings.EarlyAccess)
				{
					try
					{
						WebClient webClient = new WebClient();
						webClient.DownloadString("https:
						Game.LogTrivial("[Emergency Callouts]: Sent hit to the remote exception counter");
						if (_class == "SuspiciousActivity")
						{
							webClient.DownloadString("https:
						}
						else if (_class == "Burglary")
						{
							webClient.DownloadString("https:
						}
						else if (_class == "Trespassing")
						{
							webClient.DownloadString("https:
						}
						else if (_class == "DomesticViolence")
						{
							webClient.DownloadString("https:
						}
						else if (_class == "PublicIntoxication")
						{
							webClient.DownloadString("https:
						}
					}
					catch (WebException ex)
					{
						Game.LogTrivial("[Emergency Callouts]: v" + ex.Message);
					}
				}
			}

			
			internal static void Creation(Ped ped, Enum pedCategory)
			{
				Game.LogTrivial(string.Format("[Emergency Callouts]: Created {0} ({1}) at {2}", pedCategory, ped.Model.Name, ped.Position));
			}

			
			internal static void Creation(Vehicle vehicle, Enum pedCategory)
			{
				Game.LogTrivial(string.Format("[Emergency Callouts]: Created {0}Vehicle ({1}) at {2}", pedCategory, vehicle.Model.Name, vehicle.Position));
			}
		}

		
		internal static class Play
		{
			
			internal static void PursuitAudio()
			{
				Functions.PlayScannerAudio("OFFICERS_REPORT CRIME_RESIST_ARREST");
			}

			
			internal static void CodeFourAudio()
			{
				Functions.PlayScannerAudio("ACKNOWLEDGE CODE_FOUR NO_UNITS_REQUIRED");
			}
		}

		
		internal static class Vehicles
		{
			
			internal static string GetRandomSedan()
			{
				string[] array = new string[]
				{
					"FUGITIVE",
					"INTRUDER",
					"PREMIER",
					"TAILGATER",
					"TAILGATER2",
					"EMPEROR2",
					"GLENDALE",
					"WARRENER",
					"DUKES",
					"BUFFALO",
					"BUFFALO2",
					"ASEA",
					"DILETTANTE",
					"SULTAN",
					"ASTEROPE",
					"WASHINGTON",
					"HABANERO",
					"PRIMO"
				};
				int num = Helper.random.Next(array.Length);
				return array[num];
			}

			
			internal static string GetRandomMotorcycle()
			{
				string[] array = new string[]
				{
					"AKUMA",
					"AVARUS",
					"BAGGER",
					"BATI",
					"BATI2",
					"BF400",
					"CARBONRS",
					"CHIMERA",
					"CLIFFHANGER",
					"DAEMON",
					"DAEMON2",
					"DIABLOUS",
					"DIABLOUS2",
					"DOUBLE",
					"ENDURO",
					"ESSKEY",
					"FCR",
					"FCR2",
					"MANCHEZ",
					"NEMESIS",
					"PCJ",
					"RATBIKE",
					"RUFFIAN",
					"SANCHEZ",
					"SANCTUS",
					"SOVEREIGN",
					"THRUST",
					"VADER",
					"VINDICATOR",
					"WOLFSBANE",
					"ZOMBIEA",
					"ZOMBIEB",
					"SANCHEZ2",
					"DEFILER"
				};
				int num = Helper.random.Next(array.Length);
				return array[num];
			}

			
			internal static string GetRandomVan()
			{
				string[] array = new string[]
				{
					"SPEEDO",
					"BURRITO",
					"RUMPO",
					"RUMPO2",
					"RUMPO3",
					"BURRITO2",
					"BURRITO3",
					"BURRITO4",
					"PONY2",
					"SPEEDO4",
					"YOUGA"
				};
				int num = Helper.random.Next(array.Length);
				return array[num];
			}
		}

		
		internal static class Handle
		{
			
			internal static void ManualEnding()
			{
				if (Game.IsKeyDown(Keys.End))
				{
					Helper.Handle.AdvancedEndingSequence();
				}
			}

			
			internal static void AdvancedEndingSequence()
			{
				if (Helper.MainPlayer.IsOnFoot && !Helper.MainPlayer.IsAiming)
				{
					Functions.PlayPlayerRadioAction(Functions.GetPlayerRadioAction(), 3000);
				}
				GameFiber.Sleep(700);
				Game.DisplayNotification("~b~You~s~: Dispatch, no further assistance is needed.");
				GameFiber.Sleep(2700);
				Helper.Play.CodeFourAudio();
				GameFiber.Sleep(5000);
				Functions.StopCurrentCallout();
			}

			
			internal static void BlockPermanentEventsRadius(Vector3 location, float radius)
			{
				foreach (Ped ped in World.GetAllPeds())
				{
					if (EntityExtensions.Exists(ped) && ped.Position.DistanceTo(location) < radius)
					{
						ped.BlockPermanentEvents = true;
					}
				}
			}

			
			internal static void DeleteNearbyPeds(Ped mainPed, float radius)
			{
				foreach (Ped ped in World.GetAllPeds())
				{
					if (ped && ped.Position.DistanceTo(mainPed) <= radius && ped != mainPed && ped != Helper.MainPlayer)
					{
						ped.Delete();
					}
				}
			}

			
			internal static void DeleteNearbyPeds(Ped mainPed, Ped mainPed2, float radius)
			{
				foreach (Ped ped in World.GetAllPeds())
				{
					if (ped && ped.Position.DistanceTo(mainPed) <= radius && ped != mainPed && ped != mainPed2 && ped != Helper.MainPlayer)
					{
						ped.Delete();
					}
				}
			}

			
			internal static void DeleteNearbyTrailers(Vector3 position, float radius)
			{
				string[] source = new string[]
				{
					"ARMYTRAILER",
					"ARMYTRAILER2",
					"BALETRAILER",
					"BOATTRAILER",
					"DOCKTRAILER",
					"FREIGHTTRAILER",
					"GRAINTRAILER",
					"TRAILERLARGE",
					"TVTRAILER",
					"PROPTRAILER",
					"RAKETRAILER",
					"BOATTRAILER",
					"TRAILERLOGS",
					"TRAILERS",
					"TRAILERS2",
					"TRAILERS3",
					"TRAILERS4",
					"TRAILERSMALL",
					"TRAILERSMALL2"
				};
				foreach (Vehicle vehicle in World.GetAllVehicles())
				{
					if (vehicle.Position.DistanceTo(position) <= radius && source.Contains(vehicle.Model.Name) && EntityExtensions.Exists(vehicle))
					{
						vehicle.Delete();
					}
				}
			}

			
			internal static void PreventDistanceCrash(Vector3 CalloutPosition, bool PlayerArrived, bool PedFound)
			{
				if (Helper.MainPlayer.Position.DistanceTo(CalloutPosition) > 400f && PlayerArrived && PedFound)
				{
					Game.LogTrivial("[Emergency Callouts]: Too far from callout position, ending callout to prevent crash");
					Functions.StopCurrentCallout();
					Helper.Play.CodeFourAudio();
				}
			}

			
			internal static void PreventPickupCrash(Ped ped)
			{
				foreach (Vehicle vehicle in World.GetAllVehicles())
				{
					if (!ped.IsCollisionEnabled && ped.Position.DistanceTo(vehicle.GetOffsetPositionFront(-vehicle.Length + 1f)) <= 2f && vehicle.Model.Name == "AMBULANCE")
					{
						Helper.Play.CodeFourAudio();
						Functions.StopCurrentCallout();
					}
				}
				foreach (Ped ped2 in World.GetAllPeds())
				{
					if (ped2.Model.Name.ToLower() == "s_m_m_doctor_01" && ped.Position.DistanceTo(ped2.Position) <= 5f && ped2.IsDead)
					{
						Helper.Play.CodeFourAudio();
						Functions.StopCurrentCallout();
					}
				}
			}

			
			internal static void PreventPickupCrash(Ped ped, Ped ped2)
			{
				foreach (Vehicle vehicle in World.GetAllVehicles())
				{
					if (((!ped.IsCollisionEnabled && ped.Position.DistanceTo(vehicle.GetOffsetPositionFront(-vehicle.Length + 1f)) <= 2f) || (!ped2.IsCollisionEnabled && ped2.Position.DistanceTo(vehicle.GetOffsetPositionFront(-vehicle.Length + 1f)) <= 2f)) && vehicle.Model.Name == "AMBULANCE")
					{
						Helper.Play.CodeFourAudio();
						Functions.StopCurrentCallout();
					}
				}
				foreach (Ped ped3 in World.GetAllPeds())
				{
					if ((ped.IsDead || ped2.IsDead) && ped3.Model.Name.ToLower() == "s_m_m_doctor_01" && (ped.Position.DistanceTo(ped3.Position) <= 5f || ped2.Position.DistanceTo(ped3.Position) <= 5f))
					{
						Helper.Play.CodeFourAudio();
						Functions.StopCurrentCallout();
					}
				}
			}
		}
	}
}
