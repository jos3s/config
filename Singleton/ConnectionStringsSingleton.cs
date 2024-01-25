using System.Reflection;
using config.Models;
using System.Text.Json;

namespace config.Singleton
{
    internal class ConnectionStringsSingleton
    {
        private static ConnectionStringsSingleton _instance { get; set; }

        private static string Path =
            $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\AppData\ConnectionStrings.json";
        private List<ConnectionLine> ConnectionLines { get; set; }

        private JsonSerializerOptions Options = new() { WriteIndented = true };

        public static ConnectionStringsSingleton Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ConnectionStringsSingleton();

                return _instance;
            }
        }

        public List<ConnectionLine> Lines()
        {
            string text = File.ReadAllText(Path);
            ConnectionLines = JsonSerializer.Deserialize<List<ConnectionLine>>(text)!;

            return ConnectionLines;
        }

        public void InsertLine(ConnectionLine line)
        {
            string text = File.ReadAllText(Path);
            ConnectionLines = JsonSerializer.Deserialize<List<ConnectionLine>>(text)!;

            if (!ConnectionLines.Contains(line))
            {
                ConnectionLines.Add(line);

                var json = JsonSerializer.Serialize(ConnectionLines, Options);
                File.WriteAllText(Path, json);
            }
            else
            {
                var index = ConnectionLines.IndexOf(line);
                ConnectionLines.Insert(index, line);
            }
        }

        public void Update(List<ConnectionLine> databases)
        {
            var json = JsonSerializer.Serialize(databases, Options);
            File.WriteAllText(Path, json);
            ConnectionLines = databases;
        }
    }
}
