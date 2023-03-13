using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using StopThePed.API;

namespace ManiacCallouts.API
{
	
	internal static class StopThePedFunctions
	{
		
		public static void CallService(StopThePedFunctions.EStopThePedUnitServices serviceType)
		{
			bool isValid = StopThePedFunctions.IsValid;
			if (isValid)
			{
				switch (serviceType)
				{
				case StopThePedFunctions.EStopThePedUnitServices.Insurance:
					Functions.callInsuranceService();
					break;
				case StopThePedFunctions.EStopThePedUnitServices.AnimalControl:
					Functions.callAnimalControl();
					break;
				case StopThePedFunctions.EStopThePedUnitServices.Coroner:
					Functions.callCoroner();
					break;
				case StopThePedFunctions.EStopThePedUnitServices.PoliceTransport:
					Functions.callPoliceTransport();
					break;
				case StopThePedFunctions.EStopThePedUnitServices.TowTruck:
					Functions.callTowService();
					break;
				default:
					throw new NotSupportedException("Selected services is not supported by StopThePed");
				}
			}
		}

		
		public static STPVehicleStatus GetVehicleRegistration(Vehicle veh)
		{
			return Functions.getVehicleInsuranceStatus(veh);
		}

		
		
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event StopThePedFunctions.VehicleEvent OnVehicleSearch;

		
		
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event StopThePedFunctions.PedEvent OnPedSearch;

		
		
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event StopThePedFunctions.PedEvent OnPedBreathalyzerTest;

		
		
		
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		public static event StopThePedFunctions.PedEvent OnPedDrugSwapTest;

		
		public static STPVehicleStatus GetVehicleInsurance(Vehicle veh)
		{
			return Functions.getVehicleInsuranceStatus(veh);
		}

		
		public static void SetVehicleRegistration(Vehicle veh, StopThePedFunctions.StopThePedVehicleStatus status)
		{
			switch (status)
			{
			case StopThePedFunctions.StopThePedVehicleStatus.None:
				Functions.setVehicleRegistrationStatus(veh, 0);
				break;
			case StopThePedFunctions.StopThePedVehicleStatus.Expired:
				Functions.setVehicleRegistrationStatus(veh, 1);
				break;
			case StopThePedFunctions.StopThePedVehicleStatus.Valid:
				Functions.setVehicleRegistrationStatus(veh, 2);
				break;
			}
		}

		
		public static void SetVehicleInsurance(Vehicle veh, StopThePedFunctions.StopThePedVehicleStatus status)
		{
			switch (status)
			{
			case StopThePedFunctions.StopThePedVehicleStatus.None:
				Functions.setVehicleInsuranceStatus(veh, 0);
				break;
			case StopThePedFunctions.StopThePedVehicleStatus.Expired:
				Functions.setVehicleInsuranceStatus(veh, 1);
				break;
			case StopThePedFunctions.StopThePedVehicleStatus.Valid:
				Functions.setVehicleInsuranceStatus(veh, 2);
				break;
			}
		}

		
		public static void RequestPitManuever()
		{
			bool isValid = StopThePedFunctions.IsValid;
			if (isValid)
			{
				Functions.requestPIT();
			}
		}

		
		public static void SetPedUnderDrugsInfluence(Ped ped, bool set)
		{
			bool isValid = StopThePedFunctions.IsValid;
			if (isValid)
			{
				Functions.setPedUnderDrugsInfluence(ped, set);
			}
		}

		
		public static bool? IsPedUnderDrugsInfluence(Ped ped)
		{
			return new bool?(StopThePedFunctions.IsValid && Functions.isPedUnderDrugsInfluence(ped));
		}

		
		public static void SetPedAlcoholOverLimit(Ped ped, bool set)
		{
			bool isValid = StopThePedFunctions.IsValid;
			if (isValid)
			{
				Functions.setPedAlcoholOverLimit(ped, set);
			}
		}

		
		public static bool? IsPedAlcoholOverLimit(Ped ped)
		{
			return new bool?(StopThePedFunctions.IsValid && Functions.isPedAlcoholOverLimit(ped));
		}

		
		public static void RequestPedCheck(bool playingRadio)
		{
			bool isValid = StopThePedFunctions.IsValid;
			if (isValid)
			{
				Functions.requestDispatchPedCheck(playingRadio);
			}
		}

		
		public static void RequestVehicleCheck(bool playingRadio)
		{
			bool isValid = StopThePedFunctions.IsValid;
			if (isValid)
			{
				Functions.requestDispatchVehiclePlateCheck(playingRadio);
			}
		}

		
		public static bool? IsPedStoppedWithSTP(Ped ped)
		{
			return new bool?(StopThePedFunctions.IsValid && Functions.isPedStopped(ped));
		}

		
		public static void InjectPedItem(Ped ped, params string[] items)
		{
			StopThePedFunctions.InjectPedItem(ped, string.Join(", ", items));
		}

		
		public static void InjectPedItem(Ped ped, string item)
		{
			if (StopThePedFunctions.<>o__31.<>p__0 == null)
			{
				StopThePedFunctions.<>o__31.<>p__0 = CallSite<Func<CallSite, object, string, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "searchPed", typeof(StopThePedFunctions), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			StopThePedFunctions.<>o__31.<>p__0.Target(StopThePedFunctions.<>o__31.<>p__0, ped.Metadata, item);
		}

		
		public static void SetPedGunPermit(Ped ped, bool hasGunPermit)
		{
			if (StopThePedFunctions.<>o__32.<>p__0 == null)
			{
				StopThePedFunctions.<>o__32.<>p__0 = CallSite<Func<CallSite, object, bool, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, "hasGunPermit", typeof(StopThePedFunctions), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			StopThePedFunctions.<>o__32.<>p__0.Target(StopThePedFunctions.<>o__32.<>p__0, ped.Metadata, hasGunPermit);
		}

		
		static StopThePedFunctions()
		{
			AppDomain.CurrentDomain.AssemblyResolve += StopThePedFunctions.OnAssemblyResolve;
			bool flag = !StopThePedFunctions.IsValid;
			if (!flag)
			{
				bool isValid = StopThePedFunctions.IsValid;
				if (isValid)
				{
					Events.searchVehicleEvent += delegate(Vehicle v)
					{
						StopThePedFunctions.VehicleEvent onVehicleSearch = StopThePedFunctions.OnVehicleSearch;
						if (onVehicleSearch != null)
						{
							onVehicleSearch(v);
						}
					};
					Events.patDownPedEvent += delegate(Ped p)
					{
						StopThePedFunctions.PedEvent onPedSearch = StopThePedFunctions.OnPedSearch;
						if (onPedSearch != null)
						{
							onPedSearch(p);
						}
					};
					Events.breathalyzerTestEvent += delegate(Ped p)
					{
						StopThePedFunctions.PedEvent onPedBreathalyzerTest = StopThePedFunctions.OnPedBreathalyzerTest;
						if (onPedBreathalyzerTest != null)
						{
							onPedBreathalyzerTest(p);
						}
					};
					Events.drugSwabTestEvent += delegate(Ped p)
					{
						StopThePedFunctions.PedEvent onPedDrugSwapTest = StopThePedFunctions.OnPedDrugSwapTest;
						if (onPedDrugSwapTest != null)
						{
							onPedDrugSwapTest(p);
						}
					};
				}
			}
		}

		
		private static Assembly OnAssemblyResolve(object sender, ResolveEventArgs args)
		{
			bool flag = args.Name.ToLower().StartsWith("stoptheped, ") && StopThePedFunctions.IsValid;
			Assembly result;
			if (flag)
			{
				result = Assembly.Load(File.ReadAllBytes("Plugins/LSPDFR/StopThePed.dll"));
			}
			else
			{
				result = null;
			}
			return result;
		}

		
		private const string DllPath = "Plugins/LSPDFR/StopThePed.dll";

		
		private static readonly bool IsValid = File.Exists("Plugins/LSPDFR/StopThePed.dll");

		
		
		public delegate void VehicleEvent(Vehicle vehicle);

		
		
		public delegate void PedEvent(Ped ped);

		
		
		public delegate void STPEvent();

		
		public enum EStopThePedVehicleSearch
		{
			
			SearchTrunk,
			
			SearchDriver,
			
			SearchPassenger
		}

		
		public enum EStopThePedUnitServices
		{
			
			Insurance,
			
			AnimalControl,
			
			Coroner,
			
			PoliceTransport,
			
			TowTruck
		}

		
		public enum StopThePedVehicleStatus
		{
			
			None,
			
			Expired,
			
			Valid
		}
	}
}
