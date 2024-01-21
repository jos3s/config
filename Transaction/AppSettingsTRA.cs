using config.Models;

namespace config.Transaction;
internal class AppSettingsTRA
{
    public static IEnumerable<string> GetGroupsName(IEnumerable<AppSettingsGroup> groups)
    {
        return groups.Select(x => x.GroupName);
    }
    
    public static IEnumerable<string> GetGroupOptions(AppSettingsGroup group)
    {
        return group.Keys.Select(x => x.Key);
    }

    public static AppSettingsGroup GetGroupByName(IEnumerable<AppSettingsGroup> group, string name)
    {
        return group.FirstOrDefault(x => x.GroupName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public static IEnumerable<string> GetOptions(AppSettingsGroup group)
    {
        return group.Keys.Select(x => x.Key);
    }

    public static AppKey GetKeyByGroupAndKeyName(AppSettingsGroup group, string key)
    {
        return group.Keys.FirstOrDefault(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))!;
    }
}
