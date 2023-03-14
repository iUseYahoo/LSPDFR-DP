using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts
{
	
	[Obfuscation(Exclude = true)]
	internal class RandomCharacter
	{
		
		public static void RandomizeCharacter(Ped ped)
		{
			List<object> _heritage;
			List<object> _facialFeatures;
			List<object> _appearance;
			RandomCharacter.RandomizeCharacter(ped, out _heritage, out _facialFeatures, out _appearance);
		}

		
		public static void RandomizeCharacter(Ped ped, out List<dynamic> heritage, out List<dynamic> facialFeatures, out List<dynamic> appearance)
		{
			heritage = new List<object>
			{
				0,
				0,
				0f,
				0f
			};
			facialFeatures = new List<object>
			{
				1f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f,
				0f
			};
			appearance = new List<object>
			{
				0,
				0,
				0,
				0,
				0,
				0,
				0,
				0
			};
			RandomCharacter.SetRandomFace(ped, ref heritage);
			RandomCharacter.SetRandomFacialFeatures(ped, ref facialFeatures);
			RandomCharacter.SetRandomAppearance(ped, ref appearance);
		}

		
		private static void SetRandomFace(Ped Ped, ref List<dynamic> Heritage)
		{
			bool flag = Ped.Model == new Model("mp_m_freemode_01");
			if (flag)
			{
				RandomCharacter.SetPedFace(Ped, RandomCharacter.Roll.Next(0), RandomCharacter.Roll.Next(20), (float)RandomCharacter.Roll.NextDouble(), (float)RandomCharacter.Roll.NextDouble(), ref Heritage);
			}
			else
			{
				bool flag2 = Ped.Model == new Model("mp_f_freemode_01");
				if (flag2)
				{
					RandomCharacter.SetPedFace(Ped, RandomCharacter.Roll.Next(21, 41), RandomCharacter.Roll.Next(21, 41), (float)RandomCharacter.Roll.NextDouble(), (float)RandomCharacter.Roll.NextDouble(), ref Heritage);
				}
			}
		}

		
		private static void SetPedFace(Ped ped, int parent1, int parent2, float shapemix, float skinmix, ref List<dynamic> Heritage)
		{
			Heritage[0] = parent1;
			Heritage[1] = parent2;
			Heritage[2] = shapemix;
			Heritage[3] = skinmix;
			if (RandomCharacter.<>o__7.<>p__0 == null)
			{
				RandomCharacter.<>o__7.<>p__0 = CallSite<Action<CallSite, object, Ped, int, int, int, int, int, int, float, float, float, bool>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x9414E18B9434C2FE", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			RandomCharacter.<>o__7.<>p__0.Target(RandomCharacter.<>o__7.<>p__0, NativeFunction.Natives, ped, parent1, parent2, 0, parent1, parent2, 0, shapemix, skinmix, 0f, false);
		}

		
		private static void SetRandomFacialFeatures(Ped Ped, ref List<dynamic> FacialFeatures)
		{
			for (int i = 0; i < FacialFeatures.Count; i++)
			{
				int random = RandomCharacter.Roll.Next(2);
				bool flag = random == 1;
				float value;
				if (flag)
				{
					random = RandomCharacter.Roll.Next(2);
					bool flag2 = random == 0;
					if (flag2)
					{
						value = (float)RandomCharacter.Roll.NextDouble() * -1f;
					}
					else
					{
						value = (float)RandomCharacter.Roll.NextDouble();
					}
				}
				else
				{
					value = 0f;
				}
				RandomCharacter.SetPedFacialFeature(Ped, i, value);
				FacialFeatures[i] = value;
			}
		}

		
		private static void SetPedFacialFeature(Ped ped, int facialFeature, float value)
		{
			if (RandomCharacter.<>o__9.<>p__0 == null)
			{
				RandomCharacter.<>o__9.<>p__0 = CallSite<Action<CallSite, object, Ped, int, float>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x71A5C1DBA060049E", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			RandomCharacter.<>o__9.<>p__0.Target(RandomCharacter.<>o__9.<>p__0, NativeFunction.Natives, ped, facialFeature, value);
		}

		
		private static void SetRandomAppearance(Ped ped, ref List<dynamic> Appearance)
		{
			RandomCharacter.SetRandomHair(ped, ref Appearance);
			bool flag = RandomCharacter.Roll.Next(2) == 1;
			if (flag)
			{
				Appearance[3] = RandomCharacter.Roll.Next(5);
			}
			else
			{
				Appearance[3] = 255;
			}
			if (RandomCharacter.<>o__10.<>p__0 == null)
			{
				RandomCharacter.<>o__10.<>p__0 = CallSite<Action<CallSite, object, Ped, int, object, float>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x48F44967FA05CC1E", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			RandomCharacter.<>o__10.<>p__0.Target(RandomCharacter.<>o__10.<>p__0, NativeFunction.Natives, ped, 6, Appearance[3], 0.8f);
			bool flag2 = RandomCharacter.Roll.Next(2) == 1;
			if (flag2)
			{
				Appearance[4] = RandomCharacter.Roll.Next(5);
			}
			else
			{
				Appearance[4] = 255;
			}
			if (RandomCharacter.<>o__10.<>p__1 == null)
			{
				RandomCharacter.<>o__10.<>p__1 = CallSite<Action<CallSite, object, Ped, int, object, float>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x48F44967FA05CC1E", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			RandomCharacter.<>o__10.<>p__1.Target(RandomCharacter.<>o__10.<>p__1, NativeFunction.Natives, ped, 9, Appearance[4], 0.8f);
			bool flag3 = RandomCharacter.Roll.Next(2) == 1;
			if (flag3)
			{
				Appearance[5] = RandomCharacter.Roll.Next(5);
			}
			else
			{
				Appearance[5] = 255;
			}
			if (RandomCharacter.<>o__10.<>p__2 == null)
			{
				RandomCharacter.<>o__10.<>p__2 = CallSite<Action<CallSite, object, Ped, int, object, float>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x48F44967FA05CC1E", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			RandomCharacter.<>o__10.<>p__2.Target(RandomCharacter.<>o__10.<>p__2, NativeFunction.Natives, ped, 3, Appearance[5], 0.8f);
			Appearance[6] = RandomCharacter.Roll.Next(7);
			if (RandomCharacter.<>o__10.<>p__3 == null)
			{
				RandomCharacter.<>o__10.<>p__3 = CallSite<Action<CallSite, object, Ped, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x50B56988B170AFDF", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
				}));
			}
			RandomCharacter.<>o__10.<>p__3.Target(RandomCharacter.<>o__10.<>p__3, NativeFunction.Natives, ped, Appearance[6]);
		}

		
		private static void SetRandomHair(Ped ped, ref List<dynamic> Appearance)
		{
			List<int> excludedMaleHairItems = new List<int>
			{
				8,
				22,
				23,
				24,
				25,
				26,
				27,
				28,
				29,
				30
			};
			List<int> excludedFemaleHairItems = new List<int>();
			bool bannedItemFound = false;
			bool flag = ped.Model == new Model("mp_m_freemode_01");
			if (flag)
			{
				int randomHair = RandomCharacter.Roll.Next(NativeFunction.CallByName<int>("GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS", new NativeArgument[]
				{
					ped,
					2
				}) - 1);
				int randomBrow = RandomCharacter.Roll.Next(22);
				for (int i = 0; i < excludedMaleHairItems.Count; i++)
				{
					bool flag2 = randomHair == excludedMaleHairItems[i];
					if (flag2)
					{
						bannedItemFound = true;
					}
					bool flag3 = randomHair != excludedMaleHairItems[i];
					if (flag3)
					{
						bool flag4 = bannedItemFound;
						bannedItemFound = flag4;
					}
				}
				int beardChance = RandomCharacter.Roll.Next(10);
				bool flag5 = beardChance <= 3;
				int randomBeard;
				if (flag5)
				{
					randomBeard = RandomCharacter.Roll.Next(18);
				}
				else
				{
					randomBeard = 255;
				}
				bool flag6 = bannedItemFound;
				if (flag6)
				{
					RandomCharacter.SetRandomHair(ped, ref Appearance);
				}
				else
				{
					Appearance[0] = randomHair;
					Appearance[2] = randomBrow;
					Appearance[7] = randomBeard;
					if (RandomCharacter.<>o__11.<>p__0 == null)
					{
						RandomCharacter.<>o__11.<>p__0 = CallSite<Action<CallSite, object, Ped, int, int, int, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x262B14F48D29DE80", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					RandomCharacter.<>o__11.<>p__0.Target(RandomCharacter.<>o__11.<>p__0, NativeFunction.Natives, ped, 2, randomHair, 0, 2);
					if (RandomCharacter.<>o__11.<>p__1 == null)
					{
						RandomCharacter.<>o__11.<>p__1 = CallSite<Action<CallSite, object, Ped, int, int, float>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x48F44967FA05CC1E", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					RandomCharacter.<>o__11.<>p__1.Target(RandomCharacter.<>o__11.<>p__1, NativeFunction.Natives, ped, 2, randomBrow, 1f);
					if (RandomCharacter.<>o__11.<>p__2 == null)
					{
						RandomCharacter.<>o__11.<>p__2 = CallSite<Action<CallSite, object, Ped, int, int, float>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x48F44967FA05CC1E", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
						}));
					}
					RandomCharacter.<>o__11.<>p__2.Target(RandomCharacter.<>o__11.<>p__2, NativeFunction.Natives, ped, 1, randomBeard, 1f);
					RandomCharacter.RandomHairColor(ped, ref Appearance);
				}
			}
			else
			{
				bool flag7 = ped.Model == new Model("mp_f_freemode_01");
				if (flag7)
				{
					int randomHair = RandomCharacter.Roll.Next(1, NativeFunction.CallByName<int>("GET_NUMBER_OF_PED_DRAWABLE_VARIATIONS", new NativeArgument[]
					{
						ped,
						2
					}) - 1);
					int randomBrow = RandomCharacter.Roll.Next(22);
					for (int j = 0; j <= excludedFemaleHairItems.Count - 1; j++)
					{
						bool flag8 = randomHair == excludedFemaleHairItems[j];
						if (flag8)
						{
							bannedItemFound = true;
						}
						bool flag9 = randomHair != excludedFemaleHairItems[j];
						if (flag9)
						{
							bool flag10 = bannedItemFound;
							bannedItemFound = flag10;
						}
					}
					bool flag11 = bannedItemFound;
					if (flag11)
					{
						RandomCharacter.SetRandomHair(ped, ref Appearance);
					}
					else
					{
						Appearance[0] = randomHair;
						Appearance[2] = randomBrow;
						Appearance[7] = 255;
						if (RandomCharacter.<>o__11.<>p__3 == null)
						{
							RandomCharacter.<>o__11.<>p__3 = CallSite<Action<CallSite, object, Ped, int, int, int, int>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x262B14F48D29DE80", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						RandomCharacter.<>o__11.<>p__3.Target(RandomCharacter.<>o__11.<>p__3, NativeFunction.Natives, ped, 2, randomHair, 0, 2);
						if (RandomCharacter.<>o__11.<>p__4 == null)
						{
							RandomCharacter.<>o__11.<>p__4 = CallSite<Action<CallSite, object, Ped, int, int, float>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x48F44967FA05CC1E", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						RandomCharacter.<>o__11.<>p__4.Target(RandomCharacter.<>o__11.<>p__4, NativeFunction.Natives, ped, 2, randomBrow, 1f);
						RandomCharacter.RandomHairColor(ped, ref Appearance);
					}
				}
			}
		}

		
		private static void RandomHairColor(Ped ped, ref List<dynamic> Appearance)
		{
			List<int> excludedHairColors = new List<int>
			{
				20,
				21,
				22,
				23,
				24,
				25,
				26
			};
			bool bannedColor = false;
			int randomColor = RandomCharacter.Roll.Next(27);
			for (int i = 0; i < excludedHairColors.Count; i++)
			{
				bool flag = randomColor == excludedHairColors[i];
				if (flag)
				{
					bannedColor = true;
				}
				bool flag2 = randomColor != excludedHairColors[i];
				if (flag2)
				{
					bool flag3 = bannedColor;
					bannedColor = flag3;
				}
			}
			bool flag4 = bannedColor;
			if (flag4)
			{
				RandomCharacter.RandomHairColor(ped, ref Appearance);
			}
			else
			{
				Appearance[1] = randomColor;
				if (RandomCharacter.<>o__12.<>p__0 == null)
				{
					RandomCharacter.<>o__12.<>p__0 = CallSite<Action<CallSite, object, Ped, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x4CFFC65454C93A49", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				RandomCharacter.<>o__12.<>p__0.Target(RandomCharacter.<>o__12.<>p__0, NativeFunction.Natives, ped, Appearance[1], Appearance[1]);
				if (RandomCharacter.<>o__12.<>p__1 == null)
				{
					RandomCharacter.<>o__12.<>p__1 = CallSite<Action<CallSite, object, Ped, int, int, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x497BF74A7B9CB952", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				RandomCharacter.<>o__12.<>p__1.Target(RandomCharacter.<>o__12.<>p__1, NativeFunction.Natives, ped, 2, 1, Appearance[1], Appearance[1]);
				if (RandomCharacter.<>o__12.<>p__2 == null)
				{
					RandomCharacter.<>o__12.<>p__2 = CallSite<Action<CallSite, object, Ped, int, int, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "x497BF74A7B9CB952", null, typeof(RandomCharacter), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				RandomCharacter.<>o__12.<>p__2.Target(RandomCharacter.<>o__12.<>p__2, NativeFunction.Natives, ped, 1, 1, Appearance[1], Appearance[1]);
			}
		}

		
		private static string GetHashKey(string value)
		{
			return string.Format("0x{0:x2}", NativeFunction.CallByName<uint>("GET_HASH_KEY", new NativeArgument[]
			{
				value
			}));
		}

		
		private static Random Roll = new Random();

		
		public enum AppearanceIndex
		{
			
			Hairstyle,
			
			Haircolor,
			
			Eyebrows,
			
			Complexion,
			
			MolesFreckles,
			
			Aging,
			
			Eyecolor,
			
			FacialHair
		}

		
		public enum HeadOverlay
		{
			
			Blemishes,
			
			FacialHair,
			
			Eyebrows,
			
			Aging,
			
			Makeup,
			
			Blush,
			
			Complexion,
			
			SunDamage,
			
			Lipstick,
			
			MolesFreckles,
			
			ChestHair,
			
			BodyBlemishes,
			
			AddedBodyBlemishes
		}

		
		public enum HeritageIndex
		{
			
			Mother,
			
			Father,
			
			ShapeMix,
			
			SkinMix
		}
	}
}
