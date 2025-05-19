namespace PipeCommon.Extensions;

/// <summary>
/// Расширения для <see cref="DateTimeOffset"/>
/// </summary>
public static class DateTimeOffsetExtensions
{
    private static readonly TimeSpan MorningEnd = new TimeSpan(7, 59, 59);
    private static readonly TimeSpan DayEnd = new TimeSpan(19, 59, 59); 

    /// <summary>
    /// Получить начало смены
    /// </summary>
    public static DateTimeOffset GetShiftStart(this DateTimeOffset dateTime)
    {
        var currentTime = dateTime.TimeOfDay;
        var date = dateTime.Date;

        return currentTime switch
        {
            _ when currentTime > DayEnd => date.AddHours(20), 
            _ when currentTime <= MorningEnd => date.AddDays(-1).AddHours(20), 
            _ => date.AddHours(8)
        };
    }

    /// <summary>
    /// Получить конец смены
    /// </summary>
    public static DateTimeOffset GetShiftEnd(this DateTimeOffset dateTime)
    {
        var currentTime = dateTime.TimeOfDay;
        var date = dateTime.Date;

        return currentTime switch
        {
            _ when currentTime > DayEnd => date.AddDays(1).AddHours(7).AddMinutes(59).AddSeconds(59), 
            _ when currentTime <= MorningEnd => date.AddHours(7).AddMinutes(59).AddSeconds(59), 
            _ => date.AddHours(19).AddMinutes(59).AddSeconds(59)
        };
    }
}