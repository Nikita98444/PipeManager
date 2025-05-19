using Dapper;
using Faithlife.Utility.Dapper;
using System.Data;

namespace PipeCommon.Database;

public static partial class DapperExtension
{

    /// <summary>
    /// Executes a single-row query, returning the data typed as T.
    /// </summary>
    public static T QueryFirstOrDefaultCustom<T>(this IDbTransaction transaction, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
        Execute(() => transaction.Connection.QueryFirstOrDefault<T>(sql, param, transaction, commandTimeout, commandType));

    /// <summary>
    /// Async Executes a single-row query, returning the data typed as T.
    /// </summary>
    public async static Task<T> QueryFirstOrDefaultCustomAsync<T>(this IDbTransaction transaction, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
        await ExecuteAsync(async () => await transaction.Connection.QueryFirstOrDefaultAsync<T>(sql, param, transaction, commandTimeout, commandType));

    /// <summary>
    /// Executes a single-row query, returning the data typed as T.
    /// </summary>
    public static T QueryFirstCustom<T>(this IDbTransaction transaction, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
        Execute(() => transaction.Connection.QueryFirst<T>(sql, param, transaction, commandTimeout, commandType));

    /// <summary>
    /// Async Executes a single-row query, returning the data typed as T.
    /// </summary>
    public static async Task<T> QueryFirstCustomAsync<T>(this IDbTransaction transaction, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
        await ExecuteAsync(async () => await transaction.Connection.QueryFirstAsync<T>(sql, param, transaction, commandTimeout, commandType));

    /// <summary>
    /// Execute parameterized SQL.
    /// </summary>
    public static int ExecuteCustom(this IDbTransaction transaction, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
         Execute(() => transaction.Connection.Execute(sql, param, transaction, commandTimeout, commandType));

    /// <summary>
    /// Async execute parameterized SQL.
    /// </summary>
    public static async Task<int> ExecuteCustomAsync(this IDbTransaction transaction, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
         await ExecuteAsync(async () => await transaction.Connection.ExecuteAsync(sql, param, transaction, commandTimeout, commandType));

    /// <summary>
    /// Perform a multi-mapping query with an arbitrary number of input types. This returns
    /// a single type, combined from the raw types via map.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TDict, TReturn>(this IDbTransaction transaction, string sql, Type[] types, Func<object[], TDict, TReturn> map, object param = null,
          bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) where TDict : Dictionary<Guid, TReturn> =>
            ExecuteDictionary<TReturn>((items) => transaction.Connection.Query(sql, types, (a) => map(a, (TDict)items), param, transaction, buffered, splitOn, commandTimeout, commandType));

    /// <summary>
    /// Async perform a multi-mapping query with an arbitrary number of input types. This returns
    /// a single type, combined from the raw types via map.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TDict, TReturn>(this IDbTransaction transaction, string sql, Type[] types, Func<object[], TDict, TReturn> map, object param = null,
          bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) where TDict : Dictionary<int, TReturn> =>
        await ExecuteDictionaryAsync<TReturn>(async (items) => await transaction.Connection.QueryAsync(sql, types, (a) => map(a, (TDict)items), param, transaction, buffered, splitOn, commandTimeout, commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<T> QueryCustom<T>(this IDbTransaction transaction, string sql, object param = null,
                                 bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) =>
       Execute(() => transaction.Connection.Query<T>(sql, param, transaction, buffered, commandTimeout, commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<T>> QueryCustomAsync<T>(this IDbTransaction transaction, string sql, object param = null,
                                int? commandTimeout = null, CommandType? commandType = null) =>
       await ExecuteAsync(async () => await transaction.Connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType));

    #region Query
    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        ExecuteDictionary<TReturn>((items) => transaction.Connection.Query<TFirst, TSecond, TReturn>(sql
                                                , (a, b) => map(a, b, (TDict)items)
                                                , param
                                                , transaction
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TDict : IDictionary<int, TReturn> =>
        await ExecuteDictionaryAsync<TReturn>(async (items) => await transaction.Connection.QueryAsync<TFirst, TSecond, TReturn>(sql
                                                , (a, b) => map(a, b, (TDict)items)
                                                , param
                                                , transaction
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        ExecuteDictionary<TReturn>((items) => transaction.Connection.Query<TFirst, TSecond, TThird, TReturn>(sql
                                               , (a, b, c) => map(a, b, c, (TDict)items)
                                               , param
                                               , transaction
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        await ExecuteDictionaryAsync<TReturn>(async (items) => await transaction.Connection.QueryAsync<TFirst, TSecond, TThird, TReturn>(sql
                                               , (a, b, c) => map(a, b, c, (TDict)items)
                                               , param
                                               , transaction
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TFourth, TDict, TReturn>(this IDbTransaction transaction
                    , string sql, Func<TFirst, TSecond, TThird, TFourth, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        ExecuteDictionary<TReturn>((items) => transaction.Connection.Query<TFirst, TSecond, TThird, TFourth, TReturn>(sql
                                               , (a, b, c, d) => map(a, b, c, d, (TDict)items)
                                               , param
                                               , transaction
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TFourth, TDict, TReturn>(this IDbTransaction transaction
                    , string sql, Func<TFirst, TSecond, TThird, TFourth, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        await ExecuteDictionaryAsync<TReturn>(async (items) => await transaction.Connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TReturn>(sql
                                               , (a, b, c, d) => map(a, b, c, d, (TDict)items)
                                               , param
                                               , transaction
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));


    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TFifth : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        ExecuteDictionary<TReturn>((items) => transaction.Connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql
                                               , (a, b, c, d, e) => map(a, b, c, d, e, (TDict)items)
                                               , param
                                               , transaction
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TFifth : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        await ExecuteDictionaryAsync<TReturn>(async (items) => await transaction.Connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql
                                               , (a, b, c, d, e) => map(a, b, c, d, e, (TDict)items)
                                               , param
                                               , transaction
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TFifth : BaseEntity
                    where TSixth : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
         ExecuteDictionary<TReturn>((items) => transaction.Connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql
                                                , (a, b, c, d, e, f) => map(a, b, c, d, e, f, (TDict)items)
                                                , param
                                                , transaction
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TFifth : BaseEntity
                    where TSixth : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
         await ExecuteDictionaryAsync<TReturn>(async (items) => await transaction.Connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql
                                                , (a, b, c, d, e, f) => map(a, b, c, d, e, f, (TDict)items)
                                                , param
                                                , transaction
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TFifth : BaseEntity
                    where TSixth : BaseEntity
                    where TSeventh : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
         ExecuteDictionary<TReturn>((items) => transaction.Connection.Query<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql
                                                , (a, b, c, d, e, f, g) => map(a, b, c, d, e, f, g, (TDict)items)
                                                , param
                                                , transaction
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TDict, TReturn>(this IDbTransaction transaction
                    , string sql
                    , Func<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TThird : BaseEntity
                    where TFourth : BaseEntity
                    where TFifth : BaseEntity
                    where TSixth : BaseEntity
                    where TSeventh : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
         await ExecuteDictionaryAsync<TReturn>(async (items) => await transaction.Connection.QueryAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TSeventh, TReturn>(sql
                                                , (a, b, c, d, e, f, g) => map(a, b, c, d, e, f, g, (TDict)items)
                                                , param
                                                , transaction
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Делает множественный INSERT
    /// </summary>
    /// <typeparam name="TInsert">Тип вставляемых значений</typeparam>
    /// <param name="transaction">Транзакция</param>
    /// <param name="sql">SQL запрос</param>
    /// <param name="insertParams">Параметры SQL запроса</param>
    /// <returns></returns>
    public static int InsertBulk<TInsert>(this IDbTransaction transaction, string sql, IEnumerable<TInsert> insertParams) =>
        transaction.Connection.BulkInsert(sql, insertParams, transaction);

    /// <summary>
    /// Делает множественный INSERT async
    /// </summary>
    /// <typeparam name="TInsert">Тип вставляемых значений</typeparam>
    /// <param name="transaction">Транзакция</param>
    /// <param name="sql">SQL запрос</param>
    /// <param name="insertParams">Параметры SQL запроса</param>
    /// <returns></returns>
    public static async Task<int> InsertBulkAsync<TInsert>(this IDbTransaction transaction, string sql, IEnumerable<TInsert> insertParams) =>
        await transaction.Connection.BulkInsertAsync(sql, insertParams, transaction);
    #endregion



}
