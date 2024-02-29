namespace config.Utils;
internal static class ConfigPathHelper
{
    public static readonly string FolderName = ".configapp";

    public static readonly string SettingsFileName = @"Settings.json";

    public static readonly string DatabasesFileName = @"Databases.json";

    public static string FolderPath { get; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), FolderName);

    public static string SettingsPath { 
        get
        {
            return $@"{FolderPath}\{SettingsFileName}";
        }
    }

    public static string DatabasesPath { 
        get
        {
            return $@"{FolderPath}\{DatabasesFileName}";
        }
    }
}
