using System;
using System.Collections.Generic;
using System.Linq;
using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using LSPD_First_Response.Mod.API;
using Rage;

namespace BetterPresence
{
	
	public class Presence
	{
		
		public static void StartLoop()
		{
			if (!Presence._loopStarted)
			{
				Presence._loopStarted = true;
				Presence._stopLoop = false;
				Presence._clientReady = false;
				Presence.startTime = DateTime.Now;
				Presence.rpcClient = new DiscordRpcClient("999781142876135434");
				Presence.rpcClient.Logger = new NullLogger();
				Presence.rpcClient.OnReady += delegate(object sender, ReadyMessage e)
				{
					Presence._clientReady = true;
				};
				Presence.rpcClient.OnPresenceUpdate += delegate(object sender, PresenceMessage e)
				{
					Game.Console.Print("[BetterPresence] Presence updated.");
				};
				Presence.rpcClient.Initialize();
				while (!Presence._stopLoop)
				{
					if (Presence._clientReady)
					{
						try
						{
							string newValue = "Patrolling";
							if (Functions.GetCurrentPullover() != null)
							{
								newValue = "On a traffic stop";
							}
							else if (Functions.GetActivePursuit() != null)
							{
								newValue = "In a pursuit";
							}
							else if (Functions.GetCurrentCallout() != null)
							{
								newValue = "Responding to a call";
							}
							string currentAgencyScriptName = Functions.GetCurrentAgencyScriptName();
							string text = currentAgencyScriptName;
							IEnumerable<string> source = new string[]
							{
								"lspd",
								"lssd",
								"fib",
								"noose",
								"sahp",
								"bcso",
								"lsfd",
								"nysp",
								"sapr"
							};
							Vector3 position = Game.LocalPlayer.Character.Position;
							if (!source.Contains(text.ToLower()))
							{
								text = "unknown";
							}
							string text2 = Settings.DetailsLine;
							IDictionary<string, string> dictionary = new Dictionary<string, string>
							{
								{
									"LosSantos",
									"Los Santos"
								},
								{
									"LosSantosCounty",
									"Los Santos County"
								},
								{
									"BlaineCounty",
									"Blaine County"
								}
							};
							text2 = text2.Replace("%STATUS%", newValue);
							text2 = text2.Replace("%STREET_NAME%", World.GetStreetName(World.GetNextPositionOnStreet(position)));
							text2 = text2.Replace("%REGION%", dictionary[Functions.GetZoneAtPosition(position).County.ToString()]);
							text2 = text2.Replace("%DEPARTMENT%", currentAgencyScriptName.ToUpper());
							Presence.rpcClient.SetPresence(new RichPresence
							{
								Details = text2,
								Assets = new Assets
								{
									LargeImageKey = text,
									LargeImageText = "LSPDFR with BetterPresence v2.0.1"
								},
								Timestamps = new Timestamps
								{
									Start = new DateTime?(Presence.startTime)
								}
							});
						}
						catch (Exception)
						{
							Game.LogVerbose("BetterPresence threw an exception while setting your presence! Crash [hopefully] prevented.");
						}
					}
					GameFiber.Sleep(2500);
				}
				Presence._loopStarted = false;
				Presence.rpcClient.Dispose();
			}
		}

		
		public static void StopLoop()
		{
			if (Presence._loopStarted)
			{
				Presence._stopLoop = true;
			}
		}

		
		private static bool _stopLoop;

		
		private static bool _loopStarted;

		
		private static bool _clientReady;

		
		private static DiscordRpcClient rpcClient;

		
		public static DateTime startTime;
	}
}
