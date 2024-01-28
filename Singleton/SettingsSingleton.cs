using config.Features.Settings.Shared;
using System.Reflection;
using System.Text.Json;

namespace config.Singleton
{
    internal class SettingsSingleton
    {
        private static SettingsSingleton _instance { get; set; }

        private static string Path = $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\AppData\Settings.json";

        private List<SettingsGroupModel> Settings { get; set; }

        private JsonSerializerOptions Options = new() { WriteIndented = true };

        public static SettingsSingleton Instance
        {
            get
            {
                _instance ??= new SettingsSingleton();

                return _instance;
            }
        }

        public List<SettingsGroupModel> Lines()
        {
            var text = File.ReadAllText(Path);
            Settings = JsonSerializer.Deserialize<List<SettingsGroupModel>>(text)!;

            return Settings;
        }

        public void Update(List<SettingsGroupModel> appSettings)
        {
            var json = JsonSerializer.Serialize(appSettings, Options);
            File.WriteAllText(Path, json);
            Settings = appSettings;
        }
    }
}
