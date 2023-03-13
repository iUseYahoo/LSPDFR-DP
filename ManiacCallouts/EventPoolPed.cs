using System;
using System.Threading;
using LSPD_First_Response.Mod.API;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal static class EventPoolPed
	{
		
		public static void EventsController()
		{
			GameFiber.StartNew(delegate()
			{
				for (;;)
				{
					EventPoolPed.MaxTimeAmbientEventPed = Settings.MaxTimeAmbientEvent;
					EventPoolPed.MinTimeAmbientEventPed = Settings.MinTimeAmbientEvent;
					bool flag = EventPoolPed.MaxTimeAmbientEventPed <= EventPoolPed.MinTimeAmbientEventPed;
					if (flag)
					{
						EventPoolPed.MaxTimeAmbientEventPed = 120;
						EventPoolPed.MinTimeAmbientEventPed = 20;
					}
					bool flag2 = EventPoolPed.MinTimeAmbientEventPed <= 10;
					if (flag2)
					{
						EventPoolPed.MinTimeAmbientEventPed = 10;
					}
					GameFiber.Sleep(EventPoolPed.Random.Next(EventPoolPed.MinTimeAmbientEventPed * 1000, EventPoolPed.MaxTimeAmbientEventPed * 1000));
					bool flag3 = !EventPoolPed.IsAnyEventRunning && !Functions.IsCalloutRunning();
					if (flag3)
					{
						EventPoolPed.CreateEvent();
					}
				}
			});
		}

		
		public static void CreateEvent()
		{
			bool isAnyEventRunning = EventPoolPed.IsAnyEventRunning;
			if (isAnyEventRunning)
			{
				Game.LogTrivial("[ManiacEvents]Another event running. Aborting new event...");
			}
			else
			{
				GameFiber.StartNew(delegate()
				{
					IEventPed ambientEvent = EventPoolPed.GetRandomEvent();
					EventPoolPed.currentEvent = ambientEvent;
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
			bool flag = EventPoolPed.currentEvent != null;
			if (flag)
			{
				EventPoolPed.currentEvent.CleanUp();
			}
		}

		
		public static IEventPed GetRandomEvent()
		{
			int num = new Random().Next(0, 3);
			int num2 = num;
			IEventPed rndEvent;
			if (num2 != 0)
			{
				if (num2 != 1)
				{
					rndEvent = new DrugUser();
				}
				else
				{
					rndEvent = new Weapon();
				}
			}
			else
			{
				rndEvent = new DrunkPerson();
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

		
		private static int MaxTimeAmbientEventPed;

		
		private static int MinTimeAmbientEventPed;

		
		private static IEventPed currentEvent;
	}
}
