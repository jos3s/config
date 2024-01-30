using config.Utils.Messages;

namespace config.Transaction;
internal static class CreateFileTRA
{
    public static void ValidatePath(string path)
    {
        var pathExistes = Directory.Exists(path);
        if (path.First().Equals('.') && !pathExistes)
            Directory.CreateDirectory(path);
        else if (!pathExistes)
            throw new ArgumentException(FileMsg.EXC001);
    }


    public static void WriteLinesInFile(String path, IEnumerable<string> lines)
    {
        using (StreamWriter sw = new StreamWriter(path))
        {
            foreach (var line in lines)
            {
                sw.WriteLine(line);
            }
        }
    }
}
