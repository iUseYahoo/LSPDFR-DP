using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using CheckYourAmmo.Essential;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace CheckYourAmmo
{
	
	internal static class MagazineHandling
	{
		
		internal static void GetMagazine()
		{
			try
			{
				if (MagazineHandling.<>o__0.<>p__1 == null)
				{
					MagazineHandling.<>o__0.<>p__1 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(MagazineHandling)));
				}
				Func<CallSite, object, int> target = MagazineHandling.<>o__0.<>p__1.Target;
				CallSite <>p__ = MagazineHandling.<>o__0.<>p__1;
				if (MagazineHandling.<>o__0.<>p__0 == null)
				{
					MagazineHandling.<>o__0.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
					{
						typeof(int)
					}, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				int num = target(<>p__, MagazineHandling.<>o__0.<>p__0.Target(MagazineHandling.<>o__0.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 60309));
				uint hash = Helper.MainPlayer.Inventory.EquippedWeapon.Asset.Hash;
				string text = string.Empty;
				string text2 = "anim_casino_b@amb@casino@games@threecardpoker@ped_male@slouchy_withdrink@01a@play@v02";
				string text3 = "collect_chips";
				if (MagazineHandling.<>o__0.<>p__2 == null)
				{
					MagazineHandling.<>o__0.<>p__2 = CallSite<Func<CallSite, object, uint, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_WEAPONTYPE_GROUP", new Type[]
					{
						typeof(int)
					}, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				object obj = MagazineHandling.<>o__0.<>p__2.Target(MagazineHandling.<>o__0.<>p__2, NativeFunction.Natives, hash);
				if (obj is int)
				{
					int num2 = (int)obj;
					if (num2 <= 416676503)
					{
						if (num2 <= -1212426201)
						{
							if (num2 != -1569042529)
							{
								if (num2 == -1212426201)
								{
									text = "w_sr_sniperrifle_mag1";
								}
							}
							else
							{
								text = string.Empty;
								Helper.hasMagazine = false;
								Helper.allowAnimation = false;
								Helper.allowChecking = false;
							}
						}
						else if (num2 != -957766203)
						{
							if (num2 == 416676503)
							{
								text = "w_pi_pistol_mag1";
							}
						}
						else
						{
							text = "w_sb_microsmg_mag1";
						}
					}
					else if (num2 <= 970310034)
					{
						if (num2 != 860033945)
						{
							if (num2 == 970310034)
							{
								text = "w_ar_carbinerifle_mag1";
							}
						}
						else
						{
							text = string.Empty;
							text2 = "anim_heist@arcade_combined@";
							text3 = "jimmy@_drinking@_idle_a";
							Helper.hasMagazine = false;
						}
					}
					else if (num2 != 1159398588)
					{
						if (num2 == 1548507267)
						{
							text = string.Empty;
							Helper.hasMagazine = false;
							Helper.allowAnimation = false;
							Helper.allowChecking = false;
						}
					}
					else
					{
						text = "w_mg_mg_mag2";
						text2 = "anim_heist@arcade_combined@";
						text3 = "jimmy@_drinking@_idle_c";
						Helper.timeout = 5000;
						Helper.hasMagazine = false;
					}
				}
				else if (obj is uint)
				{
					uint num3 = (uint)obj;
					if (num3 != 2725924767U)
					{
						if (num3 == 4257178988U)
						{
							text = string.Empty;
							Helper.hasMagazine = false;
							Helper.allowAnimation = false;
							Helper.allowChecking = false;
						}
					}
					else
					{
						text = "w_mg_mg_mag2";
					}
				}
				string format = "Weapon hash: {0} from category hash: {1} | hasMagazine: {2} | allowAnimation: {3}";
				object[] array = new object[4];
				array[0] = hash;
				int num4 = 1;
				if (MagazineHandling.<>o__0.<>p__3 == null)
				{
					MagazineHandling.<>o__0.<>p__3 = CallSite<Func<CallSite, object, uint, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_WEAPONTYPE_GROUP", new Type[]
					{
						typeof(int)
					}, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				array[num4] = MagazineHandling.<>o__0.<>p__3.Target(MagazineHandling.<>o__0.<>p__3, NativeFunction.Natives, hash);
				array[2] = Helper.hasMagazine;
				array[3] = Helper.allowAnimation;
				Game.LogTrivial(string.Format(format, array));
				if (Helper.allowAnimation)
				{
					if (Helper.hasMagazine)
					{
						Helper.magazine = new Object(new Model(text), new Vector3(0f, 0f, 0f));
						if (EntityExtensions.Exists(Helper.magazine) && Helper.isChecking && Helper.hasMagazine)
						{
							if (MagazineHandling.<>o__0.<>p__4 == null)
							{
								MagazineHandling.<>o__0.<>p__4 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							MagazineHandling.<>o__0.<>p__4.Target(MagazineHandling.<>o__0.<>p__4, NativeFunction.Natives, Helper.magazine, Helper.MainPlayer, num, 0.02f, -0.02f, 0.05f, 0f, 0f, 270f, true, true, false, false, 2, 1);
							if (MagazineHandling.<>o__0.<>p__6 == null)
							{
								MagazineHandling.<>o__0.<>p__6 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(MagazineHandling)));
							}
							Func<CallSite, object, int> target2 = MagazineHandling.<>o__0.<>p__6.Target;
							CallSite <>p__2 = MagazineHandling.<>o__0.<>p__6;
							if (MagazineHandling.<>o__0.<>p__5 == null)
							{
								MagazineHandling.<>o__0.<>p__5 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_FOLLOW_PED_CAM_VIEW_MODE", new Type[]
								{
									typeof(int)
								}, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							Helper.curViewCam = target2(<>p__2, MagazineHandling.<>o__0.<>p__5.Target(MagazineHandling.<>o__0.<>p__5, NativeFunction.Natives));
							MagazineHandling.AttachMagazine();
						}
					}
					Helper.MainPlayer.Tasks.PlayAnimation(new AnimationDictionary(text2), text3, Helper.timeout, 2.5f, -1.8f, 0f, 48);
				}
				if (MagazineHandling.<>o__0.<>p__13 == null)
				{
					MagazineHandling.<>o__0.<>p__13 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target3 = MagazineHandling.<>o__0.<>p__13.Target;
				CallSite <>p__3 = MagazineHandling.<>o__0.<>p__13;
				bool flag = EntityExtensions.Exists(Helper.MainPlayer);
				object obj2;
				if (flag)
				{
					if (MagazineHandling.<>o__0.<>p__8 == null)
					{
						MagazineHandling.<>o__0.<>p__8 = CallSite<Func<CallSite, bool, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
						}));
					}
					Func<CallSite, bool, object, object> target4 = MagazineHandling.<>o__0.<>p__8.Target;
					CallSite <>p__4 = MagazineHandling.<>o__0.<>p__8;
					bool arg = flag;
					if (MagazineHandling.<>o__0.<>p__7 == null)
					{
						MagazineHandling.<>o__0.<>p__7 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
						{
							typeof(bool)
						}, typeof(MagazineHandling), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					obj2 = target4(<>p__4, arg, MagazineHandling.<>o__0.<>p__7.Target(MagazineHandling.<>o__0.<>p__7, NativeFunction.Natives, Helper.MainPlayer, 6));
				}
				else
				{
					obj2 = flag;
				}
				object obj3 = obj2;
				if (MagazineHandling.<>o__0.<>p__10 == null)
				{
					MagazineHandling.<>o__0.<>p__10 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				object obj4;
				if (!MagazineHandling.<>o__0.<>p__10.Target(MagazineHandling.<>o__0.<>p__10, obj3))
				{
					if (MagazineHandling.<>o__0.<>p__9 == null)
					{
						MagazineHandling.<>o__0.<>p__9 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					obj4 = MagazineHandling.<>o__0.<>p__9.Target(MagazineHandling.<>o__0.<>p__9, obj3, Helper.isChecking);
				}
				else
				{
					obj4 = obj3;
				}
				obj = obj4;
				if (MagazineHandling.<>o__0.<>p__12 == null)
				{
					MagazineHandling.<>o__0.<>p__12 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				object arg2;
				if (!MagazineHandling.<>o__0.<>p__12.Target(MagazineHandling.<>o__0.<>p__12, obj))
				{
					if (MagazineHandling.<>o__0.<>p__11 == null)
					{
						MagazineHandling.<>o__0.<>p__11 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					arg2 = MagazineHandling.<>o__0.<>p__11.Target(MagazineHandling.<>o__0.<>p__11, obj, Helper.hasMagazine);
				}
				else
				{
					arg2 = obj;
				}
				if (target3(<>p__3, arg2))
				{
					GameFiber.StartNew(delegate()
					{
						for (;;)
						{
							GameFiber.Yield();
							if (MagazineHandling.<>o__0.<>p__18 == null)
							{
								MagazineHandling.<>o__0.<>p__18 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							Func<CallSite, object, bool> target5 = MagazineHandling.<>o__0.<>p__18.Target;
							CallSite <>p__5 = MagazineHandling.<>o__0.<>p__18;
							if (MagazineHandling.<>o__0.<>p__15 == null)
							{
								MagazineHandling.<>o__0.<>p__15 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							Func<CallSite, object, int, object> target6 = MagazineHandling.<>o__0.<>p__15.Target;
							CallSite <>p__6 = MagazineHandling.<>o__0.<>p__15;
							if (MagazineHandling.<>o__0.<>p__14 == null)
							{
								MagazineHandling.<>o__0.<>p__14 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_FOLLOW_PED_CAM_VIEW_MODE", new Type[]
								{
									typeof(int)
								}, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
								}));
							}
							object obj5 = target6(<>p__6, MagazineHandling.<>o__0.<>p__14.Target(MagazineHandling.<>o__0.<>p__14, NativeFunction.Natives), 4);
							if (MagazineHandling.<>o__0.<>p__17 == null)
							{
								MagazineHandling.<>o__0.<>p__17 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
								}));
							}
							object arg3;
							if (!MagazineHandling.<>o__0.<>p__17.Target(MagazineHandling.<>o__0.<>p__17, obj5))
							{
								if (MagazineHandling.<>o__0.<>p__16 == null)
								{
									MagazineHandling.<>o__0.<>p__16 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								arg3 = MagazineHandling.<>o__0.<>p__16.Target(MagazineHandling.<>o__0.<>p__16, obj5, Helper.curViewCam != 4);
							}
							else
							{
								arg3 = obj5;
							}
							if (target5(<>p__5, arg3))
							{
								MagazineHandling.AttachMagazine();
								Helper.curViewCam = 4;
							}
							else
							{
								if (MagazineHandling.<>o__0.<>p__23 == null)
								{
									MagazineHandling.<>o__0.<>p__23 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MagazineHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								Func<CallSite, object, bool> target7 = MagazineHandling.<>o__0.<>p__23.Target;
								CallSite <>p__7 = MagazineHandling.<>o__0.<>p__23;
								if (MagazineHandling.<>o__0.<>p__20 == null)
								{
									MagazineHandling.<>o__0.<>p__20 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.NotEqual, typeof(MagazineHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								Func<CallSite, object, int, object> target8 = MagazineHandling.<>o__0.<>p__20.Target;
								CallSite <>p__8 = MagazineHandling.<>o__0.<>p__20;
								if (MagazineHandling.<>o__0.<>p__19 == null)
								{
									MagazineHandling.<>o__0.<>p__19 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_FOLLOW_PED_CAM_VIEW_MODE", new Type[]
									{
										typeof(int)
									}, typeof(MagazineHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								obj5 = target8(<>p__8, MagazineHandling.<>o__0.<>p__19.Target(MagazineHandling.<>o__0.<>p__19, NativeFunction.Natives), 4);
								if (MagazineHandling.<>o__0.<>p__22 == null)
								{
									MagazineHandling.<>o__0.<>p__22 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(MagazineHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
									}));
								}
								object arg4;
								if (!MagazineHandling.<>o__0.<>p__22.Target(MagazineHandling.<>o__0.<>p__22, obj5))
								{
									if (MagazineHandling.<>o__0.<>p__21 == null)
									{
										MagazineHandling.<>o__0.<>p__21 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
										}));
									}
									arg4 = MagazineHandling.<>o__0.<>p__21.Target(MagazineHandling.<>o__0.<>p__21, obj5, Helper.curViewCam == 4);
								}
								else
								{
									arg4 = obj5;
								}
								if (target7(<>p__7, arg4))
								{
									MagazineHandling.AttachMagazine();
									Helper.curViewCam = 3;
								}
							}
						}
					});
				}
			}
			catch (Exception e)
			{
				Helper.LogException(e, "MagazineHandling.GetMagazine()");
			}
		}

		
		private static void AttachMagazine()
		{
			if (MagazineHandling.<>o__1.<>p__6 == null)
			{
				MagazineHandling.<>o__1.<>p__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MagazineHandling), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, bool> target = MagazineHandling.<>o__1.<>p__6.Target;
			CallSite <>p__ = MagazineHandling.<>o__1.<>p__6;
			bool flag = EntityExtensions.Exists(Helper.magazine) && EntityExtensions.Exists(Helper.MainPlayer);
			object obj;
			if (flag)
			{
				if (MagazineHandling.<>o__1.<>p__1 == null)
				{
					MagazineHandling.<>o__1.<>p__1 = CallSite<Func<CallSite, bool, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, bool, object, object> target2 = MagazineHandling.<>o__1.<>p__1.Target;
				CallSite <>p__2 = MagazineHandling.<>o__1.<>p__1;
				bool arg = flag;
				if (MagazineHandling.<>o__1.<>p__0 == null)
				{
					MagazineHandling.<>o__1.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_ARMED", new Type[]
					{
						typeof(bool)
					}, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				obj = target2(<>p__2, arg, MagazineHandling.<>o__1.<>p__0.Target(MagazineHandling.<>o__1.<>p__0, NativeFunction.Natives, Helper.MainPlayer, 6));
			}
			else
			{
				obj = flag;
			}
			object obj2 = obj;
			if (MagazineHandling.<>o__1.<>p__3 == null)
			{
				MagazineHandling.<>o__1.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(MagazineHandling), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			object obj3;
			if (!MagazineHandling.<>o__1.<>p__3.Target(MagazineHandling.<>o__1.<>p__3, obj2))
			{
				if (MagazineHandling.<>o__1.<>p__2 == null)
				{
					MagazineHandling.<>o__1.<>p__2 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				obj3 = MagazineHandling.<>o__1.<>p__2.Target(MagazineHandling.<>o__1.<>p__2, obj2, Helper.isChecking);
			}
			else
			{
				obj3 = obj2;
			}
			object obj4 = obj3;
			if (MagazineHandling.<>o__1.<>p__5 == null)
			{
				MagazineHandling.<>o__1.<>p__5 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsFalse, typeof(MagazineHandling), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			object arg2;
			if (!MagazineHandling.<>o__1.<>p__5.Target(MagazineHandling.<>o__1.<>p__5, obj4))
			{
				if (MagazineHandling.<>o__1.<>p__4 == null)
				{
					MagazineHandling.<>o__1.<>p__4 = CallSite<Func<CallSite, object, bool, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				arg2 = MagazineHandling.<>o__1.<>p__4.Target(MagazineHandling.<>o__1.<>p__4, obj4, Helper.hasMagazine);
			}
			else
			{
				arg2 = obj4;
			}
			if (target(<>p__, arg2))
			{
				if (MagazineHandling.<>o__1.<>p__8 == null)
				{
					MagazineHandling.<>o__1.<>p__8 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(MagazineHandling)));
				}
				Func<CallSite, object, int> target3 = MagazineHandling.<>o__1.<>p__8.Target;
				CallSite <>p__3 = MagazineHandling.<>o__1.<>p__8;
				if (MagazineHandling.<>o__1.<>p__7 == null)
				{
					MagazineHandling.<>o__1.<>p__7 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_BONE_INDEX", new Type[]
					{
						typeof(int)
					}, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				int num = target3(<>p__3, MagazineHandling.<>o__1.<>p__7.Target(MagazineHandling.<>o__1.<>p__7, NativeFunction.Natives, Helper.MainPlayer, 60309));
				uint hash = Helper.MainPlayer.Inventory.EquippedWeapon.Asset.Hash;
				if (MagazineHandling.<>o__1.<>p__11 == null)
				{
					MagazineHandling.<>o__1.<>p__11 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target4 = MagazineHandling.<>o__1.<>p__11.Target;
				CallSite <>p__4 = MagazineHandling.<>o__1.<>p__11;
				if (MagazineHandling.<>o__1.<>p__10 == null)
				{
					MagazineHandling.<>o__1.<>p__10 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Func<CallSite, object, int, object> target5 = MagazineHandling.<>o__1.<>p__10.Target;
				CallSite <>p__5 = MagazineHandling.<>o__1.<>p__10;
				if (MagazineHandling.<>o__1.<>p__9 == null)
				{
					MagazineHandling.<>o__1.<>p__9 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_FOLLOW_PED_CAM_VIEW_MODE", new Type[]
					{
						typeof(int)
					}, typeof(MagazineHandling), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				if (target4(<>p__4, target5(<>p__5, MagazineHandling.<>o__1.<>p__9.Target(MagazineHandling.<>o__1.<>p__9, NativeFunction.Natives), 4)))
				{
					Game.LogTrivial("Switched magazine position to first person mode");
					if (MagazineHandling.<>o__1.<>p__12 == null)
					{
						MagazineHandling.<>o__1.<>p__12 = CallSite<Func<CallSite, object, uint, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_WEAPONTYPE_GROUP", new Type[]
						{
							typeof(int)
						}, typeof(MagazineHandling), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					obj4 = MagazineHandling.<>o__1.<>p__12.Target(MagazineHandling.<>o__1.<>p__12, NativeFunction.Natives, hash);
					if (obj4 is int)
					{
						int num2 = (int)obj4;
						if (num2 == -1212426201)
						{
							if (MagazineHandling.<>o__1.<>p__16 == null)
							{
								MagazineHandling.<>o__1.<>p__16 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							MagazineHandling.<>o__1.<>p__16.Target(MagazineHandling.<>o__1.<>p__16, NativeFunction.Natives, Helper.magazine, Helper.MainPlayer, num, -0.01f, 0.03f, 0f, 30f, -20f, 280f, true, true, false, false, 2, 1);
							return;
						}
						if (num2 != -957766203)
						{
							if (num2 != 970310034)
							{
								return;
							}
							if (MagazineHandling.<>o__1.<>p__15 == null)
							{
								MagazineHandling.<>o__1.<>p__15 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							MagazineHandling.<>o__1.<>p__15.Target(MagazineHandling.<>o__1.<>p__15, NativeFunction.Natives, Helper.magazine, Helper.MainPlayer, num, -0.02f, 0.05f, 0f, 30f, -20f, 280f, true, true, false, false, 2, 1);
							return;
						}
						else
						{
							if (hash == 324215364U)
							{
								if (MagazineHandling.<>o__1.<>p__13 == null)
								{
									MagazineHandling.<>o__1.<>p__13 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(MagazineHandling), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								MagazineHandling.<>o__1.<>p__13.Target(MagazineHandling.<>o__1.<>p__13, NativeFunction.Natives, Helper.magazine, Helper.MainPlayer, num, 0.01f, -0.01f, 0.07f, 0f, 0f, 270f, true, true, false, false, 2, 1);
								return;
							}
							if (MagazineHandling.<>o__1.<>p__14 == null)
							{
								MagazineHandling.<>o__1.<>p__14 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(MagazineHandling), new CSharpArgumentInfo[]
								{
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
									CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
								}));
							}
							MagazineHandling.<>o__1.<>p__14.Target(MagazineHandling.<>o__1.<>p__14, NativeFunction.Natives, Helper.magazine, Helper.MainPlayer, num, -0.02f, 0.04f, 0.02f, 30f, -20f, 280f, true, true, false, false, 2, 1);
							return;
						}
					}
				}
				else
				{
					Game.LogTrivial("Switched magazine position to third person mode");
					if (MagazineHandling.<>o__1.<>p__17 == null)
					{
						MagazineHandling.<>o__1.<>p__17 = CallSite<<>A<CallSite, object, Object, Ped, int, float, float, float, float, float, float, bool, bool, bool, bool, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ATTACH_ENTITY_TO_ENTITY", null, typeof(MagazineHandling), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
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
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					MagazineHandling.<>o__1.<>p__17.Target(MagazineHandling.<>o__1.<>p__17, NativeFunction.Natives, Helper.magazine, Helper.MainPlayer, num, 0.02f, -0.02f, 0.05f, 0f, 0f, 270f, true, true, false, false, 2, 1);
				}
			}
		}
	}
}
