using Dapper;
using Faithlife.Utility.Dapper;
using System.Data;

namespace PipeCommon.Database;

public static partial class DapperExtension
{
    #region QueryFirstOrDefault
    /// <summary>
    /// Executes a single-row query, returning the data typed as T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cnn"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public static T QueryFirstOrDefaultCustom<T>(this IDbConnection cnn, string sql, object param = null,
                    int? commandTimeout = null, CommandType? commandType = null) =>
        Execute(() => cnn.QueryFirstOrDefault<T>(sql, param, null, commandTimeout, commandType));

    /// <summary>
    /// Async executes a single-row query, returning the data typed as T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cnn"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>
    /// <returns></returns>
    public static async Task<T> QueryFirstOrDefaultCustomAsync<T>(this IDbConnection cnn, string sql, object param = null,
                    int? commandTimeout = null, CommandType? commandType = null) =>
        await ExecuteAsync(async () => await cnn.QueryFirstOrDefaultCustomAsync<T>(sql, param, commandTimeout, commandType));
    #endregion

    #region QueryFirst
    /// <summary>
    /// Executes a single-row query, returning the data typed as T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cnn"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>        
    /// <returns></returns>
    public static T QueryFirstCustom<T>(this IDbConnection cnn, string sql, object param = null,
                    int? commandTimeout = null, CommandType? commandType = null) =>
        Execute(() => cnn.QueryFirst<T>(sql, param, null, commandTimeout, commandType));

    /// <summary>
    /// Async executes a single-row query, returning the data typed as T.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cnn"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>        
    /// <returns></returns>
    public static async Task<T> QueryFirstCustomAsync<T>(this IDbConnection cnn, string sql, object param = null,
                    int? commandTimeout = null, CommandType? commandType = null) =>
        await ExecuteAsync(async () => await cnn.QueryFirstAsync<T>(sql, param, null, commandTimeout, commandType));
    #endregion

    #region Execute
    /// <summary>
    /// Execute parameterized SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cnn"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>     
    /// <returns></returns>
    public static int ExecuteCustom(this IDbConnection cnn, string sql, object param = null,
                      int? commandTimeout = null, CommandType? commandType = null) =>
         Execute(() => cnn.Execute(sql, param, null, commandTimeout, commandType));

    /// <summary>
    /// Async execute parameterized SQL.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="cnn"></param>
    /// <param name="sql"></param>
    /// <param name="param"></param>
    /// <param name="commandTimeout"></param>
    /// <param name="commandType"></param>     
    /// <returns></returns>
    public static async Task<int> ExecuteCustomAsync(this IDbConnection cnn, string sql, object param = null,
                      int? commandTimeout = null, CommandType? commandType = null) =>
         await ExecuteAsync(async () => await cnn.ExecuteAsync(sql, param, null, commandTimeout, commandType));
    #endregion

    #region Query
    /// <summary>
    /// Perform a multi-mapping query with an arbitrary number of input types. This returns
    /// a single type, combined from the raw types via map.
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<TReturn> QueryCustom<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null,
        bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
       Execute(() => cnn.Query(sql, types, map, param, null, buffered, splitOn, commandTimeout, commandType));

    /// <summary>
    /// Async perform a multi-mapping query with an arbitrary number of input types. This returns
    /// a single type, combined from the raw types via map.
    /// </summary>
    /// <returns></returns>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TReturn>(this IDbConnection cnn, string sql, Type[] types, Func<object[], TReturn> map, object param = null,
        bool buffered = true, string splitOn = "Id", int? commandTimeout = null, CommandType? commandType = null) =>
       await ExecuteAsync(async () => await cnn.QueryAsync(sql, types, map, param, null, buffered, splitOn, commandTimeout, commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<T> QueryCustom<T>(this IDbConnection cnn, string sql, object param = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null) =>
       Execute(() => cnn.Query<T>(sql, param, null, buffered, commandTimeout, commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<T>> QueryCustomAsync<T>(this IDbConnection cnn, string sql, object param = null, int? commandTimeout = null, CommandType? commandType = null) =>
       await ExecuteAsync(async () => await cnn.QueryAsync<T>(sql, param, null, commandTimeout, commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TDict : IDictionary<Guid, TReturn> =>
        ExecuteDictionary<TReturn>((items) => cnn.Query<TReturn, TFirst, TSecond, TReturn>(sql
                                                , (a, b, c) => map(a, b, c, (TDict)items)
                                                , param
                                                , null
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TDict, TReturn> map
                    , object param = null
                    , bool buffered = true
                    , string splitOn = "Id"
                    , int? commandTimeout = null
                    , CommandType? commandType = null)
                    where TReturn : BaseEntity
                    where TFirst : BaseEntity
                    where TSecond : BaseEntity
                    where TDict : IDictionary<Guid, TReturn>
    => await ExecuteDictionaryAsync<TReturn>(async (items) => await cnn.QueryAsync<TReturn, TFirst, TSecond, TReturn>(sql
                                    , (a, b, c) => map(a, b, c, (TDict)items)
                                    , param
                                    , null
                                    , buffered
                                    , splitOn
                                    , commandTimeout
                                    , commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TThird, TDict, TReturn> map
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
        ExecuteDictionary<TReturn>((items) => cnn.Query<TReturn, TFirst, TSecond, TThird, TReturn>(sql
                                               , (a, b, c, d) => map(a, b, c, d, (TDict)items)
                                               , param
                                               , null
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TThird, TDict, TReturn> map
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
        await ExecuteDictionaryAsync<TReturn>(async (items) => await cnn.QueryAsync<TReturn, TFirst, TSecond, TThird, TReturn>(sql
                                               , (a, b, c, d) => map(a, b, c, d, (TDict)items)
                                               , param
                                               , null
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TFourth, TDict, TReturn>(this IDbConnection cnn
                    , string sql, Func<TReturn, TFirst, TSecond, TThird, TFourth, TDict, TReturn> map
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
        ExecuteDictionary<TReturn>((items) => cnn.Query<TReturn, TFirst, TSecond, TThird, TFourth, TReturn>(sql
                                               , (a, b, c, d, e) => map(a, b, c, d, e, (TDict)items)
                                               , param
                                               , null
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TFourth, TDict, TReturn>(this IDbConnection cnn
                    , string sql, Func<TReturn, TFirst, TSecond, TThird, TFourth, TDict, TReturn> map
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
        await ExecuteDictionaryAsync<TReturn>(async (items) => await cnn.QueryAsync<TReturn, TFirst, TSecond, TThird, TFourth, TReturn>(sql
                                               , (a, b, c, d, e) => map(a, b, c, d, e, (TDict)items)
                                               , param
                                               , null
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));


    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn> map
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
        ExecuteDictionary<TReturn>((items) => cnn.Query<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql
                                               , (a, b, c, d, e, f) => map(a, b, c, d, e, f, (TDict)items)
                                               , param
                                               , null
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TDict, TReturn> map
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
        await ExecuteDictionaryAsync<TReturn>(async (items) => await cnn.QueryAsync<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(sql
                                               , (a, b, c, d, e, f) => map(a, b, c, d, e, f, (TDict)items)
                                               , param
                                               , null
                                               , buffered
                                               , splitOn
                                               , commandTimeout
                                               , commandType));

    /// <summary>
    /// Executes a query, returning the data typed as T.
    /// </summary>
    public static IEnumerable<TReturn> QueryCustom<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn> map
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
         ExecuteDictionary<TReturn>((items) => cnn.Query<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql
                                                , (a, b, c, d, e, f, g) => map(a, b, c, d, e, f, g, (TDict)items)
                                                , param
                                                , null
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));

    /// <summary>
    /// Async executes a query, returning the data typed as T.
    /// </summary>
    public static async Task<IEnumerable<TReturn>> QueryCustomAsync<TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn>(this IDbConnection cnn
                    , string sql
                    , Func<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TDict, TReturn> map
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
         await ExecuteDictionaryAsync<TReturn>(async (items) => await cnn.QueryAsync<TReturn, TFirst, TSecond, TThird, TFourth, TFifth, TSixth, TReturn>(sql
                                                , (a, b, c, d, e, f, g) => map(a, b, c, d, e, f, g, (TDict)items)
                                                , param
                                                , null
                                                , buffered
                                                , splitOn
                                                , commandTimeout
                                                , commandType));
    #endregion

    #region obsolete Query
    #endregion

    #region Insert

    /// <summary>
    /// Делает множественный INSERT
    /// </summary>
    /// <typeparam name="TInsert">Тип вставляемых значений</typeparam>
    /// <param name="connection">Подключение к БД</param>
    /// <param name="sql">SQL запрос</param>
    /// <param name="insertParams">Параметры SQL запроса</param>
    /// <param name="transaction">Транзакция</param>
    /// <returns></returns>
    public static int InsertBulk<TInsert>(this IDbConnection connection, string sql, IEnumerable<TInsert> insertParams, IDbTransaction transaction = null) =>
        connection.BulkInsert(sql, insertParams, transaction);

    /// <summary>
    /// Делает множественный INSERT Async
    /// </summary>
    /// <typeparam name="TInsert">Тип вставляемых значений</typeparam>
    /// <param name="connection">Подключение к БД</param>
    /// <param name="sql">SQL запрос</param>
    /// <param name="insertParams">Параметры SQL запроса</param>
    /// <param name="transaction">Транзакция</param>
    /// <returns></returns>
    public static async Task<int> InsertBulkAsync<TInsert>(this IDbConnection connection, string sql, IEnumerable<TInsert> insertParams, IDbTransaction transaction = null) =>
        await connection.BulkInsertAsync(sql, insertParams, transaction);

    #endregion Insert
}
