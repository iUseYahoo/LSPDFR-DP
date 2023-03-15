using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using CheckYourAmmo.Essential;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace CheckYourAmmo.Handling
{
	
	internal static class BaseHandling
	{
		
		internal static void Initialize(dynamic textBar)
		{
			try
			{
				GameFiber.StartNew(delegate()
				{
					for (;;)
					{
						GameFiber.Yield();
						if (BaseHandling.<>o__0.<>p__0 == null)
						{
							BaseHandling.<>o__0.<>p__0 = CallSite<Action<CallSite, object, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISPLAY_AMMO_THIS_FRAME", null, typeof(BaseHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						BaseHandling.<>o__0.<>p__0.Target(BaseHandling.<>o__0.<>p__0, NativeFunction.Natives, false);
						Helper.tbPool.Draw();
						if (BaseHandling.<>o__0.<>p__4 == null)
						{
							BaseHandling.<>o__0.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BaseHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, bool> target = BaseHandling.<>o__0.<>p__4.Target;
						CallSite <>p__ = BaseHandling.<>o__0.<>p__4;
						if (BaseHandling.<>o__0.<>p__1 == null)
						{
							BaseHandling.<>o__0.<>p__1 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
							{
								typeof(bool)
							}, typeof(BaseHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						object obj = BaseHandling.<>o__0.<>p__1.Target(BaseHandling.<>o__0.<>p__1, NativeFunction.Natives, Helper.MainPlayer, 6);
						if (BaseHandling.<>o__0.<>p__3 == null)
						{
							BaseHandling.<>o__0.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(BaseHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						object arg;
						if (!BaseHandling.<>o__0.<>p__3.Target(BaseHandling.<>o__0.<>p__3, obj))
						{
							if (BaseHandling.<>o__0.<>p__2 == null)
							{
								BaseHandling.<>o__0.<>p__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							arg = BaseHandling.<>o__0.<>p__2.Target(BaseHandling.<>o__0.<>p__2, obj, Helper.MainPlayer.Inventory.EquippedWeapon.MagazineSize == 1);
						}
						else
						{
							arg = obj;
						}
						if (target(<>p__, arg))
						{
							Helper.allowChecking = false;
						}
						else
						{
							Helper.allowChecking = true;
						}
						if (BaseHandling.<>o__0.<>p__8 == null)
						{
							BaseHandling.<>o__0.<>p__8 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BaseHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, bool> target2 = BaseHandling.<>o__0.<>p__8.Target;
						CallSite <>p__2 = BaseHandling.<>o__0.<>p__8;
						if (BaseHandling.<>o__0.<>p__5 == null)
						{
							BaseHandling.<>o__0.<>p__5 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
							{
								typeof(bool)
							}, typeof(BaseHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						obj = BaseHandling.<>o__0.<>p__5.Target(BaseHandling.<>o__0.<>p__5, NativeFunction.Natives, Helper.MainPlayer, 6);
						if (BaseHandling.<>o__0.<>p__7 == null)
						{
							BaseHandling.<>o__0.<>p__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(BaseHandling), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						object arg2;
						if (!BaseHandling.<>o__0.<>p__7.Target(BaseHandling.<>o__0.<>p__7, obj))
						{
							if (BaseHandling.<>o__0.<>p__6 == null)
							{
								BaseHandling.<>o__0.<>p__6 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							arg2 = BaseHandling.<>o__0.<>p__6.Target(BaseHandling.<>o__0.<>p__6, obj, Settings.HoldToCheckKey == Keys.R);
						}
						else
						{
							arg2 = obj;
						}
						if (target2(<>p__2, arg2))
						{
							if (BaseHandling.<>o__0.<>p__9 == null)
							{
								BaseHandling.<>o__0.<>p__9 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__9.Target(BaseHandling.<>o__0.<>p__9, NativeFunction.Natives, 0, 45, true);
							if (BaseHandling.<>o__0.<>p__10 == null)
							{
								BaseHandling.<>o__0.<>p__10 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__10.Target(BaseHandling.<>o__0.<>p__10, NativeFunction.Natives, 0, 140, true);
							if (BaseHandling.<>o__0.<>p__11 == null)
							{
								BaseHandling.<>o__0.<>p__11 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__11.Target(BaseHandling.<>o__0.<>p__11, NativeFunction.Natives, 0, 141, true);
							if (BaseHandling.<>o__0.<>p__12 == null)
							{
								BaseHandling.<>o__0.<>p__12 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__12.Target(BaseHandling.<>o__0.<>p__12, NativeFunction.Natives, 0, 142, true);
						}
						if (Helper.isChecking && Helper.allowAnimation)
						{
							if (BaseHandling.<>o__0.<>p__13 == null)
							{
								BaseHandling.<>o__0.<>p__13 = CallSite<Action<CallSite, object, Ped, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_PLAYER_FIRING", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__13.Target(BaseHandling.<>o__0.<>p__13, NativeFunction.Natives, Helper.MainPlayer, true);
							if (BaseHandling.<>o__0.<>p__14 == null)
							{
								BaseHandling.<>o__0.<>p__14 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__14.Target(BaseHandling.<>o__0.<>p__14, NativeFunction.Natives, 0, 21, true);
							if (BaseHandling.<>o__0.<>p__15 == null)
							{
								BaseHandling.<>o__0.<>p__15 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__15.Target(BaseHandling.<>o__0.<>p__15, NativeFunction.Natives, 0, 25, true);
							if (BaseHandling.<>o__0.<>p__16 == null)
							{
								BaseHandling.<>o__0.<>p__16 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__16.Target(BaseHandling.<>o__0.<>p__16, NativeFunction.Natives, 0, 37, true);
							if (BaseHandling.<>o__0.<>p__17 == null)
							{
								BaseHandling.<>o__0.<>p__17 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__17.Target(BaseHandling.<>o__0.<>p__17, NativeFunction.Natives, 0, 45, true);
							if (BaseHandling.<>o__0.<>p__18 == null)
							{
								BaseHandling.<>o__0.<>p__18 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__18.Target(BaseHandling.<>o__0.<>p__18, NativeFunction.Natives, 0, 102, true);
							if (BaseHandling.<>o__0.<>p__19 == null)
							{
								BaseHandling.<>o__0.<>p__19 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__19.Target(BaseHandling.<>o__0.<>p__19, NativeFunction.Natives, 0, 140, true);
							if (BaseHandling.<>o__0.<>p__20 == null)
							{
								BaseHandling.<>o__0.<>p__20 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__20.Target(BaseHandling.<>o__0.<>p__20, NativeFunction.Natives, 0, 141, true);
							if (BaseHandling.<>o__0.<>p__21 == null)
							{
								BaseHandling.<>o__0.<>p__21 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(BaseHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							BaseHandling.<>o__0.<>p__21.Target(BaseHandling.<>o__0.<>p__21, NativeFunction.Natives, 0, 142, true);
						}
					}
				}, "BaseHandling.Initialize() GF1");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "BaseHandling.Initialize()");
			}
		}
	}
}
