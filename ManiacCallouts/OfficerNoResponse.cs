using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using LSPD_First_Response.Engine.Scripting.Entities;
using LSPD_First_Response.Mod.API;
using LSPD_First_Response.Mod.Callouts;
using ManiacCallouts.API;
using Rage;

namespace ManiacCallouts.Callouts
{
	
	[CalloutInfo("Officer No Response", 2)]
	public class OfficerNoResponse : Callout
	{
		
		public override bool OnBeforeCalloutDisplayed()
		{
			int WaitCount = 0;
			for (;;)
			{
				switch (new Random().Next(1, 7))
				{
				case 1:
					this.Calloutloc = new Vector3(-56.02358f, -1318.19f, 28.8011f);
					this.Headingveh = 268;
					this.Girlsex = new Vector3(-49.50379f, -1324.452f, 28.25687f);
					this.Girlsexhead = 273;
					this.Copsex = new Vector3(-51.61308f, -1323.46f, 29.25155f);
					this.Copdrugs = new Vector3(-54.1773f, -1314.062f, 29.23875f);
					this.Copdrughead = 174;
					this.Copdown = new Vector3(-59.68276f, -1317.965f, 28.97044f);
					this.Copdownhead = 358;
					this.Susp = new Vector3(-58.85277f, -1315.804f, 29.15814f);
					this.Outside = false;
					break;
				case 2:
					this.Calloutloc = new Vector3(450.5102f, -2167.684f, 5.567219f);
					this.Headingveh = 129;
					this.Girlsex = new Vector3(445.7112f, -2169.028f, 4.917675f);
					this.Girlsexhead = 55;
					this.Copsex = new Vector3(448.0569f, -2170.814f, 5.917675f);
					this.Copdrugs = new Vector3(449.0604f, -2175.778f, 5.917676f);
					this.Copdrughead = 359;
					this.Copdown = new Vector3(453.61f, -2165.12f, 5.917677f);
					this.Copdownhead = 221;
					this.Susp = new Vector3(454.9074f, -2167.999f, 5.917676f);
					this.Outside = false;
					break;
				case 3:
					this.Calloutloc = new Vector3(-483.2008f, -452.8614f, 33.85236f);
					this.Headingveh = 261;
					this.Girlsex = new Vector3(-478.0538f, -447.9926f, 33.2013f);
					this.Girlsexhead = 2;
					this.Copsex = new Vector3(-477.8965f, -450.5123f, 34.2013f);
					this.Copdrugs = new Vector3(-482.7526f, -447.9729f, 34.20129f);
					this.Copdrughead = 162;
					this.Copdown = new Vector3(-487.0155f, -452.3421f, 34.20129f);
					this.Copdownhead = 164;
					this.Susp = new Vector3(-486.588f, -449.6795f, 34.20129f);
					this.Outside = false;
					break;
				case 4:
					this.Calloutloc = new Vector3(596.7302f, 138.4835f, 97.54226f);
					this.Headingveh = 340;
					this.Girlsex = new Vector3(594.8397f, 146.5419f, 97.04152f);
					this.Girlsexhead = 339;
					this.Copsex = new Vector3(594.2549f, 144.2123f, 98.04152f);
					this.Copdrugs = new Vector3(592.7161f, 141.9384f, 98.04152f);
					this.Copdrughead = 247;
					this.Copdown = new Vector3(595.4298f, 135.2196f, 98.04152f);
					this.Copdownhead = 57;
					this.Susp = new Vector3(598.8075f, 133.9251f, 98.04151f);
					this.Outside = false;
					break;
				case 5:
					this.Calloutloc = new Vector3(1965.388f, 3046.913f, 46.4649f);
					this.Headingveh = 231;
					this.Girlsex = new Vector3(1975.902f, 3028.575f, 46.05633f);
					this.Girlsexhead = 188;
					this.Copsex = new Vector3(1976.229f, 3030.556f, 47.05633f);
					this.Copdrugs = new Vector3(1985.714f, 3044.117f, 47.21485f);
					this.Copdrughead = 146;
					this.Copdown = new Vector3(1964.025f, 3051.93f, 46.81939f);
					this.Copdownhead = 156;
					this.Susp = new Vector3(1967.301f, 3049.468f, 47.0146f);
					this.Outside = true;
					break;
				case 6:
					this.Calloutloc = new Vector3(1676.747f, 4888.722f, 41.52567f);
					this.Headingveh = 269;
					this.Girlsex = new Vector3(1695.123f, 4886.976f, 41.03517f);
					this.Girlsexhead = 331;
					this.Copsex = new Vector3(1694.508f, 4885.203f, 42.03517f);
					this.Copdrugs = new Vector3(1688.823f, 4886.501f, 42.03343f);
					this.Copdrughead = 15;
					this.Copdown = new Vector3(1680.454f, 4887.302f, 42.03662f);
					this.Copdownhead = 190;
					this.Susp = new Vector3(1683.597f, 4887.912f, 42.02382f);
					this.Outside = true;
					break;
				}
				bool flag = this.Calloutloc.DistanceTo(Game.LocalPlayer.Character.Position) > 200f && this.Calloutloc.DistanceTo(Game.LocalPlayer.Character.Position) < (float)Settings.MaxCalloutDistance;
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
			base.ShowCalloutAreaBlipBeforeAccepting(this.Calloutloc, 30f);
			base.CalloutMessage = "Officer No Response";
			base.CalloutPosition = this.Calloutloc;
			Functions.PlayScannerAudioUsingPosition("WE_HAVE CRIME_RESISTING_ARREST_02 IN_OR_ON_POSITION", this.Calloutloc);
			return base.OnBeforeCalloutDisplayed();
			Block_4:
			return false;
		}

		
		public override bool OnCalloutAccepted()
		{
			bool outside = this.Outside;
			if (outside)
			{
				this.CVehicle = new string[]
				{
					"sheriff",
					"sheriff2"
				};
				this.Coplist = new string[]
				{
					"s_m_y_sheriff_01"
				};
			}
			else
			{
				this.CVehicle = new string[]
				{
					"POLICE",
					"POLICE2",
					"POLICE3"
				};
				this.Coplist = new string[]
				{
					"S_M_Y_COP_01"
				};
			}
			Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~r~Officer No Response", "~b~Dispatch: ~w~Follow The ~b~GPS ~w~To The Location. ~w~Respond with ~g~Code 2");
			GameFiber.Wait(100);
			Game.DisplayNotification(string.Format("~w~Press ~y~{0} ~w~Key At Anytime To End The Callout", Settings.EndCall));
			Functions.PlayScannerAudio("MC_ASSISTANCE_REQUIRED_02 MC_RESPOND_CODE_02_02");
			GameFiber.Wait(1000);
			Extensions.ClearAreaOfVehicles(this.Calloutloc, 10f);
			this.PoliceVehicle = new Vehicle(this.CVehicle[new Random().Next(this.CVehicle.Length)], this.Calloutloc, (float)this.Headingveh);
			this.PoliceVehicle.IsPersistent = true;
			this.PoliceVehicle.IsEngineOn = true;
			this._Blip = this.PoliceVehicle.AttachBlip();
			this._Blip.EnableRoute(Color.Blue);
			this._Blip.Color = Color.Yellow;
			this._Blip.Alpha = 0.5f;
			switch (new Random().Next(1, 4))
			{
			case 1:
				this.Pros();
				break;
			case 2:
				this.Drugs();
				break;
			case 3:
				this.Down();
				break;
			}
			return base.OnCalloutAccepted();
		}

		
		public override void OnCalloutNotAccepted()
		{
			base.OnCalloutNotAccepted();
		}

		
		public override void Process()
		{
			bool flag = Game.IsKeyDown(Settings.EndCall);
			if (flag)
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

		
		private void Pros()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						this.Hooker = new Ped(this.Girlsexlist[new Random().Next(this.Girlsexlist.Length)], this.Girlsex, (float)this.Girlsexhead);
						this.Hooker.IsPersistent = true;
						this.Hooker.BlockPermanentEvents = true;
						this.Hooker.IsPositionFrozen = true;
						this.Hooker.IsCollisionEnabled = false;
						this.Cop = new Ped(this.Coplist[new Random().Next(this.Coplist.Length)], this.Copsex, (float)this.Girlsexhead);
						this.Cop.Heading = this.Hooker.Heading;
						Persona.FromExistingPed(this.Cop).Wanted = false;
						StopThePedFunctions.SetPedAlcoholOverLimit(this.Cop, false);
						StopThePedFunctions.SetPedUnderDrugsInfluence(this.Cop, false);
						this.Cop.Position = this.Hooker.GetOffsetPosition(new Vector3(0f, -0.24445f, 0f));
						this.Cop.IsPersistent = true;
						this.Cop.BlockPermanentEvents = true;
						this.Cop.Inventory.Weapons.Clear();
						this.Cop.Tasks.PlayAnimation("rcmpaparazzo_2", "shag_loop_a", 6f, 1);
						this.Hooker.Tasks.PlayAnimation("rcmpaparazzo_2", "shag_loop_poppy", 6f, 1);
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Hooker) < 15f;
						if (flag)
						{
							switch (new Random().Next(1, 4))
							{
							case 1:
								this.Hooker.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_GENERIC_FEM", 1, 6);
								break;
							case 2:
								this.Hooker.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_GENERIC_FEM", 2, 6);
								break;
							case 3:
								this.Hooker.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "SEX_GENERIC_FEM", 3, 6);
								break;
							}
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = Game.LocalPlayer.Character.DistanceTo(this.Hooker) < 3f && this.Hooker.IsAlive && this.Cop.IsAlive;
						if (flag2)
						{
							Game.DisplaySubtitle("~b~Cop: ~w~Crap, my colleague is here..");
							this.Hooker.Tasks.Clear();
							this.Hooker.IsPositionFrozen = false;
							this.Hooker.IsCollisionEnabled = true;
							this.Hooker.BlockPermanentEvents = false;
							this.Cop.Tasks.Clear();
							GameFiber.Sleep(1000);
							this.Hooker.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "GENERIC_SHOCKED_MED", 1, 9);
							GameFiber.Sleep(100);
							this.Hooker.Tasks.ReactAndFlee(this.Cop);
							Functions.SetPedArrestIgnoreGroup(this.Cop, true);
							GameFiber.Sleep(1000);
							this.Cop.Tasks.AimWeaponAt(Game.LocalPlayer.Character, 10);
							this._Blip.Delete();
							break;
						}
						bool flag3 = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Hooker) < 8f && this.Hooker.IsAlive && this.Cop.IsAlive;
						if (flag3)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To Talk!", Settings.Interact), false);
							bool flag4 = Game.IsKeyDown(Settings.Interact);
							if (flag4)
							{
								Game.DisplaySubtitle("~b~Player: ~w~Excuse me, but what are you doing?!");
								GameFiber.Sleep(1000);
								this.Hooker.Tasks.Clear();
								this.Hooker.IsPositionFrozen = false;
								this.Hooker.IsCollisionEnabled = true;
								this.Hooker.BlockPermanentEvents = false;
								this.Cop.Tasks.Clear();
								GameFiber.Sleep(1000);
								this.Hooker.PlayAmbientSpeech("S_F_Y_HOOKER_01_WHITE_FULL_01", "GENERIC_SHOCKED_MED", 1, 9);
								GameFiber.Sleep(100);
								this.Hooker.Tasks.ReactAndFlee(this.Cop);
								Functions.SetPedArrestIgnoreGroup(this.Cop, true);
								GameFiber.Sleep(1000);
								this.Cop.Tasks.AimWeaponAt(Game.LocalPlayer.Character, 10);
								this._Blip.Delete();
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag5 = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Cop) < 8f && this.Cop.IsAlive;
						if (flag5)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To Talk!", Settings.Interact), false);
							bool flag6 = Game.IsKeyDown(Settings.Interact);
							if (flag6)
							{
								switch (new Random().Next(1, 4))
								{
								case 1:
									this.Robbertalk = "~b~Cop: ~w~She was so tempting, please forgive me...";
									break;
								case 2:
									this.Robbertalk = "~b~Cop: ~w~What should i do when i am horny??";
									break;
								case 3:
									this.Robbertalk = "~b~Cop: ~w~Do you not have something better to do?";
									break;
								}
								Game.DisplaySubtitle(this.Robbertalk);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag7 = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Cop) < 8f && this.Cop.IsAlive;
						if (flag7)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To Talk!", Settings.Interact), false);
							bool flag8 = Game.IsKeyDown(Settings.Interact);
							if (flag8)
							{
								Game.DisplaySubtitle("~b~Player: ~w~You're breaking the law!");
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag9 = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Cop) < 8f && this.Cop.IsAlive;
						if (flag9)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To Talk!", Settings.Interact), false);
							bool flag10 = Game.IsKeyDown(Settings.Interact);
							if (flag10)
							{
								Game.DisplaySubtitle("~b~Cop: ~w~What are you going to do about it?");
								this.Cop.Tasks.Clear();
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag11 = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Cop) < 8f && this.Cop.IsAlive;
						if (flag11)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~o~To let this be ~w~OR Press ~y~X ~r~To give consequences!", Settings.Interact), false);
							bool flag12 = Game.IsKeyDown(Settings.Interact);
							if (flag12)
							{
								this.LetItBe();
								break;
							}
							bool flag13 = Game.IsKeyDown(Keys.X);
							if (flag13)
							{
								this.Arrest();
								break;
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

		
		private void LetItBe()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						Game.DisplaySubtitle("~b~Player: ~w~Get out of here before I regret it!");
						GameFiber.Sleep(1000);
						this.Cop.BlockPermanentEvents = false;
						this.Cop.Tasks.FollowNavigationMeshToPosition(this.PoliceVehicle.LeftPosition, 2f, 2f, 1f, -1);
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = this.Cop.DistanceTo(this.PoliceVehicle) < 3f;
						if (flag)
						{
							this.Cop.Tasks.EnterVehicle(this.PoliceVehicle, -1);
							break;
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag2 = this.Cop.IsInVehicle(this.PoliceVehicle, false);
						if (flag2)
						{
							GameFiber.Wait(1000);
							this.Cop.Tasks.CruiseWithVehicle(this.PoliceVehicle, 8f, 183);
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

		
		private void Arrest()
		{
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						Game.DisplaySubtitle("~b~Player: ~w~I have to follow the law!");
						GameFiber.Sleep(1000);
						Functions.SetPedAsStopped(this.Cop, true);
					}
				}
				catch (ThreadAbortException e)
				{
					this.End();
				}
			});
		}

		
		private void Drugs()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						this.Cop = new Ped(this.Coplist[new Random().Next(this.Coplist.Length)], this.Copdrugs, (float)this.Copdrughead);
						Persona.FromExistingPed(this.Cop).Wanted = false;
						StopThePedFunctions.SetPedAlcoholOverLimit(this.Cop, false);
						StopThePedFunctions.SetPedUnderDrugsInfluence(this.Cop, true);
						this.Cop.IsPersistent = true;
						this.Cop.BlockPermanentEvents = true;
						this.Cop.Inventory.Weapons.Clear();
						Extensions.PlayTaskScen(this.Cop, "WORLD_HUMAN_SMOKING_POT", 0, false);
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Cop) < 5f && this.Cop.IsAlive;
						if (flag)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To Talk!", Settings.Interact), false);
							bool flag2 = Game.IsKeyDown(Settings.Interact);
							if (flag2)
							{
								Game.DisplaySubtitle("~b~Player: ~w~What the hell are you doing?");
								this._Blip.Delete();
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag3 = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Cop) < 8f && this.Cop.IsAlive;
						if (flag3)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~w~To Talk!", Settings.Interact), false);
							bool flag4 = Game.IsKeyDown(Settings.Interact);
							if (flag4)
							{
								switch (new Random().Next(1, 4))
								{
								case 1:
									this.Robbertalk = "~b~Cop: ~w~I take a smoke, any problem with that?";
									break;
								case 2:
									this.Robbertalk = "~b~Cop: ~w~Only this that cures against my painful back, any problem?";
									break;
								case 3:
									this.Robbertalk = "~b~Cop: ~w~Sometimes you are worth this, any problems?";
									break;
								}
								this.Cop.Tasks.Clear();
								GameFiber.Sleep(1000);
								Game.DisplaySubtitle(this.Robbertalk);
								break;
							}
						}
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag5 = Game.LocalPlayer.Character.IsOnFoot && Game.LocalPlayer.Character.DistanceTo(this.Cop) < 8f && this.Cop.IsAlive;
						if (flag5)
						{
							Game.DisplayHelp(string.Format("~w~Press ~y~{0} ~o~To let this be ~w~OR Press ~y~X ~r~To give consequences!", Settings.Interact), false);
							bool flag6 = Game.IsKeyDown(Settings.Interact);
							if (flag6)
							{
								this.LetItBe();
								break;
							}
							bool flag7 = Game.IsKeyDown(Keys.X);
							if (flag7)
							{
								this.Arrest();
								break;
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

		
		private void Down()
		{
			this.Scenariorunning = true;
			GameFiber.StartNew(delegate()
			{
				try
				{
					if (this.Scenariorunning)
					{
						GameFiber.Yield();
						this.Cop = new Ped(this.Coplist[new Random().Next(this.Coplist.Length)], this.Copdown, (float)this.Copdownhead);
						Persona.FromExistingPed(this.Cop).Wanted = false;
						this.Cop.IsPersistent = true;
						this.Cop.BlockPermanentEvents = true;
						this.Cop.Inventory.Weapons.Clear();
						this.Cop.Tasks.PlayAnimation("missprologueig_2", "idle_on_floor_gaurd", 6f, 1);
						this.Suspect = new Ped(this.Suspectlist[new Random().Next(this.Suspectlist.Length)], this.Susp, 0f);
						Persona.FromExistingPed(this.Suspect).Wanted = true;
						this.Suspect.IsPersistent = true;
						this.Suspect.BlockPermanentEvents = true;
						this.Suspect.Inventory.GiveNewWeapon("weapon_pumpshotgun", 500, true);
						this.Suspect.Tasks.FightAgainst(this.Cop, -1);
					}
					while (this.Scenariorunning)
					{
						GameFiber.Yield();
						bool flag = Game.LocalPlayer.Character.DistanceTo(this.Suspect) < 30f && this.Cop.IsDead;
						if (flag)
						{
							this.Suspect.Tasks.Clear();
							GameFiber.Wait(200);
							this.Suspect.Tasks.FightAgainst(Game.LocalPlayer.Character, -1);
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

		
		public override void End()
		{
			this.Scenariorunning = false;
			Game.LogTrivial("ManiacCallouts - Officer No Response Cleaned.");
			bool flag = EntityExtensions.Exists(this.Suspect);
			if (flag)
			{
				bool isDead = this.Suspect.IsDead;
				if (isDead)
				{
					Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_NEUTRALIZED MC_WE_ARE_CODE_4");
					Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Officer No Response", "~b~Dispatch: ~w~All Units Suspect Is Neutralized");
				}
				else
				{
					bool flag2 = Functions.IsPedArrested(this.Suspect);
					if (flag2)
					{
						Functions.PlayScannerAudio("MC_ALL_UNITS MC_SUSPECT_ARRESTED MC_WE_ARE_CODE_4");
						Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Officer No Response", "~b~Dispatch: ~w~All Units Suspect Is Arrested");
					}
					else
					{
						bool isAlive = this.Suspect.IsAlive;
						if (isAlive)
						{
							Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
							Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Officer No Response", "~b~Dispatch: ~w~All Units ~g~Code 4");
						}
					}
				}
			}
			bool flag3 = !EntityExtensions.Exists(this.Suspect);
			if (flag3)
			{
				Functions.PlayScannerAudio("MC_ATTENTION_ALL_UNITS MC_WE_ARE_CODE_4");
				Game.DisplayNotification("web_lossantospolicedept", "web_lossantospolicedept", "~w~ManiacCallouts", "~y~Officer No Response", "~b~Dispatch: ~w~All Units ~g~Code 4");
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
			bool flag6 = EntityExtensions.Exists(this.Cop);
			if (flag6)
			{
				this.Cop.Dismiss();
			}
			bool flag7 = EntityExtensions.Exists(this.Suspect);
			if (flag7)
			{
				this.Suspect.Dismiss();
			}
			bool flag8 = EntityExtensions.Exists(this.Hooker);
			if (flag8)
			{
				this.Hooker.Dismiss();
			}
			base.End();
		}

		
		private Ped Suspect;

		
		private Ped Hooker;

		
		private Ped Cop;

		
		private Blip _Blip;

		
		private Blip _Blip2;

		
		private LHandle Pursuit;

		
		private Vehicle SuspectVehicle;

		
		private Vehicle PoliceVehicle;

		
		private Vector3 Calloutloc;

		
		private Vector3 Girlsex;

		
		private Vector3 Copsex;

		
		private Vector3 Copdrugs;

		
		private Vector3 Copdown;

		
		private Vector3 Susp;

		
		private string[] Suspectlist = new string[]
		{
			"a_f_m_downtown_01",
			"a_m_m_hillbilly_02",
			"a_f_m_fatwhite_01",
			"a_f_y_hippie_01",
			"a_m_m_salton_03",
			"a_m_m_genfat_02",
			"a_m_m_soucent_03",
			"a_f_y_rurmeth_01"
		};

		
		private string[] Girlsexlist = new string[]
		{
			"s_f_y_hooker_01",
			"s_f_y_hooker_02"
		};

		
		private string[] Coplist;

		
		private string[] CVehicle;

		
		private string Robbertalk;

		
		private const string Sexactive = "rcmpaparazzo_2";

		
		private const string Sexgirl = "shag_loop_poppy";

		
		private const string Sexcop = "shag_loop_a";

		
		private const string Hostagefloor = "missprologueig_2";

		
		private const string Hostagefloor1 = "idle_on_floor_gaurd";

		
		private bool PursuitCreated = false;

		
		private bool Outside = false;

		
		private bool Scenariorunning = false;

		
		private string Policetalk;

		
		private int Headingveh;

		
		private int Girlsexhead;

		
		private int Copdrughead;

		
		private int Copdownhead;
	}
}
