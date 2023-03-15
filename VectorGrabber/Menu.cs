using System;
using System.Drawing;
using System.Threading;
using Rage;
using RAGENativeUI;
using RAGENativeUI.Elements;
using RAGENativeUI.PauseMenu;

namespace VectorGrabber
{
	
	internal class Menu
	{
		
		internal static void CreateMainMenu()
		{
			Menu.pool = new MenuPool();
			Menu.mainMenu = new UIMenu("VectorGrabber", "Main Menu");
			Menu.mainMenu.AddItem(Menu.EnableBlips);
			Menu.mainMenu.AddItem(Menu.RereadFile);
			Menu.mainMenu.AddItem(Menu.CopyClipboard);
			Menu.mainMenu.AddItem(Menu.AddLocation);
			Menu.mainMenu.AllowCameraMovement = true;
			Menu.mainMenu.MouseControlsEnabled = false;
			Menu.mainMenu.OnItemSelect += new ItemSelectEvent(Menu.mainMenuItemSelect);
			Menu.EnableBlips.CheckboxEvent += new ItemCheckboxEvent(Menu.OnBlipCheckboxEvent);
			Menu.pool.Add(Menu.mainMenu);
			Locations.setupLocationMenu();
			GameFiber.StartNew(new ThreadStart(Menu.ProcessMenus));
		}

		
		internal static void mainMenuItemSelect(UIMenu sender, UIMenuItem selectedItem, int index)
		{
			bool flag = selectedItem.Equals(Menu.RereadFile);
			if (flag)
			{
				EntryPoint.RereadFile();
			}
			else
			{
				bool flag2 = selectedItem.Equals(Menu.CopyClipboard);
				if (flag2)
				{
					EntryPoint.CopyCurrCoordToClipboard();
				}
				else
				{
					bool flag3 = selectedItem.Equals(Menu.AddLocation);
					if (flag3)
					{
						EntryPoint.AddLocation();
					}
				}
			}
		}

		
		internal static void OnBlipCheckboxEvent(UIMenuCheckboxItem sender, bool IsChecked)
		{
			if (IsChecked)
			{
				Menu.AddBlips();
				Settings.EnableVectorBlips = true;
			}
			else
			{
				Menu.DeleteBlips();
				Settings.EnableVectorBlips = false;
			}
		}

		
		internal static void AddBlips()
		{
			foreach (SavedLocation s in EntryPoint.VectorsRead)
			{
				Blip newBlip = new Blip(new Vector3(s.X, s.Y, s.Z));
				newBlip.Color = Color.Green;
				newBlip.Name = s.Title;
				EntryPoint.Blips.Add(newBlip);
			}
		}

		
		internal static void AddBlip(SavedLocation s)
		{
			float x = s.X;
			float y = s.Y;
			float z = s.Z;
			Blip newBlip = new Blip(new Vector3(s.X, s.Y, s.Z));
			newBlip.Color = Color.Green;
			newBlip.Name = s.Title;
			EntryPoint.Blips.Add(newBlip);
		}

		
		internal static void DeleteBlips()
		{
			foreach (Blip blip in EntryPoint.Blips)
			{
				bool flag = EntityExtensions.Exists(blip);
				if (flag)
				{
					blip.Delete();
				}
			}
		}

		
		private static void ProcessMenus()
		{
			for (;;)
			{
				GameFiber.Yield();
				Menu.pool.ProcessMenus();
				bool flag = Game.IsKeyDown(Settings.MenuKey) && Game.IsControlKeyDownRightNow && !UIMenu.IsAnyMenuVisible && !TabView.IsAnyPauseMenuVisible;
				if (flag)
				{
					Menu.mainMenu.Visible = true;
				}
			}
		}

		
		internal static MenuPool pool;

		
		internal static UIMenu mainMenu;

		
		internal static UIMenuItem RereadFile = new UIMenuItem("Reread file", "Rereads file and updates menu");

		
		internal static UIMenuCheckboxItem EnableBlips = new UIMenuCheckboxItem("Enable Blips", Settings.EnableVectorBlips, "Enables blips for all saved vectors");

		
		internal static UIMenuItem CopyClipboard = new UIMenuItem("Copy Coordinates", "Copies current player's coordinate to user's computer clipboard");

		
		internal static UIMenuItem AddLocation = new UIMenuItem("Add Location", "Adds current location to saved locations");
	}
}
