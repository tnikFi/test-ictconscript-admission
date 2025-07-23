using System.Diagnostics.CodeAnalysis;

namespace Common.Exceptions;

public class NotFoundException : Exception
{
    /// <summary>
    /// Throw the exception if the provided value is null
    /// </summary>
    /// <param name="argument">Value to check</param>
    /// <param name="message">Exception message</param>
    /// <exception cref="NotFoundException">Thrown if <see cref="argument"/> is null</exception>
    public static void ThrowIfNull([NotNull] object? argument, string? message = null)
    {
        if (argument is null)
            throw new NotFoundException(message);
    }

    public NotFoundException(string? message) : base(message)
    {
    }
}