using Dapper;
using PipeCommon.Extensions;
using PipeCommon.Requests;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;
using System.Data.Common;
using System.Text;

namespace PipeCommon.Database
{
    public abstract class BaseRepository : IBaseRepository
    {
        private readonly string _connectString;

        protected BaseRepository(IConfiguration config, string configParameter)
        {
            _connectString = !string.IsNullOrEmpty(configParameter) && config != null
                ? config[configParameter]
                : null;
            if (string.IsNullOrEmpty(_connectString))
            {
                throw new ArgumentException("Строка подключения пустая");
            }
            DefaultTypeMap.MatchNamesWithUnderscores = true;
        }

        public DbConnection OpenConnection()
        {
            NpgsqlConnection dbConnection = new(_connectString);
            try
            {
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Не удалось подключиться к базе данных с ConnectionSting : {_connectString}\n{ex.Message}\n{ex.StackTrace}\n");
            }
            return dbConnection;
        }

        public TReturn InTransaction<TReturn>(Func<IDbTransaction, TReturn> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            using (DbConnection dbConnection = OpenConnection())
            using (DbTransaction dbTransaction = dbConnection.BeginTransaction(isolationLevel))
            {
                try
                {
                    if (func != null)
                    {
                        TReturn result = func(dbTransaction);
                        dbTransaction.Commit();
                        return result;
                    }
                    dbTransaction.Rollback();
                    throw new ArgumentException($" \nОшибка транзакции при подключении к БД сообщений");
                }
                catch (Exception ex)
                {
                    dbTransaction.Rollback();
                    throw new ArgumentException($"{ex.Message}\n{ex.StackTrace} \nОшибка транзакции при подключении к БД сообщений");
                }
            }
        }

        public TReturn InTransaction<TReturn>(Func<DbTransaction, TReturn> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            return InTransaction((t) => func((DbTransaction)t), isolationLevel);
        }

        public async Task<DbConnection> OpenConnectionAsync()
        {
            NpgsqlConnection dbConnection = new(_connectString);
            try
            {
                await dbConnection.OpenAsync();
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Не удалось подключиться к базе данных с ConnectionSting : {_connectString}\n{ex.Message}\n{ex.StackTrace}\n");
            }
            return dbConnection;
        }

        public async Task<TReturn> InTransactionAsync<TReturn>(Func<IDbTransaction, Task<TReturn>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            using (DbConnection dbConnection = await OpenConnectionAsync())
            using (DbTransaction dbTransaction = await dbConnection.BeginTransactionAsync(isolationLevel))
            {
                try
                {
                    if (func != null)
                    {
                        TReturn result = await func(dbTransaction);
                        await dbTransaction.CommitAsync();
                        return result;
                    }
                    await dbTransaction.RollbackAsync();
                    throw new ArgumentException($"Ошибка транзакции при подключении к БД сообщений");
                }
                catch (Exception ex)
                {
                    await dbTransaction.RollbackAsync();
                    throw new ArgumentException($"{ex.Message}\n{ex.StackTrace} \nОшибка транзакции при подключении к БД сообщений");
                }
            }
        }

        public string GetBaseQueryFromFilters(IDictionary<string, object> filters, Dictionary<string, string> propertiesDictionary, out Dictionary<string, object> usedBaseQueryParameters, string @operator = " and ")
        {
            StringBuilder stringBuilder = new StringBuilder();
            IEnumerable<string> enumerable = filters.Keys.Where((string x) => filters[x] != null && !string.IsNullOrEmpty(filters[x].ToString())).Intersect(propertiesDictionary.Keys);
            usedBaseQueryParameters = new Dictionary<string, object>();
            if (enumerable.Any())
            {
                StringBuilder stringBuilder2 = stringBuilder;
                StringBuilder.AppendInterpolatedStringHandler handler = new StringBuilder.AppendInterpolatedStringHandler(8, 1, stringBuilder2);
                handler.AppendLiteral(" where ");
                handler.AppendFormatted(string.Join(@operator, enumerable.Select((string m) => propertiesDictionary[m] + "::text ilike @" + m)));
                handler.AppendLiteral(" ");
                stringBuilder2.Append(ref handler);
                foreach (string item in enumerable)
                {
                    usedBaseQueryParameters.Add(item, $"%{filters[item]}%");
                }
            }

            return stringBuilder.ToString();
        }

        protected static string GetPagingOffset(IDictionary<string, object> filters, IDictionary<string, object> parameters)
        {
            if (filters.TryGetValue<int, string, object>("maxRowCount", out var value) && value > 0 && filters.TryGetValue<int, string, object>("pageNumber", out var value2) && value2 > 0)
            {
                parameters.Add("maxRowCount", value);
                parameters.Add("pageNumber", value2);
                return "OFFSET ((@pageNumber - 1) * @maxRowCount) LIMIT @maxRowCount";
            }

            return string.Empty;
        }
        public static string GetPagingOffset(PagingRequest request, IDictionary<string, object> parameters)
        {
            if (request.MaxRowCount.HasValue && request.MaxRowCount.Value > 0 && request.PageNumber.HasValue && request.PageNumber.Value > 0)
            {
                parameters.Add("maxRowCount", request.MaxRowCount.Value);
                parameters.Add("pageNumber", request.PageNumber.Value);
                return "OFFSET ((@pageNumber - 1) * @maxRowCount) LIMIT @maxRowCount";
            }

            return string.Empty;
        }
    }
}