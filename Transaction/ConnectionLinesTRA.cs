using config.Models;

namespace config.Transaction;
internal class ConnectionLinesTRA
{
    public static IEnumerable<ConnectionLine> GetConnectionLinesByNames(IEnumerable<string> names,
        IEnumerable<ConnectionLine> lines)
    {
        return lines.Where(line => names.Contains(line.Name));
    }

}
