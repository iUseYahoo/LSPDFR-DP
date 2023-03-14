using System;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading;
using LSPD_First_Response.Mod.API;
using Rage;
using YobbinCallouts.Callouts;

namespace YobbinCallouts
{
	
	public class Main : Plugin
	{
		
		public override void Initialize()
		{
			Functions.OnOnDutyStateChanged += new Functions.OnDutyStateChangedEventHandler(Main.OnOnDutyStateChangedHandler);
			string str = "YOBBINCALLOUTS: YobbinCallouts ";
			Version version = Main.curVersion;
			Game.LogTrivial(str + ((version != null) ? version.ToString() : null) + " by YobB1n has been loaded.");
		}

		
		public override void Finally()
		{
			Game.LogTrivial("YOBBINCALLOUTS: YobbinCallouts has been cleaned up.");
		}

		
		private static void OnOnDutyStateChangedHandler(bool OnDuty)
		{
			if (OnDuty)
			{
				string text = "3dtextures";
				string text2 = "mpgroundlogo_cops";
				string text3 = "Yobbin Callouts";
				string str = "~y~v.";
				Version version = Main.curVersion;
				int num = (int)Game.DisplayNotification(text, text2, text3, str + ((version != null) ? version.ToString() : null) + " ~b~by YobB1n", " ~g~Loaded Successfully. ~b~Enjoy!");
				GameFiber.StartNew(delegate()
				{
					Game.LogTrivial("YOBBINCALLOUTS: Player Went on Duty. Checking for Updates.");
					try
					{
						Thread FetchVersionThread = new Thread(delegate()
						{
							using (WebClient client = new WebClient())
							{
								try
								{
									string s = client.DownloadString("http://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=29467&textOnly=1");
									Main.NewVersion = new Version(s);
								}
								catch (Exception)
								{
									Game.LogTrivial("YOBBINCALLOUTS: LSPDFR Update API down. Aborting checks.");
								}
							}
						});
						FetchVersionThread.Start();
						try
						{
							while (FetchVersionThread.ThreadState != ThreadState.Stopped)
							{
								GameFiber.Yield();
							}
							bool flag = Main.curVersion.CompareTo(Main.NewVersion) < 0;
							if (flag)
							{
								Game.LogTrivial("YOBBINCALLOUTS: Finished Checking Yobbin Callouts for Updates.");
								string str2 = "YOBBINCALLOUTS: Update Available for Yobbin Callouts. Installed Version ";
								Version version2 = Main.curVersion;
								string str3 = (version2 != null) ? version2.ToString() : null;
								string str4 = " ,New Version ";
								Version newVersion = Main.NewVersion;
								Game.LogTrivial(str2 + str3 + str4 + ((newVersion != null) ? newVersion.ToString() : null));
								Game.DisplayNotification("~g~Update Available~w~ for ~b~YobbinCallouts! Download at ~y~lcpdfr.com.");
								Game.DisplayNotification("It is ~y~Strongly Recommended~w~ to~g~ Update~b~ YobbinCallouts. ~w~Playing on an Old Version ~r~May Cause Issues!");
								Game.LogTrivial("====================YOBBINCALLOUTS WARNING====================");
								Game.LogTrivial("Outdated YobbinCallouts Version. Please Update if You Experience Issues!!");
								Game.LogTrivial("I'm not a Dick so I don't use a killswitch, however I strongly encourage you to update for the best experience.");
								Game.LogTrivial("====================YOBBINCALLOUTS WARNING====================");
								Main.UpToDate = false;
							}
							else
							{
								bool flag2 = Main.curVersion.CompareTo(Main.NewVersion) > 0;
								if (flag2)
								{
									Game.LogTrivial("YOBBINCALLOUTS: DETECTED BETA RELEASE. DO NOT REDISTRIBUTE. PLEASE REPORT ALL ISSUES.");
									Game.DisplayNotification("YOBBINCALLOUTS: ~r~DETECTED BETA RELEASE. ~w~DO NOT REDISTRIBUTE. PLEASE REPORT ALL ISSUES.");
									Main.UpToDate = true;
									Main.Beta = true;
								}
								else
								{
									Game.LogTrivial("YOBBINCALLOUTS: Finished Checking Yobbin Callouts for Updates.");
									Game.DisplayNotification("You are on the ~g~Latest Version~w~ of ~b~YobbinCallouts.");
									Game.LogTrivial("YOBBINCALLOUTS: Yobbin Callouts is Up to Date.");
									Main.UpToDate = true;
								}
							}
						}
						catch (Exception)
						{
							Game.LogTrivial("YOBBINCALLOUTS: Error while Processing Thread to Check for Updates.");
						}
					}
					catch (Exception)
					{
						Game.LogTrivial("YOBBINCALLOUTS: Error while checking Yobbin Callouts for updates.");
					}
				});
				Main.RegisterCallouts();
			}
		}

		
		private static void RegisterCallouts()
		{
			Game.LogTrivial("==========YOBBINCALLOUTS INFORMATION==========");
			Game.LogTrivial("YobbinCallouts by YobB1n");
			string str = "Version ";
			Version version = Main.curVersion;
			Game.LogTrivial(str + ((version != null) ? version.ToString() : null));
			bool flag = Config.INIFile.Exists();
			if (flag)
			{
				Game.LogTrivial("YobbinCallouts Config is Installed by User.");
			}
			else
			{
				Game.LogTrivial("YobbinCallouts Config is NOT Installed by User.");
			}
			bool flag2 = Functions.GetAllUserPlugins().ToList<Assembly>().Any((Assembly a) => a != null && a.FullName.Contains("StopThePed"));
			if (flag2)
			{
				Game.LogTrivial("StopThePed is Installed by User.");
				Main.STP = true;
			}
			else
			{
				Game.LogTrivial("StopThePed is NOT Installed by User.");
				Main.STP = false;
			}
			bool flag3 = Functions.GetAllUserPlugins().ToList<Assembly>().Any((Assembly a) => a != null && a.FullName.Contains("CalloutInterface"));
			if (flag3)
			{
				Game.LogTrivial("CalloutInterface is Installed by User.");
				Main.CalloutInterface = true;
			}
			else
			{
				Game.LogTrivial("CalloutInterface is NOT Installed by User.");
				Main.CalloutInterface = false;
			}
			Game.LogTrivial("Please Join My Discord Server to Report Bugs/Improvements: https:
			Game.LogTrivial("Started Registering Callouts.");
			bool flag4 = Config.BrokenDownVehicle || !Config.INIFile.Exists();
			if (flag4)
			{
				Functions.RegisterCallout(typeof(BrokenDownVehicle));
			}
			bool flag5 = Config.AssaultOnBus || !Config.INIFile.Exists();
			if (flag5)
			{
				Functions.RegisterCallout(typeof(AssaultOnBus));
			}
			bool flag6 = Config.TrafficBreak || !Config.INIFile.Exists();
			if (flag6)
			{
				Functions.RegisterCallout(typeof(TrafficBreak));
			}
			bool flag7 = Config.PhotographyOfPrivateProperty || !Config.INIFile.Exists();
			if (flag7)
			{
				Functions.RegisterCallout(typeof(PhotographyOfPrivateProperty));
			}
			bool flag8 = Config.PropertyCheck || !Config.INIFile.Exists();
			if (flag8)
			{
				Functions.RegisterCallout(typeof(PropertyCheck));
			}
			bool flag9 = Config.StolenPoliceHardware || !Config.INIFile.Exists();
			if (flag9)
			{
				Functions.RegisterCallout(typeof(StolenPoliceHardware));
			}
			bool flag10 = Config.Arson || !Config.INIFile.Exists();
			if (flag10)
			{
				Functions.RegisterCallout(typeof(Arson));
			}
			bool flag11 = Config.BarFight || !Config.INIFile.Exists();
			if (flag11)
			{
				Functions.RegisterCallout(typeof(BarFight));
			}
			bool flag12 = Config.BaitCar || !Config.INIFile.Exists();
			if (flag12)
			{
				Functions.RegisterCallout(typeof(BaitCar));
			}
			bool flag13 = Config.RoadRage || !Config.INIFile.Exists();
			if (flag13)
			{
				Functions.RegisterCallout(typeof(RoadRage));
			}
			bool flag14 = Config.StolenCellPhone || !Config.INIFile.Exists();
			if (flag14)
			{
				Functions.RegisterCallout(typeof(StolenCellPhone));
			}
			bool flag15 = Config.SovereignCitizen || !Config.INIFile.Exists();
			if (flag15)
			{
				Functions.RegisterCallout(typeof(SovereignCitizen));
			}
			bool flag16 = Config.ActiveShooter || !Config.INIFile.Exists();
			if (flag16)
			{
				Functions.RegisterCallout(typeof(ActiveShooter));
			}
			bool flag17 = Config.HumanTrafficking || !Config.INIFile.Exists();
			if (flag17)
			{
				Functions.RegisterCallout(typeof(HumanTrafficking));
			}
			bool flag18 = Config.WeaponFound || !Config.INIFile.Exists();
			if (flag18)
			{
				Functions.RegisterCallout(typeof(WeaponFound));
			}
			bool flag19 = Config.HospitalEmergency || !Config.INIFile.Exists();
			if (flag19)
			{
				Functions.RegisterCallout(typeof(HospitalEmergency));
			}
			bool flag20 = Config.LandlordTenantDispute || !Config.INIFile.Exists();
			if (flag20)
			{
				Functions.RegisterCallout(typeof(LandlordTenantDispute));
			}
			Game.LogTrivial("Finished Registering Callouts.");
			bool beta = Main.Beta;
			if (beta)
			{
				Game.LogTrivial("Started Registering Beta Callouts.");
				bool flag21 = Config.CitizenArrest || !Config.INIFile.Exists();
				if (flag21)
				{
					Functions.RegisterCallout(typeof(CitizenArrest));
				}
				bool flag22 = Config.DUIReported || !Config.INIFile.Exists();
				if (flag22)
				{
					Functions.RegisterCallout(typeof(DUIReported));
				}
				bool flag23 = Config.StolenMail || !Config.INIFile.Exists();
				if (flag23)
				{
					Functions.RegisterCallout(typeof(StolenMail));
				}
				Game.LogTrivial("Finished Registering Beta Callouts.");
			}
			bool runInvestigations = Config.RunInvestigations;
			if (runInvestigations)
			{
				Game.LogTrivial("Started Registering Investigations.");
				Game.LogTrivial("More to come soon!");
				Game.LogTrivial("Finished Registering Investigations.");
			}
			Game.LogTrivial("==========YOBBINCALLOUTS INFORMATION==========");
		}

		
		public static Version NewVersion = new Version();

		
		public static Version curVersion = new Version("1.7.0");

		
		public static bool STP;

		
		public static bool CalloutInterface;

		
		public static bool UpToDate;

		
		public static bool Beta = false;
	}
}
