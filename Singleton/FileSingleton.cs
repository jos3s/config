using config.Exceptions;
using config.Utils.Messages;

namespace config.Singleton;
internal abstract class FileSingleton
{
    public static void ValidateFile(string path)
    {
        var existis = File.Exists(path);

        if (!existis)
        {
            throw new AppDataException(ExceptionMsg.EXC0001);
        }
    }
}
