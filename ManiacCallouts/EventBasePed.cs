using System;
using System.Collections.Generic;
using System.Drawing;
using Rage;

namespace ManiacCallouts.Event
{
	
	internal abstract class EventBasePed : IEventPed
	{
		
		
		
		public virtual bool IsRunning { get; set; }

		
		
		
		public virtual bool CanBeSpawned { get; set; }

		
		
		
		public virtual List<Entity> SpawnedEntities { get; set; }

		
		
		
		public virtual List<Blip> Blips { get; set; }

		
		
		
		public virtual Vector3 SpawnPosition { get; set; }

		
		
		
		public virtual GameFiber ProcessFiber { get; set; }

		
		public EventBasePed(Vector3 spawnPosition)
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

		
		public EventBasePed() : this(EventPoolPed.GetSpawnPosition())
		{
		}

		
		public virtual bool Create()
		{
			Game.LogTrivial("[ManiacEvents]Creating Pedestrian Event");
			foreach (Entity e in this.SpawnedEntities)
			{
				bool flag = !EntityExtensions.Exists(e);
				if (flag)
				{
					this.CleanUp();
					return false;
				}
				bool pedShowMarker = Settings.PedShowMarker;
				if (pedShowMarker)
				{
					Blip b = new Blip(e);
					b.Scale = 0.5f;
					b.Color = Color.OrangeRed;
					this.Blips.Add(b);
				}
			}
			return true;
		}

		
		public virtual void Action()
		{
			Game.LogTrivial("[ManiacEvents]Pedestrian Event Created");
			EventPoolPed.IsAnyEventRunning = true;
			this.IsRunning = true;
			this.ProcessFiber.Start();
		}

		
		public virtual void Process()
		{
		}

		
		public virtual void CleanUp()
		{
			Game.LogTrivial("[ManiacEvents]Pedestrian Event Deleted");
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
			EventPoolPed.IsAnyEventRunning = false;
			this.IsRunning = false;
			this.ProcessFiber.Abort();
		}
	}
}
