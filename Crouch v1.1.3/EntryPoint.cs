using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Forms;
using Crouch.Essential;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace Crouch
{
	
	public static class EntryPoint
	{
		
		
		internal static Ped MainPlayer
		{
			get
			{
				return Game.LocalPlayer.Character;
			}
		}

		
		public static void Main()
		{
			try
			{
				Settings.Initialize();
				UpdateChecker.Initialize();
				int num = 0;
				GameFiber.StartNew(delegate()
				{
					try
					{
						for (;;)
						{
							GameFiber.Yield();
							if (Settings.CrouchKey == Keys.LControlKey)
							{
								if (EntryPoint.<>o__7.<>p__0 == null)
								{
									EntryPoint.<>o__7.<>p__0 = CallSite<Action<CallSite, object, int, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_CONTROL_ACTION", null, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								EntryPoint.<>o__7.<>p__0.Target(EntryPoint.<>o__7.<>p__0, NativeFunction.Natives, 0, 36, true);
							}
							if (EntryPoint.isCrouched)
							{
								if (EntryPoint.<>o__7.<>p__1 == null)
								{
									EntryPoint.<>o__7.<>p__1 = CallSite<Action<CallSite, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "xDE2EF5DA284CC8DF", null, typeof(EntryPoint), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
									}));
								}
								EntryPoint.<>o__7.<>p__1.Target(EntryPoint.<>o__7.<>p__1, NativeFunction.Natives);
							}
							int num;
							if (EntryPoint.MainPlayer.IsInAnyVehicle(false) && EntryPoint.isCrouched)
							{
								num = 0;
								EntryPoint.NormalWalk();
								EntryPoint.isCrouched = false;
								Game.LogTrivial("Player is not crouched anymore");
							}
							if ((Game.IsKeyDown(Settings.CrouchKey) || (Game.IsControllerButtonDown(Settings.ControllerCrouchButton) && Settings.AllowController)) && EntityExtensions.Exists(EntryPoint.MainPlayer) && EntryPoint.MainPlayer.IsOnFoot && !Game.IsKeyDown(Keys.RMenu))
							{
								if (num % 2 == 0)
								{
									EntryPoint.CrouchPlayer();
									EntryPoint.CrouchedForce = !EntryPoint.CrouchedForce;
									if (EntryPoint.CrouchedForce)
									{
										GameFiber.StartNew(new ThreadStart(EntryPoint.CrouchLoop));
									}
									num = num;
									num++;
								}
								else
								{
									EntryPoint.NormalWalk();
									if (EntryPoint.<>o__7.<>p__3 == null)
									{
										EntryPoint.<>o__7.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
										}));
									}
									Func<CallSite, object, bool> target = EntryPoint.<>o__7.<>p__3.Target;
									CallSite <>p__ = EntryPoint.<>o__7.<>p__3;
									if (EntryPoint.<>o__7.<>p__2 == null)
									{
										EntryPoint.<>o__7.<>p__2 = CallSite<Func<CallSite, object, Ped, string, string, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "HAS_ENTITY_ANIM_FINISHED", new Type[]
										{
											typeof(bool)
										}, typeof(EntryPoint), new CSharpArgumentInfo[]
										{
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
											CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
										}));
									}
									if (target(<>p__, EntryPoint.<>o__7.<>p__2.Target(EntryPoint.<>o__7.<>p__2, NativeFunction.Natives, EntryPoint.MainPlayer, "move_ped_crouched", "move_ped_crouched", 3)))
									{
										if (EntryPoint.<>o__7.<>p__4 == null)
										{
											EntryPoint.<>o__7.<>p__4 = CallSite<Action<CallSite, object, Ped, string, bool, int, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "FORCE_PED_MOTION_STATE", null, typeof(EntryPoint), new CSharpArgumentInfo[]
											{
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
												CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
											}));
										}
										EntryPoint.<>o__7.<>p__4.Target(EntryPoint.<>o__7.<>p__4, NativeFunction.Natives, EntryPoint.MainPlayer, "0xEE717723", false, 1, false);
									}
									num = num;
									num++;
								}
							}
						}
					}
					catch (ThreadAbortException)
					{
					}
					catch (Exception e2)
					{
						Helper.LogToDiscord(e2, "EntryPoint.Main() GF1");
					}
				}, "EntryPoint.Main() GF1");
				GameFiber.StartNew(delegate()
				{
					try
					{
						for (;;)
						{
							GameFiber.Yield();
							if (EntityExtensions.Exists(EntryPoint.MainPlayer) && EntryPoint.MainPlayer.IsStill && EntryPoint.MainPlayer.IsOnFoot && !EntryPoint.mayMove && EntryPoint.isCrouched)
							{
								EntryPoint.mayMove = true;
								GameFiber.Sleep(3000);
								if (EntityExtensions.Exists(EntryPoint.MainPlayer) && EntryPoint.MainPlayer.IsStill && EntryPoint.MainPlayer.IsOnFoot)
								{
									EntryPoint.MainPlayer.Tasks.GoStraightToPosition(EntryPoint.MainPlayer.Position, 1f, EntryPoint.MainPlayer.Heading, 5f, 1);
								}
								EntryPoint.mayMove = false;
							}
						}
					}
					catch (ThreadAbortException)
					{
					}
					catch (Exception e2)
					{
						Helper.LogToDiscord(e2, "EntryPoint.Main() GF2");
					}
				}, "EntryPoint.Main() GF2");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "EntryPoint.Main()");
			}
		}

		
		internal static void NormalWalk()
		{
			try
			{
				if (EntryPoint.<>o__8.<>p__0 == null)
				{
					EntryPoint.<>o__8.<>p__0 = CallSite<Action<CallSite, object, Ped, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_MAX_MOVE_BLEND_RATIO", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__8.<>p__0.Target(EntryPoint.<>o__8.<>p__0, NativeFunction.Natives, EntryPoint.MainPlayer, 1f);
				if (EntryPoint.<>o__8.<>p__1 == null)
				{
					EntryPoint.<>o__8.<>p__1 = CallSite<Action<CallSite, object, Ped, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RESET_PED_MOVEMENT_CLIPSET", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__8.<>p__1.Target(EntryPoint.<>o__8.<>p__1, NativeFunction.Natives, EntryPoint.MainPlayer, 0.55f);
				if (EntryPoint.<>o__8.<>p__2 == null)
				{
					EntryPoint.<>o__8.<>p__2 = CallSite<Action<CallSite, object, Ped>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RESET_PED_STRAFE_CLIPSET", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				EntryPoint.<>o__8.<>p__2.Target(EntryPoint.<>o__8.<>p__2, NativeFunction.Natives, EntryPoint.MainPlayer);
				if (EntryPoint.<>o__8.<>p__3 == null)
				{
					EntryPoint.<>o__8.<>p__3 = CallSite<Action<CallSite, object, Ped, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_CAN_PLAY_AMBIENT_ANIMS", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__8.<>p__3.Target(EntryPoint.<>o__8.<>p__3, NativeFunction.Natives, EntryPoint.MainPlayer, true);
				if (EntryPoint.<>o__8.<>p__4 == null)
				{
					EntryPoint.<>o__8.<>p__4 = CallSite<Action<CallSite, object, Ped, bool>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_CAN_PLAY_AMBIENT_BASE_ANIMS", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__8.<>p__4.Target(EntryPoint.<>o__8.<>p__4, NativeFunction.Natives, EntryPoint.MainPlayer, true);
				if (EntryPoint.<>o__8.<>p__5 == null)
				{
					EntryPoint.<>o__8.<>p__5 = CallSite<Action<CallSite, object, Ped>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "RESET_PED_WEAPON_MOVEMENT_CLIPSET", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				EntryPoint.<>o__8.<>p__5.Target(EntryPoint.<>o__8.<>p__5, NativeFunction.Natives, EntryPoint.MainPlayer);
				EntryPoint.isCrouched = false;
			}
			catch (ThreadAbortException)
			{
				Game.LogTrivial("Prevented thread abortion exception in NormalWalk");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "EntryPoint.NormalWalk()");
			}
		}

		
		internal static void SetupCrouch()
		{
			for (;;)
			{
				if (EntryPoint.<>o__9.<>p__2 == null)
				{
					EntryPoint.<>o__9.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target = EntryPoint.<>o__9.<>p__2.Target;
				CallSite <>p__ = EntryPoint.<>o__9.<>p__2;
				if (EntryPoint.<>o__9.<>p__1 == null)
				{
					EntryPoint.<>o__9.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object> target2 = EntryPoint.<>o__9.<>p__1.Target;
				CallSite <>p__2 = EntryPoint.<>o__9.<>p__1;
				if (EntryPoint.<>o__9.<>p__0 == null)
				{
					EntryPoint.<>o__9.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "HAS_ANIM_SET_LOADED", new Type[]
					{
						typeof(bool)
					}, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				if (!target(<>p__, target2(<>p__2, EntryPoint.<>o__9.<>p__0.Target(EntryPoint.<>o__9.<>p__0, NativeFunction.Natives, "move_ped_crouched"))))
				{
					break;
				}
				GameFiber.Wait(5);
				if (EntryPoint.<>o__9.<>p__3 == null)
				{
					EntryPoint.<>o__9.<>p__3 = CallSite<Action<CallSite, object, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "REQUEST_ANIM_SET", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__9.<>p__3.Target(EntryPoint.<>o__9.<>p__3, NativeFunction.Natives, "move_ped_crouched");
			}
		}

		
		internal static void RemoveCrouchAnim()
		{
			if (EntryPoint.<>o__10.<>p__0 == null)
			{
				EntryPoint.<>o__10.<>p__0 = CallSite<Action<CallSite, object, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "REMOVE_ANIM_DICT", null, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			EntryPoint.<>o__10.<>p__0.Target(EntryPoint.<>o__10.<>p__0, NativeFunction.Natives, "move_ped_crouched");
		}

		
		internal static bool CanCrouch()
		{
			if (EntryPoint.<>o__11.<>p__3 == null)
			{
				EntryPoint.<>o__11.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, bool> target = EntryPoint.<>o__11.<>p__3.Target;
			CallSite <>p__ = EntryPoint.<>o__11.<>p__3;
			bool flag = EntryPoint.MainPlayer.IsOnFoot && !EntryPoint.MainPlayer.IsJumping && !EntryPoint.MainPlayer.IsFalling;
			object arg2;
			if (flag)
			{
				if (EntryPoint.<>o__11.<>p__2 == null)
				{
					EntryPoint.<>o__11.<>p__2 = CallSite<Func<CallSite, bool, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.And, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, bool, object, object> target2 = EntryPoint.<>o__11.<>p__2.Target;
				CallSite <>p__2 = EntryPoint.<>o__11.<>p__2;
				bool arg = flag;
				if (EntryPoint.<>o__11.<>p__1 == null)
				{
					EntryPoint.<>o__11.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object> target3 = EntryPoint.<>o__11.<>p__1.Target;
				CallSite <>p__3 = EntryPoint.<>o__11.<>p__1;
				if (EntryPoint.<>o__11.<>p__0 == null)
				{
					EntryPoint.<>o__11.<>p__0 = CallSite<Func<CallSite, object, Ped, bool, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PED_DEAD_OR_DYING", new Type[]
					{
						typeof(bool)
					}, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				arg2 = target2(<>p__2, arg, target3(<>p__3, EntryPoint.<>o__11.<>p__0.Target(EntryPoint.<>o__11.<>p__0, NativeFunction.Natives, EntryPoint.MainPlayer, false)));
			}
			else
			{
				arg2 = flag;
			}
			return target(<>p__, arg2);
		}

		
		internal static void CrouchPlayer()
		{
			try
			{
				if (EntryPoint.<>o__12.<>p__0 == null)
				{
					EntryPoint.<>o__12.<>p__0 = CallSite<Action<CallSite, object, Ped, bool, int, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_USING_ACTION_MODE", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__12.<>p__0.Target(EntryPoint.<>o__12.<>p__0, NativeFunction.Natives, EntryPoint.MainPlayer, false, -1, "DEFAULT_ACTION");
				if (EntryPoint.<>o__12.<>p__1 == null)
				{
					EntryPoint.<>o__12.<>p__1 = CallSite<Action<CallSite, object, Ped, string, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_MOVEMENT_CLIPSET", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__12.<>p__1.Target(EntryPoint.<>o__12.<>p__1, NativeFunction.Natives, EntryPoint.MainPlayer, "move_ped_crouched", 0.55f);
				if (EntryPoint.<>o__12.<>p__2 == null)
				{
					EntryPoint.<>o__12.<>p__2 = CallSite<Action<CallSite, object, Ped, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_STRAFE_CLIPSET", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__12.<>p__2.Target(EntryPoint.<>o__12.<>p__2, NativeFunction.Natives, EntryPoint.MainPlayer, "move_ped_crouched_strafing");
				if (EntryPoint.<>o__12.<>p__3 == null)
				{
					EntryPoint.<>o__12.<>p__3 = CallSite<Action<CallSite, object, Ped, string>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_WEAPON_ANIMATION_OVERRIDE", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__12.<>p__3.Target(EntryPoint.<>o__12.<>p__3, NativeFunction.Natives, EntryPoint.MainPlayer, "Ballistic");
				EntryPoint.isCrouched = true;
				EntryPoint.hasAimed = false;
			}
			catch (ThreadAbortException)
			{
				Game.LogTrivial("Prevented thread abortion exception in CrouchPlayer");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "EntryPoint.CrouchPlayer()");
			}
		}

		
		internal static void SetPlayerAimSpeed()
		{
			try
			{
				if (EntryPoint.<>o__13.<>p__0 == null)
				{
					EntryPoint.<>o__13.<>p__0 = CallSite<Action<CallSite, object, Ped, float>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_MAX_MOVE_BLEND_RATIO", null, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				EntryPoint.<>o__13.<>p__0.Target(EntryPoint.<>o__13.<>p__0, NativeFunction.Natives, EntryPoint.MainPlayer, 0.2f);
				EntryPoint.hasAimed = true;
			}
			catch (ThreadAbortException)
			{
				Game.LogTrivial("Prevented thread abortion exception in SetPlayerAimSpeed");
			}
			catch (Exception e)
			{
				Helper.LogException(e, "EntryPoint.SetPlayerAimSpeed()");
			}
		}

		
		internal static bool IsPlayerFreeAimed()
		{
			if (EntryPoint.<>o__14.<>p__0 == null)
			{
				EntryPoint.<>o__14.<>p__0 = CallSite<Func<CallSite, object, Ped, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_PLAYER_FREE_AIMING", new Type[]
				{
					typeof(bool)
				}, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			object obj = EntryPoint.<>o__14.<>p__0.Target(EntryPoint.<>o__14.<>p__0, NativeFunction.Natives, EntryPoint.MainPlayer);
			if (EntryPoint.<>o__14.<>p__3 == null)
			{
				EntryPoint.<>o__14.<>p__3 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			object obj2;
			if (!EntryPoint.<>o__14.<>p__3.Target(EntryPoint.<>o__14.<>p__3, obj))
			{
				if (EntryPoint.<>o__14.<>p__2 == null)
				{
					EntryPoint.<>o__14.<>p__2 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object, object> target = EntryPoint.<>o__14.<>p__2.Target;
				CallSite <>p__ = EntryPoint.<>o__14.<>p__2;
				object arg = obj;
				if (EntryPoint.<>o__14.<>p__1 == null)
				{
					EntryPoint.<>o__14.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "IS_AIM_CAM_ACTIVE", new Type[]
					{
						typeof(bool)
					}, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				obj2 = target(<>p__, arg, EntryPoint.<>o__14.<>p__1.Target(EntryPoint.<>o__14.<>p__1, NativeFunction.Natives));
			}
			else
			{
				obj2 = obj;
			}
			object obj3 = obj2;
			if (EntryPoint.<>o__14.<>p__7 == null)
			{
				EntryPoint.<>o__14.<>p__7 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			if (!EntryPoint.<>o__14.<>p__7.Target(EntryPoint.<>o__14.<>p__7, obj3))
			{
				if (EntryPoint.<>o__14.<>p__6 == null)
				{
					EntryPoint.<>o__14.<>p__6 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target2 = EntryPoint.<>o__14.<>p__6.Target;
				CallSite <>p__2 = EntryPoint.<>o__14.<>p__6;
				if (EntryPoint.<>o__14.<>p__5 == null)
				{
					EntryPoint.<>o__14.<>p__5 = CallSite<Func<CallSite, object, object, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.BinaryOperationLogical, ExpressionType.Or, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, object, object> target3 = EntryPoint.<>o__14.<>p__5.Target;
				CallSite <>p__3 = EntryPoint.<>o__14.<>p__5;
				object arg2 = obj3;
				if (EntryPoint.<>o__14.<>p__4 == null)
				{
					EntryPoint.<>o__14.<>p__4 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "_IS_AIM_CAM_THIRD_PERSON_ACTIVE", new Type[]
					{
						typeof(bool)
					}, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				if (!target2(<>p__2, target3(<>p__3, arg2, EntryPoint.<>o__14.<>p__4.Target(EntryPoint.<>o__14.<>p__4, NativeFunction.Natives))))
				{
					return false;
				}
			}
			return true;
		}

		
		internal static void CrouchLoop()
		{
			try
			{
				EntryPoint.SetupCrouch();
				while (EntryPoint.CrouchedForce)
				{
					bool flag = EntryPoint.CanCrouch();
					if (flag && EntryPoint.isCrouched && EntryPoint.IsPlayerFreeAimed())
					{
						EntryPoint.SetPlayerAimSpeed();
					}
					else if (flag && (!EntryPoint.isCrouched || EntryPoint.hasAimed))
					{
						EntryPoint.CrouchPlayer();
					}
					else if (!flag && EntryPoint.isCrouched)
					{
						EntryPoint.CrouchedForce = false;
						EntryPoint.NormalWalk();
					}
					GameFiber.Wait(5);
				}
				EntryPoint.NormalWalk();
				EntryPoint.RemoveCrouchAnim();
			}
			catch (ThreadAbortException)
			{
				Game.LogTrivial("Prevented thread abortion exception in CrouchLoop");
			}
			catch (Exception ex)
			{
				if (!ex.Message.ToLower().StartsWith("address"))
				{
					Game.LogTrivial(ex.Message);
					Helper.LogException(ex, "EntryPoint.CrouchLoop()");
				}
			}
		}

		
		internal static bool isCrouched;

		
		internal static bool mayMove;

		
		internal static bool CrouchedForce;

		
		internal static bool hasAimed;

		
		internal static int lastCam;
	}
}
