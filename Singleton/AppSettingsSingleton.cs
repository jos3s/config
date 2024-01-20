using config.Models;

using System.Text.Json;

namespace config.Singleton
{
    internal class AppSettingsSingleton
    {
        private static AppSettingsSingleton _instance { get; set; }

        private static string Path = @"./Data/AppSettings.json";

        private List<AppSettingsGroup> AppSettings { get; set; }

        private JsonSerializerOptions Options = new() { WriteIndented = true };

        public static AppSettingsSingleton Instance
        {
            get
            {
                _instance ??= new AppSettingsSingleton();

                return _instance;
            }
        }

        public List<AppSettingsGroup> Lines()
        {
            var text = File.ReadAllText("D:\\study\\my\\console\\config\\Data\\AppSettings.json");
            AppSettings = JsonSerializer.Deserialize<List<AppSettingsGroup>>(text)!;

            return AppSettings;
        }

        public void Update(List<AppSettingsGroup> appSettings)
        {
            var json = JsonSerializer.Serialize(appSettings, Options);
            File.WriteAllText("D:\\study\\my\\console\\config\\Data\\AppSettings.json", json);
            AppSettings = appSettings;
        }
    }
}
