using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using GTA;
using GTA.Native;

namespace WeaponJamming
{
	public class Class1 : Script
	{
		private bool IS_WEAPON_FIREARM(WeaponHash hash)
		{
			return this.weaponRates.ContainsKey(hash);
		}

		private float GET_WEAPON_DEPLETION_RATE(WeaponHash hash)
		{
			bool flag = this.weaponRates.ContainsKey(hash);
			float result;
			if (flag)
			{
				result = this.weaponRates[hash];
			}
			else
			{
				result = 0f;
			}
			return result;
		}

		private string GET_WEAPON_HASH_NAME(WeaponHash hash)
		{
			return Enum.Parse(typeof(WeaponHash), hash.ToString()).ToString();
		}

		private void GET_CONFIG_VALUES_INT(ScriptSettings config, string section, string name, string default_value, char delimiter, int[] array, int array_size)
		{
			string[] array2 = config.GetValue<string>(section, name, default_value).Split(new char[]
			{
				delimiter
			});
			for (int i = 0; i < array_size; i++)
			{
				array[i] = int.Parse(array2[i]);
			}
		}

		public Class1()
		{
			ScriptSettings scriptSettings = ScriptSettings.Load(Application.StartupPath + "\\scripts\\WeaponJamming.ini");
			ScriptSettings values = ScriptSettings.Load(Application.StartupPath + "\\scripts\\WeaponJamming.dat");
			int[] boxBackColor = new int[4];
			int[] boxLowColor = new int[4];
			int[] boxModerateColor = new int[4];
			int[] boxHighColor = new int[4];
			bool boxEnabled = scriptSettings.GetValue<bool>("Box", "BoxEnabled", true);
			float boxX = scriptSettings.GetValue<float>("Box", "BoxX", 0.094f);
			float boxY = scriptSettings.GetValue<float>("Box", "BoxY", 0.797f);
			float boxWidth = scriptSettings.GetValue<float>("Box", "BoxWidth", 0.155f);
			float boxHeight = scriptSettings.GetValue<float>("Box", "BoxHeight", 0.01f);
			bool boxEnabled3 = boxEnabled;
			if (boxEnabled3)
			{
				this.GET_CONFIG_VALUES_INT(scriptSettings, "Box", "BoxBackColor", "255,255,255,100", ',', boxBackColor, 4);
				this.GET_CONFIG_VALUES_INT(scriptSettings, "Box", "BoxLowColor", "255,0,0,100", ',', boxLowColor, 4);
				this.GET_CONFIG_VALUES_INT(scriptSettings, "Box", "BoxModerateColor", "255,255,0,100", ',', boxModerateColor, 4);
				this.GET_CONFIG_VALUES_INT(scriptSettings, "Box", "BoxHighColor", "0,255,0,100", ',', boxHighColor, 4);
			}
			int unjamChance = scriptSettings.GetValue<int>("General", "UnjamChance", 15);
			int dischargeChance = scriptSettings.GetValue<int>("General", "DischargeChance", 20);
			bool showMessages = scriptSettings.GetValue<bool>("General", "ShowMessages", true);
			bool isPersistent = scriptSettings.GetValue<bool>("General", "Persistent", true);
			int unjamCooldown = scriptSettings.GetValue<int>("General", "UnjamCooldown", 1000);
			string[] array = scriptSettings.GetValue<string>("General", "UnjamCondition", "15,30").Split(new char[]
			{
				','
			});
			int minRepairValue = int.Parse(array[0]);
			int maxRepairValue = int.Parse(array[1]);
			string animDict = scriptSettings.GetValue<string>("Animation", "AnimDictionary", "anim@weapons@first_person@aim_rng@generic@pistol@singleshot@str");
			string animName = scriptSettings.GetValue<string>("Animation", "AnimName", "reload_aim");
			float blendInSpeed = scriptSettings.GetValue<float>("Animation", "AnimBlendSpeed", 2f);
			int animDuration = scriptSettings.GetValue<int>("Animation", "AnimDuration", 750);
			bool flag = unjamChance < 0;
			if (flag)
			{
				unjamChance = 0;
			}
			else
			{
				bool flag2 = unjamChance > 100;
				if (flag2)
				{
					unjamChance = 100;
				}
			}
			bool flag3 = dischargeChance < 0;
			if (flag3)
			{
				dischargeChance = 0;
			}
			else
			{
				bool flag4 = dischargeChance > 100;
				if (flag4)
				{
					dischargeChance = 100;
				}
			}
			bool flag5 = minRepairValue < 0;
			if (flag5)
			{
				minRepairValue = 0;
			}
			else
			{
				bool flag6 = minRepairValue > 100;
				if (flag6)
				{
					minRepairValue = 100;
				}
			}
			bool flag7 = maxRepairValue < 0;
			if (flag7)
			{
				maxRepairValue = 0;
			}
			else
			{
				bool flag8 = maxRepairValue > 100;
				if (flag8)
				{
					maxRepairValue = 100;
				}
			}
			bool flag9 = isPersistent && !File.Exists(Application.StartupPath + "\\scripts\\WeaponJamming.dat");
			if (flag9)
			{
				File.Create(Application.StartupPath + "\\scripts\\WeaponJamming.dat");
			}
			foreach (KeyValuePair<WeaponHash, float> keyValuePair in this.weaponRates.ToList<KeyValuePair<WeaponHash, float>>())
			{
				this.weaponRates[keyValuePair.Key] = scriptSettings.GetValue<float>("Rates", this.GET_WEAPON_HASH_NAME(keyValuePair.Key), keyValuePair.Value);
			}
			Function.Call(-3189321952077349130L, new InputArgument[]
			{
				animDict
			});
			base.Tick += delegate(object o, EventArgs e)
			{
				bool isPersistent;
				foreach (object obj in Enum.GetValues(typeof(WeaponHash)))
				{
					WeaponHash weaponHash = (WeaponHash)obj;
					bool flag10 = Game.Player.Character.Weapons.HasWeapon(weaponHash) && !this.weaponData.ContainsKey(weaponHash);
					if (flag10)
					{
						bool flag11 = this.currentMoney != 0 && Game.Player.Money < this.currentMoney;
						if (flag11)
						{
							this.weaponData.Add(weaponHash, 100f);
						}
						else
						{
							int num = this.random.Next(50, 75);
							isPersistent = isPersistent;
							if (isPersistent)
							{
								this.weaponData.Add(weaponHash, values.GetValue<float>("Values", this.GET_WEAPON_HASH_NAME(weaponHash), (float)num));
							}
							else
							{
								this.weaponData.Add(weaponHash, (float)num);
							}
						}
					}
					else
					{
						bool flag12 = !Game.Player.Character.Weapons.HasWeapon(weaponHash) && this.weaponData.ContainsKey(weaponHash);
						if (flag12)
						{
							this.weaponData.Remove(weaponHash);
							bool isPersistent2 = isPersistent;
							if (isPersistent2)
							{
								values.SetValue<float>("Values", this.GET_WEAPON_HASH_NAME(weaponHash), (float)this.random.Next(50, 75));
								values.Save();
							}
						}
					}
				}
				bool flag13 = this.currentMoney != Game.Player.Money;
				if (flag13)
				{
					this.currentMoney = Game.Player.Money;
				}
				bool flag14 = this.IS_WEAPON_FIREARM(Game.Player.Character.Weapons.Current.Hash);
				if (flag14)
				{
					float num2 = this.weaponData[Game.Player.Character.Weapons.Current.Hash];
					bool flag15 = num2 < 0f;
					if (flag15)
					{
						num2 = 0f;
					}
					else
					{
						bool flag16 = num2 > 100f;
						if (flag16)
						{
							num2 = 100f;
						}
					}
					bool boxEnabled2 = boxEnabled;
					if (boxEnabled2)
					{
						bool flag17 = num2 >= 0f && num2 < 25f;
						int num3;
						int num4;
						int num5;
						int num6;
						if (flag17)
						{
							num3 = boxLowColor[0];
							num4 = boxLowColor[1];
							num5 = boxLowColor[2];
							num6 = boxLowColor[3];
						}
						else
						{
							bool flag18 = num2 >= 25f && num2 < 50f;
							if (flag18)
							{
								num3 = boxModerateColor[0];
								num4 = boxModerateColor[1];
								num5 = boxModerateColor[2];
								num6 = boxModerateColor[3];
							}
							else
							{
								num3 = boxHighColor[0];
								num4 = boxHighColor[1];
								num5 = boxHighColor[2];
								num6 = boxHighColor[3];
							}
						}
						Function.Call(4206795403398567152L, new InputArgument[]
						{
							boxX,
							boxY,
							boxWidth,
							boxHeight,
							boxBackColor[0],
							boxBackColor[1],
							boxBackColor[2],
							boxBackColor[3]
						});
						Function.Call(4206795403398567152L, new InputArgument[]
						{
							boxX - (100f - num2) * (boxWidth / 200f),
							boxY,
							boxWidth - (100f - num2) * (boxWidth / 100f),
							boxHeight,
							num3,
							num4,
							num5,
							num6
						});
					}
					bool flag19 = isPersistent && Game.GameTime - this.lastUpdate > 1000;
					if (flag19)
					{
						values.SetValue<float>("Values", this.GET_WEAPON_HASH_NAME(Game.Player.Character.Weapons.Current.Hash), num2);
						values.Save();
						this.lastUpdate = Game.GameTime;
					}
					bool flag20 = num2 <= 0f;
					if (flag20)
					{
						bool flag21 = Game.GameTime - this.lastCurse > 2000 && Game.IsControlPressed(2, 24);
						bool showMessages;
						if (flag21)
						{
							switch (Function.Call<int>(-70476366192974276L, new InputArgument[]
							{
								Game.Player.Character
							}))
							{
							case 0:
								Function.Call(3829013244756636440L, new InputArgument[]
								{
									Game.Player.Character,
									"GENERIC_CURSE_HIGH",
									"MICHAEL_NORMAL",
									"SPEECH_PARAMS_FORCE",
									0
								});
								goto IL_6EE;
							case 1:
								Function.Call(3829013244756636440L, new InputArgument[]
								{
									Game.Player.Character,
									"GENERIC_CURSE_HIGH",
									"FRANKLIN_NORMAL",
									"SPEECH_PARAMS_FORCE",
									0
								});
								goto IL_6EE;
							case 3:
								Function.Call(3829013244756636440L, new InputArgument[]
								{
									Game.Player.Character,
									"GENERIC_CURSE_HIGH",
									"TREVOR_NORMAL",
									"SPEECH_PARAMS_FORCE",
									0
								});
								goto IL_6EE;
							}
							Function.Call(-8213159594590722974L, new InputArgument[]
							{
								Game.Player.Character,
								"GENERIC_CURSE_HIGH",
								"SPEECH_PARAMS_FORCE",
								0
							});
							IL_6EE:
							this.lastCurse = Game.GameTime;
						}
						else
						{
							bool flag22 = Game.IsControlPressed(2, 51) && Game.GameTime - this.lastUnjam > unjamCooldown;
							if (flag22)
							{
								int num7 = this.random.Next(0, 100);
								bool flag23 = num7 > unjamChance;
								if (flag23)
								{
									bool flag24 = dischargeChance > 0 && this.random.Next(0, 100) <= dischargeChance;
									if (flag24)
									{
										Function.Call(-7592965275345899078L, new InputArgument[]
										{
											Game.Player.Character,
											0,
											0,
											0,
											true
										});
									}
									Game.Player.Character.Task.PlayAnimation(animDict, animName, blendInSpeed, animDuration, 48);
									this.lastUnjam = Game.GameTime;
								}
								else
								{
									this.weaponData[Game.Player.Character.Weapons.Current.Hash] = (float)this.random.Next(minRepairValue, maxRepairValue);
									Audio.PlaySoundFrontend("SELECT", "HUD_FRONTEND_DEFAULT_SOUNDSET");
									showMessages = showMessages;
									if (showMessages)
									{
										UI.Notify("Jam cleared!");
									}
								}
							}
						}
						bool showMessages2 = showMessages;
						if (showMessages2)
						{
							Function.Call(-8860350453193909743L, new InputArgument[]
							{
								"STRING"
							});
							Function.Call(7789129354908300458L, new InputArgument[]
							{
								"Press ~INPUT_CONTEXT~ to attempt to unjam your weapon."
							});
							Function.Call(2562546386151446694L, new InputArgument[]
							{
								0,
								0,
								1,
								-1
							});
						}
						Game.Player.DisableFiringThisFrame();
					}
					else
					{
						bool isShooting = Game.Player.Character.IsShooting;
						if (isShooting)
						{
							Dictionary<WeaponHash, float> dictionary = this.weaponData;
							WeaponHash hash = Game.Player.Character.Weapons.Current.Hash;
							dictionary[hash] -= this.GET_WEAPON_DEPLETION_RATE(Game.Player.Character.Weapons.Current.Hash);
							bool flag25 = this.weaponData[Game.Player.Character.Weapons.Current.Hash] < 0f;
							if (flag25)
							{
								this.weaponData[Game.Player.Character.Weapons.Current.Hash] = 0f;
							}
						}
					}
				}
			};
		}

		private Dictionary<WeaponHash, float> weaponData = new Dictionary<WeaponHash, float>();

		private Random random = new Random();

		private int currentMoney = 0;

		private int lastCurse = 0;

		private int lastUnjam = 0;

		private int lastUpdate = 0;

		private Dictionary<WeaponHash, float> weaponRates = new Dictionary<WeaponHash, float>
		{
			{
				453432689,
				0.24f
			},
			{
				1593441988,
				0.24f
			},
			{
				584646201,
				0.28f
			},
			{
				-1716589765,
				0.7f
			},
			{
				-598887786,
				0.4f
			},
			{
				-1045183535,
				0.48f
			},
			{
				324215364,
				0.16f
			},
			{
				736523883,
				0.16f
			},
			{
				-270015777,
				0.2f
			},
			{
				171789620,
				0.2f
			},
			{
				-1074790547,
				0.2f
			},
			{
				-2084633992,
				0.16f
			},
			{
				-1357824103,
				0.16f
			},
			{
				1649403952,
				0.2f
			},
			{
				-1660422300,
				0.16f
			},
			{
				2144741730,
				0.16f
			},
			{
				487013001,
				0.7f
			},
			{
				2017895192,
				0.65f
			},
			{
				-494615257,
				0.6f
			},
			{
				-1654528753,
				0.66f
			},
			{
				-275439685,
				0.55f
			},
			{
				100416529,
				0.56f
			},
			{
				205991906,
				0.96f
			},
			{
				-1076751822,
				0.24f
			},
			{
				-1063057011,
				0.16f
			},
			{
				-771403250,
				0.32f
			},
			{
				2132975508,
				0.24f
			},
			{
				137902532,
				0.2f
			},
			{
				-952879014,
				0.28f
			},
			{
				984333226,
				0.69f
			},
			{
				1627465347,
				0.16f
			},
			{
				-619010992,
				0.2f
			}
		};

		private enum WeaponClass
		{
			None,
			Pistol,
			SMG,
			Shotgun,
			AssaultRifle,
			MG,
			Sniper
		}
	}
}
