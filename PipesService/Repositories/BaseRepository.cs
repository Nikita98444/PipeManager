

using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data.Common;
using System.Data;
using PipeCommon.Database;
using StackExchange.Profiling;

namespace PipeService.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        /// <summary>
        /// Строка подключения
        /// </summary>
        public string ConnectionString { get; private set; }

        public BaseRepository(IConfiguration moduleConfiguration, string connectionStringParamName = "ConnectionString")
        {
            ConnectionString = moduleConfiguration.GetConnectionString("DefaultConnection");
            DefaultTypeMap.MatchNamesWithUnderscores = true;

            if (bool.Parse(moduleConfiguration["WithProfile"] ?? "false"))
            {
                DapperExtension.MiniProfiler = MiniProfiler.StartNew(GetType().Name);
            }
        }

        public TReturn InTransaction<TReturn>(Func<DbTransaction, TReturn> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
          => InTransaction((t) => func((DbTransaction)t), isolationLevel);

        public TReturn InTransaction<TReturn>(Func<IDbTransaction, TReturn> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            using var connection = OpenConnection();
            using var transaction = connection.BeginTransaction(isolationLevel);
            try
            {
                var result = func.Invoke(transaction);
                transaction.Commit();
                return result;
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
        }

        public async Task<TReturn> InTransactionAsync<TReturn>(Func<IDbTransaction, Task<TReturn>> func,
           IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
        {
            await using var connection = await OpenConnectionAsync();
            await using var transaction = await connection.BeginTransactionAsync(isolationLevel);
            try
            {
                var result = await func.Invoke(transaction);
                await transaction.CommitAsync();
                return result;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public DbConnection OpenConnection()
        {
            DbConnection conn = null;
            var cnn = new NpgsqlConnection(ConnectionString);
            if (DapperExtension.MiniProfiler != null)
            {
                conn = new StackExchange.Profiling.Data.ProfiledDbConnection(cnn, DapperExtension.MiniProfiler).WrappedConnection;
            }
            else
            {
                conn = cnn;
            }

            conn.Open();
            return conn;
        }

        public async Task<DbConnection> OpenConnectionAsync()
        {
            var cnn = new NpgsqlConnection(ConnectionString);

            var conn = DapperExtension.MiniProfiler != null
                ? new StackExchange.Profiling.Data.ProfiledDbConnection(cnn, DapperExtension.MiniProfiler).WrappedConnection
                : cnn;

            await conn.OpenAsync();
            return conn;
        }
    }
}
