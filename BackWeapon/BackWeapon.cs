using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace BackWeapon
{
	
	internal class BackWeapon
	{
		
		public static void PlayerLoop()
		{
			Weapon weaponOnBack = null;
			for (;;)
			{
				Ped ped = Game.LocalPlayer.Character;
				weaponOnBack = BackWeapon.Process(weaponOnBack, ped);
				weaponOnBack = BackWeapon.CheckInventory(weaponOnBack, ped);
				if (BackWeapon.hideWhileInVehicle)
				{
					weaponOnBack = BackWeapon.CheckVehicle(weaponOnBack, ped);
				}
				if (weaponOnBack != null && Game.IsKeyDown(BackWeapon.deleteWeaponKey))
				{
					weaponOnBack.Delete();
					weaponOnBack = null;
				}
				GameFiber.Yield();
			}
		}

		
		public static void AIPedsLoop()
		{
			Dictionary<Ped, uint> allPedsCurrentWeapon = new Dictionary<Ped, uint>();
			Dictionary<Ped, Weapon> allPedsWeaponOnBack = new Dictionary<Ped, Weapon>();
			for (;;)
			{
				foreach (Ped ped in World.EnumeratePeds().ToArray<Ped>())
				{
					if (ped && EntityExtensions.Exists(ped) && ped.IsHuman && !ped.IsPlayer && (!BackWeapon.copsOnly || !(ped.RelationshipGroup != RelationshipGroup.Cop)))
					{
						if (BackWeapon.<>o__14.<>p__0 == null)
						{
							BackWeapon.<>o__14.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_IS_TASK_ACTIVE", new Type[]
							{
								typeof(bool)
							}, typeof(BackWeapon), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						object task = BackWeapon.<>o__14.<>p__0.Target(BackWeapon.<>o__14.<>p__0, NativeFunction.Natives, ped, 56);
						if (BackWeapon.<>o__14.<>p__2 == null)
						{
							BackWeapon.<>o__14.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BackWeapon), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						Func<CallSite, object, bool> target = BackWeapon.<>o__14.<>p__2.Target;
						CallSite <>p__ = BackWeapon.<>o__14.<>p__2;
						if (BackWeapon.<>o__14.<>p__1 == null)
						{
							BackWeapon.<>o__14.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(BackWeapon), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
							}));
						}
						if (target(<>p__, BackWeapon.<>o__14.<>p__1.Target(BackWeapon.<>o__14.<>p__1, task)))
						{
							if (BackWeapon.enableBestWeapon)
							{
								if (BackWeapon.<>o__14.<>p__4 == null)
								{
									BackWeapon.<>o__14.<>p__4 = CallSite<Func<CallSite, object, uint>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(uint), typeof(BackWeapon)));
								}
								Func<CallSite, object, uint> target2 = BackWeapon.<>o__14.<>p__4.Target;
								CallSite <>p__2 = BackWeapon.<>o__14.<>p__4;
								if (BackWeapon.<>o__14.<>p__3 == null)
								{
									BackWeapon.<>o__14.<>p__3 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_BEST_PED_WEAPON", new Type[]
									{
										typeof(uint)
									}, typeof(BackWeapon), new CSharpArgumentInfo[]
									{
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
										CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
									}));
								}
								uint bestWeapon = target2(<>p__2, BackWeapon.<>o__14.<>p__3.Target(BackWeapon.<>o__14.<>p__3, NativeFunction.Natives, ped, 0));
								if (!allPedsWeaponOnBack.ContainsKey(ped) && !allPedsCurrentWeapon.ContainsKey(ped) && BackWeapon.aiAcceptedWeapons.Contains(bestWeapon))
								{
									Game.LogTrivial("Adding best weapon to ped from inventory");
									Weapon weaponOnBack = BackWeapon.AIProcess(bestWeapon, ped);
									allPedsWeaponOnBack.Add(ped, weaponOnBack);
								}
							}
							WeaponDescriptor currentWeapon = ped.Inventory.EquippedWeapon;
							Weapon currentWeaponObject = ped.Inventory.EquippedWeaponObject;
							if (currentWeapon != null && currentWeaponObject != null)
							{
								if (BackWeapon.aiAcceptedWeapons.Contains(currentWeapon.Hash) && !allPedsCurrentWeapon.ContainsKey(ped))
								{
									allPedsCurrentWeapon.Add(ped, currentWeapon.Hash);
									Game.LogTrivial("Ped with accepted weapon added");
								}
								if (allPedsWeaponOnBack.ContainsKey(ped) && allPedsWeaponOnBack[ped].Asset.Hash == currentWeaponObject.Asset.Hash)
								{
									allPedsWeaponOnBack[ped].Delete();
									allPedsWeaponOnBack.Remove(ped);
									Game.LogTrivial("Weapon deleted because it was equipped");
								}
								if (allPedsCurrentWeapon.ContainsKey(ped) && currentWeaponObject.Asset.Hash != allPedsCurrentWeapon[ped])
								{
									if (allPedsWeaponOnBack.ContainsKey(ped))
									{
										allPedsWeaponOnBack[ped].Delete();
										allPedsWeaponOnBack.Remove(ped);
										Game.LogTrivial("Weapon deleted because another weapon needs to take its place");
									}
									Game.LogTrivial("Creating weapon on back of ped because ped equipped new weapon");
									Weapon weaponOnBack = BackWeapon.AIProcess(allPedsCurrentWeapon[ped], ped);
									allPedsWeaponOnBack.Add(ped, weaponOnBack);
									allPedsCurrentWeapon.Remove(ped);
								}
							}
							else if (allPedsCurrentWeapon.ContainsKey(ped))
							{
								Weapon weaponOnBack = BackWeapon.AIProcess(allPedsCurrentWeapon[ped], ped);
								allPedsWeaponOnBack.Add(ped, weaponOnBack);
								allPedsCurrentWeapon.Remove(ped);
								Game.LogTrivial("Weapon created on back of ped because ped put weapon away");
							}
						}
					}
				}
				foreach (Ped ped2 in allPedsCurrentWeapon.Keys.ToArray<Ped>())
				{
					if (!EntityExtensions.Exists(ped2))
					{
						allPedsCurrentWeapon.Remove(ped2);
						Game.LogTrivial("Ped with weapon equipped removed because it is no longer valid");
					}
				}
				foreach (Ped ped3 in allPedsWeaponOnBack.Keys.ToArray<Ped>())
				{
					if (!EntityExtensions.Exists(ped3))
					{
						if (EntityExtensions.Exists(allPedsWeaponOnBack[ped3]))
						{
							allPedsWeaponOnBack[ped3].Delete();
						}
						allPedsWeaponOnBack.Remove(ped3);
						Game.LogTrivial("Ped with weapon on back removed because it is no longer valid");
					}
					else
					{
						allPedsWeaponOnBack[ped3] = BackWeapon.CheckInventory(allPedsWeaponOnBack[ped3], ped3);
						if (allPedsWeaponOnBack[ped3] == null)
						{
							allPedsWeaponOnBack.Remove(ped3);
							Game.LogTrivial("Weapon deleted because it is not in ped's inventory");
						}
						else if (BackWeapon.aiHideWhileInVehicle)
						{
							allPedsWeaponOnBack[ped3] = BackWeapon.CheckVehicle(allPedsWeaponOnBack[ped3], ped3);
						}
					}
				}
				GameFiber.Sleep(100);
				GameFiber.Yield();
			}
		}

		
		private static Weapon AIProcess(uint weapontoadd, Ped ped)
		{
			Weapon weaponOnBack = null;
			weaponOnBack = new Weapon(weapontoadd, ped.Position, 30);
			foreach (uint hash in BackWeapon.GetComponentHashes(weapontoadd, ped))
			{
				if (BackWeapon.<>o__15.<>p__0 == null)
				{
					BackWeapon.<>o__15.<>p__0 = CallSite<Action<CallSite, object, Weapon, uint>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GIVE_WEAPON_COMPONENT_TO_WEAPON_OBJECT", null, typeof(BackWeapon), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				BackWeapon.<>o__15.<>p__0.Target(BackWeapon.<>o__15.<>p__0, NativeFunction.Natives, weaponOnBack, hash);
			}
			if (BackWeapon.<>o__15.<>p__2 == null)
			{
				BackWeapon.<>o__15.<>p__2 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(BackWeapon)));
			}
			Func<CallSite, object, int> target = BackWeapon.<>o__15.<>p__2.Target;
			CallSite <>p__ = BackWeapon.<>o__15.<>p__2;
			if (BackWeapon.<>o__15.<>p__1 == null)
			{
				BackWeapon.<>o__15.<>p__1 = CallSite<Func<CallSite, object, Ped, uint, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_WEAPON_TINT_INDEX", new Type[]
				{
					typeof(int)
				}, typeof(BackWeapon), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			int tint = target(<>p__, BackWeapon.<>o__15.<>p__1.Target(BackWeapon.<>o__15.<>p__1, NativeFunction.Natives, ped, weapontoadd));
			if (BackWeapon.<>o__15.<>p__3 == null)
			{
				BackWeapon.<>o__15.<>p__3 = CallSite<Action<CallSite, object, Weapon, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_WEAPON_OBJECT_TINT_INDEX", null, typeof(BackWeapon), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			BackWeapon.<>o__15.<>p__3.Target(BackWeapon.<>o__15.<>p__3, NativeFunction.Natives, weaponOnBack, tint);
			weaponOnBack.AttachTo(ped, ped.GetBoneIndex(24818), BackWeapon.aiOffsetPosition, BackWeapon.aiRotation);
			return weaponOnBack;
		}

		
		private static Weapon Process(Weapon weaponOnBack, Ped ped)
		{
			if (BackWeapon.<>o__16.<>p__0 == null)
			{
				BackWeapon.<>o__16.<>p__0 = CallSite<Func<CallSite, object, Ped, int, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_IS_TASK_ACTIVE", new Type[]
				{
					typeof(bool)
				}, typeof(BackWeapon), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			object task = BackWeapon.<>o__16.<>p__0.Target(BackWeapon.<>o__16.<>p__0, NativeFunction.Natives, ped, 56);
			if (BackWeapon.<>o__16.<>p__2 == null)
			{
				BackWeapon.<>o__16.<>p__2 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BackWeapon), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			Func<CallSite, object, bool> target = BackWeapon.<>o__16.<>p__2.Target;
			CallSite <>p__ = BackWeapon.<>o__16.<>p__2;
			if (BackWeapon.<>o__16.<>p__1 == null)
			{
				BackWeapon.<>o__16.<>p__1 = CallSite<Func<CallSite, object, object>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.Not, typeof(BackWeapon), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			if (target(<>p__, BackWeapon.<>o__16.<>p__1.Target(BackWeapon.<>o__16.<>p__1, task)))
			{
				WeaponDescriptor currentWeapon = ped.Inventory.EquippedWeapon;
				Weapon currentWeaponObject = ped.Inventory.EquippedWeaponObject;
				if (currentWeapon != null && currentWeaponObject != null && BackWeapon.acceptedWeapons.Contains(currentWeapon.Hash))
				{
					if (weaponOnBack != null && weaponOnBack.Asset.Hash == currentWeaponObject.Asset.Hash)
					{
						weaponOnBack.Delete();
						weaponOnBack = null;
					}
					try
					{
						while (ped.Inventory.EquippedWeapon == currentWeapon)
						{
							GameFiber.Yield();
						}
					}
					catch
					{
						return weaponOnBack;
					}
					if (weaponOnBack != null)
					{
						weaponOnBack.Delete();
						weaponOnBack = null;
					}
					weaponOnBack = new Weapon(currentWeapon.Asset, ped.Position, 30);
					foreach (uint hash in BackWeapon.GetComponentHashes(currentWeapon.Hash, ped))
					{
						if (BackWeapon.<>o__16.<>p__3 == null)
						{
							BackWeapon.<>o__16.<>p__3 = CallSite<Action<CallSite, object, Weapon, uint>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "GIVE_WEAPON_COMPONENT_TO_WEAPON_OBJECT", null, typeof(BackWeapon), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
							}));
						}
						BackWeapon.<>o__16.<>p__3.Target(BackWeapon.<>o__16.<>p__3, NativeFunction.Natives, weaponOnBack, hash);
					}
					if (BackWeapon.<>o__16.<>p__5 == null)
					{
						BackWeapon.<>o__16.<>p__5 = CallSite<Func<CallSite, object, int>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(int), typeof(BackWeapon)));
					}
					Func<CallSite, object, int> target2 = BackWeapon.<>o__16.<>p__5.Target;
					CallSite <>p__2 = BackWeapon.<>o__16.<>p__5;
					if (BackWeapon.<>o__16.<>p__4 == null)
					{
						BackWeapon.<>o__16.<>p__4 = CallSite<Func<CallSite, object, Ped, uint, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_PED_WEAPON_TINT_INDEX", new Type[]
						{
							typeof(int)
						}, typeof(BackWeapon), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					int tint = target2(<>p__2, BackWeapon.<>o__16.<>p__4.Target(BackWeapon.<>o__16.<>p__4, NativeFunction.Natives, ped, currentWeapon.Hash));
					if (BackWeapon.<>o__16.<>p__6 == null)
					{
						BackWeapon.<>o__16.<>p__6 = CallSite<Action<CallSite, object, Weapon, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_WEAPON_OBJECT_TINT_INDEX", null, typeof(BackWeapon), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					BackWeapon.<>o__16.<>p__6.Target(BackWeapon.<>o__16.<>p__6, NativeFunction.Natives, weaponOnBack, tint);
					weaponOnBack.AttachTo(ped, ped.GetBoneIndex(24818), BackWeapon.offsetPosition, BackWeapon.Rotation);
					return weaponOnBack;
				}
			}
			return weaponOnBack;
		}

		
		public static List<uint> GetComponentHashes(uint currentWeaponHash, Ped ped)
		{
			List<uint> allComponentHashes = new List<uint>
			{
				3610841222U,
				2608252716U,
				1215999497U,
				2242268665U,
				930927479U,
				3634075224U,
				1319990579U,
				1077065191U,
				2008591151U,
				2053798779U,
				371102273U,
				4081463091U,
				3323197061U,
				4007263587U,
				1351683121U,
				2539772380U,
				2112683568U,
				1062111910U,
				146278587U,
				3800804335U,
				2062808965U,
				2436343040U,
				1530822070U,
				3885209186U,
				4275109233U,
				3978713628U,
				899381934U,
				1709866683U,
				3610841222U,
				119648377U,
				3598405421U,
				3271853210U,
				3328527730U,
				834974250U,
				614078421U,
				2608252716U,
				580369945U,
				3654528146U,
				2805810788U,
				2008591151U,
				384708672U,
				2492708877U,
				3917905123U,
				4169150169U,
				2063610803U,
				2150886575U,
				222992026U,
				1694090795U,
				2053798779U,
				3122911422U,
				3336103030U,
				15712037U,
				284438159U,
				231258687U,
				1108334355U,
				77277509U,
				654802123U,
				3225415071U,
				11918884U,
				176157112U,
				4074914441U,
				288456487U,
				398658626U,
				628697006U,
				925911836U,
				1222307441U,
				552442715U,
				3646023783U,
				21392614U,
				3465283442U,
				2418909806U,
				3870121849U,
				2366665730U,
				3239176998U,
				1246324211U,
				1205768792U,
				2860680127U,
				259780317U,
				2321624822U,
				1996130345U,
				2839309484U,
				2626704212U,
				1308243489U,
				1122574335U,
				1420313469U,
				109848390U,
				593945703U,
				1142457062U,
				3891161322U,
				691432737U,
				987648331U,
				3863286761U,
				3447384986U,
				4202375078U,
				3800418970U,
				730876697U,
				583159708U,
				2366463693U,
				520557834U,
				2499030370U,
				1591132456U,
				634039983U,
				733837882U,
				2248057097U,
				1329061674U,
				2396306288U,
				1140676955U,
				568543123U,
				1550611612U,
				368550800U,
				2525897947U,
				24902297U,
				4066925682U,
				3710005734U,
				3141791350U,
				1301287696U,
				1597093459U,
				1769871776U,
				2467084625U,
				3036451504U,
				438243936U,
				3839888240U,
				740920107U,
				3753350949U,
				1809261196U,
				2648428428U,
				3004802348U,
				3330502162U,
				1135718771U,
				1253942266U,
				1168357051U,
				867832552U,
				3621517063U,
				3410538224U,
				283556395U,
				2637152041U,
				1215999497U,
				643254679U,
				889808635U,
				2043113590U,
				2076495324U,
				1019656791U,
				663170192U,
				2366834608U,
				3141985303U,
				663517359U,
				2227745491U,
				2474561719U,
				1277460590U,
				3112393518U,
				2146055916U,
				3650233061U,
				974903034U,
				190476639U,
				2681951826U,
				3842157419U,
				1038927834U,
				3113485012U,
				3362234491U,
				3725708239U,
				3968886988U,
				48731514U,
				880736428U,
				1303784126U,
				3641720545U,
				2774849419U,
				3298267239U,
				940943685U,
				1263226800U,
				3966931456U,
				1224100642U,
				899228776U,
				616006309U,
				2733014785U,
				572063080U,
				1170588613U,
				966612367U,
				1198425599U,
				3106695545U,
				2850671348U,
				1125642654U,
				860508675U,
				1857603803U,
				202788691U,
				2855028148U,
				3859329886U,
				2732039643U,
				2242268665U,
				2498239431U,
				2260565874U,
				2205435306U,
				3449028929U,
				2676628469U,
				1315288101U,
				3914869031U,
				1004815965U,
				1060929921U,
				2890063729U,
				1602080333U,
				3820854852U,
				387223451U,
				617753366U,
				4072589040U,
				8741501U,
				3693681093U,
				3783533691U,
				3639579478U,
				4012490698U,
				1739501925U,
				1178671645U,
				844049759U,
				2535257853U,
				2294798931U,
				3193891350U,
				2971750299U,
				3689981245U,
				1319990579U,
				2680042476U,
				2433783441U,
				3127044405U,
				2698550338U,
				3634075224U,
				4203716879U,
				2395064697U,
				930927479U,
				3334989185U,
				2089537806U,
				1801039530U,
				1929467122U,
				3315675008U,
				3009973007U,
				2824322168U,
				25766362U,
				4021290536U,
				2183159977U,
				2845636954U,
				4205311469U,
				1130501904U,
				3350057221U,
				1704640795U,
				1005743559U,
				2640679034U,
				2923451831U,
				3104173419U,
				2797881576U,
				2491819116U,
				2318995410U,
				36929477U,
				4026522462U,
				3720197850U,
				3412267557U,
				2826785822U,
				3320426066U,
				382112385U,
				3726614828U,
				2271594122U,
				3724612230U,
				1362433589U,
				1346235024U,
				3328927042U,
				3879097257U,
				4185880635U,
				3557537083U,
				1125852043U,
				886015732U,
				3032680157U,
				3999758885U,
				3750812792U,
				172765678U,
				2312089847U,
				2072122460U,
				2308747125U,
				1377355801U,
				2249208895U,
				3509242479U,
				4012669121U,
				4218476627U,
				2816286296U,
				1675665560U,
				1134861606U,
				1447477866U,
				2434475183U,
				937772107U,
				1401650071U,
				628662130U,
				3309920045U,
				3482022833U,
				2847614993U,
				4234628436U,
				2088750491U,
				2781053842U,
				3115408816U,
				1283078430U,
				1574296533U,
				391640422U,
				1025884839U,
				626875735U,
				1141059345U,
				2201368575U,
				2335983627U,
				1272803094U,
				1080719624U,
				792221348U,
				3842785869U,
				3548192559U,
				2250671235U,
				4095795318U,
				2866892280U,
				2559813981U,
				1796459838U,
				3663056191U,
				1363085923U,
				1509923832U,
				3322377230U,
				4097109892U,
				2182449991U,
				1006677997U,
				3604658878U,
				3791631178U,
				3603274966U,
				2466172125U,
				1227564412U,
				400507625U,
				4133787461U,
				3274096058U,
				696788003U,
				1475288264U,
				3276730932U,
				3051509595U,
				1249283253U,
				3437259709U,
				3197423398U,
				1980349969U,
				1219453777U,
				2441508106U,
				2220186280U,
				457967755U,
				235171324U,
				42685294U,
				3607349581U,
				484812453U,
				3939025520U,
				2613461129U,
				3527687644U,
				3159677559U,
				1077065191U,
				1198478068U,
				2497785294U,
				3872379306U,
				3615105746U,
				1842849902U,
				4100968569U,
				3779763923U,
				1528590652U,
				941317513U,
				1748450780U,
				2425682848U,
				1931539634U,
				1624199183U,
				4268133183U,
				4084561241U,
				423313640U,
				276639596U,
				3303610433U,
				2612118995U,
				996213771U,
				3080918746U,
				4196276776U,
				752418717U,
				247526935U,
				4164277972U,
				1005144310U,
				2313935527U,
				2193687427U,
				3061846192U,
				776198721U,
				1764221345U,
				2425761975U,
				277524638U,
				4164123906U,
				3317620069U,
				3916506229U,
				329939175U,
				643374672U,
				807875052U,
				2893163128U,
				3198471901U,
				3447155842U,
				2881858759U,
				1815270123U,
				3627761985U,
				3439143621U,
				471997210U,
				371102273U,
				296639639U
			};
			List<uint> componentHashes = new List<uint>();
			if (BackWeapon.disableFlashlight)
			{
				foreach (uint hash in new List<uint>
				{
					899381934U,
					1246324211U,
					1140676955U,
					2076495324U
				})
				{
					allComponentHashes.Remove(hash);
				}
			}
			foreach (uint hash2 in allComponentHashes)
			{
				if (BackWeapon.<>o__17.<>p__1 == null)
				{
					BackWeapon.<>o__17.<>p__1 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(BackWeapon), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target = BackWeapon.<>o__17.<>p__1.Target;
				CallSite <>p__ = BackWeapon.<>o__17.<>p__1;
				if (BackWeapon.<>o__17.<>p__0 == null)
				{
					BackWeapon.<>o__17.<>p__0 = CallSite<Func<CallSite, object, Ped, uint, uint, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "HAS_PED_GOT_WEAPON_COMPONENT", new Type[]
					{
						typeof(bool)
					}, typeof(BackWeapon), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				if (target(<>p__, BackWeapon.<>o__17.<>p__0.Target(BackWeapon.<>o__17.<>p__0, NativeFunction.Natives, ped, currentWeaponHash, hash2)))
				{
					componentHashes.Add(hash2);
				}
			}
			return componentHashes;
		}

		
		private static Weapon CheckInventory(Weapon weaponOnBack, Ped ped)
		{
			if (weaponOnBack != null && !ped.Inventory.Weapons.Contains(weaponOnBack.Asset.Hash))
			{
				weaponOnBack.Delete();
				weaponOnBack = null;
			}
			return weaponOnBack;
		}

		
		private static Weapon CheckVehicle(Weapon weaponOnBack, Ped ped)
		{
			if (weaponOnBack != null)
			{
				if (ped.CurrentVehicle != null && weaponOnBack.IsVisible)
				{
					if (BackWeapon.<>o__19.<>p__0 == null)
					{
						BackWeapon.<>o__19.<>p__0 = CallSite<Action<CallSite, object, Weapon, bool, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_ENTITY_VISIBLE", null, typeof(BackWeapon), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					BackWeapon.<>o__19.<>p__0.Target(BackWeapon.<>o__19.<>p__0, NativeFunction.Natives, weaponOnBack, false, 0);
				}
				if (ped.CurrentVehicle == null && !weaponOnBack.IsVisible)
				{
					if (BackWeapon.<>o__19.<>p__1 == null)
					{
						BackWeapon.<>o__19.<>p__1 = CallSite<Action<CallSite, object, Weapon, bool, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_ENTITY_VISIBLE", null, typeof(BackWeapon), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					BackWeapon.<>o__19.<>p__1.Target(BackWeapon.<>o__19.<>p__1, NativeFunction.Natives, weaponOnBack, true, 0);
				}
			}
			return weaponOnBack;
		}

		
		private static Dictionary<string, object> iniValues = ConfigLoader.GetIniValues();

		
		private static List<uint> acceptedWeapons = (List<uint>)BackWeapon.iniValues["AcceptedWeapons"];

		
		private static Vector3 offsetPosition = (Vector3)BackWeapon.iniValues["OffsetPosition"];

		
		private static Rotator Rotation = (Rotator)BackWeapon.iniValues["Rotation"];

		
		private static bool hideWhileInVehicle = (bool)BackWeapon.iniValues["HideWhileInVehicle"];

		
		private static bool disableFlashlight = (bool)BackWeapon.iniValues["DisableFlashlight"];

		
		private static Keys deleteWeaponKey = (Keys)BackWeapon.iniValues["DeleteWeaponKey"];

		
		private static bool copsOnly = (bool)BackWeapon.iniValues["CopsOnly"];

		
		private static List<uint> aiAcceptedWeapons = (List<uint>)BackWeapon.iniValues["AIAcceptedWeapons"];

		
		private static Vector3 aiOffsetPosition = (Vector3)BackWeapon.iniValues["AIOffsetPosition"];

		
		private static Rotator aiRotation = (Rotator)BackWeapon.iniValues["AIRotation"];

		
		private static bool aiHideWhileInVehicle = (bool)BackWeapon.iniValues["AIHideWhileInVehicle"];

		
		private static bool enableBestWeapon = (bool)BackWeapon.iniValues["EnableBestWeapon"];
	}
}
