using System.Reflection;
using config.Models;
using System.Text.Json;

namespace config.Singleton
{
    internal class ConnectionStringsSingleton
    {
        private static ConnectionStringsSingleton _instance { get; set; }

        private static string Path =
            $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\AppData\Databases.json";
        private List<Database> Databases { get; set; }

        private JsonSerializerOptions Options = new() { WriteIndented = true };

        public static ConnectionStringsSingleton Instance
        {
            get
            {
                _instance ??= new ConnectionStringsSingleton();
                return _instance;
            }
        }

        public List<Database> Lines()
        {
            string text = File.ReadAllText(Path);
            Databases = JsonSerializer.Deserialize<List<Database>>(text)!;

            return Databases;
        }

        public void InsertLine(Database line)
        {
            string text = File.ReadAllText(Path);
            Databases = JsonSerializer.Deserialize<List<Database>>(text)!;

            if (!Databases.Contains(line))
            {
                Databases.Add(line);

                var json = JsonSerializer.Serialize(Databases, Options);
                File.WriteAllText(Path, json);
            }
            else
            {
                var index = Databases.IndexOf(line);
                Databases.Insert(index, line);
            }
        }

        public void Update(List<Database> databases)
        {
            var json = JsonSerializer.Serialize(databases, Options);
            File.WriteAllText(Path, json);
            Databases = databases;
        }
    }
}
