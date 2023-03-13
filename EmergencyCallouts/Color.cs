using System;
using System.Drawing;
using Rage;

namespace EmergencyCallouts.Essential
{
	
	internal static class Color
	{
		
		internal static void SetColorRed(this Blip blip)
		{
			blip.Color = Color.FromArgb(224, 50, 50);
		}

		
		internal static void SetColorYellow(this Blip blip)
		{
			blip.Color = Color.FromArgb(240, 200, 80);
		}

		
		internal static void SetColorBlue(this Blip blip)
		{
			blip.Color = Color.FromArgb(93, 182, 229);
		}

		
		internal static void SetColorOrange(this Blip blip)
		{
			blip.Color = Color.FromArgb(234, 142, 80);
		}

		
		internal static void SetColorGreen(this Blip blip)
		{
			blip.Color = Color.FromArgb(114, 204, 114);
		}

		
		internal static void SetColorPurple(this Blip blip)
		{
			blip.Color = Color.FromArgb(171, 60, 230);
		}
	}
}
