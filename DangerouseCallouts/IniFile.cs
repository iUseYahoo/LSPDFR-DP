using System;
using System.Runtime.InteropServices;
using System.Text;

namespace TutorialCallouts
{
	
	public class IniFile
	{
		
		[DllImport("kernel32")]
		private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);

		
		[DllImport("kernel32")]
		private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

		
		public IniFile(string path)
		{
			this.path = path;
		}

		
		public void IniWriteValue(string section, string key, string value)
		{
			IniFile.WritePrivateProfileString(section, key, value, this.path);
		}

		
		public string IniReadValue(string section, string key)
		{
			StringBuilder stringBuilder = new StringBuilder(255);
			int privateProfileString = IniFile.GetPrivateProfileString(section, key, "", stringBuilder, 255, this.path);
			return stringBuilder.ToString();
		}

		
		public string path;
	}
}
