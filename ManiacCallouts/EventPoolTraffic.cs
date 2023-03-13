using System;
using System.Threading;
using LSPD_First_Response.Mod.API;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal static class EventPoolTraffic
	{
		
		public static void EventsController()
		{
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					EventPoolTraffic.MaxTimeAmbientEventTraffic = Settings.MaxTimeAmbientEvent;
					EventPoolTraffic.MinTimeAmbientEventTraffic = Settings.MinTimeAmbientEvent;
					bool flag = EventPoolTraffic.MaxTimeAmbientEventTraffic <= EventPoolTraffic.MinTimeAmbientEventTraffic;
					if (flag)
					{
						EventPoolTraffic.MaxTimeAmbientEventTraffic = 120;
						EventPoolTraffic.MinTimeAmbientEventTraffic = 20;
					}
					bool flag2 = EventPoolTraffic.MinTimeAmbientEventTraffic <= 10;
					if (flag2)
					{
						EventPoolTraffic.MinTimeAmbientEventTraffic = 10;
					}
					GameFiber.Sleep(EventPoolTraffic.Random.Next(EventPoolTraffic.MinTimeAmbientEventTraffic * 1000, EventPoolTraffic.MaxTimeAmbientEventTraffic * 1000));
					bool flag3 = !EventPoolTraffic.IsAnyEventRunning && !Functions.IsCalloutRunning();
					if (flag3)
					{
						EventPoolTraffic.CreateEvent();
					}
				}
			});
		}

		
		public static void CreateEvent()
		{
			bool isAnyEventRunning = EventPoolTraffic.IsAnyEventRunning;
			if (isAnyEventRunning)
			{
				Game.LogTrivial("[ManiacEvents]Another event running. Aborting new event...");
			}
			else
			{
				GameFiber.StartNew(delegate()
				{
					IEventTraffic ambientEvent = EventPoolTraffic.GetRandomEvent();
					EventPoolTraffic.currentEvent = ambientEvent;
					bool canBeSpawned = ambientEvent.CanBeSpawned;
					if (canBeSpawned)
					{
						bool flag = ambientEvent.Create();
						if (flag)
						{
							ambientEvent.Action();
						}
					}
				});
			}
		}

		
		public static void EndCurrentEvent()
		{
			bool flag = EventPoolTraffic.currentEvent != null;
			if (flag)
			{
				EventPoolTraffic.currentEvent.CleanUp();
			}
		}

		
		public static IEventTraffic GetRandomEvent()
		{
			IEventTraffic rndEvent;
			switch (new Random().Next(0, 6))
			{
			case 0:
				rndEvent = new Stolen();
				break;
			case 1:
				rndEvent = new Speeding();
				break;
			case 2:
				rndEvent = new DrugDriver();
				break;
			case 3:
				rndEvent = new LightDriver();
				break;
			case 4:
				rndEvent = new DrunkDriver();
				break;
			default:
				rndEvent = new JunkDriver();
				break;
			}
			return rndEvent;
		}

		
		public static Vector3 GetSpawnPosition()
		{
			try
			{
				Vector3 result = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(200f));
				int counter = 0;
				while (result.DistanceTo(Game.LocalPlayer.Character.Position) < 62.5f || counter < 150)
				{
					result = World.GetNextPositionOnStreet(Game.LocalPlayer.Character.Position.Around(200f));
					counter++;
					GameFiber.Yield();
				}
				return result;
			}
			catch (ThreadAbortException)
			{
			}
			return Vector3.Zero;
		}

		
		public static bool IsAnyEventRunning = false;

		
		public static Random Random = new Random();

		
		private static int MaxTimeAmbientEventTraffic;

		
		private static int MinTimeAmbientEventTraffic;

		
		private static IEventTraffic currentEvent;
	}
}
