using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using LSPD_First_Response.Engine.Scripting.Entities;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace ManiacCallouts.API
{
	
	public static class Extensions
	{
		
		public static Vector3 GetClosestMajorVehicleNode(this Vector3 startPoint)
		{
			if (Extensions.<>o__1.<>p__0 == null)
			{
				Extensions.<>o__1.<>p__0 = CallSite<<>A{00000400}<CallSite, object, float, float, float, Vector3, float, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GET_CLOSEST_MAJOR_VEHICLE_NODE", new Type[]
				{
					typeof(bool)
				}, typeof(Extensions), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.IsOut, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			Vector3 ClosestMajorVehicleNode;
			Extensions.<>o__1.<>p__0.Target(Extensions.<>o__1.<>p__0, NativeFunction.Natives, startPoint.X, startPoint.Y, startPoint.Z, ref ClosestMajorVehicleNode, 3f, 0f);
			return ClosestMajorVehicleNode;
		}

		
		public static void PlayTaskScen(Ped ped, string scenario, int p1, bool EntryExit)
		{
			if (Extensions.<>o__2.<>p__0 == null)
			{
				Extensions.<>o__2.<>p__0 = CallSite<Action<CallSite, object, Ped, string, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_START_SCENARIO_IN_PLACE", null, typeof(Extensions), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			Extensions.<>o__2.<>p__0.Target(Extensions.<>o__2.<>p__0, NativeFunction.Natives, ped, scenario, p1, EntryExit);
		}

		
		public static void DoorControll(long Door, Vector3 Location, bool Lock, float P1, float P2, float P3)
		{
			NativeFunction.CallByHash<uint>(11174268100976307632UL, new NativeArgument[]
			{
				Door,
				Location.X,
				Location.Y,
				Location.Z,
				Lock,
				P1,
				P2,
				P3
			});
		}

		
		public static void DoorAddSystem(long Door, long Hash, Vector3 Location, float P1, float P2, float P3)
		{
			NativeFunction.CallByHash<uint>(8036736002072363558UL, new NativeArgument[]
			{
				Door,
				Hash,
				Location.X,
				Location.Y,
				Location.Z,
				P1,
				P2,
				P3
			});
		}

		
		public static void RandomiseLicencePlate(this Vehicle vehicle)
		{
			vehicle.LicensePlate = string.Concat(new string[]
			{
				MathHelper.GetRandomInteger(9).ToString(),
				MathHelper.GetRandomInteger(9).ToString(),
				Convert.ToChar(MathHelper.GetRandomInteger(0, 25) + 65).ToString(),
				Convert.ToChar(MathHelper.GetRandomInteger(0, 25) + 65).ToString(),
				Convert.ToChar(MathHelper.GetRandomInteger(0, 25) + 65).ToString(),
				MathHelper.GetRandomInteger(9).ToString(),
				MathHelper.GetRandomInteger(9).ToString(),
				MathHelper.GetRandomInteger(9).ToString()
			});
		}

		
		public static void ClearAreaOfPeds(Vector3 position, float radius)
		{
			Ped[] allPeds = World.GetAllPeds();
			foreach (Ped ped in allPeds)
			{
				bool flag = !ped;
				if (!flag)
				{
					bool flag2 = ped.DistanceTo2D(position) > radius;
					if (!flag2)
					{
						bool flag3 = ped != Game.LocalPlayer.Character;
						if (flag3)
						{
							ped.Delete();
						}
					}
				}
			}
		}

		
		public static Ped Addcop(Ped Human, Vector3 position, float radius)
		{
			Ped[] possiblePedsNearInWorld = (from x in World.GetAllPeds()
			where x != Game.LocalPlayer.Character && x.IsAlive && x.IsHuman && Vector3.Distance(x.Position, position) < radius
			select x).ToArray<Ped>();
			return possiblePedsNearInWorld.FirstOrDefault<Ped>();
		}

		
		public static void ClearAreaOfObject(Vector3 position, float radius)
		{
			Object[] allObj = World.GetAllObjects();
			foreach (Object Obj in allObj)
			{
				bool flag = !Obj;
				if (!flag)
				{
					bool flag2 = Obj.DistanceTo2D(position) > radius;
					if (!flag2)
					{
						Obj.Delete();
					}
				}
			}
		}

		
		public static void SetMovementAnimationSet(this Ped ped, string animationSet)
		{
			AnimationSet moveSet;
			moveSet..ctor(animationSet);
			moveSet.LoadAndWait();
			ped.MovementAnimationSet = new AnimationSet?(moveSet);
		}

		
		public static void ClearAreaOfVehicles(Vector3 position, float radius)
		{
			Vehicle[] allVeh = World.GetAllVehicles();
			foreach (Vehicle vehicle in allVeh)
			{
				bool flag = !vehicle;
				if (!flag)
				{
					bool flag2 = vehicle.DistanceTo2D(position) > radius;
					if (!flag2)
					{
						bool flag3 = vehicle != Game.LocalPlayer.Character;
						if (flag3)
						{
							vehicle.Delete();
						}
					}
				}
			}
		}

		
		public static void SetPedWanted(Ped ped, bool set)
		{
			Persona.FromExistingPed(ped).Wanted = set;
		}

		
		public static void Shuffle<T>(this IList<T> list)
		{
			int i = list.Count;
			while (i > 1)
			{
				int j = Extensions.Random.Next(i--);
				T temp = list[i];
				list[i] = list[j];
				list[j] = temp;
			}
		}

		
		public static T GetRandomElement<T>(this IList<T> list, bool shuffle = false)
		{
			bool flag = list == null || list.Count <= 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				if (shuffle)
				{
					list.Shuffle<T>();
				}
				result = list[Extensions.Random.Next(list.Count)];
			}
			return result;
		}

		
		public static T GetRandomElement<T>(this IEnumerable<T> enumarable, bool shuffle = false)
		{
			bool flag = enumarable == null || enumarable.Count<T>() <= 0;
			T result;
			if (flag)
			{
				result = default(T);
			}
			else
			{
				T[] array = enumarable.ToArray<T>();
				result = array.GetRandomElement(shuffle);
			}
			return result;
		}

		
		public static T GetRandomElement<T>(this Enum items)
		{
			bool flag = typeof(T).BaseType != typeof(Enum);
			if (flag)
			{
				throw new InvalidCastException();
			}
			Array types = Enum.GetValues(typeof(T));
			return types.Cast<T>().GetRandomElement(false);
		}

		
		public static Random Random = new Random();
	}
}
