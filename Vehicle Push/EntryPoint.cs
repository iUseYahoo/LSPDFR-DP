using System;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;
using RAGENativeUI;
using RAGENativeUI.Elements;
using VehiclePush.Essential;

namespace VehiclePush
{
	
	public static class EntryPoint
	{
		
		public static void Main()
		{
			try
			{
				EntryPoint.<>c__DisplayClass0_0 CS$<>8__locals1 = new EntryPoint.<>c__DisplayClass0_0();
				Settings.Initialize();
				UpdateChecker.Initialize();
				EntryPoint.<>c__DisplayClass0_0 CS$<>8__locals2 = CS$<>8__locals1;
				if (EntryPoint.<>o__0.<>p__1 == null)
				{
					EntryPoint.<>o__0.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(EntryPoint)));
				}
				Func<CallSite, object, int> target = EntryPoint.<>o__0.<>p__1.Target;
				CallSite <>p__ = EntryPoint.<>o__0.<>p__1;
				if (EntryPoint.<>o__0.<>p__0 == null)
				{
					EntryPoint.<>o__0.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
					{
						typeof(int)
					}, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				CS$<>8__locals2.boneIndex = target(<>p__, EntryPoint.<>o__0.<>p__0.Target(EntryPoint.<>o__0.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 6286));
				CS$<>8__locals1.resWidth = 960f;
				CS$<>8__locals1.resHeight = 995.39166f;
				GameFiber.StartNew(delegate()
				{
					try
					{
						for (;;)
						{
							GameFiber.Yield();
							if (Game.IsKeyDownRightNow(Settings.PushVehicleModifierKey) && Game.IsKeyDown(Settings.PushVehicleKey) && EntityExtensions.Exists(Helper.MainPlayer))
							{
								int num = new Random().Next(0, Helper.coolLines.Length);
								foreach (Vehicle vehicle in from veh in World.GetAllVehicles()
								where EntityExtensions.Exists(veh)
								orderby veh.DistanceTo(Helper.MainPlayer.Position)
								select veh)
								{
									if (EntityExtensions.Exists(vehicle) && vehicle.Position.DistanceTo(Helper.MainPlayer.Position) <= 6f && !Helper.vehicleSet && (vehicle.IsCar || vehicle.IsBike || vehicle.IsQuadBike))
									{
										Game.DisplayHelp("~" + InstructionalKeyExtensions.GetId(262150) + "~ " + Helper.coolLines[num]);
										GameFiber.Wait(2000);
										Helper.ClosestVehicle = vehicle;
										Helper.vehicleSet = true;
										break;
									}
								}
							}
							if (EntityExtensions.Exists(Helper.ClosestVehicle) && EntityExtensions.Exists(Helper.MainPlayer) && Helper.MainPlayer.Position.DistanceTo(Helper.ClosestVehicle.Position) > 6f && !Helper.isPushing)
							{
								Helper.vehicleSet = false;
							}
							else if (!EntityExtensions.Exists(Helper.ClosestVehicle))
							{
								Helper.vehicleSet = false;
							}
							if (EntityExtensions.Exists(Helper.ClosestVehicle) && EntityExtensions.Exists(Helper.MainPlayer) && Helper.ClosestVehicle.Position.DistanceTo(Helper.MainPlayer.Position) <= 5f)
							{
								if (EntryPoint.<>o__0.<>p__14 == null)
								{
									EntryPoint.<>o__0.<>p__14 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, object, bool> target2 = EntryPoint.<>o__0.<>p__14.Target;
								CallSite <>p__2 = EntryPoint.<>o__0.<>p__14;
								if (EntryPoint.<>o__0.<>p__13 == null)
								{
									EntryPoint.<>o__0.<>p__13 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.GreaterThan, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, object, object, object> target3 = EntryPoint.<>o__0.<>p__13.Target;
								CallSite <>p__3 = EntryPoint.<>o__0.<>p__13;
								if (EntryPoint.<>o__0.<>p__6 == null)
								{
									EntryPoint.<>o__0.<>p__6 = CallSite<Func<CallSite, object, object, object, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_DISTANCE_BETWEEN_COORDS", new Type[]
									{
										typeof(float)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								Func<CallSite, object, object, object, bool, object> target4 = EntryPoint.<>o__0.<>p__6.Target;
								CallSite <>p__4 = EntryPoint.<>o__0.<>p__6;
								object natives = NativeFunction.Natives;
								if (EntryPoint.<>o__0.<>p__4 == null)
								{
									EntryPoint.<>o__0.<>p__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, object, object, object> target5 = EntryPoint.<>o__0.<>p__4.Target;
								CallSite <>p__5 = EntryPoint.<>o__0.<>p__4;
								if (EntryPoint.<>o__0.<>p__2 == null)
								{
									EntryPoint.<>o__0.<>p__2 = CallSite<Func<CallSite, object, Vehicle, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_ENTITY_COORDS", new Type[]
									{
										typeof(Vector3)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								object arg = EntryPoint.<>o__0.<>p__2.Target(EntryPoint.<>o__0.<>p__2, NativeFunction.Natives, Helper.ClosestVehicle, true);
								if (EntryPoint.<>o__0.<>p__3 == null)
								{
									EntryPoint.<>o__0.<>p__3 = CallSite<Func<CallSite, object, Vehicle, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_ENTITY_FORWARD_VECTOR", new Type[]
									{
										typeof(Vector3)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								object arg2 = target5(<>p__5, arg, EntryPoint.<>o__0.<>p__3.Target(EntryPoint.<>o__0.<>p__3, NativeFunction.Natives, Helper.ClosestVehicle));
								if (EntryPoint.<>o__0.<>p__5 == null)
								{
									EntryPoint.<>o__0.<>p__5 = CallSite<Func<CallSite, object, Ped, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_ENTITY_COORDS", new Type[]
									{
										typeof(Vector3)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								object arg3 = target4(<>p__4, natives, arg2, EntryPoint.<>o__0.<>p__5.Target(EntryPoint.<>o__0.<>p__5, NativeFunction.Natives, Helper.MainPlayer, true), true);
								if (EntryPoint.<>o__0.<>p__12 == null)
								{
									EntryPoint.<>o__0.<>p__12 = CallSite<Func<CallSite, object, object, object, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_DISTANCE_BETWEEN_COORDS", new Type[]
									{
										typeof(float)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								Func<CallSite, object, object, object, bool, object> target6 = EntryPoint.<>o__0.<>p__12.Target;
								CallSite <>p__6 = EntryPoint.<>o__0.<>p__12;
								object natives2 = NativeFunction.Natives;
								if (EntryPoint.<>o__0.<>p__10 == null)
								{
									EntryPoint.<>o__0.<>p__10 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Add, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, object, object, object> target7 = EntryPoint.<>o__0.<>p__10.Target;
								CallSite <>p__7 = EntryPoint.<>o__0.<>p__10;
								if (EntryPoint.<>o__0.<>p__7 == null)
								{
									EntryPoint.<>o__0.<>p__7 = CallSite<Func<CallSite, object, Vehicle, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_ENTITY_COORDS", new Type[]
									{
										typeof(Vector3)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								object arg4 = EntryPoint.<>o__0.<>p__7.Target(EntryPoint.<>o__0.<>p__7, NativeFunction.Natives, Helper.ClosestVehicle, true);
								if (EntryPoint.<>o__0.<>p__9 == null)
								{
									EntryPoint.<>o__0.<>p__9 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Multiply, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								Func<CallSite, object, int, object> target8 = EntryPoint.<>o__0.<>p__9.Target;
								CallSite <>p__8 = EntryPoint.<>o__0.<>p__9;
								if (EntryPoint.<>o__0.<>p__8 == null)
								{
									EntryPoint.<>o__0.<>p__8 = CallSite<Func<CallSite, object, Vehicle, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_ENTITY_FORWARD_VECTOR", new Type[]
									{
										typeof(Vector3)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								object arg5 = target7(<>p__7, arg4, target8(<>p__8, EntryPoint.<>o__0.<>p__8.Target(EntryPoint.<>o__0.<>p__8, NativeFunction.Natives, Helper.ClosestVehicle), -1));
								if (EntryPoint.<>o__0.<>p__11 == null)
								{
									EntryPoint.<>o__0.<>p__11 = CallSite<Func<CallSite, object, Ped, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_ENTITY_COORDS", new Type[]
									{
										typeof(Vector3)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								if (target2(<>p__2, target3(<>p__3, arg3, target6(<>p__6, natives2, arg5, EntryPoint.<>o__0.<>p__11.Target(EntryPoint.<>o__0.<>p__11, NativeFunction.Natives, Helper.MainPlayer, true), true))))
								{
									Helper.isInFront = false;
								}
								else
								{
									Helper.isInFront = true;
								}
							}
						}
					}
					catch (ThreadAbortException)
					{
					}
					catch (Exception e2)
					{
						Helper.LogException(e2, "EntryPoint.Main() GF1");
					}
				}, "EntryPoint.Main() GF1");
				GameFiber.StartNew(delegate()
				{
					try
					{
						for (;;)
						{
							GameFiber.Yield();
							if (EntityExtensions.Exists(Helper.ClosestVehicle) && EntityExtensions.Exists(Helper.MainPlayer) && !Helper.MainPlayer.IsInAnyVehicle(false) && Helper.ClosestVehicle.Position.DistanceTo(Helper.MainPlayer.Position) <= 6f && (Helper.ClosestVehicle.IsCar || Helper.ClosestVehicle.IsBike || Helper.ClosestVehicle.IsQuadBike) && Helper.ClosestVehicle.Length <= 10f && Helper.vehicleSet)
							{
								if (Game.IsKeyDownRightNow(Settings.PushVehicleModifierKey) && Game.IsKeyDownRightNow(Settings.PushVehicleKey) && Helper.ClosestVehicle.IsSeatFree(-1))
								{
									if (EntryPoint.<>o__0.<>p__15 == null)
									{
										EntryPoint.<>o__0.<>p__15 = CallSite<Action<CallSite, object, Vehicle>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "NETWORK_REQUEST_CONTROL_OF_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
										}));
									}
									EntryPoint.<>o__0.<>p__15.Target(EntryPoint.<>o__0.<>p__15, NativeFunction.Natives, Helper.ClosestVehicle);
									Helper.isPushing = true;
									if (EntryPoint.<>o__0.<>p__21 == null)
									{
										EntryPoint.<>o__0.<>p__21 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}));
									}
									Func<CallSite, object, bool> target2 = EntryPoint.<>o__0.<>p__21.Target;
									CallSite <>p__2 = EntryPoint.<>o__0.<>p__21;
									if (EntryPoint.<>o__0.<>p__16 == null)
									{
										EntryPoint.<>o__0.<>p__16 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
										{
											typeof(bool)
										}, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									object obj = EntryPoint.<>o__0.<>p__16.Target(EntryPoint.<>o__0.<>p__16, NativeFunction.Natives, Helper.MainPlayer, 7);
									if (EntryPoint.<>o__0.<>p__18 == null)
									{
										EntryPoint.<>o__0.<>p__18 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}));
									}
									object obj2;
									if (!EntryPoint.<>o__0.<>p__18.Target(EntryPoint.<>o__0.<>p__18, obj))
									{
										if (EntryPoint.<>o__0.<>p__17 == null)
										{
											EntryPoint.<>o__0.<>p__17 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(EntryPoint), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
											}));
										}
										obj2 = EntryPoint.<>o__0.<>p__17.Target(EntryPoint.<>o__0.<>p__17, obj, Helper.isPushing);
									}
									else
									{
										obj2 = obj;
									}
									object obj3 = obj2;
									if (EntryPoint.<>o__0.<>p__20 == null)
									{
										EntryPoint.<>o__0.<>p__20 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}));
									}
									object arg;
									if (!EntryPoint.<>o__0.<>p__20.Target(EntryPoint.<>o__0.<>p__20, obj3))
									{
										if (EntryPoint.<>o__0.<>p__19 == null)
										{
											EntryPoint.<>o__0.<>p__19 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(EntryPoint), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
											}));
										}
										arg = EntryPoint.<>o__0.<>p__19.Target(EntryPoint.<>o__0.<>p__19, obj3, !Helper.holsteredWeapon);
									}
									else
									{
										arg = obj3;
									}
									if (target2(<>p__2, arg))
									{
										Helper.MainPlayer.Inventory.GiveNewWeapon("WEAPON_UNARMED", -1, true);
										Game.LogTrivial("Holstered the player's firearm");
										Helper.holsteredWeapon = true;
									}
									if (Helper.isInFront)
									{
										if (Helper.ClosestVehicle.IsCar && !Helper.ClosestVehicle.IsPoliceVehicle)
										{
											if (EntryPoint.<>o__0.<>p__22 == null)
											{
												EntryPoint.<>o__0.<>p__22 = CallSite<<>A<CallSite, object, Ped, Vehicle, int, float, float, float, float, float, float, bool, bool, bool, bool, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
												{
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
												}));
											}
											EntryPoint.<>o__0.<>p__22.Target(EntryPoint.<>o__0.<>p__22, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, CS$<>8__locals1.boneIndex, 0f, Helper.ClosestVehicle.Length - 2f, 0.4f, 0f, 0f, 180f, false, false, false, true, 0, true);
										}
										else if (Helper.ClosestVehicle.IsBike)
										{
											if (EntryPoint.<>o__0.<>p__23 == null)
											{
												EntryPoint.<>o__0.<>p__23 = CallSite<<>A<CallSite, object, Ped, Vehicle, int, float, float, float, float, float, float, bool, bool, bool, bool, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
												{
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
												}));
											}
											EntryPoint.<>o__0.<>p__23.Target(EntryPoint.<>o__0.<>p__23, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, CS$<>8__locals1.boneIndex, 0f, Helper.ClosestVehicle.Length - 0.55f, 0.6f, 0f, 0f, 180f, false, false, false, true, 0, true);
										}
										else if (Helper.ClosestVehicle.IsQuadBike)
										{
											if (EntryPoint.<>o__0.<>p__24 == null)
											{
												EntryPoint.<>o__0.<>p__24 = CallSite<<>A<CallSite, object, Ped, Vehicle, int, float, float, float, float, float, float, bool, bool, bool, bool, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
												{
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
													CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
												}));
											}
											EntryPoint.<>o__0.<>p__24.Target(EntryPoint.<>o__0.<>p__24, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, CS$<>8__locals1.boneIndex, 0f, Helper.ClosestVehicle.Length - 0.75f, 0.4f, 0f, 0f, 180f, false, false, false, true, 0, true);
										}
										else if (Helper.ClosestVehicle.IsPoliceVehicle)
										{
											Helper.MainPlayer.AttachTo(Helper.ClosestVehicle, 0, new Vector3(0f, Helper.ClosestVehicle.Length - 2f, 0.4f), new Rotator(0f, 0f, 180f));
										}
									}
									else if (Helper.ClosestVehicle.IsCar && !Helper.ClosestVehicle.IsPoliceVehicle)
									{
										if (EntryPoint.<>o__0.<>p__25 == null)
										{
											EntryPoint.<>o__0.<>p__25 = CallSite<<>A<CallSite, object, Ped, Vehicle, int, float, float, float, float, float, float, bool, bool, bool, bool, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										EntryPoint.<>o__0.<>p__25.Target(EntryPoint.<>o__0.<>p__25, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, CS$<>8__locals1.boneIndex, 0f, -Helper.ClosestVehicle.Length + 2f, 0.4f, 0f, 0f, 0f, false, false, false, true, 0, true);
									}
									else if (Helper.ClosestVehicle.IsBike)
									{
										if (EntryPoint.<>o__0.<>p__26 == null)
										{
											EntryPoint.<>o__0.<>p__26 = CallSite<<>A<CallSite, object, Ped, Vehicle, int, float, float, float, float, float, float, bool, bool, bool, bool, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										EntryPoint.<>o__0.<>p__26.Target(EntryPoint.<>o__0.<>p__26, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, CS$<>8__locals1.boneIndex, 0f, -Helper.ClosestVehicle.Length + 0.5f, 0.6f, 0f, 0f, 0f, false, false, false, true, 0, true);
									}
									else if (Helper.ClosestVehicle.IsQuadBike)
									{
										if (EntryPoint.<>o__0.<>p__27 == null)
										{
											EntryPoint.<>o__0.<>p__27 = CallSite<<>A<CallSite, object, Ped, Vehicle, int, float, float, float, float, float, float, bool, bool, bool, bool, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										EntryPoint.<>o__0.<>p__27.Target(EntryPoint.<>o__0.<>p__27, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, CS$<>8__locals1.boneIndex, 0f, -Helper.ClosestVehicle.Length + 0.5f, 0.4f, 0f, 0f, 0f, false, false, false, true, 0, true);
									}
									else if (Helper.ClosestVehicle.IsPoliceVehicle)
									{
										Helper.MainPlayer.AttachTo(Helper.ClosestVehicle, 0, new Vector3(0f, -Helper.ClosestVehicle.Length + 2f, 0.4f), new Rotator(0f, 0f, 0f));
									}
									if (!Helper.playingAnimation)
									{
										Helper.MainPlayer.Tasks.PlayAnimation(new AnimationDictionary("missfinale_c2ig_11"), "pushcar_offcliff_m", 5f, 33);
										Helper.playingAnimation = true;
									}
									for (;;)
									{
										GameFiber.Yield();
										if (EntityExtensions.Exists(Helper.ClosestVehicle))
										{
											if (EntityExtensions.Exists(Helper.ClosestVehicle) && Helper.ClosestVehicle.IsUpsideDown)
											{
												if (EntryPoint.<>o__0.<>p__28 == null)
												{
													EntryPoint.<>o__0.<>p__28 = CallSite<Action<CallSite, object, Vehicle, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_VEHICLE_ON_GROUND_PROPERLY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
													{
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
													}));
												}
												EntryPoint.<>o__0.<>p__28.Target(EntryPoint.<>o__0.<>p__28, NativeFunction.Natives, Helper.ClosestVehicle, 5f);
											}
											if (Game.IsKeyDown(Keys.A))
											{
												if (EntryPoint.<>o__0.<>p__29 == null)
												{
													EntryPoint.<>o__0.<>p__29 = CallSite<Action<CallSite, object, Ped, Vehicle, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_VEHICLE_TEMP_ACTION", null, typeof(EntryPoint), new CSharpArgumentInfo[]
													{
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
													}));
												}
												EntryPoint.<>o__0.<>p__29.Target(EntryPoint.<>o__0.<>p__29, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, 11, 1000);
											}
											else if (Game.IsKeyDown(Keys.D))
											{
												if (EntryPoint.<>o__0.<>p__30 == null)
												{
													EntryPoint.<>o__0.<>p__30 = CallSite<Action<CallSite, object, Ped, Vehicle, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "TASK_VEHICLE_TEMP_ACTION", null, typeof(EntryPoint), new CSharpArgumentInfo[]
													{
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
													}));
												}
												EntryPoint.<>o__0.<>p__30.Target(EntryPoint.<>o__0.<>p__30, NativeFunction.Natives, Helper.MainPlayer, Helper.ClosestVehicle, 10, 1000);
											}
											if (Helper.isInFront)
											{
												if (EntryPoint.<>o__0.<>p__31 == null)
												{
													EntryPoint.<>o__0.<>p__31 = CallSite<Action<CallSite, object, Vehicle, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_VEHICLE_FORWARD_SPEED", null, typeof(EntryPoint), new CSharpArgumentInfo[]
													{
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
													}));
												}
												EntryPoint.<>o__0.<>p__31.Target(EntryPoint.<>o__0.<>p__31, NativeFunction.Natives, Helper.ClosestVehicle, -1f);
											}
											else
											{
												if (EntryPoint.<>o__0.<>p__32 == null)
												{
													EntryPoint.<>o__0.<>p__32 = CallSite<Action<CallSite, object, Vehicle, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_VEHICLE_FORWARD_SPEED", null, typeof(EntryPoint), new CSharpArgumentInfo[]
													{
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
														CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
													}));
												}
												EntryPoint.<>o__0.<>p__32.Target(EntryPoint.<>o__0.<>p__32, NativeFunction.Natives, Helper.ClosestVehicle, 1f);
											}
											if (!Game.IsKeyDownRightNow(Settings.PushVehicleModifierKey) && !Game.IsKeyDown(Settings.PushVehicleKey))
											{
												break;
											}
										}
									}
									if (EntryPoint.<>o__0.<>p__33 == null)
									{
										EntryPoint.<>o__0.<>p__33 = CallSite<Action<CallSite, object, Ped, bool, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DETACH_ENTITY", null, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									EntryPoint.<>o__0.<>p__33.Target(EntryPoint.<>o__0.<>p__33, NativeFunction.Natives, Helper.MainPlayer, false, false);
									if (EntryPoint.<>o__0.<>p__34 == null)
									{
										EntryPoint.<>o__0.<>p__34 = CallSite<Action<CallSite, object, Ped, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "FREEZE_ENTITY_POSITION", null, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									EntryPoint.<>o__0.<>p__34.Target(EntryPoint.<>o__0.<>p__34, NativeFunction.Natives, Helper.MainPlayer, false);
									Helper.MainPlayer.Tasks.Clear();
									Helper.playingAnimation = false;
									Helper.isPushing = false;
								}
								else if (Game.IsKeyDownRightNow(Settings.PushVehicleModifierKey) && Game.IsKeyDownRightNow(Settings.PushVehicleKey) && !Helper.ClosestVehicle.IsSeatFree(-1))
								{
									Game.DisplayHelp("~BLIP_INFO_ICON~  There is a driver in the vehicle");
								}
								else if (EntityExtensions.Exists(Helper.ClosestVehicle) && Helper.ClosestVehicle.Position.DistanceTo(Helper.MainPlayer.Position) <= 6f && Helper.ClosestVehicle.Length <= 10f)
								{
									ResText.Draw("[~y~" + Helper.ClosestVehicle.Model.Name + "~s~]", new Point((int)CS$<>8__locals1.resWidth, (int)CS$<>8__locals1.resHeight), 0.5f, Color.White, 0, 1, false, true, default(Size));
									if (Helper.isInFront)
									{
										ResText.Draw(string.Format("[~b~{0}~s~ + ~b~{1}~s~] Push Front | [~r~{2}~s~] Undesignate", Settings.PushVehicleModifierKey, Settings.PushVehicleKey, Settings.ClearDesignatedVehicleKey), new Point((int)CS$<>8__locals1.resWidth, (int)CS$<>8__locals1.resHeight + 40), 0.35f, Color.White, 0, 1, false, true, default(Size));
									}
									else
									{
										ResText.Draw(string.Format("[~b~{0}~s~ + ~b~{1}~s~] Push Rear | [~r~{2}~s~] Undesignate", Settings.PushVehicleModifierKey, Settings.PushVehicleKey, Settings.ClearDesignatedVehicleKey), new Point((int)CS$<>8__locals1.resWidth, (int)CS$<>8__locals1.resHeight + 40), 0.35f, Color.White, 0, 1, false, true, default(Size));
									}
									if (!Helper.hidHelpMessage)
									{
										Game.HideHelp();
										Helper.hidHelpMessage = true;
									}
								}
								if (EntityExtensions.Exists(Helper.ClosestVehicle) && Game.IsKeyDown(Settings.ClearDesignatedVehicleKey) && !Helper.isPushing)
								{
									Helper.vehicleSet = false;
									Helper.hidHelpMessage = false;
									Helper.holsteredWeapon = false;
									Game.LogTrivial("Undesignated " + Helper.ClosestVehicle.Model.Name);
								}
							}
						}
					}
					catch (ThreadAbortException)
					{
					}
					catch (InvalidOperationException)
					{
					}
					catch (Exception e2)
					{
						Helper.LogException(e2, "EntryPoint.Main() GF2");
					}
				}, "EntryPoint.Main() GF2");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "EntryPoint.Main()");
			}
		}
	}
}
