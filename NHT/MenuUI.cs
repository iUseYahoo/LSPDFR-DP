using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;
using NativeUI;
using NHT.NHTUI;

namespace NHT
{
	
	public class MenuUI : Script
	{
		
		public MenuUI()
		{
			MenuUI.Inv = new MenuNHT("Inventori", "Select");
			this.SettingsL = new UIMenu("Settings", "");
			MenuUI.Pool.Add(this.ShopFastFood);
			MenuUI.Pool.Add(this.AIShopFastFood);
			MenuUI.Pool.Add(this.ShopDrink);
			MenuUI.Pool.Add(this.ShopAlhocol);
			MenuUI.Pool.Add(this.ShopFruits);
			MenuUI.Pool.Add(MenuUI.Inv);
			MenuUI.Pool.Add(this.SettingsL);
			base.Tick += this.MenuUI_Tick;
			base.KeyDown += this.MenuUI_KeyDown;
			this.ShopItemFastFood();
			this.AIShopItemFastFood();
			this.ShopItemDrink();
			this.ShopItemAlhocol();
			this.ShopItemFruits();
			this.BlipForAdd();
			this.Settings0();
			this.Load();
			this.Optional();
		}

		
		private void MenuUI_KeyDown(object sender, KeyEventArgs e)
		{
			bool flag = e.KeyCode == this.KeySet;
			if (flag)
			{
				this.SettingsL.Visible = !this.SettingsL.Visible;
			}
			bool flag2 = e.KeyCode == this.key && !MenuUI.Pool.IsAnyMenuOpen();
			if (flag2)
			{
				MenuUI.Inv.Visible = !MenuUI.Inv.Visible;
			}
			else
			{
				bool flag3 = e.KeyCode == this.key && MenuUI.Inv.Visible;
				if (flag3)
				{
					MenuUI.Inv.Visible = !MenuUI.Inv.Visible;
				}
			}
			for (int i = 0; i < this.PosShopFastFood.Length; i++)
			{
				this.MenuVis(e, this.PosShopFastFood[i], this.ShopFastFood, 1.2f);
			}
			for (int j = 0; j < this.PosShopDrink.Length; j++)
			{
				this.MenuVis(e, this.PosShopDrink[j], this.ShopDrink, 1.2f);
			}
			for (int k = 0; k < this.PosAIShopFastFood.Length; k++)
			{
				this.MenuVis(e, this.PosAIShopFastFood[k], this.AIShopFastFood, 1.2f);
			}
		}

		
		private void MenuUI_Tick(object sender, EventArgs e)
		{
			MenuUI.Pool.ProcessMenus();
			this.UIDraw();
			bool visible = MenuUI.Inv.Visible;
			if (visible)
			{
				this.Save();
			}
			this.DrawMarker();
			this.PosPlayer = Game.Player.Character.Position;
			this.Max();
			this.FoodBar();
			this.VisibleText(ref this.HungerBar, ref this.Hu, this.HungerSpeed);
			this.VisibleText(ref this.ThirstBar, ref this.Ti, this.ThirstSpeed);
			this.DrunkDrug();
			this.Drug();
		}

		
		public void DrawMarker()
		{
			for (int i = 0; i < this.PosAIShopFastFood.Length; i++)
			{
				World.DrawMarker(1, this.PosAIShopFastFood[i], Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 0.7f), Color.Green);
			}
			for (int j = 0; j < this.PosShopFastFood.Length; j++)
			{
				World.DrawMarker(1, this.PosShopFastFood[j], Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 0.7f), Color.Yellow);
			}
			for (int k = 0; k < this.PosShopDrink.Length; k++)
			{
				World.DrawMarker(1, this.PosShopDrink[k], Vector3.Zero, Vector3.Zero, new Vector3(1f, 1f, 0.7f), Color.Blue);
			}
		}

		
		public void BlipForAdd()
		{
			for (int i = 0; i < this.BlipMarket.Length; i++)
			{
				Blip blip = World.CreateBlip(this.BlipMarket[i]);
				blip.Sprite = 52;
				blip.Color = 0;
				blip.Name = "Store";
				blip.IsShortRange = true;
				this.DelBlip.Add(blip);
			}
		}

		
		public void FoodBar()
		{
			float num = this.HungerBar - 0.1f;
			float num2 = this.ThirstBar - 0.1f;
			this.W = num / 2f;
			this.H = num2 / 2f;
			Function.Call(4206795403398567152L, new InputArgument[]
			{
				this.x,
				this.y - 0.035f,
				0.1f,
				0.03f,
				this.R0,
				this.G0,
				this.B0,
				125
			});
			Function.Call(4206795403398567152L, new InputArgument[]
			{
				this.x,
				this.y,
				0.1f,
				0.03f,
				this.R1,
				this.G1,
				this.B1,
				125
			});
			Function.Call(4206795403398567152L, new InputArgument[]
			{
				this.x + this.W,
				this.y - 0.035f,
				this.HungerBar,
				0.03f,
				this.R0,
				this.G0,
				this.B0,
				255
			});
			Function.Call(4206795403398567152L, new InputArgument[]
			{
				this.x + this.H,
				this.y,
				this.ThirstBar,
				0.03f,
				this.R1,
				this.G1,
				this.B1,
				255
			});
		}

		
		public void VisibleText(ref float Bar, ref int ti, int Speed)
		{
			ti += Speed;
			bool flag = ti >= 4800;
			if (flag)
			{
				Bar -= 0.001f;
				ti = 0;
			}
		}

		
		public void UIDraw()
		{
			UI.DrawTexture(this.Folds + "/scripts/NHT/Sprite/Food.png", 0, 0, 200, new Point(this.PosX, this.PosY), new Size(20, 20));
			UI.DrawTexture(this.Folds + "/scripts/NHT/Sprite/Drink.png", 0, -100, 200, new Point(this.PosX, this.PosY + 24), new Size(20, 20));
		}

		
		public void PressEAdd(Vector3 Pos, float Distantion)
		{
			this.PressE = new UIText("Press ~g~E", new Point(237, 660), 0.5f);
			UISprite Test = new UISprite("\\scripts\\png", "Kup.png", new Size(3, 3), new Point(100, 100));
			base.Tick += delegate(object e, EventArgs ee)
			{
				bool flag = World.GetDistance(Pos, this.PosPlayer) < Distantion;
				if (flag)
				{
					Function.Call(4206795403398567152L, new InputArgument[]
					{
						0.22f,
						0.938f,
						0.1f,
						0.1f,
						10,
						10,
						10,
						255
					});
					this.PressE.Draw();
					Test.Draw();
				}
			};
		}

		
		public void MenuVis(KeyEventArgs e, Vector3 Marker, UIMenu Menu, float Distantion)
		{
			bool flag = World.GetDistance(Marker, this.PosPlayer) < Distantion;
			if (flag)
			{
				this.PressEAdd(Marker, Distantion);
				bool flag2 = e.KeyCode == Keys.E && !MenuUI.Pool.IsAnyMenuOpen();
				if (flag2)
				{
					Menu.Visible = true;
				}
			}
		}

		
		public void Max()
		{
			bool flag = this.HungerBar > this.HungerMax;
			if (flag)
			{
				this.HungerBar = this.HungerMax;
			}
			bool flag2 = this.ThirstBar > this.ThirstMax;
			if (flag2)
			{
				this.ThirstBar = this.ThirstMax;
			}
			bool flag3 = this.HungerBar <= 0f;
			if (flag3)
			{
				Ped character = Game.Player.Character;
				int health = character.Health;
				character.Health = health - 1;
				this.HungerBar = 0f;
			}
			bool flag4 = this.ThirstBar <= 0f;
			if (flag4)
			{
				Ped character2 = Game.Player.Character;
				int health = character2.Health;
				character2.Health = health - 1;
				this.ThirstBar = 0f;
			}
			bool isDead = Game.Player.Character.IsDead;
			if (isDead)
			{
				bool flag5 = this.HungerBar == 0f || this.ThirstBar == 0f;
				if (flag5)
				{
					this.HungerBar = this.HungerMax;
					this.ThirstBar = this.ThirstMax;
				}
			}
		}

		
		public void Item(MenuNHT m, int Count, float Satisfying, string Name, int t = 0, int Quality = 0, MenuUI.Effects E = MenuUI.Effects.Drunk)
		{
			UIMenu SelectMenu = new UIMenu("Actions", "");
			ItemNHT item = new ItemNHT(Name);
			item.Count = Count;
			item.Type = t;
			item.Satisfying = Satisfying;
			item.Quality = Quality;
			bool flag = t == 3;
			if (flag)
			{
				item.Description = string.Format("Качество-~b~{0}%", item.Quality);
			}
			base.Tick += delegate(object e, EventArgs ee)
			{
				Count = item.Count;
				item.SetRightLabel(Convert.ToString(Count));
			};
			UIMenuItem uimenuItem = new UIMenuItem("Use");
			UIMenuItem uimenuItem2 = new UIMenuItem("Delete");
			MenuUI.Pool.Add(SelectMenu);
			m.AddItemNHT(item);
			bool flag2 = t != -1;
			if (flag2)
			{
				SelectMenu.AddItem(uimenuItem);
			}
			SelectMenu.AddItem(uimenuItem2);
			item.Activated += delegate(UIMenu e, UIMenuItem ee)
			{
				m.Visible = !m.Visible;
				SelectMenu.Visible = !SelectMenu.Visible;
			};
			uimenuItem.Activated += delegate(UIMenu e, UIMenuItem ee)
			{
				bool flag3 = t == -1;
				if (!flag3)
				{
					bool flag4 = t == 0;
					if (flag4)
					{
						this.HungerBar += Satisfying;
					}
					else
					{
						bool flag5 = t == 1;
						if (flag5)
						{
							this.ThirstBar += Satisfying;
						}
						else
						{
							bool flag6 = t == 2;
							if (flag6)
							{
								bool flag7 = Function.Call<int>(-147535234440955034L, Array.Empty<InputArgument>()) == (int)E;
								if (flag7)
								{
									MenuUI.Drunk += Satisfying;
									Function.Call(-9013954871696349517L, new InputArgument[]
									{
										MenuUI.Drunk
									});
									GameplayCamera.ShakeAmplitude = MenuUI.Drunk;
								}
								else
								{
									MenuUI.Drunk = Satisfying;
									Function.Call(-9013954871696349517L, new InputArgument[]
									{
										MenuUI.Drunk
									});
									Function.Call(3211975551654944577L, new InputArgument[]
									{
										string.Concat(E)
									});
									GameplayCamera.Shake(7, MenuUI.Drunk);
								}
							}
							else
							{
								bool flag8 = t == 3;
								if (flag8)
								{
									bool flag9 = Function.Call<int>(-147535234440955034L, Array.Empty<InputArgument>()) == 594;
									if (flag9)
									{
										MenuUI.EffectDrugs += 0.01f * (float)Quality;
										Function.Call(-9013954871696349517L, new InputArgument[]
										{
											MenuUI.EffectDrugs
										});
										GameplayCamera.ShakeAmplitude = MenuUI.EffectDrugs;
									}
									else
									{
										MenuUI.EffectDrugs = 0.01f * (float)Quality;
										Function.Call(-9013954871696349517L, new InputArgument[]
										{
											MenuUI.EffectDrugs
										});
										Function.Call(3211975551654944577L, new InputArgument[]
										{
											"Drug_deadman"
										});
										GameplayCamera.Shake(7, MenuUI.EffectDrugs);
									}
								}
							}
						}
					}
				}
				base.<Item>g__ItemDef|4(1);
			};
			uimenuItem2.Activated += delegate(UIMenu e, UIMenuItem ee)
			{
				base.<Item>g__ItemDef|4(item.Count);
			};
		}

		
		public void DrunkDrug()
		{
			bool flag = MenuUI.Drunk != 0f;
			if (flag)
			{
				MenuUI.Drunk -= 0.0002f;
				Function.Call(-9013954871696349517L, new InputArgument[]
				{
					MenuUI.Drunk
				});
				GameplayCamera.ShakeAmplitude = MenuUI.Drunk;
			}
			bool flag2 = MenuUI.Drunk > 0.01f && MenuUI.Drunk < 0.05f;
			if (flag2)
			{
				GameplayCamera.StopShaking();
			}
			bool flag3 = MenuUI.Drunk < 0f;
			if (flag3)
			{
				MenuUI.Drunk = 0f;
			}
			bool flag4 = MenuUI.Drunk > 1.15f;
			if (flag4)
			{
				MenuUI.Drunk = 1.15f;
			}
		}

		
		public void Drug()
		{
			bool flag = MenuUI.EffectDrugs != 0f;
			if (flag)
			{
				MenuUI.EffectDrugs -= 0.0002f;
				Function.Call(-9013954871696349517L, new InputArgument[]
				{
					MenuUI.EffectDrugs
				});
				GameplayCamera.ShakeAmplitude = MenuUI.EffectDrugs;
			}
			bool flag2 = MenuUI.EffectDrugs > 0.01f && MenuUI.EffectDrugs < 0.05f;
			if (flag2)
			{
				GameplayCamera.StopShaking();
			}
			bool flag3 = MenuUI.EffectDrugs < 0f;
			if (flag3)
			{
				MenuUI.EffectDrugs = 0f;
			}
			bool flag4 = MenuUI.EffectDrugs > 1.15f;
			if (flag4)
			{
				MenuUI.EffectDrugs = 1.15f;
			}
		}

		
		public void AddItem(UIMenu m, string Name, float col, int money, int t = 0, int Quality = 0, MenuUI.Effects E = MenuUI.Effects.Drunk)
		{
			UIMenuItem item0 = new UIMenuItem(Name);
			item0.SetRightLabel(string.Format("{0}", money) + "~g~$");
			m.AddItem(item0);
			item0.Activated += delegate(UIMenu e, UIMenuItem ee)
			{
				bool flag = true;
				try
				{
					int num = Convert.ToInt32(Game.GetUserInput("", 2));
					this.li = MenuUI.Inv.MenuItemsNHT;
					for (int i = 0; i < this.li.Count; i++)
					{
						bool flag2 = Game.Player.Money < money * num;
						if (flag2)
						{
							UI.Notify("Not enough ~g~money");
						}
						else
						{
							bool flag3 = this.li[i].Text == Name;
							if (flag3)
							{
								string rightLabel = this.li[i].RightLabel;
								int num2 = num + Convert.ToInt32(rightLabel);
								this.li[i].Count += num;
								flag = false;
								Game.Player.Money -= money * num;
							}
						}
					}
					bool flag4 = flag;
					if (flag4)
					{
						bool flag5 = Game.Player.Money < money * num;
						if (flag5)
						{
							UI.Notify("Not enough ~g~money");
						}
						else
						{
							this.Item(MenuUI.Inv, num, col, item0.Text, t, Quality, E);
							Game.Player.Money -= money * num;
						}
					}
					MenuUI.Inv.RefreshIndex();
				}
				catch
				{
					UI.Notify("Need to write in ~r~numbers");
				}
			};
		}

		
		private void AIShopItemFastFood()
		{
			this.AddItem(this.AIShopFastFood, "Sandwich", 0.026f, 10, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.AIShopFastFood, "Hut Dog", 0.025f, 8, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.AIShopFastFood, "Burito", 0.025f, 9, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.AIShopFastFood, "Hamburger", 0.027f, 12, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.AIShopFastFood, "Pizza", 0.035f, 20, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.AIShopFastFood, "BigHamburger", 0.033f, 22, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.AIShopFastFood, "~r~VeriBigHamburger", 0.05f, 40, 0, 0, MenuUI.Effects.Drunk);
		}

		
		private void ShopItemFastFood()
		{
			this.AddItem(this.ShopFastFood, "Sandwich", 0.026f, 7, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Hut Dog", 0.025f, 5, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Burito", 0.025f, 6, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Hamburger", 0.027f, 8, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Pizza", 0.035f, 15, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "BigHamburger", 0.033f, 14, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "~r~VeriBigHamburger", 0.05f, 30, 0, 0, MenuUI.Effects.Drunk);
		}

		
		private void ShopItemDrink()
		{
			this.AddItem(this.ShopDrink, "Water", 0.038f, 5, 1, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Milk", 0.035f, 7, 1, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Milkshake", 0.025f, 9, 1, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Cola", 0.025f, 8, 1, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Soda", 0.022f, 8, 1, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Juice", 0.026f, 7, 1, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Lemonade", 0.031f, 6, 1, 0, MenuUI.Effects.Drunk);
		}

		
		private void ShopItemAlhocol()
		{
			this.AddItem(this.ShopDrink, "Peer", 0.25f, 10, 2, 25, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Wine", 0.33f, 15, 2, 25, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Absinthe", 0.25f, 20, 2, 24, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Vodka", 0.67f, 25, 2, 67, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Whiskey", 0.33f, 50, 2, 25, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopDrink, "Brandy", 0.35f, 65, 2, 25, MenuUI.Effects.Drunk);
		}

		
		private void ShopItemFruits()
		{
			this.AddItem(this.ShopFastFood, "Banana", 0.025f, 8, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Apple", 0.025f, 6, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Lemon", 0.025f, 8, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Mandarin", 0.025f, 8, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Kiwi", 0.025f, 10, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Pineapple", 0.025f, 14, 0, 0, MenuUI.Effects.Drunk);
			this.AddItem(this.ShopFastFood, "Granet", 0.025f, 10, 0, 0, MenuUI.Effects.Drunk);
		}

		
		public void Save()
		{
			this.li = MenuUI.Inv.MenuItemsNHT;
			StreamWriter streamWriter = new StreamWriter(this.Fold + "\\NHT\\\\GTA.ini", false);
			streamWriter.WriteLine("[Name]");
			for (int i = 0; i < this.li.Count; i++)
			{
				streamWriter.WriteLine(string.Format("Item{0}=", i) + this.li[i].Text);
			}
			streamWriter.WriteLine();
			streamWriter.WriteLine("[Count]");
			for (int j = 0; j < this.li.Count; j++)
			{
				streamWriter.WriteLine(string.Format("Count{0}=", j) + this.li[j].Count);
			}
			streamWriter.WriteLine();
			streamWriter.WriteLine("[Type]");
			for (int k = 0; k < this.li.Count; k++)
			{
				streamWriter.WriteLine(string.Format("Type{0}=", k) + this.li[k].Type);
			}
			streamWriter.WriteLine();
			streamWriter.WriteLine("[Quality]");
			for (int l = 0; l < this.li.Count; l++)
			{
				streamWriter.WriteLine(string.Format("Quality{0}=", l) + this.li[l].Quality);
			}
			streamWriter.WriteLine();
			streamWriter.WriteLine("[Satisfying]");
			for (int m = 0; m < this.li.Count; m++)
			{
				streamWriter.WriteLine(string.Format("Satisfying{0}=", m) + this.li[m].Satisfying);
			}
			streamWriter.WriteLine();
			streamWriter.WriteLine("[ItemCount]");
			streamWriter.WriteLine(string.Format("Count={0}", this.li.Count));
			streamWriter.WriteLine();
			streamWriter.WriteLine("[NHT]");
			streamWriter.WriteLine(string.Format("Hunger={0}", this.HungerBar));
			streamWriter.WriteLine(string.Format("Thirst={0}", this.ThirstBar));
			streamWriter.WriteLine();
			streamWriter.WriteLine();
			streamWriter.Close();
		}

		
		public void Load()
		{
			ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT\\\\GTA.ini");
			int value = scriptSettings.GetValue<int>("ItemCount", "Count", 0);
			for (int i = 0; i < value; i++)
			{
				int value2 = scriptSettings.GetValue<int>("Count", string.Format("Count{0}", i), 0);
				string value3 = scriptSettings.GetValue<string>("Name", string.Format("Item{0}", i), "j");
				int value4 = scriptSettings.GetValue<int>("Type", string.Format("Type{0}", i), 0);
				int value5 = scriptSettings.GetValue<int>("Quality", string.Format("Quality{0}", i), 0);
				float value6 = scriptSettings.GetValue<float>("Satisfying", string.Format("Satisfying{0}", i), 0.02f);
				this.Item(MenuUI.Inv, value2, value6, value3, value4, value5, MenuUI.Effects.Drunk);
			}
			float value7 = scriptSettings.GetValue<float>("NHT", "Hunger", 0.05f);
			float value8 = scriptSettings.GetValue<float>("NHT", "Thirst", 0.05f);
			this.HungerBar = value7;
			this.ThirstBar = value8;
			MenuUI.Inv.RefreshIndex();
		}

		
		public void Optional()
		{
			ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
			this.key = scriptSettings.GetValue<Keys>("Keys", "OpenInventori", Keys.Z);
			this.KeySet = scriptSettings.GetValue<Keys>("Keys", "Settings", Keys.F9);
			this.HungerSpeed = scriptSettings.GetValue<int>("Speed", "Hunger", 5);
			this.ThirstSpeed = scriptSettings.GetValue<int>("Speed", "Thirst", 6);
			this.R0 = scriptSettings.GetValue<int>("ColorHud", "RH", 214);
			this.G0 = scriptSettings.GetValue<int>("ColorHud", "GH", 131);
			this.B0 = scriptSettings.GetValue<int>("ColorHud", "BH", 29);
			this.R1 = scriptSettings.GetValue<int>("ColorHud", "RT", 28);
			this.G1 = scriptSettings.GetValue<int>("ColorHud", "GT", 112);
			this.B1 = scriptSettings.GetValue<int>("ColorHud", "BT", 215);
			this.PosX = scriptSettings.GetValue<int>("PositionUI", "X", 1180);
			float num = (float)Convert.ToInt64(this.PosX) * 0.00078f;
			this.x = num + 0.007f;
			this.PosY = scriptSettings.GetValue<int>("PositionUI", "Y", 592);
			float num2 = (float)Convert.ToInt64(this.PosY) * 0.00139f;
			this.y = num2 + 0.047f;
		}

		
		public void Settings0()
		{
			UIMenu menu = MenuUI.Pool.AddSubMenu(this.SettingsL, "Hud");
			UIMenu uimenu = MenuUI.Pool.AddSubMenu(menu, "Position Hud");
			UIMenu menu2 = MenuUI.Pool.AddSubMenu(menu, "Color Hud");
			UIMenu uimenu2 = MenuUI.Pool.AddSubMenu(menu2, "Color Hud Hunger");
			UIMenu uimenu3 = MenuUI.Pool.AddSubMenu(menu2, "Color Hud Thirst");
			UIMenuItem ButtonColorR0 = new UIMenuItem("Red");
			UIMenuItem ButtonColorG0 = new UIMenuItem("Green");
			UIMenuItem ButtonColorB0 = new UIMenuItem("Blue");
			UIMenuItem ButtonColorR1 = new UIMenuItem("Red");
			UIMenuItem ButtonColorG1 = new UIMenuItem("Green");
			UIMenuItem ButtonColorB1 = new UIMenuItem("Blue");
			UIMenuItem ButtonX = new UIMenuItem("PosX");
			UIMenuItem ButtonY = new UIMenuItem("PosY");
			float fx = 0.00078f;
			float fy = 0.00139f;
			uimenu.AddItem(ButtonX);
			uimenu.AddItem(ButtonY);
			uimenu2.AddItem(ButtonColorB0);
			uimenu2.AddItem(ButtonColorG0);
			uimenu2.AddItem(ButtonColorR0);
			uimenu3.AddItem(ButtonColorB1);
			uimenu3.AddItem(ButtonColorG1);
			uimenu3.AddItem(ButtonColorR1);
			base.Tick += delegate(object e, EventArgs ee)
			{
				ButtonX.SetRightLabel(string.Format("{0}", this.PosX));
				ButtonY.SetRightLabel(string.Format("{0}", this.PosY));
				ButtonColorB0.SetRightLabel(string.Format("{0}", this.B0));
				ButtonColorG0.SetRightLabel(string.Format("{0}", this.G0));
				ButtonColorR0.SetRightLabel(string.Format("{0}", this.R0));
				ButtonColorB1.SetRightLabel(string.Format("{0}", this.B1));
				ButtonColorG1.SetRightLabel(string.Format("{0}", this.G1));
				ButtonColorR1.SetRightLabel(string.Format("{0}", this.R1));
			};
			base.KeyDown += delegate(object e, KeyEventArgs ee)
			{
				bool flag = ButtonX.Selected && ee.KeyCode == Keys.Right;
				if (flag)
				{
					this.PosX++;
					float num = (float)Convert.ToInt64(this.PosX) * fx;
					this.x = num + 0.007f;
					ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
					scriptSettings.SetValue<int>("PositionUI", "X", this.PosX);
					scriptSettings.Save();
				}
				bool flag2 = ButtonX.Selected && ee.KeyCode == Keys.Left;
				if (flag2)
				{
					this.PosX--;
					float num2 = (float)Convert.ToInt64(this.PosX) * fx;
					this.x = num2 + 0.007f;
					ScriptSettings scriptSettings2 = ScriptSettings.Load(this.Fold + "\\NHT.ini");
					scriptSettings2.SetValue<int>("PositionUI", "X", this.PosX);
					scriptSettings2.Save();
				}
				bool flag3 = ButtonY.Selected && ee.KeyCode == Keys.Left;
				if (flag3)
				{
					this.PosY--;
					float num3 = (float)Convert.ToInt64(this.PosY) * fy;
					this.y = num3 + 0.047f;
					ScriptSettings scriptSettings3 = ScriptSettings.Load(this.Fold + "\\NHT.ini");
					scriptSettings3.SetValue<int>("PositionUI", "Y", this.PosY);
					scriptSettings3.Save();
				}
				bool flag4 = ButtonY.Selected && ee.KeyCode == Keys.Right;
				if (flag4)
				{
					this.PosY++;
					float num4 = (float)Convert.ToInt64(this.PosY) * fy;
					this.y = num4 + 0.047f;
					ScriptSettings scriptSettings4 = ScriptSettings.Load(this.Fold + "\\NHT.ini");
					scriptSettings4.SetValue<int>("PositionUI", "Y", this.PosY);
					scriptSettings4.Save();
				}
			};
			ButtonX.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.PosX = Convert.ToInt32(Game.GetUserInput("", 6));
				float num = (float)Convert.ToInt64(this.PosX) * fx;
				this.x = num + 0.007f;
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("PositionUI", "X", this.PosX);
				scriptSettings.Save();
			};
			ButtonY.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.PosY = Convert.ToInt32(Game.GetUserInput("", 6));
				float num = (float)Convert.ToInt64(this.PosY) * fy;
				this.y = num + 0.047f;
				UI.ShowSubtitle(string.Format("{0}", this.y));
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("PositionUI", "Y", this.PosY);
				scriptSettings.Save();
			};
			ButtonColorB0.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.B0 = Convert.ToInt32(Game.GetUserInput("", 6));
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("ColorHud", "BH", this.B0);
				scriptSettings.Save();
			};
			ButtonColorG0.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.G0 = Convert.ToInt32(Game.GetUserInput("", 6));
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("ColorHud", "GH", this.G0);
				scriptSettings.Save();
			};
			ButtonColorR0.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.R0 = Convert.ToInt32(Game.GetUserInput("", 6));
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("ColorHud", "RH", this.R0);
				scriptSettings.Save();
			};
			ButtonColorB1.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.B1 = Convert.ToInt32(Game.GetUserInput("", 6));
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("ColorHud", "BT", this.B1);
				scriptSettings.Save();
			};
			ButtonColorG1.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.G1 = Convert.ToInt32(Game.GetUserInput("", 6));
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("ColorHud", "GT", this.G1);
				scriptSettings.Save();
			};
			ButtonColorR1.Activated += delegate(UIMenu ee, UIMenuItem e)
			{
				this.R1 = Convert.ToInt32(Game.GetUserInput("", 6));
				ScriptSettings scriptSettings = ScriptSettings.Load(this.Fold + "\\NHT.ini");
				scriptSettings.SetValue<int>("ColorHud", "RT", this.R1);
				scriptSettings.Save();
			};
		}

		
		private UIResRectangle ResT = new UIResRectangle();

		
		private List<ItemNHT> ListDelNHT = new List<ItemNHT>();

		
		private List<UIMenu> ListDel = new List<UIMenu>();

		
		private List<ItemNHT> li;

		
		private string Fold = AppDomain.CurrentDomain.BaseDirectory.ToString();

		
		private string Folds = Application.StartupPath;

		
		private string FoldSprite = "/scripts/NTH/Sprite/";

		
		private float ThirstBar = 0.1f;

		
		private float HungerBar = 0.1f;

		
		private int HungerSpeed = 16;

		
		private int ThirstSpeed = 24;

		
		private float ThirstMax = 0.1f;

		
		private float HungerMax = 0.1f;

		
		public static float Drunk = 0f;

		
		private int m = 100;

		
		private int Hu;

		
		private int Ti;

		
		private float W;

		
		private float H;

		
		private float x = 0.93f;

		
		private float y = 0.87f;

		
		private int R0 = 214;

		
		private int G0 = 131;

		
		private int B0 = 29;

		
		private int R1 = 28;

		
		private int G1 = 112;

		
		private int B1 = 215;

		
		private Keys key;

		
		private Keys KeySet;

		
		private UIText PressE;

		
		private List<Blip> DelBlip = new List<Blip>();

		
		private string[] ItemComponemtDrugs = new string[]
		{
			"AS-02",
			"CR2",
			"P21",
			"MEX"
		};

		
		private string[] ItemFoodID0 = new string[]
		{
			"Pizza",
			"Burrito",
			"Hut Dog",
			"Donut",
			"Sandwich",
			"Chips"
		};

		
		private string[] ItemFoodID1 = new string[]
		{
			"Marijuana",
			"Meth",
			"Cocaine"
		};

		
		private string[] ItemFoodID3 = new string[]
		{
			"Cola",
			"Juice",
			"Lemonade",
			"Soda",
			"Milk"
		};

		
		private string[] ItemFoodID4 = new string[]
		{
			"Peer",
			"Vodka",
			"Whiskey"
		};

		
		public static MenuPool Pool = new MenuPool();

		
		public static MenuNHT Inv;

		
		private UIMenu AIShopFastFood = new UIMenu("ShopFood", "Select");

		
		private UIMenu ShopFastFood = new UIMenu("ShopFastFood", "Select");

		
		private UIMenu ShopDrink = new UIMenu("ShopDrink", "Select");

		
		private UIMenu ShopAlhocol = new UIMenu("ShopDrunk", "Select");

		
		private UIMenu ShopFruits = new UIMenu("ShopFruits", "Select");

		
		private UIMenu SettingsL;

		
		private float posx;

		
		private float posy;

		
		private float w;

		
		private float h;

		
		private int PosX;

		
		private int PosY;

		
		private int InPosX;

		
		private int InPosY;

		
		public static float EffectDrugs;

		
		private Vector3[] PosShopDrugs = new Vector3[]
		{
			new Vector3(1390.428f, 3599.825f, 37.96355f)
		};

		
		private Vector3[] PosBaseDrug = new Vector3[]
		{
			new Vector3(1541.019f, 3596.353f, 34.45258f),
			new Vector3(-1165.234f, -1566.779f, 3.451435f),
			new Vector3(-330.3014f, 48.62083f, 53.42983f),
			new Vector3(-1807.042f, -403.1883f, 43.62779f),
			new Vector3(498.9174f, -521.0894f, 23.76115f)
		};

		
		private Vector3[] PosComponentDrugs = new Vector3[]
		{
			new Vector3(-908.5685f, -334.9887f, 37.97893f),
			new Vector3(1481.57f, -1916.43f, 70.43635f),
			new Vector3(1725.292f, 3325.216f, 40.22351f)
		};

		
		private Vector3[] PosGreateDrugs = new Vector3[]
		{
			new Vector3(2129.13f, 776.2463f, 256.9849f)
		};

		
		private Vector3 PosPlayer;

		
		private Vector3[] PosShopFastFood = new Vector3[]
		{
			new Vector3(-51.59321f, -1748.728f, 28.421f),
			new Vector3(32.46356f, -1343.351f, 28.49703f),
			new Vector3(1154.402f, -320.3268f, 68.20512f),
			new Vector3(-715.7748f, -909.4221f, 18.21559f),
			new Vector3(381.2526f, 328.426f, 102.5664f),
			new Vector3(2678.317f, 3288.377f, 54.24114f),
			new Vector3(1965.024f, 3747.369f, 31.34375f),
			new Vector3(541.6506f, 2666.347f, 41.15651f),
			new Vector3(1706.952f, 4928.343f, 41.06366f),
			new Vector3(1736.697f, 6415.279f, 34.03724f),
			new Vector3(-3245.603f, 1008.297f, 11.83071f),
			new Vector3(-3045.216f, 590.9316f, 6.908932f),
			new Vector3(-1829.99f, 790.7025f, 137.2883f),
			new Vector3(2553.593f, 388.8124f, 107.623f)
		};

		
		private Vector3[] PosAIShopFastFood = new Vector3[]
		{
			new Vector3(1135.636f, -980.4771f, 45.41584f),
			new Vector3(-1224.508f, -907.8585f, 11.32635f),
			new Vector3(-1486.096f, -380.5542f, 39.1634f),
			new Vector3(1392.619f, 3604.561f, 33.98092f),
			new Vector3(-2968.318f, 389.1511f, 14.04331f)
		};

		
		private Vector3[] PosShopDrink = new Vector3[]
		{
			new Vector3(-54.36428f, -1748.905f, 28.421f),
			new Vector3(27.2f, -1345.196f, 28.49703f),
			new Vector3(1152.889f, -322.6309f, 68.20512f),
			new Vector3(-717.882f, -911.3918f, 18.21559f),
			new Vector3(-1226.191f, -907.394f, 11.32635f),
			new Vector3(-1486.242f, -382.3165f, 39.1634f),
			new Vector3(375.6419f, 327.8845f, 102.5664f),
			new Vector3(2555.155f, 383.4825f, 107.623f),
			new Vector3(2677.555f, 3282.744f, 54.24114f),
			new Vector3(1961.35f, 3743.029f, 31.34375f),
			new Vector3(546.7109f, 2668.94f, 41.15651f),
			new Vector3(1706.454f, 4931.304f, 41.06367f),
			new Vector3(1731.043f, 6416.038f, 34.03724f),
			new Vector3(-3244.199f, 1002.693f, 11.83071f),
			new Vector3(-3041.749f, 586.2324f, 6.908932f),
			new Vector3(-1830.186f, 787.91f, 137.3207f)
		};

		
		private Vector3[] PosShopAlcohol = new Vector3[]
		{
			new Vector3(-55.34562f, -1750.062f, 28.421f),
			new Vector3(33.32756f, -1346.693f, 28.49703f),
			new Vector3(1136.949f, -978.9973f, 47.41584f),
			new Vector3(1153.037f, -324.184f, 68.20512f),
			new Vector3(-717.9874f, -912.9858f, 18.21559f),
			new Vector3(381.3202f, 324.7907f, 102.5664f),
			new Vector3(2556.988f, 389.8186f, 107.623f),
			new Vector3(2681.886f, 3287.444f, 54.24114f),
			new Vector3(1967.44f, 3745.079f, 31.34375f),
			new Vector3(1398.659f, 3605.427f, 33.98092f),
			new Vector3(540.3076f, 2669.607f, 41.15651f),
			new Vector3(1705.201f, 4932.196f, 41.06367f),
			new Vector3(1736.091f, 6411.803f, 34.03723f),
			new Vector3(-3242.025f, 1008.887f, 11.83071f),
			new Vector3(-3042.267f, 592.9033f, 6.908933f),
			new Vector3(-2969.777f, 388.0489f, 14.04331f),
			new Vector3(-1829.175f, 786.7863f, 137.3157f)
		};

		
		private Vector3[] PosShopFruits = new Vector3[]
		{
			new Vector3(-54.68467f, -1753.375f, 28.42101f),
			new Vector3(26.61925f, -1347.925f, 28.49703f),
			new Vector3(1156.097f, -326.0332f, 68.20512f),
			new Vector3(374.5623f, 325.3259f, 102.5664f),
			new Vector3(2557.603f, 383.0245f, 107.623f),
			new Vector3(2679.647f, 3280.997f, 54.24114f),
			new Vector3(1962.168f, 3740.719f, 31.34375f),
			new Vector3(546.8014f, 2671.651f, 41.15651f),
			new Vector3(1701.954f, 4931.233f, 41.06367f),
			new Vector3(1729.591f, 6413.791f, 34.03724f),
			new Vector3(-3241.55f, 1002.133f, 11.83071f),
			new Vector3(-3039.067f, 586.7573f, 6.908932f),
			new Vector3(-1825.787f, 786.8701f, 137.2589f),
			new Vector3(-715.3794f, -915.3494f, 18.21559f)
		};

		
		private Vector3[] BlipMarket = new Vector3[]
		{
			new Vector3(-50.5f, -1759.5f, 30f),
			new Vector3(26.5f, -1345.8f, 30f),
			new Vector3(-711.5f, -912.8f, 30f),
			new Vector3(-1223.3f, -905.6f, 30f),
			new Vector3(-1488.7f, -381.2f, 30f),
			new Vector3(1137.6f, -981.5f, 30f),
			new Vector3(1159.8f, -322.3f, 30f),
			new Vector3(377.6f, 327f, 30f),
			new Vector3(2555.2f, 385.5f, 30f),
			new Vector3(-1824.5f, 790.6f, 30f),
			new Vector3(-2970.2f, 390.3f, 30f),
			new Vector3(-3042.3f, 588.4f, 30f),
			new Vector3(-3242.7f, 1004.8f, 30f),
			new Vector3(544.7f, 2670.2f, 30f),
			new Vector3(2679f, 3284.8f, 30f),
			new Vector3(1962.4f, 3743f, 30f),
			new Vector3(1392.4f, 3603.1f, 30f),
			new Vector3(1702.3f, 4927.9f, 30f),
			new Vector3(1731f, 6411f, 30f)
		};

		
		public enum Effects
		{
			
			Mp_apart_mid = 494,
			
			li = 0,
			
			underwater,
			
			ufo_deathray = 240,
			
			trevorspliff = 581,
			
			TrevorColorCodeBright = 650,
			
			TrevorColorCodeBasic = 555,
			
			TrevorColorCode = 550,
			
			TREVOR = 8,
			
			torpedo = 360,
			
			TinyPink02 = 710,
			
			TinyPink01 = 709,
			
			TinyGreen02 = 712,
			
			TinyGreen01 = 711,
			
			telescope = 119,
			
			spectator1 = 142,
			
			spectator2,
			
			spectator3,
			
			spectator4,
			
			spectator5,
			
			spectator6,
			
			spectator7,
			
			spectator8,
			
			spectator9,
			
			rply_saturation = 503,
			
			REDMIST = 537,
			
			REDMIST_blend,
			
			NG_filmic12 = 674,
			
			NG_filmic10 = 672,
			
			MP_deathfail_night = 689,
			
			LectroDark = 662,
			
			InchPurple02 = 708,
			
			hud_def_desatcrunch = 566,
			
			drug_wobbly = 599,
			
			DRUG_gas_huffin = 593,
			
			drug_flying_base = 590,
			
			drug_flying_01,
			
			drug_flying_02,
			
			drug_drive_blend01 = 597,
			
			drug_drive_blend02,
			
			Drug_deadman = 594,
			
			Drug_deadman_blend,
			
			CopsSPLASH = 18,
			
			CrossLine02 = 704,
			
			stoned = 585,
			
			SALTONSEA = 53,
			
			pulse = 101,
			
			Drunk = 589
		}
	}
}
