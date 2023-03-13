using System;
using System.Drawing;
using System.Threading;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Store Robbery Sandy/Paleto", 2)]
	public class StoreSandyPaleto : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 16))
				{
				case 1:
					this.Rob = new Vector3(547.3723f, 2669.218f, 42.15653f);
					this.Own = new Vector3(549.7178f, 2669.545f, 42.1565f);
					this.Headingown = 91;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 31;
					break;
				case 2:
					this.Rob = new Vector3(615.2648f, 2760.173f, 42.0881f);
					this.Own = new Vector3(612.8028f, 2762.054f, 42.0881f);
					this.Headingown = 263;
					this.h1 = false;
					this.h2 = false;
					this.h3 = true;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 196;
					break;
				case 3:
					this.Rob = new Vector3(1166.978f, 2707.315f, 38.15769f);
					this.Own = new Vector3(1165.673f, 2711.554f, 38.15769f);
					this.Headingown = 180;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 212;
					break;
				case 4:
					this.Rob = new Vector3(1198.212f, 2709.606f, 38.22264f);
					this.Own = new Vector3(1197.027f, 2711.911f, 38.22264f);
					this.Headingown = 182;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = true;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 169;
					break;
				case 5:
					this.Rob = new Vector3(-1099.7f, 2711.065f, 19.10787f);
					this.Own = new Vector3(-1102.311f, 2711.978f, 19.10787f);
					this.Headingown = 222;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = true;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 215;
					break;
				case 6:
					this.Rob = new Vector3(1730.269f, 6415.729f, 35.03722f);
					this.Own = new Vector3(1728.295f, 6416.757f, 35.03725f);
					this.Headingown = 242;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 178;
					break;
				case 7:
					this.Rob = new Vector3(3.115488f, 6511.943f, 31.87785f);
					this.Own = new Vector3(5.703722f, 6510.937f, 31.87785f);
					this.Headingown = 30;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = true;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 42;
					break;
				case 8:
					this.Rob = new Vector3(-279.9649f, 6228.875f, 31.69554f);
					this.Own = new Vector3(-277.5211f, 6230.109f, 31.72726f);
					this.Headingown = 47;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = true;
					this.h6 = false;
					this.Headinghostage = 32;
					break;
				case 9:
					this.Rob = new Vector3(1961.187f, 3742.237f, 32.34375f);
					this.Own = new Vector3(1958.959f, 3741.32f, 32.34375f);
					this.Headingown = 299;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 233;
					break;
				case 10:
					this.Rob = new Vector3(1699.438f, 4924.952f, 42.06367f);
					this.Own = new Vector3(1698.068f, 4921.866f, 42.06367f);
					this.Headingown = 330;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 233;
					break;
				case 11:
					this.Rob = new Vector3(1693.282f, 4820.517f, 42.06312f);
					this.Own = new Vector3(1695.74f, 4822.726f, 42.06312f);
					this.Headingown = 105;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = true;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 85;
					break;
				case 12:
					this.Rob = new Vector3(2677.669f, 3281.943f, 55.24113f);
					this.Own = new Vector3(2676.168f, 3280.002f, 55.24113f);
					this.Headingown = 328;
					this.h1 = true;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = false;
					this.Headinghostage = 256;
					break;
				case 13:
					this.Rob = new Vector3(1933.492f, 3728.762f, 32.84548f);
					this.Own = new Vector3(1930.643f, 3728.477f, 32.84446f);
					this.Headingown = 210;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = true;
					this.h6 = false;
					this.Headinghostage = 206;
					break;
				case 14:
					this.Rob = new Vector3(-291.8091f, 6197.145f, 31.48833f);
					this.Own = new Vector3(-292.5188f, 6200.007f, 31.4911f);
					this.Headingown = 225;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = true;
					this.Headinghostage = 315;
					break;
				case 15:
					this.Rob = new Vector3(1863.18f, 3751.132f, 33.03188f);
					this.Own = new Vector3(1863.308f, 3748.367f, 33.03188f);
					this.Headingown = 42;
					this.h1 = false;
					this.h2 = false;
					this.h3 = false;
					this.h4 = false;
					this.h5 = false;
					this.h6 = true;
					this.Headinghostage = 118;
					break;
				}
				bool flag = this.Rob.DistanceTo(Game.LocalPlayer.Character.Position) > 200f && this.Rob.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
				if (flag)
				{
					break;
				}
				GameFiber.Yield();
				WaitCount++;
				bool flag2 = WaitCount > 100;
				if (flag2)
				{
					goto Block_4;
				}
			}
			base.ShowCalloutAreaBlipBeforeAccepting(this.Rob, 30f);
			base.CalloutMessage = "Robbery In Progress";
			base.CalloutPosition = this.Rob;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Rob);
			int num = new Random().Next(1, 3);
			int num2 = num;
			if (num2 != 1)
			{
				if (num2 == 2)
				{
					this.Hostage();
				}
			}
			else
			{
				this.ShootandFight();
			}
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Robbery In Progress", "~b~Dispatch: ~w~Follow The ~y~GPS ~w~To The Location. ~w~Respond with ~g~Code 3");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_CRIME_ROBBERY_03 MC_RESPOND_CODE3");
			GameFiber.Wait(1000);
			this._Searcharea = this.Rob.Around2D(1f, 2f);
			this._Blip = new Blip(this._Searcharea, 40f);
			this._Blip.EnableRoute(Color.Yellow);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			this.Robber = new Ped(this.Robberylist[new Random().Next(this.Robberylist.Length)], this.Rob, 0f);
			this.Robber.IsPersistent = true;
			this.Robber.BlockPermanentEvents = true;
			this.Robber.Inventory.GiveNewWeapon(new WeaponAsset(this.Weaponlist[new Random().Next(this.Weaponlist.Length)]), 500, true);
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			base.OnCalloutNotAccepted();
		}

		
		public override void Process()
		{
			bool flag = StopThePedFunctions.IsPedStoppedWithSTP(this.Robber).Value && !this.Stoppedped;
			if (flag)
			{
				GameFiber.Wait(500);
				this.Owner.Tasks.Flee(this.Rob, 500f, -1);
				this.Robdeath = true;
				this.Stoppedped = true;
			}
			bool flag2 = this.Robber.IsDead && EntityExtensions.Exists(this.Robber);
			if (flag2)
			{
				GameFiber.Wait(500);
				this.Owner.Tasks.Flee(this.Rob, 500f, -1);
				this.Robdeath = true;
			}
			bool flag3 = Functions.IsPedArrested(this.Robber) || this.Robber.IsCuffed;
			if (flag3)
			{
				GameFiber.Wait(500);
				this.Robdeath = true;
				this.Owner.Tasks.Flee(this.Rob, 500f, -1);
			}
			bool flag4 = Functions.IsPedInPursuit(this.Robber) && !this.PursuitCreated;
			if (flag4)
			{
				this.PursuitCreated = true;
			}
			bool flag5 = Game.IsKeyDown(Settings.EndCall);
			if (flag5)
			{
				this.End();
			}
			bool isDead = Game.LocalPlayer.Character.IsDead;
			if (isDead)
			{
				this.End();
			}
			base.Process();
		}

		
		private void ShootandFight()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Own) <= 24f;
						if (flag)
						{
							bool flag2 = !this.h1 && !this.h2 && !this.h3 && !this.h4 && !this.h5 && !this.h6;
							if (flag2)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag3 = this.h1;
							if (flag3)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag4 = this.h2;
							if (flag4)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_m_shop_high", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag5 = this.h3;
							if (flag5)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_y_shop_mid", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag6 = this.h4;
							if (flag6)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_y_shop_low", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag7 = this.h5;
							if (flag7)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("s_f_m_fembarber", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
							bool flag8 = this.h6;
							if (flag8)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(500);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Owner = new Ped("u_m_y_tattoo_01", this.Own, (float)this.Headingown);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								this.Owner.BlockPermanentEvents = true;
								this.Owner.Tasks.PutHandsUp(-1, this.Robber);
								this.Robber.Tasks.AimWeaponAt(this.Owner, -1);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag9 = !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 18f;
						if (flag9)
						{
							new RelationshipGroup("BAD");
							this.Robber.RelationshipGroup = "BAD";
							Game.SetRelationshipBetweenRelationshipGroups("BAD", "COP", 5);
							this.Robber.Tasks.FightAgainst(this.Owner, -1);
							this.Ownerdeath();
							break;
						}
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void Hostage()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Own) <= 24f;
						if (flag)
						{
							bool flag2 = !this.h1 && !this.h2 && !this.h3 && !this.h4 && !this.h5 && !this.h6;
							if (flag2)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.SetPositionWithSnap(this.Own);
								this.Robber.Heading = (float)this.Headingown;
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag3 = this.h1;
							if (flag3)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped(this.Storelist[new Random().Next(this.Storelist.Length)], this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag4 = this.h2;
							if (flag4)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_m_shop_high", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag5 = this.h3;
							if (flag5)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_y_shop_mid", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag6 = this.h4;
							if (flag6)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_y_shop_low", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag7 = this.h5;
							if (flag7)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("s_f_m_fembarber", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
							bool flag8 = this.h6;
							if (flag8)
							{
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(300);
								World.SpawnExplosion(this.Own, 0, 0f, false, true, 0f);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								GameFiber.Sleep(100);
								Extensions.ClearAreaOfPeds(this.Own, 2f);
								this.Robber.Heading = (float)this.Headinghostage;
								this.Owner = new Ped("u_m_y_tattoo_01", this.Robber.Position + this.Robber.ForwardVector * 0.9f, 0f);
								this.Owner.Heading = this.Robber.Heading;
								this.Owner.Position = this.Robber.GetOffsetPosition(new Vector3(0f, 0.14445f, 0f));
								this.Robber.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop", 6f, 1);
								this.Owner.Tasks.PlayAnimation("misssagrab_inoffice", "hostage_loop_mrk", 6f, 1);
								this.Owner.IsPersistent = true;
								this.Owner.MaxHealth = 175;
								this.Owner.Health = 100;
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag9 = !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
						if (flag9)
						{
							Game.DisplaySubtitle("~r~Suspect: ~w~Dont get closer!!!!");
							break;
						}
						bool isShooting = Game.LocalPlayer.Character.IsShooting;
						if (isShooting)
						{
							GameFiber.Sleep(200);
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag10 = !this.Robdeath && !this.Close && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
						if (flag10)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to try talk to the suspect!", Settings.Interact), false);
							bool flag11 = Game.IsKeyDown(Settings.Interact);
							if (flag11)
							{
								int num = new Random().Next(1, 3);
								int num2 = num;
								if (num2 != 1)
								{
									if (num2 == 2)
									{
										this.Giveup = false;
									}
								}
								else
								{
									this.Giveup = true;
								}
								Game.DisplaySubtitle("~b~Player: ~w~Put down your weapon and release the hostage!!");
								break;
							}
						}
						bool flag12 = !this.Giveup && !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 6f;
						if (flag12)
						{
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
						bool isShooting2 = Game.LocalPlayer.Character.IsShooting;
						if (isShooting2)
						{
							GameFiber.Sleep(200);
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag13 = !this.Giveup && !this.Robdeath && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 6f;
						if (flag13)
						{
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
						bool isShooting3 = Game.LocalPlayer.Character.IsShooting;
						if (isShooting3)
						{
							GameFiber.Sleep(200);
							this.Close = true;
							this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
							this.Ownerdeath();
							break;
						}
						bool flag14 = this.Giveup && !this.Robdeath && !this.Close;
						if (flag14)
						{
							bool flag15 = !this.Robdeath && !this.Close && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
							if (flag15)
							{
								Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
								bool flag16 = Game.IsKeyDown(Settings.Interact);
								if (flag16)
								{
									switch (new Random().Next(1, 5))
									{
									case 1:
										this.Robbertalk = "~r~Suspect: ~w~Sorry, it was not meant to end like this...";
										break;
									case 2:
										this.Robbertalk = "~r~Suspect: ~w~Ok dont shoot, i will surrender...";
										break;
									case 3:
										this.Robbertalk = "~r~Suspect: ~w~I only wanted some money for food, dont shoot...";
										break;
									case 4:
										this.Robbertalk = "~r~Suspect: ~w~I'm not a killer, dont shoot...";
										break;
									}
									Game.DisplaySubtitle(this.Robbertalk);
									GameFiber.Sleep(2800);
									this.Robber.Inventory.EquippedWeapon.DropToGround();
									this.Robber.Tasks.PutHandsUp(-1, Game.LocalPlayer.Character);
									this.Owner.Tasks.Flee(this.Rob, 500f, -1);
									break;
								}
							}
						}
						bool flag17 = !this.Giveup && !this.Robdeath && !this.Close;
						if (flag17)
						{
							bool flag18 = !this.Robdeath && !this.Close && Game.LocalPlayer.Character.DistanceTo(this.Robber) <= 7f;
							if (flag18)
							{
								Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~to continue talk!", Settings.Interact), false);
								bool flag19 = Game.IsKeyDown(Settings.Interact);
								if (flag19)
								{
									switch (new Random().Next(1, 4))
									{
									case 1:
										this.Robbertalk = "~r~Suspect: ~w~Do you think I want to end up in jail!!";
										break;
									case 2:
										this.Robbertalk = "~r~Suspect: ~w~You are funny!!";
										break;
									case 3:
										this.Robbertalk = "~r~Suspect: ~w~Shut up!!";
										break;
									}
									Game.DisplaySubtitle(this.Robbertalk);
									GameFiber.Sleep(2000);
									this.Robber.Tasks.FireWeaponAt(this.Owner, -1, -687903391);
									this.Ownerdeath();
									this.counter++;
									break;
								}
							}
						}
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void Ownerdeath()
		{
			while (this.Scenariorunning)
			{
				GameFiber.Yield();
				bool flag = EntityExtensions.Exists(this.Owner) && this.Owner.IsDead;
				if (flag)
				{
					this.Robber.Tasks.Clear();
					GameFiber.Wait(100);
					this.Robber.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
					break;
				}
			}
		}

		
		public override void End()
		{
			this.Scenariorunning = false;
			Game.LogTrivial("ManiacCallouts - Suspicious Activity Cleaned.");
			bool flag = EntityExtensions.Exists(this.Robber);
			if (flag)
			{
				bool isDead = this.Robber.IsDead;
				if (isDead)
				{
					Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_NEUTRALIZED MC_WE_ARE_CODE_4");
					Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units Suspect Is Neutralized");
				}
				else
				{
					bool flag2 = Functions.IsPedArrested(this.Robber);
					if (flag2)
					{
						Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_ARRESTED MC_WE_ARE_CODE_4");
						Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units Suspect Is Arrested");
					}
					else
					{
						bool isAlive = this.Robber.IsAlive;
						if (isAlive)
						{
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units ~g~Code 4");
						}
					}
				}
			}
			bool flag3 = !EntityExtensions.Exists(this.Robber);
			if (flag3)
			{
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Robbery In Progress", "~b~Dispatch: ~w~All Units ~g~Code 4");
			}
			bool flag4 = EntityExtensions.Exists(this._Blip);
			if (flag4)
			{
				this._Blip.Delete();
			}
			bool flag5 = EntityExtensions.Exists(this._Blip2);
			if (flag5)
			{
				this._Blip2.Delete();
			}
			bool flag6 = EntityExtensions.Exists(this.Owner);
			if (flag6)
			{
				this.Owner.Dismiss();
			}
			this.h1 = false;
			bool flag7 = EntityExtensions.Exists(this.Robber);
			if (flag7)
			{
				this.Robber.Dismiss();
			}
			base.End();
		}

		
		private Ped Robber;

		
		private Ped Owner;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private const string GrabHostage = "misssagrab_inoffice";

		
		private const string SuspectGrab = "hostage_loop";

		
		private const string HostageGrabbed = "hostage_loop_mrk";

		
		private Vector3 _Searcharea;

		
		private Vector3 Rob;

		
		private Vector3 Own;

		
		private string[] Storelist = new string[]
		{
			"mp_m_shopkeep_01",
			"s_f_m_sweatshop_01"
		};

		
		private string[] Robberylist = new string[]
		{
			"g_m_m_chicold_01",
			"a_f_y_rurmeth_01",
			"a_m_m_tramp_01"
		};

		
		private string[] Weaponlist = new string[]
		{
			"weapon_doubleaction",
			"weapon_dbshotgun",
			"weapon_microsmg"
		};

		
		private bool PursuitCreated = false;

		
		private bool Blip = false;

		
		private bool Scenariorunning = false;

		
		private bool Robdeath = false;

		
		private bool Stoppedped = false;

		
		private bool Close = false;

		
		private bool h1;

		
		private bool h2;

		
		private bool h3;

		
		private bool h4;

		
		private bool h5;

		
		private bool h6;

		
		private bool Giveup = false;

		
		private string Robbertalk;

		
		private int Headingown;

		
		private int Headinghostage;

		
		private int counter = 0;
	}
}
