
using System;
using GTA;
using GTA.Native;


public class Weapon_Recoil : Script
{
	
	public Weapon_Recoil()
	{
		base.Tick += this.OnTick;
		base.Interval = 1;
	}

	
	private void OnTick(object sender, EventArgs e)
	{
		if (Game.Player.Character.Exists() && this.is_mod_enabled && Game.Player.Character.IsAlive && Game.Player.Character.IsShooting)
		{
			if (Game.Player.Character.IsInCover())
			{
				if (!Game.Player.Character.IsAimingFromCover)
				{
					this.Shooting_In_Cover();
					return;
				}
				if (Game.Player.Character.IsStopped)
				{
					this.Shooting_Still();
					return;
				}
				this.Shooting_Moving();
				return;
			}
			else if (Function.Call<bool>(8947185480862490559L, new InputArgument[]
			{
				Game.Player.Character
			}))
			{
				if (Game.Player.Character.IsStopped)
				{
					this.Shooting_Still_Stealth();
					return;
				}
				if (Game.Player.Character.IsSprinting)
				{
					this.Shooting_Sprintig_Stealth();
					return;
				}
				this.Shooting_Moving_Stealth();
				return;
			}
			else
			{
				if (Game.Player.Character.IsStopped)
				{
					this.Shooting_Still();
					return;
				}
				if (Game.Player.Character.IsSprinting)
				{
					this.Shooting_Sprintig();
					return;
				}
				this.Shooting_Moving();
			}
		}
	}

	
	private void Shooting_In_Cover()
	{
		if (Game.Player.Character.Weapons.Current.Hash == -1074790547 || Game.Player.Character.Weapons.Current.Hash == 2132975508 || Game.Player.Character.Weapons.Current.Hash == -2084633992 || Game.Player.Character.Weapons.Current.Hash == 1649403952 || Game.Player.Character.Weapons.Current.Hash == -1357824103 || Game.Player.Character.Weapons.Current.Hash == -1063057011)
		{
			this.recoil = 1.2f;
			if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
			{
				this.recoil -= this.grip_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1323216997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1284994289) || Game.Player.Character.Weapons.Current.IsComponentActive(-1861183855) || Game.Player.Character.Weapons.Current.IsComponentActive(1509923832) || Game.Player.Character.Weapons.Current.IsComponentActive(-1899902599) || Game.Player.Character.Weapons.Current.IsComponentActive(2089537806))
			{
				this.recoil -= this.extended_mag_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
			{
				this.recoil -= this.flashlight_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
			{
				this.recoil -= this.scope_reduced_recoil;
			}
			switch (this.rnd.Next(6))
			{
			case 0:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
				return;
			case 1:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.3f;
				return;
			case 2:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.3f;
				return;
			case 3:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
				return;
			case 4:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.3f;
				return;
			case 5:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.3f;
				return;
			default:
				return;
			}
		}
		else
		{
			WeaponGroup group = Game.Player.Character.Weapons.Current.Group;
			if (group > 1159398588)
			{
				if (group != -1569042529)
				{
					if (group != -1212426201)
					{
						if (group != -957766203)
						{
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 324215364)
						{
							this.recoil = 1.2f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(283556395))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -270015777 || Game.Player.Character.Weapons.Current.Hash == 736523883 || Game.Player.Character.Weapons.Current.Hash == 171789620)
						{
							this.recoil = 1.2f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1152981993) || Game.Player.Character.Weapons.Current.IsComponentActive(889808635) || Game.Player.Character.Weapons.Current.IsComponentActive(860508675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324) || Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -619010992)
						{
							this.recoil = 1.2f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1188271751))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else
						{
							this.recoil = 1.2f;
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == 100416529)
						{
							this.recoil = 2.5f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 205991906)
						{
							this.recoil = 3.5f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == -952879014)
						{
							this.recoil = 2f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								GameplayCamera.Shake(4, 0.175f);
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-855823675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == -1312131151)
					{
						this.recoil = 3.5f;
						GameplayCamera.Shake(4, this.recoil);
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == 1119849093)
					{
						this.recoil = 4.5f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == -1568386805)
						{
							this.recoil = 1.75f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						this.recoil = 2.25f;
						GameplayCamera.Shake(4, this.recoil);
					}
				}
				return;
			}
			if (group != 416676503)
			{
				if (group != 860033945)
				{
					if (group != 1159398588)
					{
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == -1660422300)
					{
						this.recoil = 1.5f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else if (Game.Player.Character.Weapons.Current.Hash == 2144741730)
					{
						this.recoil = 1.5f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-691692330))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
						{
							this.recoil -= this.grip_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						this.recoil = 1.5f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == 487013001 || Game.Player.Character.Weapons.Current.Hash == 2017895192 || Game.Player.Character.Weapons.Current.Hash == -275439685)
					{
						this.recoil = 2.5f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
						{
							this.recoil -= this.flashlight_reduced_recoil;
						}
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash != -494615257 && Game.Player.Character.Weapons.Current.Hash != 984333226 && Game.Player.Character.Weapons.Current.Hash != -1654528753)
					{
						this.recoil = 3f;
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					this.recoil = 4f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(-2034401422) || Game.Player.Character.Weapons.Current.IsComponentActive(-1759709443))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
					{
						this.recoil -= this.grip_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 453432689 || Game.Player.Character.Weapons.Current.Hash == 137902532 || Game.Player.Character.Weapons.Current.Hash == -1076751822)
			{
				this.recoil = 1.2f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-316253668) || Game.Player.Character.Weapons.Current.IsComponentActive(867832552) || Game.Player.Character.Weapons.Current.IsComponentActive(2063610803))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 1593441988)
			{
				this.recoil = 1.2f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-696561875))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else
			{
				if (Game.Player.Character.Weapons.Current.Hash == -771403250 || Game.Player.Character.Weapons.Current.Hash == -1716589765)
				{
					this.recoil = 1.2f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(1694090795) || Game.Player.Character.Weapons.Current.IsComponentActive(-640439150))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
					return;
				}
				if (Game.Player.Character.Weapons.Current.Hash == 584646201)
				{
					this.recoil = 1.2f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(614078421))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else if (Game.Player.Character.Weapons.Current.Hash == -598887786 || Game.Player.Character.Weapons.Current.Hash == -1045183535)
				{
					this.recoil = 1.2f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else
				{
					this.recoil = 1.2f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
		}
	}

	
	private void Shooting_Still()
	{
		if (Game.Player.Character.Weapons.Current.Hash == -1074790547 || Game.Player.Character.Weapons.Current.Hash == 2132975508 || Game.Player.Character.Weapons.Current.Hash == -2084633992 || Game.Player.Character.Weapons.Current.Hash == 1649403952 || Game.Player.Character.Weapons.Current.Hash == -1357824103 || Game.Player.Character.Weapons.Current.Hash == -1063057011)
		{
			this.recoil = 0.27f;
			if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
			{
				this.recoil -= this.grip_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1323216997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1284994289) || Game.Player.Character.Weapons.Current.IsComponentActive(-1861183855) || Game.Player.Character.Weapons.Current.IsComponentActive(1509923832) || Game.Player.Character.Weapons.Current.IsComponentActive(-1899902599) || Game.Player.Character.Weapons.Current.IsComponentActive(2089537806))
			{
				this.recoil -= this.extended_mag_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
			{
				this.recoil -= this.flashlight_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
			{
				this.recoil -= this.scope_reduced_recoil;
			}
			switch (this.rnd.Next(6))
			{
			case 0:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
				return;
			case 1:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
				return;
			case 2:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
				return;
			case 3:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
				return;
			case 4:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
				return;
			case 5:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
				return;
			default:
				return;
			}
		}
		else
		{
			WeaponGroup group = Game.Player.Character.Weapons.Current.Group;
			if (group > 1159398588)
			{
				if (group != -1569042529)
				{
					if (group != -1212426201)
					{
						if (group != -957766203)
						{
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 324215364)
						{
							this.recoil = 0.213f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(283556395))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -270015777 || Game.Player.Character.Weapons.Current.Hash == 736523883 || Game.Player.Character.Weapons.Current.Hash == 171789620)
						{
							this.recoil = 0.24f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1152981993) || Game.Player.Character.Weapons.Current.IsComponentActive(889808635) || Game.Player.Character.Weapons.Current.IsComponentActive(860508675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324) || Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -619010992)
						{
							this.recoil = 0.2f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1188271751))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else
						{
							this.recoil = 0.213f;
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == 100416529)
						{
							this.recoil = 0.6f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 205991906)
						{
							this.recoil = 1f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == -952879014)
						{
							this.recoil = 0.35f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								GameplayCamera.Shake(4, 0.175f);
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-855823675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == -1312131151)
					{
						this.recoil = 1.25f;
						GameplayCamera.Shake(4, this.recoil);
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == 1119849093)
					{
						this.recoil = 2.45f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == -1568386805)
						{
							this.recoil = 0.5f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						this.recoil = 1.25f;
						GameplayCamera.Shake(4, this.recoil);
					}
				}
				return;
			}
			if (group != 416676503)
			{
				if (group != 860033945)
				{
					if (group != 1159398588)
					{
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == -1660422300)
					{
						this.recoil = 0.35f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else if (Game.Player.Character.Weapons.Current.Hash == 2144741730)
					{
						this.recoil = 0.325f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-691692330))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
						{
							this.recoil -= this.grip_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						this.recoil = 0.25f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == 487013001 || Game.Player.Character.Weapons.Current.Hash == 2017895192 || Game.Player.Character.Weapons.Current.Hash == -275439685)
					{
						this.recoil = 0.8f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
						{
							this.recoil -= this.flashlight_reduced_recoil;
						}
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash != -494615257 && Game.Player.Character.Weapons.Current.Hash != 984333226 && Game.Player.Character.Weapons.Current.Hash != -1654528753)
					{
						this.recoil = 0.8f;
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					this.recoil = 2f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
					{
						this.recoil -= 0.01f;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(-2034401422) || Game.Player.Character.Weapons.Current.IsComponentActive(-1759709443))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
					{
						this.recoil -= this.grip_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 453432689 || Game.Player.Character.Weapons.Current.Hash == 137902532 || Game.Player.Character.Weapons.Current.Hash == -1076751822)
			{
				this.recoil = 0.27f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-316253668) || Game.Player.Character.Weapons.Current.IsComponentActive(867832552) || Game.Player.Character.Weapons.Current.IsComponentActive(2063610803))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 1593441988)
			{
				this.recoil = 0.3f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-696561875))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else
			{
				if (Game.Player.Character.Weapons.Current.Hash == -771403250 || Game.Player.Character.Weapons.Current.Hash == -1716589765)
				{
					this.recoil = 0.34f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(1694090795) || Game.Player.Character.Weapons.Current.IsComponentActive(-640439150))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
					return;
				}
				if (Game.Player.Character.Weapons.Current.Hash == 584646201)
				{
					this.recoil = 0.275f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(614078421))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else if (Game.Player.Character.Weapons.Current.Hash == -598887786 || Game.Player.Character.Weapons.Current.Hash == -1045183535)
				{
					this.recoil = 0.42f;
					int num = this.rnd.Next(2);
					if (num == 0)
					{
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					}
					if (num != 1)
					{
						return;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				}
				else
				{
					this.recoil = 0.27f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
		}
	}

	
	private void Shooting_Moving()
	{
		if (Game.Player.Character.Weapons.Current.Hash == -1074790547 || Game.Player.Character.Weapons.Current.Hash == 2132975508 || Game.Player.Character.Weapons.Current.Hash == -2084633992 || Game.Player.Character.Weapons.Current.Hash == 1649403952 || Game.Player.Character.Weapons.Current.Hash == -1357824103 || Game.Player.Character.Weapons.Current.Hash == -1063057011)
		{
			this.recoil = 0.35f;
			if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
			{
				this.recoil -= this.grip_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1323216997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1284994289) || Game.Player.Character.Weapons.Current.IsComponentActive(-1861183855) || Game.Player.Character.Weapons.Current.IsComponentActive(1509923832) || Game.Player.Character.Weapons.Current.IsComponentActive(-1899902599) || Game.Player.Character.Weapons.Current.IsComponentActive(2089537806))
			{
				this.recoil -= this.extended_mag_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
			{
				this.recoil -= this.flashlight_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
			{
				this.recoil -= this.scope_reduced_recoil;
			}
			switch (this.rnd.Next(6))
			{
			case 0:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
				return;
			case 1:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.25f;
				return;
			case 2:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1525f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.25f;
				return;
			case 3:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
				return;
			case 4:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.25f;
				return;
			case 5:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.25f;
				return;
			default:
				return;
			}
		}
		else
		{
			WeaponGroup group = Game.Player.Character.Weapons.Current.Group;
			if (group > 1159398588)
			{
				if (group != -1569042529)
				{
					if (group != -1212426201)
					{
						if (group != -957766203)
						{
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 324215364)
						{
							this.recoil = 0.313f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(283556395))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -270015777 || Game.Player.Character.Weapons.Current.Hash == 736523883 || Game.Player.Character.Weapons.Current.Hash == 171789620)
						{
							this.recoil = 0.33f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1152981993) || Game.Player.Character.Weapons.Current.IsComponentActive(889808635) || Game.Player.Character.Weapons.Current.IsComponentActive(860508675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324) || Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -619010992)
						{
							this.recoil = 0.325f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1188271751))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else
						{
							this.recoil = 0.313f;
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == 100416529)
						{
							this.recoil = 1f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 205991906)
						{
							this.recoil = 1.5f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == -952879014)
						{
							this.recoil = 0.6f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								GameplayCamera.Shake(4, 0.175f);
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-855823675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == -1312131151)
					{
						this.recoil = 1.75f;
						GameplayCamera.Shake(4, this.recoil);
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == 1119849093)
					{
						this.recoil = 3f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == -1568386805)
						{
							this.recoil = 0.8f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						this.recoil = 1.25f;
						GameplayCamera.Shake(4, this.recoil);
					}
				}
				return;
			}
			if (group != 416676503)
			{
				if (group != 860033945)
				{
					if (group != 1159398588)
					{
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == -1660422300)
					{
						this.recoil = 0.43f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else if (Game.Player.Character.Weapons.Current.Hash == 2144741730)
					{
						this.recoil = 0.425f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-691692330))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
						{
							this.recoil -= this.grip_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						this.recoil = 0.275f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == 487013001 || Game.Player.Character.Weapons.Current.Hash == 2017895192 || Game.Player.Character.Weapons.Current.Hash == -275439685)
					{
						this.recoil = 1.2f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
						{
							this.recoil -= this.flashlight_reduced_recoil;
						}
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash != -494615257 && Game.Player.Character.Weapons.Current.Hash != 984333226 && Game.Player.Character.Weapons.Current.Hash != -1654528753)
					{
						this.recoil = 1.2f;
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					this.recoil = 2.5f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
					{
						this.recoil -= 0.01f;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(-2034401422) || Game.Player.Character.Weapons.Current.IsComponentActive(-1759709443))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
					{
						this.recoil -= this.grip_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 453432689 || Game.Player.Character.Weapons.Current.Hash == 137902532 || Game.Player.Character.Weapons.Current.Hash == -1076751822)
			{
				this.recoil = 0.325f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-316253668) || Game.Player.Character.Weapons.Current.IsComponentActive(867832552) || Game.Player.Character.Weapons.Current.IsComponentActive(2063610803))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 1593441988)
			{
				this.recoil = 0.25f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-696561875))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else
			{
				if (Game.Player.Character.Weapons.Current.Hash == -771403250 || Game.Player.Character.Weapons.Current.Hash == -1716589765)
				{
					this.recoil = 0.28f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(1694090795) || Game.Player.Character.Weapons.Current.IsComponentActive(-640439150))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
					return;
				}
				if (Game.Player.Character.Weapons.Current.Hash == 584646201)
				{
					this.recoil = 0.33f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(614078421))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else if (Game.Player.Character.Weapons.Current.Hash == -598887786 || Game.Player.Character.Weapons.Current.Hash == -1045183535)
				{
					this.recoil = 0.5f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else
				{
					this.recoil = 0.325f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
		}
	}

	
	private void Shooting_Sprintig()
	{
		if (Game.Player.Character.Weapons.Current.Hash == -1074790547 || Game.Player.Character.Weapons.Current.Hash == 2132975508 || Game.Player.Character.Weapons.Current.Hash == -2084633992 || Game.Player.Character.Weapons.Current.Hash == 1649403952 || Game.Player.Character.Weapons.Current.Hash == -1357824103 || Game.Player.Character.Weapons.Current.Hash == -1063057011)
		{
			this.recoil = 0.445f;
			if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
			{
				this.recoil -= this.grip_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1323216997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1284994289) || Game.Player.Character.Weapons.Current.IsComponentActive(-1861183855) || Game.Player.Character.Weapons.Current.IsComponentActive(1509923832) || Game.Player.Character.Weapons.Current.IsComponentActive(-1899902599) || Game.Player.Character.Weapons.Current.IsComponentActive(2089537806))
			{
				this.recoil -= this.extended_mag_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
			{
				this.recoil -= this.flashlight_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
			{
				this.recoil -= this.scope_reduced_recoil;
			}
			switch (this.rnd.Next(6))
			{
			case 0:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
				return;
			case 1:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.3f;
				return;
			case 2:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.3f;
				return;
			case 3:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
				return;
			case 4:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.3f;
				return;
			case 5:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.3f;
				return;
			default:
				return;
			}
		}
		else
		{
			WeaponGroup group = Game.Player.Character.Weapons.Current.Group;
			if (group > 1159398588)
			{
				if (group != -1569042529)
				{
					if (group != -1212426201)
					{
						if (group != -957766203)
						{
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 324215364)
						{
							this.recoil = 0.375f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(283556395))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -270015777 || Game.Player.Character.Weapons.Current.Hash == 736523883 || Game.Player.Character.Weapons.Current.Hash == 171789620)
						{
							this.recoil = 0.383f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1152981993) || Game.Player.Character.Weapons.Current.IsComponentActive(889808635) || Game.Player.Character.Weapons.Current.IsComponentActive(860508675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324) || Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -619010992)
						{
							this.recoil = 0.4f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1188271751))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else
						{
							this.recoil = 0.375f;
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == 100416529)
						{
							this.recoil = 1.5f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 205991906)
						{
							this.recoil = 2f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == -952879014)
						{
							this.recoil = 1f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								GameplayCamera.Shake(4, 0.175f);
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-855823675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == -1312131151)
					{
						this.recoil = 2.25f;
						GameplayCamera.Shake(4, this.recoil);
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == 1119849093)
					{
						this.recoil = 3.75f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == -1568386805)
						{
							this.recoil = 1.25f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						this.recoil = 1.25f;
						GameplayCamera.Shake(4, this.recoil);
					}
				}
				return;
			}
			if (group != 416676503)
			{
				if (group != 860033945)
				{
					if (group != 1159398588)
					{
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == -1660422300)
					{
						this.recoil = 0.525f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else if (Game.Player.Character.Weapons.Current.Hash == 2144741730)
					{
						this.recoil = 0.55f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-691692330))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
						{
							this.recoil -= this.grip_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						this.recoil = 0.335f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == 487013001 || Game.Player.Character.Weapons.Current.Hash == 2017895192 || Game.Player.Character.Weapons.Current.Hash == -275439685)
					{
						this.recoil = 1.75f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
						{
							this.recoil -= this.flashlight_reduced_recoil;
						}
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash != -494615257 && Game.Player.Character.Weapons.Current.Hash != 984333226 && Game.Player.Character.Weapons.Current.Hash != -1654528753)
					{
						this.recoil = 1.75f;
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					this.recoil = 3f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
					{
						this.recoil -= 0.01f;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(-2034401422) || Game.Player.Character.Weapons.Current.IsComponentActive(-1759709443))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
					{
						this.recoil -= this.grip_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 453432689 || Game.Player.Character.Weapons.Current.Hash == 137902532 || Game.Player.Character.Weapons.Current.Hash == -1076751822)
			{
				this.recoil = 0.35f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-316253668) || Game.Player.Character.Weapons.Current.IsComponentActive(867832552) || Game.Player.Character.Weapons.Current.IsComponentActive(2063610803))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 1593441988)
			{
				this.recoil = 0.38f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-696561875))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else
			{
				if (Game.Player.Character.Weapons.Current.Hash == -771403250 || Game.Player.Character.Weapons.Current.Hash == -1716589765)
				{
					this.recoil = 0.41f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(1694090795) || Game.Player.Character.Weapons.Current.IsComponentActive(-640439150))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
					return;
				}
				if (Game.Player.Character.Weapons.Current.Hash == 584646201)
				{
					this.recoil = 0.36f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(614078421))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else if (Game.Player.Character.Weapons.Current.Hash == -598887786 || Game.Player.Character.Weapons.Current.Hash == -1045183535)
				{
					this.recoil = 0.6f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else
				{
					this.recoil = 0.35f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
		}
	}

	
	private void Shooting_Still_Stealth()
	{
		if (Game.Player.Character.Weapons.Current.Hash == -1074790547 || Game.Player.Character.Weapons.Current.Hash == 2132975508 || Game.Player.Character.Weapons.Current.Hash == -2084633992 || Game.Player.Character.Weapons.Current.Hash == 1649403952 || Game.Player.Character.Weapons.Current.Hash == -1357824103 || Game.Player.Character.Weapons.Current.Hash == -1063057011)
		{
			this.recoil = 0.22f;
			if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
			{
				this.recoil -= this.grip_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1323216997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1284994289) || Game.Player.Character.Weapons.Current.IsComponentActive(-1861183855) || Game.Player.Character.Weapons.Current.IsComponentActive(1509923832) || Game.Player.Character.Weapons.Current.IsComponentActive(-1899902599) || Game.Player.Character.Weapons.Current.IsComponentActive(2089537806))
			{
				this.recoil -= this.extended_mag_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
			{
				this.recoil -= this.flashlight_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
			{
				this.recoil -= this.scope_reduced_recoil;
			}
			switch (this.rnd.Next(6))
			{
			case 0:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
				return;
			case 1:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.15f;
				return;
			case 2:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.15f;
				return;
			case 3:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
				return;
			case 4:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.15f;
				return;
			case 5:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.15f;
				return;
			default:
				return;
			}
		}
		else
		{
			WeaponGroup group = Game.Player.Character.Weapons.Current.Group;
			if (group > 1159398588)
			{
				if (group != -1569042529)
				{
					if (group != -1212426201)
					{
						if (group != -957766203)
						{
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 324215364)
						{
							this.recoil = 0.163f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(283556395))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -270015777 || Game.Player.Character.Weapons.Current.Hash == 736523883 || Game.Player.Character.Weapons.Current.Hash == 171789620)
						{
							this.recoil = 0.18f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1152981993) || Game.Player.Character.Weapons.Current.IsComponentActive(889808635) || Game.Player.Character.Weapons.Current.IsComponentActive(860508675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324) || Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -619010992)
						{
							this.recoil = 0.14f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1188271751))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else
						{
							this.recoil = 0.163f;
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == 100416529)
						{
							this.recoil = 0.375f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 205991906)
						{
							this.recoil = 0.75f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == -952879014)
						{
							this.recoil = 0.175f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								GameplayCamera.Shake(4, 0.175f);
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-855823675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == -1312131151)
					{
						this.recoil = 0.5f;
						GameplayCamera.Shake(4, this.recoil);
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == 1119849093)
					{
						this.recoil = 0.45f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == -1568386805)
						{
							this.recoil = 0.45f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								UI.Notify("Grip");
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						this.recoil = 1.25f;
						GameplayCamera.Shake(4, this.recoil);
					}
				}
				return;
			}
			if (group != 416676503)
			{
				if (group != 860033945)
				{
					if (group != 1159398588)
					{
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == -1660422300)
					{
						this.recoil = 0.3f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else if (Game.Player.Character.Weapons.Current.Hash == 2144741730)
					{
						this.recoil = 0.25f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-691692330))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
						{
							this.recoil -= this.grip_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						this.recoil = 0.25f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == 487013001 || Game.Player.Character.Weapons.Current.Hash == 2017895192 || Game.Player.Character.Weapons.Current.Hash == -275439685)
					{
						this.recoil = 0.63f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
						{
							this.recoil -= this.flashlight_reduced_recoil;
						}
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash != -494615257 && Game.Player.Character.Weapons.Current.Hash != 984333226 && Game.Player.Character.Weapons.Current.Hash != -1654528753)
					{
						this.recoil = 0.63f;
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					this.recoil = 1.5f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
					{
						this.recoil -= 0.01f;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(-2034401422) || Game.Player.Character.Weapons.Current.IsComponentActive(-1759709443))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
					{
						this.recoil -= this.grip_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 453432689 || Game.Player.Character.Weapons.Current.Hash == 137902532 || Game.Player.Character.Weapons.Current.Hash == -1076751822)
			{
				this.recoil = 0.23f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-316253668) || Game.Player.Character.Weapons.Current.IsComponentActive(867832552) || Game.Player.Character.Weapons.Current.IsComponentActive(2063610803))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 1593441988)
			{
				this.recoil = 0.253f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-696561875))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else
			{
				if (Game.Player.Character.Weapons.Current.Hash == -771403250 || Game.Player.Character.Weapons.Current.Hash == -1716589765)
				{
					this.recoil = 0.274f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(1694090795) || Game.Player.Character.Weapons.Current.IsComponentActive(-640439150))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
					return;
				}
				if (Game.Player.Character.Weapons.Current.Hash == 584646201)
				{
					this.recoil = 0.1825f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(614078421))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else if (Game.Player.Character.Weapons.Current.Hash == -598887786 || Game.Player.Character.Weapons.Current.Hash == -1045183535)
				{
					this.recoil = 0.35f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else
				{
					this.recoil = 0.23f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
		}
	}

	
	private void Shooting_Moving_Stealth()
	{
		if (Game.Player.Character.Weapons.Current.Hash == -1074790547 || Game.Player.Character.Weapons.Current.Hash == 2132975508 || Game.Player.Character.Weapons.Current.Hash == -2084633992 || Game.Player.Character.Weapons.Current.Hash == 1649403952 || Game.Player.Character.Weapons.Current.Hash == -1357824103 || Game.Player.Character.Weapons.Current.Hash == -1063057011)
		{
			this.recoil = 0.285f;
			if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
			{
				this.recoil -= this.grip_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1323216997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1284994289) || Game.Player.Character.Weapons.Current.IsComponentActive(-1861183855) || Game.Player.Character.Weapons.Current.IsComponentActive(1509923832) || Game.Player.Character.Weapons.Current.IsComponentActive(-1899902599) || Game.Player.Character.Weapons.Current.IsComponentActive(2089537806))
			{
				this.recoil -= this.extended_mag_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
			{
				this.recoil -= this.flashlight_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
			{
				this.recoil -= this.scope_reduced_recoil;
			}
			switch (this.rnd.Next(6))
			{
			case 0:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
				return;
			case 1:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
				return;
			case 2:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
				return;
			case 3:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
				return;
			case 4:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
				return;
			case 5:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
				return;
			default:
				return;
			}
		}
		else
		{
			WeaponGroup group = Game.Player.Character.Weapons.Current.Group;
			if (group > 1159398588)
			{
				if (group != -1569042529)
				{
					if (group != -1212426201)
					{
						if (group != -957766203)
						{
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 324215364)
						{
							this.recoil = 0.233f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(283556395))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -270015777 || Game.Player.Character.Weapons.Current.Hash == 736523883 || Game.Player.Character.Weapons.Current.Hash == 171789620)
						{
							this.recoil = 0.255f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1152981993) || Game.Player.Character.Weapons.Current.IsComponentActive(889808635) || Game.Player.Character.Weapons.Current.IsComponentActive(860508675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324) || Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -619010992)
						{
							this.recoil = 0.237f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1188271751))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else
						{
							this.recoil = 0.233f;
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == 100416529)
						{
							this.recoil = 0.5f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 205991906)
						{
							this.recoil = 1f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == -952879014)
						{
							this.recoil = 0.175f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								GameplayCamera.Shake(4, 0.175f);
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-855823675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == -1312131151)
					{
						this.recoil = 0.75f;
						GameplayCamera.Shake(4, this.recoil);
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == 1119849093)
					{
						this.recoil = 1.25f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == -1568386805)
						{
							this.recoil = 0.5f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						this.recoil = 1.25f;
						GameplayCamera.Shake(4, this.recoil);
					}
				}
				return;
			}
			if (group != 416676503)
			{
				if (group != 860033945)
				{
					if (group != 1159398588)
					{
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == -1660422300)
					{
						this.recoil = 0.365f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else if (Game.Player.Character.Weapons.Current.Hash == 2144741730)
					{
						this.recoil = 0.4f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-691692330))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
						{
							this.recoil -= this.grip_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						this.recoil = 0.32f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == 487013001 || Game.Player.Character.Weapons.Current.Hash == 2017895192 || Game.Player.Character.Weapons.Current.Hash == -275439685)
					{
						this.recoil = 1f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
						{
							this.recoil -= this.flashlight_reduced_recoil;
						}
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash != -494615257 && Game.Player.Character.Weapons.Current.Hash != 984333226 && Game.Player.Character.Weapons.Current.Hash != -1654528753)
					{
						this.recoil = 1f;
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					this.recoil = 2f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
					{
						this.recoil -= 0.01f;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(-2034401422) || Game.Player.Character.Weapons.Current.IsComponentActive(-1759709443))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
					{
						this.recoil -= this.grip_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 453432689 || Game.Player.Character.Weapons.Current.Hash == 137902532 || Game.Player.Character.Weapons.Current.Hash == -1076751822)
			{
				this.recoil = 0.27f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-316253668) || Game.Player.Character.Weapons.Current.IsComponentActive(867832552) || Game.Player.Character.Weapons.Current.IsComponentActive(2063610803))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 1593441988)
			{
				this.recoil = 0.32f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-696561875))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else
			{
				if (Game.Player.Character.Weapons.Current.Hash == -771403250 || Game.Player.Character.Weapons.Current.Hash == -1716589765)
				{
					this.recoil = 0.354f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(1694090795) || Game.Player.Character.Weapons.Current.IsComponentActive(-640439150))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
					return;
				}
				if (Game.Player.Character.Weapons.Current.Hash == 584646201)
				{
					this.recoil = 0.26f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(614078421))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else if (Game.Player.Character.Weapons.Current.Hash == -598887786 || Game.Player.Character.Weapons.Current.Hash == -1045183535)
				{
					this.recoil = 0.412f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else
				{
					this.recoil = 0.27f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
		}
	}

	
	private void Shooting_Sprintig_Stealth()
	{
		if (Game.Player.Character.Weapons.Current.Hash == -1074790547 || Game.Player.Character.Weapons.Current.Hash == 2132975508 || Game.Player.Character.Weapons.Current.Hash == -2084633992 || Game.Player.Character.Weapons.Current.Hash == 1649403952 || Game.Player.Character.Weapons.Current.Hash == -1357824103 || Game.Player.Character.Weapons.Current.Hash == -1063057011)
		{
			this.recoil = 0.38f;
			if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
			{
				this.recoil -= this.grip_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1323216997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1284994289) || Game.Player.Character.Weapons.Current.IsComponentActive(-1861183855) || Game.Player.Character.Weapons.Current.IsComponentActive(1509923832) || Game.Player.Character.Weapons.Current.IsComponentActive(-1899902599) || Game.Player.Character.Weapons.Current.IsComponentActive(2089537806))
			{
				this.recoil -= this.extended_mag_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
			{
				this.recoil -= this.flashlight_reduced_recoil;
			}
			if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
			{
				this.recoil -= this.scope_reduced_recoil;
			}
			switch (this.rnd.Next(6))
			{
			case 0:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
				return;
			case 1:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.25f;
				return;
			case 2:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.25f;
				return;
			case 3:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
				return;
			case 4:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.25f;
				return;
			case 5:
				GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
				GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.25f;
				return;
			default:
				return;
			}
		}
		else
		{
			WeaponGroup group = Game.Player.Character.Weapons.Current.Group;
			if (group > 1159398588)
			{
				if (group != -1569042529)
				{
					if (group != -1212426201)
					{
						if (group != -957766203)
						{
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 324215364)
						{
							this.recoil = 0.3125f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(283556395))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -270015777 || Game.Player.Character.Weapons.Current.Hash == 736523883 || Game.Player.Character.Weapons.Current.Hash == 171789620)
						{
							this.recoil = 0.345f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1152981993) || Game.Player.Character.Weapons.Current.IsComponentActive(889808635) || Game.Player.Character.Weapons.Current.IsComponentActive(860508675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324) || Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
							{
								this.recoil -= this.scope_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else if (Game.Player.Character.Weapons.Current.Hash == -619010992)
						{
							this.recoil = 0.325f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-1188271751))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
						else
						{
							this.recoil = 0.3125f;
							switch (this.rnd.Next(6))
							{
							case 0:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
								return;
							case 1:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 2:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
								return;
							case 3:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
								return;
							case 4:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							case 5:
								GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
								GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
								return;
							default:
								return;
							}
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == 100416529)
						{
							this.recoil = 1f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == 205991906)
						{
							this.recoil = 0.35f;
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						if (Game.Player.Character.Weapons.Current.Hash == -952879014)
						{
							this.recoil = 0.65f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								GameplayCamera.Shake(4, 0.175f);
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(-855823675))
							{
								this.recoil -= this.extended_mag_reduced_recoil;
							}
							if (Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
							{
								this.recoil -= this.flashlight_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == -1312131151)
					{
						this.recoil = 1.55f;
						GameplayCamera.Shake(4, this.recoil);
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == 1119849093)
					{
						this.recoil = 0.65f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						if (Game.Player.Character.Weapons.Current.Hash == -1568386805)
						{
							this.recoil = 0.75f;
							if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
							{
								this.recoil -= this.grip_reduced_recoil;
							}
							GameplayCamera.Shake(4, this.recoil);
							return;
						}
						this.recoil = 1.25f;
						GameplayCamera.Shake(4, this.recoil);
					}
				}
				return;
			}
			if (group != 416676503)
			{
				if (group != 860033945)
				{
					if (group != 1159398588)
					{
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash == -1660422300)
					{
						this.recoil = 0.46f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else if (Game.Player.Character.Weapons.Current.Hash == 2144741730)
					{
						this.recoil = 0.47f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-691692330))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
						{
							this.recoil -= this.grip_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-2112517305))
						{
							this.recoil -= this.extended_mag_reduced_recoil;
						}
						if (Game.Player.Character.Weapons.Current.IsComponentActive(-1596416958) || Game.Player.Character.Weapons.Current.IsComponentActive(-1439939148) || Game.Player.Character.Weapons.Current.IsComponentActive(1006677997) || Game.Player.Character.Weapons.Current.IsComponentActive(-1657815255) || Game.Player.Character.Weapons.Current.IsComponentActive(1019656791) || Game.Player.Character.Weapons.Current.IsComponentActive(-1135289737) || Game.Player.Character.Weapons.Current.IsComponentActive(-767279652) || Game.Player.Character.Weapons.Current.IsComponentActive(471997210))
						{
							this.recoil -= this.scope_reduced_recoil;
						}
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
					else
					{
						this.recoil = 0.28f;
						switch (this.rnd.Next(6))
						{
						case 0:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
							return;
						case 1:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 2:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
							return;
						case 3:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
							return;
						case 4:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						case 5:
							GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
							GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
							return;
						default:
							return;
						}
					}
				}
				else
				{
					if (Game.Player.Character.Weapons.Current.Hash == 487013001 || Game.Player.Character.Weapons.Current.Hash == 2017895192 || Game.Player.Character.Weapons.Current.Hash == -275439685)
					{
						this.recoil = 1.325f;
						if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
						{
							this.recoil -= this.flashlight_reduced_recoil;
						}
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					if (Game.Player.Character.Weapons.Current.Hash != -494615257 && Game.Player.Character.Weapons.Current.Hash != 984333226 && Game.Player.Character.Weapons.Current.Hash != -1654528753)
					{
						this.recoil = 1.325f;
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
						return;
					}
					this.recoil = 2.7f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934) || Game.Player.Character.Weapons.Current.IsComponentActive(2076495324))
					{
						this.recoil -= 0.01f;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(-2034401422) || Game.Player.Character.Weapons.Current.IsComponentActive(-1759709443))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(202788691))
					{
						this.recoil -= this.grip_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 453432689 || Game.Player.Character.Weapons.Current.Hash == 137902532 || Game.Player.Character.Weapons.Current.Hash == -1076751822)
			{
				this.recoil = 0.32f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-316253668) || Game.Player.Character.Weapons.Current.IsComponentActive(867832552) || Game.Player.Character.Weapons.Current.IsComponentActive(2063610803))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else if (Game.Player.Character.Weapons.Current.Hash == 1593441988)
			{
				this.recoil = 0.35f;
				if (Game.Player.Character.Weapons.Current.IsComponentActive(-696561875))
				{
					this.recoil -= this.extended_mag_reduced_recoil;
				}
				if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
				{
					this.recoil -= this.flashlight_reduced_recoil;
				}
				switch (this.rnd.Next(6))
				{
				case 0:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
					return;
				case 1:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 2:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
					return;
				case 3:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
					return;
				case 4:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				case 5:
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
					return;
				default:
					return;
				}
			}
			else
			{
				if (Game.Player.Character.Weapons.Current.Hash == -771403250 || Game.Player.Character.Weapons.Current.Hash == -1716589765)
				{
					this.recoil = 0.38f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(1694090795) || Game.Player.Character.Weapons.Current.IsComponentActive(-640439150))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.8f);
					GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.85f;
					return;
				}
				if (Game.Player.Character.Weapons.Current.Hash == 584646201)
				{
					this.recoil = 0.33f;
					if (Game.Player.Character.Weapons.Current.IsComponentActive(614078421))
					{
						this.recoil -= this.extended_mag_reduced_recoil;
					}
					if (Game.Player.Character.Weapons.Current.IsComponentActive(899381934))
					{
						this.recoil -= this.flashlight_reduced_recoil;
					}
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else if (Game.Player.Character.Weapons.Current.Hash == -598887786 || Game.Player.Character.Weapons.Current.Hash == -1045183535)
				{
					this.recoil = 0.5f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
				else
				{
					this.recoil = 0.32f;
					switch (this.rnd.Next(6))
					{
					case 0:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.155f);
						return;
					case 1:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 2:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * 0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * -0.2f;
						return;
					case 3:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.155f);
						return;
					case 4:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.1f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					case 5:
						GameplayCamera.Shake(4, (this.recoil + (float)this.rnd.NextDouble()) * -0.125f);
						GameplayCamera.RelativeHeading += (this.recoil + (float)this.rnd.NextDouble()) * 0.2f;
						return;
					default:
						return;
					}
				}
			}
		}
	}

	
	private Random rnd = new Random();

	
	private bool is_mod_enabled = true;

	
	private float recoil;

	
	private float flashlight_reduced_recoil = 0.0125f;

	
	private float grip_reduced_recoil = 0.075f;

	
	private float extended_mag_reduced_recoil = 0.027f;

	
	private float scope_reduced_recoil = 0.02f;
}
