using config.Features.Database.Models;

namespace config.Transaction;

internal class DatabasesTRA
{
    public static IEnumerable<Database> GetConnectionLinesByNames(IEnumerable<string> names,
        IEnumerable<Database> lines)
    {
        return lines.Where(line => names.Contains(line.Name));
    }
}