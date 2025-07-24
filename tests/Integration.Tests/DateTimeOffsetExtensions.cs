namespace Integration.Tests;

public static class DateTimeOffsetExtensions
{
    /// <summary>
    /// Returns true if the given times are within the given time span of each other.
    /// Ignores the order in which the date times are provided.
    /// </summary>
    /// <param name="dateTime"></param>
    /// <param name="other"><see cref="DateTimeOffset"/> to compare with</param>
    /// <param name="timeSpan">Comparison tolerance (inclusive)</param>
    /// <returns></returns>
    public static bool IsWithin(this DateTimeOffset dateTime, DateTimeOffset other, TimeSpan timeSpan)
    {
        if (dateTime == other) return true;
        if (timeSpan == TimeSpan.Zero) return dateTime == other;
        if (dateTime < other)
            return other - dateTime <= timeSpan;
        return dateTime - other <= timeSpan;
    }
}