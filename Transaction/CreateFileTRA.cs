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

    public static DirectoryInfo CreateDirectoryIfNotExistes(string path)
    {
        var directoryInfo = new DirectoryInfo(path);
        if (!directoryInfo.Exists)
            directoryInfo = Directory.CreateDirectory(path);

        return directoryInfo;
    }

    public static bool FileExistes(string path)
    {
        return File.Exists(path);
    }

    public static void WriteLinesInFile(String path, IEnumerable<string> lines)
    {
        using StreamWriter sw = new StreamWriter(path);
        foreach (var line in lines)
        {
            sw.WriteLine(line);
        }
    }

    public static void WriteInFile(String path, string text)
    {
        using StreamWriter sw = new StreamWriter(path);
        sw.Write(text);
    }
}
