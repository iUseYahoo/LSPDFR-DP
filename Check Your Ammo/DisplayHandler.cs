using System;
using System.Drawing;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using CheckYourAmmo.Essential;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;
using RAGENativeUI.Elements;

namespace CheckYourAmmo.Handling
{
	
	internal static class DisplayHandling
	{
		
		internal static void DisplayCount(dynamic textBar)
		{
			try
			{
				GameFiber.StartNew(delegate()
				{
					try
					{
						Func<CallSite, object, bool> target;
						CallSite <>p__;
						do
						{
							GameFiber.Yield();
							if (DisplayHandling.<>o__1.<>p__1 == null)
							{
								DisplayHandling.<>o__1.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							target = DisplayHandling.<>o__1.<>p__1.Target;
							<>p__ = DisplayHandling.<>o__1.<>p__1;
							if (DisplayHandling.<>o__1.<>p__0 == null)
							{
								DisplayHandling.<>o__1.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
								{
									typeof(bool)
								}, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
						}
						while (!target(<>p__, DisplayHandling.<>o__1.<>p__0.Target(DisplayHandling.<>o__1.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 6)));
						GameFiber.Wait(1000);
						Helper.loadedAmmo = (float)Helper.MainPlayer.Inventory.EquippedWeapon.LoadedAmmo;
						Helper.magazineSize = (float)Helper.MainPlayer.Inventory.EquippedWeapon.MagazineSize;
						Helper.percentage = Helper.loadedAmmo / Helper.magazineSize * 100f;
						if (Helper.percentage <= 40f)
						{
							if (DisplayHandling.<>o__1.<>p__2 == null)
							{
								DisplayHandling.<>o__1.<>p__2 = CallSite<Func<CallSite, object, Color, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Highlight", typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							DisplayHandling.<>o__1.<>p__2.Target(DisplayHandling.<>o__1.<>p__2, textBar, Color.Red);
							if (Settings.VoiceLines)
							{
								if (Helper.MainPlayer.IsMale)
								{
									Helper.voiceNumber = new Random().Next(2, 4);
									Helper.MainPlayer.PlayAmbientSpeech("zombie", "GENERIC_CURSE_MED", Helper.voiceNumber, 3);
								}
								else
								{
									Helper.voiceNumber = new Random().Next(2, 4);
									Helper.MainPlayer.PlayAmbientSpeech("jane", "GENERIC_CURSE_MED", Helper.voiceNumber, 3);
								}
								Game.LogTrivial(string.Format("Playing ambient speech num 0{0} | Male: {1}", Helper.voiceNumber, Helper.MainPlayer.IsMale));
							}
						}
						else if (Helper.percentage > 40f && Helper.percentage <= 70f)
						{
							if (DisplayHandling.<>o__1.<>p__3 == null)
							{
								DisplayHandling.<>o__1.<>p__3 = CallSite<Func<CallSite, object, Color, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Highlight", typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							DisplayHandling.<>o__1.<>p__3.Target(DisplayHandling.<>o__1.<>p__3, textBar, Color.Orange);
						}
						else
						{
							if (DisplayHandling.<>o__1.<>p__4 == null)
							{
								DisplayHandling.<>o__1.<>p__4 = CallSite<Func<CallSite, object, Color, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Highlight", typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							DisplayHandling.<>o__1.<>p__4.Target(DisplayHandling.<>o__1.<>p__4, textBar, Color.LightGreen);
						}
						Game.LogTrivial(string.Format("Seeing {0}/{1} bullets", Helper.loadedAmmo, Helper.magazineSize));
						if (DisplayHandling.<>o__1.<>p__5 == null)
						{
							DisplayHandling.<>o__1.<>p__5 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "Text", typeof(DisplayHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						DisplayHandling.<>o__1.<>p__5.Target(DisplayHandling.<>o__1.<>p__5, textBar, Helper.MainPlayer.Inventory.EquippedWeapon.LoadedAmmo.ToString());
						if (DisplayHandling.<>o__1.<>p__6 == null)
						{
							DisplayHandling.<>o__1.<>p__6 = CallSite<Action<CallSite, TimerBarPool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Remove", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						DisplayHandling.<>o__1.<>p__6.Target(DisplayHandling.<>o__1.<>p__6, Helper.tbPool, textBar);
						if (DisplayHandling.<>o__1.<>p__7 == null)
						{
							DisplayHandling.<>o__1.<>p__7 = CallSite<Action<CallSite, TimerBarPool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Add", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						DisplayHandling.<>o__1.<>p__7.Target(DisplayHandling.<>o__1.<>p__7, Helper.tbPool, textBar);
						if (Helper.timeout == 5000)
						{
							GameFiber.Wait(4000);
							Helper.timeout = 3500;
						}
						else
						{
							GameFiber.Wait(2600);
						}
						Helper.isChecking = false;
						if (EntityExtensions.Exists(Helper.magazine))
						{
							Helper.magazine.Delete();
						}
						GameFiber.Wait(2000);
						if (DisplayHandling.<>o__1.<>p__8 == null)
						{
							DisplayHandling.<>o__1.<>p__8 = CallSite<Action<CallSite, TimerBarPool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "Remove", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						DisplayHandling.<>o__1.<>p__8.Target(DisplayHandling.<>o__1.<>p__8, Helper.tbPool, textBar);
						GameFiber.Wait(800);
						Helper.hasMagazine = true;
						Helper.allowAnimation = true;
						Helper.allowChecking = true;
					}
					catch (ThreadAbortException)
					{
					}
					catch (Exception e2)
					{
						Helper.LogException(e2, "DisplayHandling.DisplayCount GF1");
					}
				}, "DisplayHandling.DisplayCount() GF1");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "DisplayHandling.DisplayCount()");
			}
		}

		
		internal static void WeaponCensoring()
		{
			try
			{
				Game.LogTrivial("Initialized WeaponCensoring.cs");
				GameFiber.StartNew(delegate()
				{
					try
					{
						for (;;)
						{
							GameFiber.Yield();
							if (DisplayHandling.<>o__2.<>p__16 == null)
							{
								DisplayHandling.<>o__2.<>p__16 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							Func<CallSite, object, bool> target = DisplayHandling.<>o__2.<>p__16.Target;
							CallSite <>p__ = DisplayHandling.<>o__2.<>p__16;
							bool weaponWheelLoadedAmmoCensoring = Settings.WeaponWheelLoadedAmmoCensoring;
							object obj;
							if (weaponWheelLoadedAmmoCensoring)
							{
								if (DisplayHandling.<>o__2.<>p__1 == null)
								{
									DisplayHandling.<>o__2.<>p__1 = CallSite<Func<CallSite, bool, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, bool, object, object> target2 = DisplayHandling.<>o__2.<>p__1.Target;
								CallSite <>p__2 = DisplayHandling.<>o__2.<>p__1;
								bool arg = weaponWheelLoadedAmmoCensoring;
								if (DisplayHandling.<>o__2.<>p__0 == null)
								{
									DisplayHandling.<>o__2.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
									{
										typeof(bool)
									}, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								obj = target2(<>p__2, arg, DisplayHandling.<>o__2.<>p__0.Target(DisplayHandling.<>o__2.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 6));
							}
							else
							{
								obj = weaponWheelLoadedAmmoCensoring;
							}
							object obj2 = obj;
							if (DisplayHandling.<>o__2.<>p__7 == null)
							{
								DisplayHandling.<>o__2.<>p__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							object obj4;
							if (!DisplayHandling.<>o__2.<>p__7.Target(DisplayHandling.<>o__2.<>p__7, obj2))
							{
								if (DisplayHandling.<>o__2.<>p__6 == null)
								{
									DisplayHandling.<>o__2.<>p__6 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, object, object, object> target3 = DisplayHandling.<>o__2.<>p__6.Target;
								CallSite <>p__3 = DisplayHandling.<>o__2.<>p__6;
								object arg2 = obj2;
								if (DisplayHandling.<>o__2.<>p__2 == null)
								{
									DisplayHandling.<>o__2.<>p__2 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_HUD_COMPONENT_ACTIVE", new Type[]
									{
										typeof(bool)
									}, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								object obj3 = DisplayHandling.<>o__2.<>p__2.Target(DisplayHandling.<>o__2.<>p__2, NativeFunction.Natives, 19);
								if (DisplayHandling.<>o__2.<>p__5 == null)
								{
									DisplayHandling.<>o__2.<>p__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								object arg4;
								if (!DisplayHandling.<>o__2.<>p__5.Target(DisplayHandling.<>o__2.<>p__5, obj3))
								{
									if (DisplayHandling.<>o__2.<>p__4 == null)
									{
										DisplayHandling.<>o__2.<>p__4 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(DisplayHandling), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}));
									}
									Func<CallSite, object, object, object> target4 = DisplayHandling.<>o__2.<>p__4.Target;
									CallSite <>p__4 = DisplayHandling.<>o__2.<>p__4;
									object arg3 = obj3;
									if (DisplayHandling.<>o__2.<>p__3 == null)
									{
										DisplayHandling.<>o__2.<>p__3 = CallSite<Func<CallSite, object, int, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_CONTROL_PRESSED", new Type[]
										{
											typeof(bool)
										}, typeof(DisplayHandling), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									arg4 = target4(<>p__4, arg3, DisplayHandling.<>o__2.<>p__3.Target(DisplayHandling.<>o__2.<>p__3, NativeFunction.Natives, 0, 37));
								}
								else
								{
									arg4 = obj3;
								}
								obj4 = target3(<>p__3, arg2, arg4);
							}
							else
							{
								obj4 = obj2;
							}
							object obj5 = obj4;
							if (DisplayHandling.<>o__2.<>p__9 == null)
							{
								DisplayHandling.<>o__2.<>p__9 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							object obj6;
							if (!DisplayHandling.<>o__2.<>p__9.Target(DisplayHandling.<>o__2.<>p__9, obj5))
							{
								if (DisplayHandling.<>o__2.<>p__8 == null)
								{
									DisplayHandling.<>o__2.<>p__8 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								obj6 = DisplayHandling.<>o__2.<>p__8.Target(DisplayHandling.<>o__2.<>p__8, obj5, !Helper.MainPlayer.IsRagdoll);
							}
							else
							{
								obj6 = obj5;
							}
							object obj7 = obj6;
							if (DisplayHandling.<>o__2.<>p__11 == null)
							{
								DisplayHandling.<>o__2.<>p__11 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							object obj8;
							if (!DisplayHandling.<>o__2.<>p__11.Target(DisplayHandling.<>o__2.<>p__11, obj7))
							{
								if (DisplayHandling.<>o__2.<>p__10 == null)
								{
									DisplayHandling.<>o__2.<>p__10 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								obj8 = DisplayHandling.<>o__2.<>p__10.Target(DisplayHandling.<>o__2.<>p__10, obj7, !Helper.MainPlayer.IsFalling);
							}
							else
							{
								obj8 = obj7;
							}
							object obj9 = obj8;
							if (DisplayHandling.<>o__2.<>p__13 == null)
							{
								DisplayHandling.<>o__2.<>p__13 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							object obj10;
							if (!DisplayHandling.<>o__2.<>p__13.Target(DisplayHandling.<>o__2.<>p__13, obj9))
							{
								if (DisplayHandling.<>o__2.<>p__12 == null)
								{
									DisplayHandling.<>o__2.<>p__12 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								obj10 = DisplayHandling.<>o__2.<>p__12.Target(DisplayHandling.<>o__2.<>p__12, obj9, Helper.MainPlayer.IsAlive);
							}
							else
							{
								obj10 = obj9;
							}
							object obj11 = obj10;
							if (DisplayHandling.<>o__2.<>p__15 == null)
							{
								DisplayHandling.<>o__2.<>p__15 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(DisplayHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							object arg5;
							if (!DisplayHandling.<>o__2.<>p__15.Target(DisplayHandling.<>o__2.<>p__15, obj11))
							{
								if (DisplayHandling.<>o__2.<>p__14 == null)
								{
									DisplayHandling.<>o__2.<>p__14 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								arg5 = DisplayHandling.<>o__2.<>p__14.Target(DisplayHandling.<>o__2.<>p__14, obj11, Helper.MainPlayer.IsOnFoot);
							}
							else
							{
								arg5 = obj11;
							}
							if (target(<>p__, arg5))
							{
								if (DisplayHandling.<>o__2.<>p__18 == null)
								{
									DisplayHandling.<>o__2.<>p__18 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(uint), typeof(DisplayHandling)));
								}
								Func<CallSite, object, uint> target5 = DisplayHandling.<>o__2.<>p__18.Target;
								CallSite <>p__5 = DisplayHandling.<>o__2.<>p__18;
								if (DisplayHandling.<>o__2.<>p__17 == null)
								{
									DisplayHandling.<>o__2.<>p__17 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "xA48931185F0536FE", new Type[]
									{
										typeof(uint)
									}, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								uint arg6 = target5(<>p__5, DisplayHandling.<>o__2.<>p__17.Target(DisplayHandling.<>o__2.<>p__17, NativeFunction.Natives));
								if (DisplayHandling.<>o__2.<>p__20 == null)
								{
									DisplayHandling.<>o__2.<>p__20 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(DisplayHandling)));
								}
								Func<CallSite, object, int> target6 = DisplayHandling.<>o__2.<>p__20.Target;
								CallSite <>p__6 = DisplayHandling.<>o__2.<>p__20;
								if (DisplayHandling.<>o__2.<>p__19 == null)
								{
									DisplayHandling.<>o__2.<>p__19 = CallSite<Func<CallSite, object, uint, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_WEAPON_CLIP_SIZE", new Type[]
									{
										typeof(int)
									}, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								target6(<>p__6, DisplayHandling.<>o__2.<>p__19.Target(DisplayHandling.<>o__2.<>p__19, NativeFunction.Natives, arg6));
								if (!DisplayHandling.activated)
								{
									GameFiber.Wait(55);
								}
								DisplayHandling.activated = true;
								if (Helper.MainPlayer.Inventory.EquippedWeapon.Asset.ToString() != "WEAPON_STUNGUN")
								{
									if (DisplayHandling.<>o__2.<>p__21 == null)
									{
										DisplayHandling.<>o__2.<>p__21 = CallSite<Action<CallSite, object, float, float, float, float, int, int, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DRAW_RECT", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
									DisplayHandling.<>o__2.<>p__21.Target(DisplayHandling.<>o__2.<>p__21, NativeFunction.Natives, 0.5094f, 0.21825f, 0.00785f, 0.019f, 140, 140, 140, 255);
								}
								if (DisplayHandling.<>o__2.<>p__22 == null)
								{
									DisplayHandling.<>o__2.<>p__22 = CallSite<Action<CallSite, object, float, float, float, float, int, int, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DRAW_RECT", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
								DisplayHandling.<>o__2.<>p__22.Target(DisplayHandling.<>o__2.<>p__22, NativeFunction.Natives, 0.6285f, 0.4715f, 0.01f, 0.019f, 140, 140, 140, 255);
								if (DisplayHandling.<>o__2.<>p__23 == null)
								{
									DisplayHandling.<>o__2.<>p__23 = CallSite<Action<CallSite, object, float, float, float, float, int, int, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DRAW_RECT", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
								DisplayHandling.<>o__2.<>p__23.Target(DisplayHandling.<>o__2.<>p__23, NativeFunction.Natives, 0.43125f, 0.61165f, 0.0085f, 0.0195f, 140, 140, 140, 255);
								if (DisplayHandling.<>o__2.<>p__24 == null)
								{
									DisplayHandling.<>o__2.<>p__24 = CallSite<Action<CallSite, object, float, float, float, float, int, int, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DRAW_RECT", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
								DisplayHandling.<>o__2.<>p__24.Target(DisplayHandling.<>o__2.<>p__24, NativeFunction.Natives, 0.618f, 0.3035f, 0.015f, 0.019f, 140, 140, 140, 255);
								if (DisplayHandling.<>o__2.<>p__25 == null)
								{
									DisplayHandling.<>o__2.<>p__25 = CallSite<Action<CallSite, object, float, float, float, float, int, int, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DRAW_RECT", null, typeof(DisplayHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
								DisplayHandling.<>o__2.<>p__25.Target(DisplayHandling.<>o__2.<>p__25, NativeFunction.Natives, 0.588f, 0.61165f, 0.0085f, 0.0195f, 140, 140, 140, 255);
							}
							else
							{
								DisplayHandling.activated = false;
							}
						}
					}
					catch (ThreadAbortException)
					{
					}
					catch (Exception e2)
					{
						Helper.LogException(e2, "DisplayHandling.WeaponCensoring() GF1");
					}
				}, "DisplayHandling.WeaponCensoring() GF1");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "DisplayHandling.WeaponCensoring()");
			}
		}

		
		private static bool activated;
	}
}
