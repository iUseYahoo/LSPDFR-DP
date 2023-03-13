using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace EmergencyCallouts.Essential
{
	
	internal static class ExtensionMethods
	{
		
		public static bool GetSafePositionForPed(this Vector3 CalloutPosition, out Vector3 SafePosition)
		{
			if (ExtensionMethods.<>o__0.<>p__2 == null)
			{
				ExtensionMethods.<>o__0.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(ExtensionMethods), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, bool> target = ExtensionMethods.<>o__0.<>p__2.Target;
			CallSite <>p__ = ExtensionMethods.<>o__0.<>p__2;
			if (ExtensionMethods.<>o__0.<>p__1 == null)
			{
				ExtensionMethods.<>o__0.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(ExtensionMethods), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, object> target2 = ExtensionMethods.<>o__0.<>p__1.Target;
			CallSite <>p__2 = ExtensionMethods.<>o__0.<>p__1;
			if (ExtensionMethods.<>o__0.<>p__0 == null)
			{
				ExtensionMethods.<>o__0.<>p__0 = CallSite<<>F{00001000}<CallSite, object, float, float, float, bool, Vector3, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_SAFE_COORD_FOR_PED", new Type[]
				{
					typeof(bool)
				}, typeof(ExtensionMethods), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			Vector3 vector;
			if (!target(<>p__, target2(<>p__2, ExtensionMethods.<>o__0.<>p__0.Target(ExtensionMethods.<>o__0.<>p__0, NativeFunction.Natives, CalloutPosition.X, CalloutPosition.Y, CalloutPosition.Z, true, ref vector, 0))))
			{
				SafePosition = vector;
				return true;
			}
			vector = World.GetNextPositionOnStreet(CalloutPosition);
			Entity closestEntity = World.GetClosestEntity(vector, 25f, 256L);
			if (EntityExtensions.Exists(closestEntity))
			{
				vector = closestEntity.Position;
				SafePosition = vector;
				return true;
			}
			SafePosition = vector;
			return false;
		}

		
		internal static void SetIntoxicated(this Ped ped)
		{
			AnimationSet value;
			value..ctor("move_m@drunk@verydrunk");
			value.LoadAndWait();
			ped.MovementAnimationSet = new AnimationSet?(value);
		}

		
		internal static void SetInjured(this Ped ped, int health)
		{
			AnimationSet value;
			value..ctor("move_m@injured");
			value.LoadAndWait();
			ped.MovementAnimationSet = new AnimationSet?(value);
			if (ped.IsAlive)
			{
				ped.Health = health;
			}
		}
	}
}
