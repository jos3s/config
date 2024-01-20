using config.Models;

using System.Reflection;
using System.Text.Json;

namespace config.Singleton
{
    internal class AppSettingsSingleton
    {
        private static AppSettingsSingleton _instance { get; set; }

        private static string Path = $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\AppData\AppSettings.json";

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
            var text = File.ReadAllText(Path);
            AppSettings = JsonSerializer.Deserialize<List<AppSettingsGroup>>(text)!;

            return AppSettings;
        }

        public void Update(List<AppSettingsGroup> appSettings)
        {
            var json = JsonSerializer.Serialize(appSettings, Options);
            File.WriteAllText(Path, json);
            AppSettings = appSettings;
        }
    }
}
