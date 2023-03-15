using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using Microsoft.CSharp.RuntimeBinder;
using Rage;
using Rage.Native;

namespace VectorGrabber
{
	
	internal static class EntryPoint
	{
		
		
		internal static Ped Player
		{
			get
			{
				return Game.LocalPlayer.Character;
			}
		}

		
		internal static void Main()
		{
			GameFiber.StartNew(new ThreadStart(Menu.CreateMainMenu));
			VersionChecker.CheckForUpdates();
			Settings.Initialize();
			bool flag = !Directory.Exists(EntryPoint.CsharpFileDirectory);
			if (flag)
			{
				Directory.CreateDirectory(EntryPoint.CsharpFileDirectory);
			}
			bool flag2 = !File.Exists(EntryPoint.CsharpFilePath);
			if (flag2)
			{
				File.Create(EntryPoint.CsharpFilePath);
			}
			else
			{
				EntryPoint.ReadFile();
			}
			for (;;)
			{
				GameFiber.Yield();
				bool flag3 = EntryPoint.Player.IsValid() && Game.IsKeyDown(Settings.SaveKey) && Game.IsKeyDown(Settings.ModifierKey);
				if (flag3)
				{
					string locationTitle;
					EntryPoint.AppendToFile(EntryPoint.getCoordsAndFormat(out locationTitle), EntryPoint.CsharpFilePath);
					EntryPoint.AddVectorAndHeadingToList(locationTitle);
					Game.DisplayNotification("Coordinates were saved to text file.");
				}
				bool flag4 = EntryPoint.Player.IsValid() && Game.IsKeyDown(Settings.TeleportNextKey) && Game.IsKeyDown(Settings.ModifierKey);
				if (flag4)
				{
					EntryPoint.HandleArrow(EntryPoint.direction.RIGHT);
				}
				bool flag5 = EntryPoint.Player.IsValid() && Game.IsKeyDown(Settings.TeleportBackKey) && Game.IsKeyDown(Settings.ModifierKey);
				if (flag5)
				{
					EntryPoint.HandleArrow(EntryPoint.direction.LEFT);
				}
				bool flag6 = EntryPoint.Player.IsValid() && Game.IsKeyDown(Settings.TeleportKey) && Game.IsKeyDown(Settings.ModifierKey);
				if (flag6)
				{
					EntryPoint.TeleportToSpecificCoordinate();
				}
				bool flag7 = EntryPoint.Player.IsValid() && Game.IsKeyDown(Settings.RereadFile) && Game.IsKeyDown(Settings.ModifierKey);
				if (flag7)
				{
					EntryPoint.RereadFile();
				}
				bool flag8 = EntryPoint.Player.IsValid() && Game.IsKeyDown(Settings.ClipboardKey) && Game.IsKeyDown(Settings.ModifierKey);
				if (flag8)
				{
					EntryPoint.CopyCurrCoordToClipboard();
				}
			}
		}

		
		internal static void OnUnload(bool Exit)
		{
			Menu.DeleteBlips();
			Settings.UpdateINI();
			Game.LogTrivial("Vector Grabber Unloaded.");
		}

		
		internal static void AddLocation()
		{
			string locationTitle;
			EntryPoint.AppendToFile(EntryPoint.getCoordsAndFormat(out locationTitle), EntryPoint.CsharpFilePath);
			EntryPoint.AddVectorAndHeadingToList(locationTitle);
			Game.DisplayNotification("Coordinates were saved to text file.");
		}

		
		internal static void RereadFile()
		{
			EntryPoint.VectorsRead.Clear();
			Locations.LocationMenu.Clear();
			Menu.DeleteBlips();
			EntryPoint.ReadFile();
			Game.DisplayNotification("Text file was reread.");
		}

		
		internal static void AddVectorAndHeadingToList(string title)
		{
			bool flag = title.Equals("");
			if (flag)
			{
				title = string.Format("Location at Line Number: {0}", EntryPoint.VectorsRead.Count + 1);
			}
			SavedLocation s = new SavedLocation(EntryPoint.Player.Position.X, EntryPoint.Player.Position.Y, EntryPoint.Player.Position.Z, EntryPoint.Player.Heading, title);
			EntryPoint.VectorsRead.Add(s);
			Locations.AddItem(s);
			Menu.AddBlip(s);
		}

		
		internal static void CopyCurrCoordToClipboard()
		{
			string text;
			Game.SetClipboardText(EntryPoint.getCoordsAndFormat(out text));
		}

		
		internal static void AppendToFile(string str, string path)
		{
			using (FileStream fs = new FileStream(path, FileMode.Append, FileAccess.Write))
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine(str);
				}
			}
		}

		
		internal static void ReadFile()
		{
			try
			{
				string[] Vectors = File.ReadAllLines(EntryPoint.CsharpFilePath);
				string[] titleSeps = new string[]
				{
					"
				};
				for (int i = 0; i < Vectors.Length; i++)
				{
					string[] values = Regex.Replace(Vectors[i].Trim(), "Vector3|[^0-9,-.]", "").Split(new char[]
					{
						','
					});
					string[] titleSplit = Vectors[i].Split(titleSeps, StringSplitOptions.None);
					bool flag = titleSplit.Length == 1;
					string title;
					if (flag)
					{
						title = string.Format("Location at Line Number: {0}", i + 1);
					}
					else
					{
						title = (titleSplit[1].Trim() ?? "");
					}
					SavedLocation s = new SavedLocation(Convert.ToSingle(values[0]), Convert.ToSingle(values[1]), Convert.ToSingle(values[2]), Convert.ToSingle(values[3]), title);
					EntryPoint.VectorsRead.Add(s);
				}
				Locations.AddItems();
			}
			catch (Exception e)
			{
				Game.DisplayNotification("Error occurred while reading the file. Blame yourself. git gud kid. jk");
				Game.LogTrivial("Error occurred while reading the file: " + e.Message);
			}
		}

		
		internal static void HandleArrow(EntryPoint.direction directionGiven)
		{
			bool flag = directionGiven == EntryPoint.direction.LEFT;
			if (flag)
			{
				bool flag2 = EntryPoint.GlobalIndexForArray == 0;
				if (flag2)
				{
					Game.LogTrivial("Vector Grabber:Back Key pressed when index was 0.");
					Game.DisplayNotification("No More Vectors!");
				}
				else
				{
					EntryPoint.GlobalIndexForArray--;
					EntryPoint.TeleportAndDisplay();
				}
			}
			bool flag3 = directionGiven == EntryPoint.direction.RIGHT;
			if (flag3)
			{
				int lastIndex = EntryPoint.VectorsRead.Count - 1;
				bool flag4 = EntryPoint.GlobalIndexForArray >= lastIndex;
				if (flag4)
				{
					Game.LogTrivial("Vector Grabber:Next Key pressed when array was at its end.");
					Game.DisplayNotification("No More Vectors!");
				}
				else
				{
					EntryPoint.GlobalIndexForArray++;
					EntryPoint.TeleportAndDisplay();
				}
			}
		}

		
		internal static void TeleportAndDisplay()
		{
			float x = EntryPoint.VectorsRead[EntryPoint.GlobalIndexForArray].X;
			float y = EntryPoint.VectorsRead[EntryPoint.GlobalIndexForArray].Y;
			float z = EntryPoint.VectorsRead[EntryPoint.GlobalIndexForArray].Z;
			float heading = EntryPoint.VectorsRead[EntryPoint.GlobalIndexForArray].Heading;
			World.TeleportLocalPlayer(new Vector3(x, y, z), false);
			EntryPoint.Player.Heading = heading;
			Game.DisplayNotification(string.Format("Vector: ({0},{1},{2})", x, y, z) + string.Format("\nHeader: {0}", heading) + string.Format("\nLine Number: {0}", EntryPoint.GlobalIndexForArray + 1));
		}

		
		internal static void TeleportBasedOnIndexAndDisplay(int index)
		{
			float x = EntryPoint.VectorsRead[index].X;
			float y = EntryPoint.VectorsRead[index].Y;
			float z = EntryPoint.VectorsRead[index].Z;
			float heading = EntryPoint.VectorsRead[index].Heading;
			World.TeleportLocalPlayer(new Vector3(x, y, z), false);
			EntryPoint.Player.Heading = heading;
			Game.DisplayNotification(string.Format("Vector: ({0},{1},{2})", x, y, z) + string.Format("\nHeader: {0}", heading) + string.Format("\nLine Number: {0}", index + 1));
		}

		
		internal static string getCoordsAndFormat(out string title)
		{
			string str = "";
			title = EntryPoint.OpenTextInput("VectorGrabber", "", 100);
			str += string.Format("(new Vector3({0}f, {1}f, {2}f), {3}f);", new object[]
			{
				EntryPoint.Player.Position.X,
				EntryPoint.Player.Position.Y,
				EntryPoint.Player.Position.Z,
				EntryPoint.Player.Heading
			});
			bool flag = !title.Equals("");
			if (flag)
			{
				str = str + "  
			}
			Game.LogTrivial("The string is " + str);
			return str;
		}

		
		internal static string OpenTextInput(string windowTitle, string defaultText, int maxLength)
		{
			if (EntryPoint.<>o__20.<>p__0 == null)
			{
				EntryPoint.<>o__20.<>p__0 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISABLE_ALL_CONTROL_ACTIONS", null, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			EntryPoint.<>o__20.<>p__0.Target(EntryPoint.<>o__20.<>p__0, NativeFunction.Natives, 2);
			if (EntryPoint.<>o__20.<>p__1 == null)
			{
				EntryPoint.<>o__20.<>p__1 = CallSite<Action<CallSite, object, bool, string, int, string, int, int, int, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "DISPLAY_ONSCREEN_KEYBOARD", null, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			EntryPoint.<>o__20.<>p__1.Target(EntryPoint.<>o__20.<>p__1, NativeFunction.Natives, true, windowTitle, 0, defaultText, 0, 0, 0, maxLength);
			for (;;)
			{
				if (EntryPoint.<>o__20.<>p__4 == null)
				{
					EntryPoint.<>o__20.<>p__4 = CallSite<Func<CallSite, object, bool>>.Create(Binder.UnaryOperation(CSharpBinderFlags.None, ExpressionType.IsTrue, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
					}));
				}
				Func<CallSite, object, bool> target = EntryPoint.<>o__20.<>p__4.Target;
				CallSite <>p__ = EntryPoint.<>o__20.<>p__4;
				if (EntryPoint.<>o__20.<>p__3 == null)
				{
					EntryPoint.<>o__20.<>p__3 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.BinaryOperation(CSharpBinderFlags.None, ExpressionType.Equal, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
					}));
				}
				Func<CallSite, object, int, object> target2 = EntryPoint.<>o__20.<>p__3.Target;
				CallSite <>p__2 = EntryPoint.<>o__20.<>p__3;
				if (EntryPoint.<>o__20.<>p__2 == null)
				{
					EntryPoint.<>o__20.<>p__2 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "UPDATE_ONSCREEN_KEYBOARD", new Type[]
					{
						typeof(int)
					}, typeof(EntryPoint), new CSharpArgumentInfo[]
					{
						CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
					}));
				}
				if (!target(<>p__, target2(<>p__2, EntryPoint.<>o__20.<>p__2.Target(EntryPoint.<>o__20.<>p__2, NativeFunction.Natives), 0)))
				{
					break;
				}
				GameFiber.Yield();
			}
			if (EntryPoint.<>o__20.<>p__5 == null)
			{
				EntryPoint.<>o__20.<>p__5 = CallSite<Action<CallSite, object, int>>.Create(Binder.InvokeMember(CSharpBinderFlags.ResultDiscarded, "ENABLE_ALL_CONTROL_ACTIONS", null, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null),
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType | CSharpArgumentInfoFlags.Constant, null)
				}));
			}
			EntryPoint.<>o__20.<>p__5.Target(EntryPoint.<>o__20.<>p__5, NativeFunction.Natives, 2);
			if (EntryPoint.<>o__20.<>p__7 == null)
			{
				EntryPoint.<>o__20.<>p__7 = CallSite<Func<CallSite, object, string>>.Create(Binder.Convert(CSharpBinderFlags.None, typeof(string), typeof(EntryPoint)));
			}
			Func<CallSite, object, string> target3 = EntryPoint.<>o__20.<>p__7.Target;
			CallSite <>p__3 = EntryPoint.<>o__20.<>p__7;
			if (EntryPoint.<>o__20.<>p__6 == null)
			{
				EntryPoint.<>o__20.<>p__6 = CallSite<Func<CallSite, object, object>>.Create(Binder.InvokeMember(CSharpBinderFlags.None, "GET_ONSCREEN_KEYBOARD_RESULT", new Type[]
				{
					typeof(string)
				}, typeof(EntryPoint), new CSharpArgumentInfo[]
				{
					CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, null)
				}));
			}
			return target3(<>p__3, EntryPoint.<>o__20.<>p__6.Target(EntryPoint.<>o__20.<>p__6, NativeFunction.Natives) ?? "");
		}

		
		internal static bool isInputValid(string input)
		{
			foreach (char c in input)
			{
				bool flag = !char.IsDigit(c);
				if (flag)
				{
					Game.DisplayNotification("Invalid input");
					return false;
				}
			}
			return true;
		}

		
		internal static void TeleportToSpecificCoordinate()
		{
			string input = EntryPoint.OpenTextInput("VectorGrabber", "", 10);
			bool flag = input.Equals("");
			if (flag)
			{
				Game.DisplayNotification("No input given.");
			}
			else
			{
				bool flag2 = EntryPoint.isInputValid(input);
				if (flag2)
				{
					int index = int.Parse(input) - 1;
					bool flag3 = index >= 0 && index < EntryPoint.VectorsRead.Count;
					if (flag3)
					{
						float x = EntryPoint.VectorsRead[index].X;
						float y = EntryPoint.VectorsRead[index].Y;
						float z = EntryPoint.VectorsRead[index].Z;
						float heading = EntryPoint.VectorsRead[index].Heading;
						World.TeleportLocalPlayer(new Vector3(x, y, z), false);
						EntryPoint.Player.Heading = heading;
						Game.DisplayNotification("Player teleported to line number: " + input);
					}
				}
			}
		}

		
		internal static bool CheckClipboardModifierKey()
		{
			bool flag = Settings.ModifierKey == Keys.None;
			return flag || Game.IsKeyDownRightNow(Settings.ModifierKey);
		}

		
		internal static List<SavedLocation> VectorsRead = new List<SavedLocation>();

		
		internal static List<Blip> Blips = new List<Blip>();

		
		internal static int GlobalIndexForArray = 0;

		
		internal static string CsharpFilePath = "Plugins\\VectorGrabber\\VectorsInCsharpNotation.txt";

		
		internal static string CsharpFileDirectory = "Plugins\\VectorGrabber\\";

		
		internal enum direction
		{
			
			LEFT,
			
			RIGHT
		}
	}
}
