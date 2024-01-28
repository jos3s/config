using config.Features.Database.Shared;

namespace config.Transaction;

internal class DatabasesTRA
{
    public static IEnumerable<DatabaseModel> GetConnectionLinesByNames(IEnumerable<string> names,
        IEnumerable<DatabaseModel> lines)
    {
        return lines.Where(line => names.Contains(line.Name));
    }
}