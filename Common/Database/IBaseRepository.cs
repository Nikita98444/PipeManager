using System.Data;
using System.Data.Common;

namespace PipeCommon.Database;

public interface IBaseRepository
{
    DbConnection OpenConnection();

    TReturn InTransaction<TReturn>(Func<IDbTransaction, TReturn> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    TReturn InTransaction<TReturn>(Func<DbTransaction, TReturn> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);

    Task<DbConnection> OpenConnectionAsync();

    Task<TReturn> InTransactionAsync<TReturn>(Func<IDbTransaction, Task<TReturn>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}