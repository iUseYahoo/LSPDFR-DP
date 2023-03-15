using System;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace CheckYourAmmo.Other
{
	
	internal static class Testing
	{
		
		internal static void Initialize()
		{
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					GameFiber.Yield();
					if (Game.IsKeyDownRightNow(Keys.LShiftKey) && Game.IsKeyDown(Keys.I))
					{
						foreach (Object @object in World.GetAllObjects())
						{
							if (EntityExtensions.Exists(@object))
							{
								@object.Delete();
							}
						}
					}
					if (Game.IsKeyDown(Keys.K))
					{
						if (Testing.<>o__0.<>p__0 == null)
						{
							Testing.<>o__0.<>p__0 = CallSite<Action<CallSite, object, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xBCFC632DB7673BF0", null, typeof(Testing), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						Testing.<>o__0.<>p__0.Target(Testing.<>o__0.<>p__0, NativeFunction.Natives, 45, -170);
					}
				}
			});
		}
	}
}
