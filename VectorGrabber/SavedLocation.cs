using System;

namespace VectorGrabber
{
	
	public struct SavedLocation
	{
		
		public SavedLocation(float x, float y, float z, float heading, string title)
		{
			this.X = x;
			this.Y = y;
			this.Z = z;
			this.Heading = heading;
			this.Title = title;
		}

		
		public float X;

		
		public float Y;

		
		public float Z;

		
		public float Heading;

		
		public string Title;
	}
}
