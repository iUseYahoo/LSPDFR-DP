using System;
using RAGENativeUI;
using RAGENativeUI.Elements;

namespace VectorGrabber
{
	
	public class Locations
	{
		
		internal static void setupLocationMenu()
		{
			Menu.mainMenu.AddItem(Locations.ShowAllLocations);
			Menu.mainMenu.BindMenuToItem(Locations.LocationMenu, Locations.ShowAllLocations);
			Locations.LocationMenu.ParentMenu = Menu.mainMenu;
			Menu.pool.Add(Locations.LocationMenu);
			Locations.LocationMenu.OnItemSelect += new ItemSelectEvent(Locations.OnLocationSelect);
			Locations.LocationMenu.MouseControlsEnabled = false;
			Locations.LocationMenu.AllowCameraMovement = true;
		}

		
		internal static void AddItems()
		{
			foreach (SavedLocation s in EntryPoint.VectorsRead)
			{
				Locations.LocationMenu.AddItem(new UIMenuItem(s.Title ?? "", string.Format("x: {0} | y: {1} | z: {2} | heading: {3}", new object[]
				{
					s.X,
					s.Y,
					s.Z,
					s.Heading
				})));
			}
			bool enableVectorBlips = Settings.EnableVectorBlips;
			if (enableVectorBlips)
			{
				Menu.AddBlips();
			}
		}

		
		internal static void AddItem(SavedLocation s)
		{
			Locations.LocationMenu.AddItem(new UIMenuItem(s.Title ?? "", string.Format("x: {0} | y: {1} | z: {2} | heading: {3}", new object[]
			{
				s.X,
				s.Y,
				s.Z,
				s.Heading
			})));
		}

		
		internal static void OnLocationSelect(UIMenu sender, UIMenuItem selectedItem, int index)
		{
			EntryPoint.TeleportBasedOnIndexAndDisplay(index);
		}

		
		internal static UIMenu LocationMenu = new UIMenu("Locations", "Select Option");

		
		internal static UIMenuItem ShowAllLocations = new UIMenuItem("Teleport to location", "Teleport to any of your saved locations");
	}
}
