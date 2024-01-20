using config.Models;
using System.Text.Json;

namespace config.Singleton
{
	internal class AppSettingsSingleton
    {
		private static AppSettingsSingleton _instance { get; set; }

		private static string Path = @"./Data/AppSettings.json";

		private AppSettings AppSettings { get; set; }

		private JsonSerializerOptions Options = new () { WriteIndented = true };

		public static AppSettingsSingleton Instance
		{
			get
			{
                _instance ??= new AppSettingsSingleton();

				return _instance;
			}
		}

		public AppSettings Lines()
		{
			var text = File.ReadAllText("D:\\study\\my\\console\\config\\Data\\AppSettings.json");
			AppSettings = JsonSerializer.Deserialize<AppSettings>(text)!;

			return AppSettings;
		}
	}
}
