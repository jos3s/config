using config.Models;

using System.Reflection;
using System.Text.Json;

namespace config.Singleton
{
    internal class SettingsSingleton
    {
        private static SettingsSingleton _instance { get; set; }

        private static string Path = $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\AppData\Settings.json";

        private List<SettingsGroup> Settings { get; set; }

        private JsonSerializerOptions Options = new() { WriteIndented = true };

        public static SettingsSingleton Instance
        {
            get
            {
                _instance ??= new SettingsSingleton();

                return _instance;
            }
        }

        public List<SettingsGroup> Lines()
        {
            var text = File.ReadAllText(Path);
            Settings = JsonSerializer.Deserialize<List<SettingsGroup>>(text)!;

            return Settings;
        }

        public void Update(List<SettingsGroup> appSettings)
        {
            var json = JsonSerializer.Serialize(appSettings, Options);
            File.WriteAllText(Path, json);
            Settings = appSettings;
        }
    }
}
