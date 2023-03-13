using System;
using System.Drawing;
using NativeUI;

namespace NHT.NHTUI
{
	
	public class ItemNHT : UIMenuItem
	{
		
		
		
		public int Quality { get; set; }

		
		
		
		public float Satisfying { get; set; }

		
		
		
		public int Type { get; set; }

		
		
		
		public int Count { get; set; }

		
		
		
		public int VehicleHach { get; set; }

		
		
		
		public Color VehiclePrimeColor { get; set; }

		
		public ItemNHT(string Text) : base(Text)
		{
		}

		
		public ItemNHT(string Text, string Description) : base(Text, Description)
		{
		}
	}
}
