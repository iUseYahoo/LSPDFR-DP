using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using CheckYourAmmo.Essential;
using CheckYourAmmo.Handling;
using CheckYourAmmo.Other;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;
using RAGENativeUI.Elements;

namespace CheckYourAmmo
{
	
	internal static class EntryPoint
	{
		
		internal static void Main()
		{
			try
			{
				TextTimerBar textBar = new TextTimerBar("AMMO", "TEXT");
				Settings.Initialize();
				UpdateChecker.Initialize();
				BaseHandling.Initialize(textBar);
				if (Settings.EarlyAccess)
				{
					Testing.Initialize();
				}
				GameFiber.StartNew(delegate()
				{
					try
					{
						for (;;)
						{
							GameFiber.Yield();
							if (EntryPoint.<>o__0.<>p__1 == null)
							{
								EntryPoint.<>o__0.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							Func<CallSite, object, bool> target = EntryPoint.<>o__0.<>p__1.Target;
							CallSite <>p__ = EntryPoint.<>o__0.<>p__1;
							if (EntryPoint.<>o__0.<>p__0 == null)
							{
								EntryPoint.<>o__0.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
								{
									typeof(bool)
								}, typeof(EntryPoint), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							if (target(<>p__, EntryPoint.<>o__0.<>p__0.Target(EntryPoint.<>o__0.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 6)))
							{
								if (EntryPoint.<>o__0.<>p__5 == null)
								{
									EntryPoint.<>o__0.<>p__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, object, bool> target2 = EntryPoint.<>o__0.<>p__5.Target;
								CallSite <>p__2 = EntryPoint.<>o__0.<>p__5;
								if (EntryPoint.<>o__0.<>p__2 == null)
								{
									EntryPoint.<>o__0.<>p__2 = CallSite<Func<CallSite, object, int, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_DISABLED_CONTROL_JUST_RELEASED", new Type[]
									{
										typeof(bool)
									}, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								object obj = EntryPoint.<>o__0.<>p__2.Target(EntryPoint.<>o__0.<>p__2, NativeFunction.Natives, 0, 45);
								if (EntryPoint.<>o__0.<>p__4 == null)
								{
									EntryPoint.<>o__0.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								object arg;
								if (!EntryPoint.<>o__0.<>p__4.Target(EntryPoint.<>o__0.<>p__4, obj))
								{
									if (EntryPoint.<>o__0.<>p__3 == null)
									{
										EntryPoint.<>o__0.<>p__3 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
										}));
									}
									arg = EntryPoint.<>o__0.<>p__3.Target(EntryPoint.<>o__0.<>p__3, obj, Helper.keyDownCounter < Helper.keyDownDuration);
								}
								else
								{
									arg = obj;
								}
								if (target2(<>p__2, arg))
								{
									if (EntryPoint.<>o__0.<>p__6 == null)
									{
										EntryPoint.<>o__0.<>p__6 = CallSite<Action<CallSite, object, Ped>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "MAKE_PED_RELOAD", null, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
										}));
									}
									EntryPoint.<>o__0.<>p__6.Target(EntryPoint.<>o__0.<>p__6, NativeFunction.Natives, Helper.MainPlayer);
									Helper.keyDownCounter = 0;
								}
								if (Helper.MainPlayer.Inventory.EquippedWeapon.Asset.Hash != 911657153U && !Helper.isChecking && Helper.allowChecking && Helper.MainPlayer.IsAlive && !Helper.MainPlayer.IsReloading && !Helper.MainPlayer.IsFalling && !Helper.MainPlayer.IsRagdoll && Helper.MainPlayer.IsOnFoot)
								{
									if (Game.IsKeyDownRightNow(Settings.HoldToCheckKey))
									{
										Helper.keyDownCounter++;
									}
									else
									{
										Helper.keyDownCounter = 0;
									}
									if (Helper.keyDownCounter >= Helper.keyDownDuration)
									{
										Helper.isChecking = true;
										Game.LogTrivial("Counting ammo...");
										if (Helper.MainPlayer.IsAiming)
										{
											GameFiber.Wait(300);
										}
										MagazineHandling.GetMagazine();
										DisplayHandling.DisplayCount(textBar);
									}
								}
							}
						}
					}
					catch (ThreadAbortException)
					{
					}
					catch (Exception e2)
					{
						Helper.LogException(e2, "EntryPoint.Initialize() GF1");
					}
				}, "EntryPoint.Initialize() GF1");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "EntryPoint.Initialize()");
			}
		}
	}
}
