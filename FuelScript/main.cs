using System;
using System.Collections.Generic;
using System.Drawing;
using GTA;
using GTA.Math;
using GTA.Native;

namespace FuelScript
{
	
	public class Main : Script
	{
		
		public Main()
		{
			this.config = ScriptSettings.Load("scripts\\FuelScript.ini");
			this.safezoneSize = this.config.GetValue<int>("SETTINGS", "SAFEZONESIZE", this.safezoneSize);
			this.barX = this.config.GetValue<int>("SETTINGS", "POS_X", this.barX);
			this.barY = this.config.GetValue<int>("SETTINGS", "POS_Y", this.barY);
			this.consMulti = this.config.GetValue<float>("SETTINGS", "CONSUMPTION_MULTIPLIER", this.consMulti);
			Function.Call(-3189321952077349130L, new InputArgument[]
			{
				"weapon@w_sp_jerrycan"
			});
			for (int i = 0; i < this.gasStations.Length; i++)
			{
				this.fuelBlip = World.CreateBlip(this.gasStations[i]);
				Function.Call(-2345436420171534929L, new InputArgument[]
				{
					this.fuelBlip,
					361
				});
				this.fuelBlip.Name = "Gas Station";
				this.fuelBlip.Scale = 0.8f;
				this.fuelBlip.IsShortRange = true;
			}
			base.Tick += this.onTick;
			base.Interval = 1;
		}

		
		private void onTick(object sender, EventArgs e)
		{
			this.playerPed = Game.Player.Character;
			this.playerPos = this.playerPed.Position;
			bool flag = !Game.Player.Character.IsDead && Game.Player.Character.Health > 0;
			if (flag)
			{
				bool flag2 = Game.Player.Character.IsInVehicle();
				if (flag2)
				{
					bool flag3 = Game.Player.Character.CurrentVehicle.Model.IsCar || Game.Player.Character.CurrentVehicle.Model.IsQuadbike || Game.Player.Character.CurrentVehicle.Model.IsBike;
					if (flag3)
					{
						bool isBicycle = Game.Player.Character.CurrentVehicle.Model.IsBicycle;
						if (isBicycle)
						{
							return;
						}
						bool flag4 = !this.inCar;
						if (flag4)
						{
							this.inCar = true;
							this.currentCar = Game.Player.Character.CurrentVehicle;
							bool flag5 = this.carsList.ContainsKey(this.currentCar.NumberPlate);
							if (flag5)
							{
								this.currentFuel = this.carsList[this.currentCar.NumberPlate];
							}
							else
							{
								bool flag6 = this.carsList.Count > 32;
								if (flag6)
								{
									this.carsList.Clear();
								}
								float value = Function.Call<float>(3547962977077129165L, new InputArgument[]
								{
									50f,
									100f
								});
								this.carsList.Add(this.currentCar.NumberPlate, value);
								this.currentFuel = value;
							}
							this.gastankisDamaged = false;
							this.currentCar.FuelLevel = this.currentFuel / 1.7f;
						}
					}
					bool flag7 = Game.Player.Character.CurrentVehicle.Model.IsCar || Game.Player.Character.CurrentVehicle.Model.IsQuadbike || Game.Player.Character.CurrentVehicle.Model.IsBike;
					if (flag7)
					{
						bool isBicycle2 = Game.Player.Character.CurrentVehicle.Model.IsBicycle;
						if (!isBicycle2)
						{
							this.DisplayFuelbar();
							bool flag8 = this.currentCar != null && this.currentCar.IsAlive;
							if (flag8)
							{
								bool flag9 = this.currentCar.Speed < 1f && this.currentFuel > 5f && !this.refillCar;
								if (flag9)
								{
									for (int i = 0; i < this.gasStations.Length; i++)
									{
										bool flag10 = this.playerPos.DistanceTo2D(this.gasStations[i]) <= 12f;
										if (flag10)
										{
											bool flag11 = Game.Player.Character.CurrentVehicle.Model.IsBike || Game.Player.Character.CurrentVehicle.Model.IsQuadbike;
											if (flag11)
											{
												this.fuelCost = (int)Math.Floor((double)(100f - this.currentFuel)) / 2;
											}
											else
											{
												this.fuelCost = (int)Math.Floor((double)(100f - this.currentFuel));
											}
											bool flag12 = this.fuelCost > 0;
											if (flag12)
											{
												this.DisplayHelpTextThisFrame(string.Concat(new object[]
												{
													"Press ~INPUT_VEH_HORN~ to refuel your vehicle~n~Your cash ~g~$",
													Game.Player.Money,
													"~s~~n~Cost ~g~$",
													this.fuelCost
												}));
												bool flag13 = Game.IsControlPressed(2, 86);
												if (flag13)
												{
													this.refillCar = true;
												}
											}
										}
									}
								}
								bool flag14 = this.refillCar;
								if (flag14)
								{
									bool flag15 = this.currentFuel < 100f;
									if (flag15)
									{
										bool flag16 = Game.Player.Money >= this.fuelCost;
										if (flag16)
										{
											this.currentFuel += 0.4f;
											this.currentCar.FuelLevel = this.currentFuel / 1.7f;
											bool engineRunning = this.currentCar.EngineRunning;
											if (engineRunning)
											{
												this.currentCar.EngineRunning = false;
											}
										}
										else
										{
											UI.Notify("Not enough cash", true);
											this.refillCar = false;
											bool flag17 = this.currentFuel > 0f;
											if (flag17)
											{
												this.currentCar.EngineRunning = true;
											}
										}
									}
									else
									{
										this.currentFuel = 100f;
										this.currentCar.FuelLevel = this.currentFuel / 1.7f;
										UI.Notify("Gas tank refilled~n~Cost ~g~$" + this.fuelCost);
										Game.Player.Money -= this.fuelCost;
										this.refillCar = false;
										this.currentCar.EngineRunning = true;
									}
								}
								bool flag18 = this.currentFuel > 0f;
								if (flag18)
								{
									bool flag19 = this.currentCar.FuelLevel < this.currentFuel / 1.7f - 0.2f && !this.gastankisDamaged;
									if (flag19)
									{
										this.gastankisDamaged = true;
										this.currentFuel = this.currentCar.FuelLevel * 1.7f;
									}
									bool flag20 = this.gastankisDamaged;
									if (flag20)
									{
										this.currentFuel -= 0.02f;
										this.currentCar.FuelLevel = this.currentFuel / 1.7f;
									}
									bool flag21 = this.currentCar.Speed > 1f && !this.gastankisDamaged;
									if (flag21)
									{
										this.currentFuel -= this.currentCar.Speed * this.consMulti / 8000f;
										this.currentCar.FuelLevel = this.currentFuel / 1.7f;
									}
								}
								else
								{
									this.currentFuel = 0f;
									this.currentCar.FuelLevel = this.currentFuel / 1.7f;
									bool engineRunning2 = this.currentCar.EngineRunning;
									if (engineRunning2)
									{
										this.currentCar.EngineRunning = false;
									}
									UI.ShowSubtitle("~r~OUT OF FUEL~s~", 1000);
								}
							}
						}
					}
				}
				else
				{
					bool flag22 = this.inCar;
					if (flag22)
					{
						bool flag23 = this.carsList.ContainsKey(this.currentCar.NumberPlate);
						if (flag23)
						{
							this.carsList.Remove(this.currentCar.NumberPlate);
							this.carsList.Add(this.currentCar.NumberPlate, this.currentFuel);
						}
						this.inCar = false;
					}
					else
					{
						bool flag24 = this.playerPed.Weapons.Current.Hash == 883325847 && this.playerPed.Weapons.Current.Ammo > 0 && this.currentCar != null;
						if (flag24)
						{
							Vector3 boneCoord = this.currentCar.GetBoneCoord("wheel_lr");
							Vector3 boneCoord2 = this.currentCar.GetBoneCoord("petroltank_l");
							bool flag25 = this.playerPos.DistanceTo(boneCoord) < 1.7f || this.playerPos.DistanceTo(boneCoord2) < 1.7f;
							if (flag25)
							{
								this.DisplayHelpTextThisFrame("Hold ~INPUT_VEH_HORN~ to refuel your vehicle");
								this.DisplayFuelbar();
								bool flag26 = this.carsList.ContainsKey(this.currentCar.NumberPlate);
								if (flag26)
								{
									this.currentFuel = this.carsList[this.currentCar.NumberPlate];
								}
								bool flag27 = Game.IsControlPressed(2, 86) && this.currentFuel < 100f;
								if (flag27)
								{
									this.currentFuel += 0.1f;
									this.currentCar.FuelLevel = this.currentFuel / 1.7f;
									this.playerPed.Weapons.Current.Ammo -= 8;
									this.playerPed.Heading = (this.currentCar.Position - this.playerPed.Position).ToHeading();
									this.playerPed.Task.PlayAnimation("weapon@w_sp_jerrycan", "fire", 1f, 600, 16);
									this.carsList.Remove(this.currentCar.NumberPlate);
									this.carsList.Add(this.currentCar.NumberPlate, this.currentFuel);
								}
							}
						}
					}
				}
			}
		}

		
		private void DisplayFuelbar()
		{
			UIRectangle uirectangle = new UIRectangle(new Point(this.barX - this.safezoneSize * 6, this.barY + this.safezoneSize * 4 - 2), new Size(178, 10), Color.FromArgb(120, 0, 0, 0));
			int num = (int)Math.Floor((double)this.currentFuel);
			uirectangle.Draw();
			bool flag = this.currentFuel > 25f;
			if (flag)
			{
				UIRectangle uirectangle2 = new UIRectangle(new Point(this.barX - this.safezoneSize * 6, this.barY + this.safezoneSize * 4), new Size((int)Math.Floor((double)((float)num * 1.78f)), 6), this.barColor);
				uirectangle2.Draw();
			}
			else
			{
				UIRectangle uirectangle3 = new UIRectangle(new Point(this.barX - this.safezoneSize * 6, this.barY + this.safezoneSize * 4), new Size((int)Math.Floor((double)((float)num * 1.78f)), 6), this.barLowColor);
				uirectangle3.Draw();
			}
		}

		
		private void DisplayHelpTextThisFrame(string text)
		{
			Function.Call(-8860350453193909743L, new InputArgument[]
			{
				"STRING"
			});
			Function.Call(7789129354908300458L, new InputArgument[]
			{
				text
			});
			Function.Call(2562546386151446694L, new InputArgument[]
			{
				0,
				0,
				1,
				-1
			});
		}

		
		private void Main_Aborted(object sender, EventArgs e)
		{
			bool flag = this.fuelBlip.Exists();
			if (flag)
			{
				this.fuelBlip.Remove();
			}
		}

		
		private string ModName = "Fuel Script";

		
		private string Developer = "Sakis25";

		
		private string Version = "1.0.3";

		
		private Vector3[] gasStations = new Vector3[]
		{
			new Vector3(-724f, -935f, 30f),
			new Vector3(-71f, -1762f, 30f),
			new Vector3(265f, -1261f, 30f),
			new Vector3(819f, -1027f, 30f),
			new Vector3(-2097f, -320f, 30f),
			new Vector3(1212f, 2657f, 30f),
			new Vector3(2683f, 3264f, 30f),
			new Vector3(-2555f, 2334f, 30f),
			new Vector3(180f, 6603f, 30f),
			new Vector3(2581f, 362f, 30f),
			new Vector3(1702f, 6418f, 30f),
			new Vector3(-1799f, 803f, 30f),
			new Vector3(-90f, 6415f, 30f),
			new Vector3(264f, 2609f, 30f),
			new Vector3(50f, 2776f, 30f),
			new Vector3(2537f, 2593f, 30f),
			new Vector3(1182f, -330f, 30f),
			new Vector3(-526f, -1212f, 30f),
			new Vector3(1209f, -1402f, 30f),
			new Vector3(2005f, 3775f, 30f),
			new Vector3(621f, 269f, 30f),
			new Vector3(-1434f, -274f, 30f),
			new Vector3(1687f, 4929f, 30f)
		};

		
		private Dictionary<string, float> carsList = new Dictionary<string, float>();

		
		private bool refillCar = false;

		
		private bool inCar = false;

		
		private int fuelCost = 0;

		
		private Blip fuelBlip;

		
		private Ped playerPed;

		
		private Vector3 playerPos;

		
		private Vehicle currentCar;

		
		private float currentFuel = 100f;

		
		private bool gastankisDamaged = false;

		
		private int barX = 64;

		
		private int barY = 542;

		
		private int safezoneSize = 8;

		
		private float consMulti = 1f;

		
		private ScriptSettings config;

		
		private Color barColor = Color.FromArgb(200, 250, 150, 0);

		
		private Color barLowColor = Color.FromArgb(200, 250, 0, 0);
	}
}
