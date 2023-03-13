using System;
using System.Collections.Generic;
using System.Drawing;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal abstract class EventBaseTraffic : IEventTraffic
	{
		
		
		
		public virtual bool IsRunning { get; set; }

		
		
		
		public virtual bool CanBeSpawned { get; set; }

		
		
		
		public virtual List<Entity> SpawnedEntities { get; set; }

		
		
		
		public virtual List<Blip> Blips { get; set; }

		
		
		
		public virtual Vector3 SpawnPosition { get; set; }

		
		
		
		public virtual GameFiber ProcessFiber { get; set; }

		
		public EventBaseTraffic(Vector3 spawnPosition)
		{
			Game.LogTrivial("[ManiacEvents] Initialized");
			this.SpawnPosition = spawnPosition;
			this.SpawnedEntities = new List<Entity>();
			this.Blips = new List<Blip>();
			this.ProcessFiber = new GameFiber(delegate()
			{
				while (this.IsRunning)
				{
					GameFiber.Yield();
					this.Process();
				}
			}, base.GetType().Name + " Proccess");
		}

		
		public EventBaseTraffic() : this(EventPoolTraffic.GetSpawnPosition())
		{
		}

		
		public virtual bool Create()
		{
			Game.LogTrivial("[ManiacEvents]Creating Traffic Event");
			foreach (Entity e in this.SpawnedEntities)
			{
				bool flag = !EntityExtensions.Exists(e);
				if (flag)
				{
					this.CleanUp();
					return false;
				}
				bool trafficShowMarker = Settings.TrafficShowMarker;
				if (trafficShowMarker)
				{
					Blip b = new Blip(e);
					b.Scale = 0.7f;
					b.Color = Color.OrangeRed;
					this.Blips.Add(b);
				}
			}
			return true;
		}

		
		public virtual void Action()
		{
			Game.LogTrivial("[ManiacEvents]Traffic Event Created");
			EventPoolTraffic.IsAnyEventRunning = true;
			this.IsRunning = true;
			this.ProcessFiber.Start();
		}

		
		public virtual void Process()
		{
		}

		
		public virtual void CleanUp()
		{
			Game.LogTrivial("[ManiacEvents]Traffic Event Deleted");
			foreach (Entity e in this.SpawnedEntities)
			{
				bool flag = e != null && EntityExtensions.Exists(e);
				if (flag)
				{
					e.Dismiss();
				}
			}
			foreach (Blip b in this.Blips)
			{
				bool flag2 = b != null && EntityExtensions.Exists(b);
				if (flag2)
				{
					b.Delete();
				}
			}
			EventPoolTraffic.IsAnyEventRunning = false;
			this.IsRunning = false;
			this.ProcessFiber.Abort();
		}
	}
}
