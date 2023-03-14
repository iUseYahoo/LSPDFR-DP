using System;

namespace YobbinCallouts
{
	
	public struct Component
	{
		
		
		
		public int DrawableVariation { get; private set; }

		
		
		
		public int TextureVariation { get; private set; }

		
		public Component(Enum component, int drawableVariation, int textureVariation)
		{
			this.DrawableVariation = drawableVariation;
			this.TextureVariation = textureVariation;
		}
	}
}
