using config.Features.Settings.Shared;

namespace config.Transaction;
internal class SettingsTRA
{
    public static IEnumerable<string> GetGroupsName(IEnumerable<SettingsGroupModel> groups)
    {
        return groups.Select(x => x.GroupName);
    }

    public static IEnumerable<string> GetGroupOptions(SettingsGroupModel group)
    {
        return group.Keys.Select(x => x.Key);
    }

    public static SettingsGroupModel GetGroupByName(IEnumerable<SettingsGroupModel> group, string name)
    {
        return group.FirstOrDefault(x => x.GroupName.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public static IEnumerable<string> GetOptions(SettingsGroupModel group)
    {
        return group.Keys.Select(x => x.Key);
    }

    public static SettingModel GetKeyByGroupAndKeyName(SettingsGroupModel group, string key)
    {
        return group.Keys.FirstOrDefault(x => x.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase))!;
    }
}
