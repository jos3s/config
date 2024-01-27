using config.Models;

namespace config.Transaction;
internal class SettingsTRA
{
    public static IEnumerable<string> GetGroupsName(IEnumerable<SettingsGroup> groups)
    {
        return groups.Select(x => x.GroupName);
    }
    
    public static IEnumerable<string> GetGroupOptions(SettingsGroup group)
    {
        return group.Keys.Select(x => x.Key);
    }

    public static SettingsGroup GetGroupByName(IEnumerable<SettingsGroup> group, string name)
    {
        return group.FirstOrDefault(x => x.GroupName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public static IEnumerable<string> GetOptions(SettingsGroup group)
    {
        return group.Keys.Select(x => x.Key);
    }

    public static Setting GetKeyByGroupAndKeyName(SettingsGroup group, string key)
    {
        return group.Keys.FirstOrDefault(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))!;
    }
}
