using config.Features.Database.Shared;

using System.Reflection;
using System.Text.Json;

namespace config.Singleton
{
    internal class ConnectionStringsSingleton : FileSingleton
    {
        private static ConnectionStringsSingleton _instance { get; set; }

        private static string Path =
            $@"{System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\AppData\Databases.json";
        private List<DatabaseModel> Databases { get; set; }

        private JsonSerializerOptions Options = new() { WriteIndented = true };

        public static ConnectionStringsSingleton Instance
        {
            get
            {
                _instance ??= new ConnectionStringsSingleton();
                ValidateFile(Path);
                return _instance;
            }
        }

        public List<DatabaseModel> Lines()
        {
            string text = File.ReadAllText(Path);
            if (!string.IsNullOrEmpty(text) || !string.IsNullOrWhiteSpace(text))
                Databases = JsonSerializer.Deserialize<List<DatabaseModel>>(text)!;
            else Databases = new List<DatabaseModel>();

            return Databases;
        }

        public void InsertLine(DatabaseModel line)
        {
            string text = File.ReadAllText(Path);
            Databases = JsonSerializer.Deserialize<List<DatabaseModel>>(text)!;

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

        public void Update(List<DatabaseModel> databases)
        {
            var json = JsonSerializer.Serialize(databases, Options);
            File.WriteAllText(Path, json);
            Databases = databases;
        }
    }
}
