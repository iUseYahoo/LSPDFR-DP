using System;
using System.Collections.Generic;
using NativeUI;

namespace NHT.NHTUI
{
	
	public class MenuNHT : UIMenu
	{
		
		public MenuNHT(string Title, string Subtitle) : base(Title, Subtitle)
		{
		}

		
		public void AddItemNHT(ItemNHT item)
		{
			this.MenuItemsNHT.Add(item);
			base.AddItem(item);
		}

		
		public void RemoveNHT(int index)
		{
			this.MenuItemsNHT.RemoveAt(index);
			base.RemoveItemAt(index);
		}

		
		public void ClearNHT()
		{
			this.MenuItemsNHT.Clear();
			this.MenuItems.Clear();
		}

		
		private UIMenuItem Dummy = new UIMenuItem("Dummy");

		
		public List<ItemNHT> MenuItemsNHT = new List<ItemNHT>();
	}
}
