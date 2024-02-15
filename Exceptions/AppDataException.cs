namespace config.Exceptions;
internal class AppDataException : Exception
{
    public AppDataException()
    {
    }

    public AppDataException(string? message) : base(message)
    {
    }

    public AppDataException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}
