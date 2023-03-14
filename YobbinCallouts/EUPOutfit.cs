using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace YobbinCallouts
{
	
	public class EUPOutfit
	{
		
		
		
		public string Description { get; private set; }

		
		
		
		public IReadOnlyDictionary<EPedComponents, Component> Components { get; private set; } = new Dictionary<EPedComponents, Component>();

		
		
		
		public IReadOnlyDictionary<EProps, Component> Props { get; private set; } = new Dictionary<EProps, Component>();

		
		
		
		public EGender PedGender { get; private set; }

		
		public EUPOutfit(string description, EGender gender, IDictionary<EPedComponents, Component> componentConfig, IDictionary<EProps, Component> propConfig)
		{
			this.PedGender = gender;
			this.Components = componentConfig.ToDictionary((KeyValuePair<EPedComponents, Component> x) => x.Key, (KeyValuePair<EPedComponents, Component> x) => x.Value);
			this.Props = propConfig.ToDictionary((KeyValuePair<EProps, Component> x) => x.Key, (KeyValuePair<EProps, Component> x) => x.Value);
			this.Description = description;
		}

		
		public EUPOutfit(InitializationFile INI, string sectionName, out bool success)
		{
			this.Description = sectionName;
			success = this._initializePedFromINI(INI, sectionName);
		}

		
		public EUPOutfit(string sectionName, out bool success)
		{
			this.Description = sectionName;
			InitializationFile wardrobeINI = new InitializationFile("Plugins\\EUP\\wardrobe.ini");
			InitializationFile presetsINI = new InitializationFile("Plugins\\EUP\\presetoutfits.ini");
			bool wardrobeSuccess = this._initializePedFromINI(wardrobeINI, sectionName);
			Game.LogTrivial("Found " + sectionName + " in wardrobe: " + wardrobeSuccess.ToString());
			bool flag = wardrobeSuccess;
			if (flag)
			{
				success = true;
			}
			else
			{
				bool presetSuccess = this._initializePedFromINI(presetsINI, sectionName);
				success = presetSuccess;
				Game.LogTrivial("Found " + sectionName + " in presets: " + presetSuccess.ToString());
			}
		}

		
		private bool _initializePedFromINI(InitializationFile INI, string sectionName)
		{
			this.Description = sectionName;
			Game.LogTrivial("Reading ped configuration for " + sectionName + " from INI file " + INI.FileName);
			this.PedGender = INI.ReadEnum<EGender>(sectionName, "Gender", EGender.UNKNOWN);
			Dictionary<EPedComponents, Component> newProps = new Dictionary<EPedComponents, Component>();
			this.Components = this._readComponentSettingsFromINI<EPedComponents>(INI, sectionName).ToDictionary((KeyValuePair<Enum, Component> x) => (EPedComponents)x.Key, (KeyValuePair<Enum, Component> x) => x.Value);
			this.Props = this._readComponentSettingsFromINI<EProps>(INI, sectionName).ToDictionary((KeyValuePair<Enum, Component> x) => (EProps)x.Key, (KeyValuePair<Enum, Component> x) => x.Value);
			return INI.DoesSectionExist(sectionName);
		}

		
		private bool _convertIniEntryToVariations(string part, string compSetting, out Tuple<int, int> variation)
		{
			variation = Tuple.Create<int, int>(0, 0);
			string[] compSettings = compSetting.Split(new char[]
			{
				':'
			});
			bool flag = compSettings.Length != 2;
			bool result;
			if (flag)
			{
				Game.LogTrivial("Setting for " + part + " does not match expected format of num:num");
				result = false;
			}
			else
			{
				int drawableVar = 0;
				int textureVar = 0;
				bool success = true;
				success = (success && int.TryParse(compSettings[0], out drawableVar));
				success = (success && int.TryParse(compSettings[1], out textureVar));
				bool flag2 = !success;
				if (flag2)
				{
					Game.LogTrivial("Setting for " + part + " contained invalid non-int values");
					result = false;
				}
				else
				{
					drawableVar--;
					textureVar--;
					Game.LogTrivial(string.Concat(new string[]
					{
						"Successfully parsed component ",
						part,
						" to variation ",
						drawableVar.ToString(),
						", texture ",
						textureVar.ToString()
					}));
					variation = Tuple.Create<int, int>(drawableVar, textureVar);
					result = true;
				}
			}
			return result;
		}

		
		private IReadOnlyDictionary<Enum, Component> _readComponentSettingsFromINI<T>(InitializationFile INI, string sectionName)
		{
			Dictionary<Enum, Component> newComponents = new Dictionary<Enum, Component>();
			foreach (object obj in Enum.GetValues(typeof(T)))
			{
				Enum component = (Enum)obj;
				string compSetting = INI.ReadString(sectionName, component.ToString(), null);
				bool flag = compSetting == null;
				if (flag)
				{
					string str = "No setting found for ";
					Enum @enum = component;
					Game.LogTrivial(str + ((@enum != null) ? @enum.ToString() : null));
				}
				else
				{
					Tuple<int, int> variation;
					bool success = this._convertIniEntryToVariations(component.ToString(), compSetting, out variation);
					newComponents.Add(component, new Component(component, variation.Item1, variation.Item2));
				}
			}
			return newComponents;
		}

		
		private Model _getModelForGender()
		{
			EGender pedGender = this.PedGender;
			EGender egender = pedGender;
			Model result;
			if (egender != EGender.Male)
			{
				if (egender != EGender.Female)
				{
					result = new Model("ERROR_INVALID_GENDER");
				}
				else
				{
					result = new Model("MP_F_FREEMODE_01");
				}
			}
			else
			{
				result = new Model("MP_M_FREEMODE_01");
			}
			return result;
		}

		
		public bool ApplyOutfitToPlayer(bool allowChangeModel, bool allowChangeModelInVehicle = true, bool restoreWeapons = true, bool randomizeFace = false)
		{
			Model requiredModel = this._getModelForGender();
			Vehicle prevVehicle = Game.LocalPlayer.Character.CurrentVehicle;
			int prevSeat = -1;
			Dictionary<WeaponHash, short> inventoryBefore = Game.LocalPlayer.Character.Inventory.Weapons.ToDictionary((WeaponDescriptor w) => w.Hash, (WeaponDescriptor w) => w.Ammo);
			bool flag = Game.LocalPlayer.Model != requiredModel;
			if (flag)
			{
				Game.LogTrivial("Player model " + Game.LocalPlayer.Model.Name + " does not match outfit model of " + requiredModel.Name);
				if (!allowChangeModel)
				{
					Game.LogTrivial("Cannot apply outfit because models do not match");
					return false;
				}
				bool flag2 = prevVehicle && !allowChangeModelInVehicle;
				if (flag2)
				{
					Game.LogTrivial("Cannot change player model in vehicle");
					return false;
				}
				Game.LogTrivial("Changing ped model to " + requiredModel.Name);
				bool flag3 = !requiredModel.IsValid;
				if (flag3)
				{
					Game.LogTrivial("Unable to change ped model to " + requiredModel.Name + " - model may be invalid or gender may not have been specified");
					return false;
				}
				Game.LocalPlayer.Model = requiredModel;
				GameFiber.StartNew(delegate()
				{
					bool flag4 = prevVehicle;
					if (flag4)
					{
						Game.LocalPlayer.Character.WarpIntoVehicle(prevVehicle, prevSeat);
					}
					GameFiber.Sleep(300);
					bool restoreWeapons2 = restoreWeapons;
					if (restoreWeapons2)
					{
						foreach (KeyValuePair<WeaponHash, short> wpn in inventoryBefore)
						{
							Game.LogTrivial("Restoring weapon " + wpn.Key.ToString());
							Game.LocalPlayer.Character.Inventory.GiveNewWeapon(wpn.Key, wpn.Value, false);
							GameFiber.Yield();
						}
					}
				});
			}
			return this.ApplyOutfitToPed(Game.LocalPlayer.Character, randomizeFace);
		}

		
		public bool ApplyOutfitToPed(Ped ped, bool randomizeFace)
		{
			bool flag = !ped;
			bool result;
			if (flag)
			{
				Game.LogTrivial("Ped is invalid or does not exist");
				result = false;
			}
			else
			{
				bool flag2 = ped.Model != this._getModelForGender();
				if (flag2)
				{
					Game.LogTrivial("Ped model " + ped.Model.Name + " does not match requirement for specified gender " + this.PedGender.ToString());
					result = false;
				}
				else
				{
					Game.LogTrivial("Applying outfit " + this.Description + " to ped");
					if (EUPOutfit.<>o__24.<>p__0 == null)
					{
						EUPOutfit.<>o__24.<>p__0 = CallSite<Action<CallSite, object, Ped>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "CLEAR_ALL_PED_PROPS", null, typeof(EUPOutfit), new CSharpArgumentInfo[]
						{
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
							CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
						}));
					}
					EUPOutfit.<>o__24.<>p__0.Target(EUPOutfit.<>o__24.<>p__0, NativeFunction.Natives, ped);
					foreach (KeyValuePair<EProps, Component> prop in this.Props)
					{
						if (EUPOutfit.<>o__24.<>p__1 == null)
						{
							EUPOutfit.<>o__24.<>p__1 = CallSite<Action<CallSite, object, Ped, int, int, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "SET_PED_PROP_INDEX", null, typeof(EUPOutfit), new CSharpArgumentInfo[]
							{
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
								CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
							}));
						}
						EUPOutfit.<>o__24.<>p__1.Target(EUPOutfit.<>o__24.<>p__1, NativeFunction.Natives, ped, (int)prop.Key, prop.Value.DrawableVariation, prop.Value.TextureVariation, 0);
					}
					foreach (KeyValuePair<EPedComponents, Component> comp in this.Components)
					{
						Game.LogTrivial(string.Concat(new string[]
						{
							"Setting component ",
							((int)comp.Key).ToString(),
							" (",
							comp.Key.ToString(),
							") to model ",
							comp.Value.DrawableVariation.ToString(),
							", texture ",
							comp.Value.TextureVariation.ToString()
						}));
						ped.SetVariation((int)comp.Key, comp.Value.DrawableVariation, comp.Value.TextureVariation);
					}
					if (randomizeFace)
					{
						Game.LogTrivial("Randomizing face");
						RandomCharacter.RandomizeCharacter(ped);
					}
					result = true;
				}
			}
			return result;
		}

		
		public Ped Spawn(Vector3 spawnPosition, float heading, out bool success, bool randomizeFace = true)
		{
			Model pedModel = this._getModelForGender();
			bool flag = !pedModel.IsValid || !pedModel.IsPed;
			Ped result;
			if (flag)
			{
				Game.LogTrivial("No valid ped model found for specified gender");
				success = false;
				result = null;
			}
			else
			{
				Game.LogTrivial("Spawning " + pedModel.Name + " at " + spawnPosition.ToString());
				Ped ped = new Ped(pedModel, spawnPosition, heading);
				success = this.ApplyOutfitToPed(ped, randomizeFace);
				result = ped;
			}
			return result;
		}
	}
}
