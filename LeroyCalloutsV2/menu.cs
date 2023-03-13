using System;
using System.Windows.Forms;
using LSPD_First_Response.Mod.API;
using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;

namespace LeroyCalloutsV2
{
	
	internal class menu
	{
		
		public static void Start()
		{
			menu._menuPool = new MenuPool();
			menu.mainMenu = new UIMenu("Leroy Callouts", "By Leroy100");
			menu.calloutsMenu = new UIMenu("Start Callout", "Leroy Callouts");
			menu._menuPool.Add(menu.mainMenu);
			menu._menuPool.Add(menu.calloutsMenu);
			menu.calloutsMenu.ParentMenu = menu.mainMenu;
			menu.mainMenu.AddItem(menu.calloutsMenuSelector = new UIMenuItem("Start a callout"));
			menu.mainMenu.AddItem(menu.ViolentProb = new UIMenuNumericScrollerItem<int>("Violent Probability", "Probability that a callout will become violent. Applied immediately.", 0, 100, 5));
			menu.mainMenu.AddItem(menu.MinDistance = new UIMenuNumericScrollerItem<int>("Minimum Callout Distance", "The minimum distance away a callout spawn point must be. Applied immediately.", 25, 10000, 200));
			menu.mainMenu.AddItem(menu.MaxDistance = new UIMenuNumericScrollerItem<int>("Maximum Callout Distance", "The maximum distance away a callout spawn point can be. Applied immediately.", 25, 10000, 200));
			menu.mainMenu.AddItem(menu.checkUpdates = new UIMenuCheckboxItem("Check for Updates", menu.CheckUpdatesDef, "Check for update on start. No affect until LSPDFR restarts."));
			menu.mainMenu.AddItem(menu.save = new UIMenuItem("Save to .ini", "Click this if you want to save all current settings to the .ini file."));
			bool panicButton = Main.PanicButton;
			if (panicButton)
			{
				menu.calloutsMenu.AddItem(menu.PanicButton = new UIMenuItem("Panic Button"));
			}
			bool carjacking = Main.Carjacking;
			if (carjacking)
			{
				menu.calloutsMenu.AddItem(menu.Carjacking = new UIMenuItem("Carjacking"));
			}
			bool stalking = Main.Stalking;
			if (stalking)
			{
				menu.calloutsMenu.AddItem(menu.Stalking = new UIMenuItem("Stalking"));
			}
			bool publicUrination = Main.PublicUrination;
			if (publicUrination)
			{
				menu.calloutsMenu.AddItem(menu.PublicUrination = new UIMenuItem("Public Urination"));
			}
			bool aggressivePanhandling = Main.AggressivePanhandling;
			if (aggressivePanhandling)
			{
				menu.calloutsMenu.AddItem(menu.AggressivePanhandling = new UIMenuItem("Aggressive Panhandling"));
			}
			bool customerRefusingToLeave = Main.CustomerRefusingToLeave;
			if (customerRefusingToLeave)
			{
				menu.calloutsMenu.AddItem(menu.CustomerRefusingToLeave = new UIMenuItem("Customer Refusing to Leave"));
			}
			bool personVandalizingVehicle = Main.PersonVandalizingVehicle;
			if (personVandalizingVehicle)
			{
				menu.calloutsMenu.AddItem(menu.PersonVandalizingVehicle = new UIMenuItem("Person Vandalizing Vehicle"));
			}
			bool elderlyCivilianLost = Main.ElderlyCivilianLost;
			if (elderlyCivilianLost)
			{
				menu.calloutsMenu.AddItem(menu.ElderlyCivilianLost = new UIMenuItem("Elderly Civilian Lost"));
			}
			bool underageWithFakeID = Main.UnderageWithFakeID;
			if (underageWithFakeID)
			{
				menu.calloutsMenu.AddItem(menu.UnderageWithFakeID = new UIMenuItem("Underage Person with Fake ID"));
			}
			bool homelessPersonOnPrivateProperty = Main.HomelessPersonOnPrivateProperty;
			if (homelessPersonOnPrivateProperty)
			{
				menu.calloutsMenu.AddItem(menu.HomelessPersonOnPrivateProperty = new UIMenuItem("Homeless Person on Private Property"));
			}
			bool securityGuardAttacked = Main.SecurityGuardAttacked;
			if (securityGuardAttacked)
			{
				menu.calloutsMenu.AddItem(menu.SecurityGuardAttacked = new UIMenuItem("Security Guard Attacked"));
			}
			bool disturbance = Main.Disturbance;
			if (disturbance)
			{
				menu.calloutsMenu.AddItem(menu.Disturbance = new UIMenuItem("Disturbance"));
			}
			bool fighting = Main.Fighting;
			if (fighting)
			{
				menu.calloutsMenu.AddItem(menu.Fighting = new UIMenuItem("Fighting"));
			}
			menu.mainMenu.OnItemSelect += new ItemSelectEvent(menu.optionSelected);
			menu.calloutsMenu.OnItemSelect += new ItemSelectEvent(menu.optionSelected);
			menu.ViolentProb.IndexChanged += new ItemScrollerEvent(menu.OnProbViolentChanged);
			menu.MinDistance.IndexChanged += new ItemScrollerEvent(menu.OnProbViolentChanged);
			menu.MaxDistance.IndexChanged += new ItemScrollerEvent(menu.OnProbViolentChanged);
			menu.mainMenu.MouseControlsEnabled = false;
			menu.mainMenu.AllowCameraMovement = true;
			menu.calloutsMenu.MouseControlsEnabled = false;
			menu.calloutsMenu.AllowCameraMovement = true;
			menu.ViolentProb.Value = Main.probViolent;
			menu.MinDistance.Value = Main.minSpawnDistance;
			menu.MaxDistance.Value = Main.maxSpawnDistance;
			menu.mainLogic();
			GameFiber.Hibernate();
		}

		
		public static void OnProbViolentChanged(UIMenuScrollerItem sender, int oldIndex, int newIndex)
		{
			bool flag = sender == menu.ViolentProb;
			if (flag)
			{
				Main.probViolent = menu.ViolentProb.Value;
			}
			bool flag2 = sender == menu.MinDistance;
			if (flag2)
			{
				Main.minSpawnDistance = menu.MinDistance.Value;
			}
			bool flag3 = sender == menu.MaxDistance;
			if (flag3)
			{
				Main.maxSpawnDistance = menu.MaxDistance.Value;
			}
		}

		
		public static void optionSelected(UIMenu sender, UIMenuItem selectedItem, int idex)
		{
			bool flag = sender == menu.mainMenu;
			if (flag)
			{
				bool flag2 = selectedItem == menu.calloutsMenuSelector;
				if (flag2)
				{
					sender.Visible = false;
					menu.calloutsMenu.Visible = true;
				}
				bool flag3 = selectedItem == menu.save;
				if (flag3)
				{
					string path = "Plugins/LSPDFR/LeroyCallouts.ini";
					InitializationFile ini = new InitializationFile(path);
					ini.Write("Other", "CheckForUpdatesOnStart", Main.checkForUpdatesOnStart);
					ini.Write("Other", "ViolentOutcomeProbability", Main.probViolent);
					ini.Write("Other", "MinimumCalloutDistance", Main.minSpawnDistance);
					ini.Write("Other", "MaximumCalloutDistance", Main.maxSpawnDistance);
				}
			}
			bool flag4 = sender == menu.calloutsMenu;
			if (flag4)
			{
				try
				{
					bool flag5 = selectedItem == menu.PanicButton;
					if (flag5)
					{
						Functions.StartCallout("LC - PanicButton");
					}
					bool flag6 = selectedItem == menu.Carjacking;
					if (flag6)
					{
						Functions.StartCallout("LC - Carjacking");
					}
					bool flag7 = selectedItem == menu.Stalking;
					if (flag7)
					{
						Functions.StartCallout("LC - Stalking");
					}
					bool flag8 = selectedItem == menu.PublicUrination;
					if (flag8)
					{
						Functions.StartCallout("LC - PublicUrination");
					}
					bool flag9 = selectedItem == menu.AggressivePanhandling;
					if (flag9)
					{
						Functions.StartCallout("LC - AggressivePanhandling");
					}
					bool flag10 = selectedItem == menu.CustomerRefusingToLeave;
					if (flag10)
					{
						Functions.StartCallout("LC - CustomerRefusingToLeave");
					}
					bool flag11 = selectedItem == menu.PersonVandalizingVehicle;
					if (flag11)
					{
						Functions.StartCallout("LC - PersonVandalizingVehicle");
					}
					bool flag12 = selectedItem == menu.ElderlyCivilianLost;
					if (flag12)
					{
						Functions.StartCallout("LC - ElderlyCivilianLost");
					}
					bool flag13 = selectedItem == menu.UnderageWithFakeID;
					if (flag13)
					{
						Functions.StartCallout("LC - UnderageWithFakeID");
					}
					bool flag14 = selectedItem == menu.HomelessPersonOnPrivateProperty;
					if (flag14)
					{
						Functions.StartCallout("LC - HomelessPersonOnPrivateProperty");
					}
					bool flag15 = selectedItem == menu.SecurityGuardAttacked;
					if (flag15)
					{
						Functions.StartCallout("LC - SecurityGuardAttacked");
					}
					bool flag16 = selectedItem == menu.Disturbance;
					if (flag16)
					{
						Functions.StartCallout("LC - Disturbance");
					}
					bool flag17 = selectedItem == menu.Fighting;
					if (flag17)
					{
						Functions.StartCallout("LC - Fighting");
					}
				}
				catch (Exception ex)
				{
					Common.WriteErrorToLog("Error starting callout from menu: " + ex.ToString());
				}
			}
			bool flag18 = selectedItem == menu.checkUpdates;
			if (flag18)
			{
				Main.checkForUpdatesOnStart = !menu.checkUpdates.Checked;
			}
		}

		
		public static void mainLogic()
		{
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					GameFiber.Yield();
					bool flag = Game.IsKeyDown(menu.openMenuKey);
					if (flag)
					{
						menu.mainMenu.Visible = !menu.mainMenu.Visible;
					}
					menu._menuPool.ProcessMenus();
				}
			});
		}

		
		private static Keys openMenuKey = Main.MenuKey;

		
		internal static bool CheckUpdatesDef = Main.checkForUpdatesOnStart;

		
		internal static int ViolentProbDef = Main.probViolent;

		
		internal static int MaxDistanceDef = Main.maxSpawnDistance;

		
		internal static int MinDistanceDef = Main.minSpawnDistance;

		
		private static UIMenu mainMenu;

		
		private static UIMenu calloutsMenu;

		
		private static MenuPool _menuPool;

		
		private static UIMenuItem calloutsMenuSelector;

		
		private static UIMenuItem PanicButton;

		
		private static UIMenuItem Carjacking;

		
		private static UIMenuItem Stalking;

		
		private static UIMenuItem PublicUrination;

		
		private static UIMenuItem AggressivePanhandling;

		
		private static UIMenuItem CustomerRefusingToLeave;

		
		private static UIMenuItem PersonVandalizingVehicle;

		
		private static UIMenuItem ElderlyCivilianLost;

		
		private static UIMenuItem UnderageWithFakeID;

		
		private static UIMenuItem HomelessPersonOnPrivateProperty;

		
		private static UIMenuItem SecurityGuardAttacked;

		
		private static UIMenuItem Disturbance;

		
		private static UIMenuItem Fighting;

		
		private static UIMenuCheckboxItem checkUpdates;

		
		private static UIMenuNumericScrollerItem<int> ViolentProb;

		
		private static UIMenuNumericScrollerItem<int> MaxDistance;

		
		private static UIMenuNumericScrollerItem<int> MinDistance;

		
		private static UIMenuItem save;
	}
}
