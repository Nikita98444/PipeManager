using Npgsql;
using StackExchange.Profiling;

namespace PipeCommon.Database;


public static partial class DapperExtension
{
    /// <summary>
    /// Профайлер для вывода запросов в out
    /// </summary>
    public static MiniProfiler MiniProfiler { get; set; }
    /// <summary>
    /// Выполнить запрос в бд
    /// </summary>
    private static T Execute<T>(Func<T> func)
    {
        T result = default(T);
        try
        {
            result = func.Invoke();
        }
        catch (NpgsqlException ex)
        {
            if (MiniProfiler != null)
            {
                // npgsqlStatement removed https://www.npgsql.org/doc/release-notes/6.0.html
                var statement = ex.BatchCommand;
                System.Diagnostics.Debug.WriteLine($"Message: {ex.Message} SQL: {statement?.CommandText} InputParameters: {string.Join(", ", statement.Parameters.Select(c => $"{"{"}{c.ParameterName}: {c.Value}"))}{"}"}");
            }

            throw;
        }
        finally
        {
            if (MiniProfiler != null)
            {
                System.Diagnostics.Debug.WriteLine(MiniProfiler.Head.CustomTimingsJson);
            }
        }
        return result;
    }
    /// <summary>
    /// Асинхронно выполнить запрос в бд
    /// </summary>
    private static async Task<T> ExecuteAsync<T>(Func<Task<T>> func)
    {
        T result = default(T);
        try
        {
            result = await func.Invoke();
        }
        catch (NpgsqlException ex)
        {
            if (MiniProfiler != null)
            {
                // npgsqlStatement removed https://www.npgsql.org/doc/release-notes/6.0.html
                var statement = ex.BatchCommand;
                System.Diagnostics.Debug.WriteLine($"Message: {ex.Message} SQL: {statement?.CommandText} InputParameters: {string.Join(", ", statement.Parameters.Select(c => $"{"{"}{c.ParameterName}: {c.Value}"))}{"}"}");
            }

            throw;
        }
        finally
        {
            if (MiniProfiler != null)
            {
                System.Diagnostics.Debug.WriteLine(MiniProfiler.Head.CustomTimingsJson);
            }
        }
        return result;
    }
    /// <summary>
    /// Выполнить запрос в бд со словарем
    /// </summary>
    private static IEnumerable<T> ExecuteDictionary<T>(Func<IDictionary<Guid, T>, IEnumerable<T>> func)
    {
        IEnumerable<T> result = null;
        try
        {
            Dictionary<Guid, T> items = new Dictionary<Guid, T>();
            func.Invoke(items);
            result = items.Values;
        }
        catch (NpgsqlException ex)
        {
            if (MiniProfiler != null)
            {
                // npgsqlStatement removed https://www.npgsql.org/doc/release-notes/6.0.html
                var statement = ex.BatchCommand;
                System.Diagnostics.Debug.WriteLine($"Message: {ex.Message} SQL: {statement?.CommandText} InputParameters: {string.Join(", ", statement.Parameters.Select(c => $"{"{"}{c.ParameterName}: {c.Value}"))}{"}"}");
            }
            throw;
        }
        finally
        {
            if (MiniProfiler != null)
            {
                System.Diagnostics.Debug.WriteLine(MiniProfiler.Head.CustomTimingsJson);
            }
        }
        return result;
    }

    /// <summary>
    /// Выполнить запрос в бд со словарем асинхронно
    /// </summary>
    private static async Task<IEnumerable<T>> ExecuteDictionaryAsync<T>(Func<IDictionary<int, T>, Task<IEnumerable<T>>> func)
    {
        IEnumerable<T> result = null;
        try
        {
            Dictionary<int, T> items = new();
            await func.Invoke(items);
            result = items.Values;
        }
        catch (NpgsqlException ex)
        {
            if (MiniProfiler != null)
            {
                // npgsqlStatement removed https://www.npgsql.org/doc/release-notes/6.0.html
                var statement = ex.BatchCommand;
                System.Diagnostics.Debug.WriteLine($"Message: {ex.Message} SQL: {statement?.CommandText} InputParameters: {string.Join(", ", statement.Parameters.Select(c => $"{"{"}{c.ParameterName}: {c.Value}"))}{"}"}");
            }
            throw;
        }
        catch (Exception ex)
        {
            System.Diagnostics.Debug.WriteLine($"Message: {ex.Message} func.Invoke Error");
            throw;
        }
        finally
        {
            if (MiniProfiler != null)
            {
                System.Diagnostics.Debug.WriteLine(MiniProfiler.Head.CustomTimingsJson);
            }
        }
        return result;
    }
}