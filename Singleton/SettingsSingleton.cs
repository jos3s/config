using config.Features.Settings.Shared;
using config.Utils;

using System.Text.Json;

namespace config.Singleton;

internal class SettingsSingleton : FileSingleton
{
    private static SettingsSingleton _instance { get; set; }

    private static readonly string Path = ConfigPathHelper.SettingsPath;

    private List<SettingsGroupModel> Settings { get; set; }

    private JsonSerializerOptions Options = new() { WriteIndented = true };

    public static SettingsSingleton Instance
    {
        get
        {
            _instance ??= new SettingsSingleton();

            ValidateFile(Path);

            return _instance;
        }
    }

    public List<SettingsGroupModel> Lines()
    {
        var text = File.ReadAllText(Path);
        if (!string.IsNullOrEmpty(text) || !string.IsNullOrWhiteSpace(text))
            Settings = JsonSerializer.Deserialize<List<SettingsGroupModel>>(text)!;
        else Settings = new List<SettingsGroupModel>();

        return Settings;
    }

    public void Update(List<SettingsGroupModel> appSettings)
    {
        var json = JsonSerializer.Serialize(appSettings, Options);
        File.WriteAllText(Path, json);
        Settings = appSettings;
    }
}
