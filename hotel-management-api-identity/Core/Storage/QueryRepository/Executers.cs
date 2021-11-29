using hotel_management_api_identity.Core.Helpers;
using System.Data.SqlClient;
using Dapper;
using hotel_management_api_identity.Core.Error.Exceptions;

namespace hotel_management_api_identity.Core.Storage.QueryRepository
{
    public interface IExecuters : IAutoDependencyCore
    {
        void ExecuteCommand(string connStr, Action<SqlConnection, SqlTransaction> task);
        T ExecuteCommand<T>(string connStr, Func<SqlConnection, SqlTransaction, T> task);
        Task<T> ExecuteCommandAsync<T>(string connStr, Func<SqlConnection, SqlTransaction, Task<T>> task);
        void ExecuteCommand<T>(string connStr, string query, object param);
        Task ExecuteCommandAsync<T>(string connStr, string query, object param);

        IEnumerable<T> ExecuteReader<T>(string connStr, Func<SqlConnection, SqlTransaction, IEnumerable<T>> task);
        IEnumerable<T> ExecuteReader<T>(string connStr, string query, object param);
        Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connStr, string query, object param);
        Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connStr, Func<SqlConnection, SqlTransaction, Task<IEnumerable<T>>> task);
        Task ExecuteCommandAsync<T>(string connStr, string query, object param, SqlTransaction sqlTransaction);
        Task ExecuteCommandAsync<T>(string query, object param, SqlTransaction sqlTransaction);
        void ExecuteCommand<T>(string query, object param, SqlTransaction sqlTransaction);
    }

    public class Executers : IExecuters
    {
        private readonly IConfiguration _configuration;
        public Executers(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Execute the SQL command passed to the action.
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        public void ExecuteCommand(string connStr, Action<SqlConnection, SqlTransaction> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                SqlTransaction _sqlTransaction = null;
                try
                {
                    conn.Open();
                    _sqlTransaction = conn.BeginTransaction();
                    task(conn, _sqlTransaction);
                    _sqlTransaction.Commit();
                }
                catch (SqlException ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Execute the SQL command passed to the function delegate. Use if an object is to be returned after execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public T ExecuteCommand<T>(string connStr, Func<SqlConnection, SqlTransaction, T> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                SqlTransaction _sqlTransaction = null;
                try
                {
                    conn.Open();
                    _sqlTransaction = conn.BeginTransaction();
                    T responseObject = task(conn, _sqlTransaction);
                    _sqlTransaction.Commit();
                    return responseObject;
                }
                catch (SqlException ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Asynchronously execute the SQL command passed to the function delegate. Use if an object is to be returned after execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<T> ExecuteCommandAsync<T>(string connStr, Func<SqlConnection, SqlTransaction, Task<T>> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                SqlTransaction _sqlTransaction = null;
                try
                {
                    conn.Open();
                    _sqlTransaction = conn.BeginTransaction();
                    var returnData = await task(conn, _sqlTransaction);
                    _sqlTransaction.Commit();
                    return returnData;
                }
                catch (SqlException ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Execute an SQL command using the parameters passed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="query"></param>
        /// <param name="param"></param>
        public void ExecuteCommand<T>(string connStr, string query, object param)
        {
            using (var conn = new SqlConnection(connStr))
            {
                SqlTransaction _sqlTransaction = null;
                try
                {
                    conn.Open();
                    _sqlTransaction = conn.BeginTransaction();
                    conn.Execute(query, param, transaction: _sqlTransaction);
                    _sqlTransaction.Commit();
                }
                catch (SqlException ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Asynchronously execute an SQL command using the parameters passed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="query"></param>
        /// <param name="param"></param>
        public async Task ExecuteCommandAsync<T>(string connStr, string query, object param)
        {
            using (var conn = new SqlConnection(connStr))
            {
                SqlTransaction _sqlTransaction = null;
                try
                {
                    conn.Open();
                    _sqlTransaction = conn.BeginTransaction();
                    await conn.ExecuteAsync(query, param, transaction: _sqlTransaction);
                    _sqlTransaction.Commit();
                }
                catch (SqlException ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
                catch (Exception ex)
                {
                    if (_sqlTransaction?.Connection != null)
                    {
                        _sqlTransaction.Rollback();
                    }
                    throw ex;
                }
            }
        }



        public IEnumerable<T> ExecuteReader<T>(string connStr, Func<SqlConnection, SqlTransaction, IEnumerable<T>> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                SqlTransaction _sqlTransaction = null;
                try
                {
                    conn.Open();
                    IEnumerable<T> responseObject = task(conn, _sqlTransaction);
                    return responseObject;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Asynchronously execute the SQL query passed to the function delegate.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="task"></param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connStr, Func<SqlConnection, SqlTransaction, Task<IEnumerable<T>>> task)
        {
            using (var conn = new SqlConnection(connStr))
            {
                SqlTransaction _sqlTransaction = null;
                try
                {
                    conn.Open();
                    IEnumerable<T> responseObject = await task(conn, _sqlTransaction);
                    return responseObject;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Execute an SQL query using the parameters passed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="query"></param>
        /// <param name="param"></param>
        public IEnumerable<T> ExecuteReader<T>(string connStr, string query, object param)
        {
            using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    IEnumerable<T> responseObject = conn.Query<T>(query, param, commandTimeout: _configuration.GetValue<int>("AppSettings:DatabaseReadTimeout"));
                    return responseObject;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Asynchronously execute an SQL query using the parameters passed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="query"></param>
        /// <param name="param"></param>
        public async Task<IEnumerable<T>> ExecuteReaderAsync<T>(string connStr, string query, object param)
        {
            using (var conn = new SqlConnection(connStr))
            {
                try
                {
                    conn.Open();
                    IEnumerable<T> responseObject = await conn.QueryAsync<T>(query, param, commandTimeout: _configuration.GetValue<int>("AppSettings:DatabaseReadTimeout"));
                    return responseObject;
                }
                catch (SqlException ex)
                {
                    throw ex;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Asynchronously execute an SQL command using the parameters passed. Also takes an sql connection to use
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connStr"></param>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        public async Task ExecuteCommandAsync<T>(string connStr, string query, object param, SqlTransaction sqlTransaction)
        {
            SqlTransaction _sqlTransaction = sqlTransaction;
            if (_sqlTransaction == null) throw new SqlTransactionNotInitializedException(Core.Constants.ResponseMessages.SQlTransactionNotInitialized);

            try
            {
                await _sqlTransaction.Connection.ExecuteAsync(query, param, transaction: _sqlTransaction);
            }
            catch (SqlException ex)
            {
                if (_sqlTransaction?.Connection != null)
                {
                    _sqlTransaction.Rollback();
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (_sqlTransaction?.Connection != null)
                {
                    _sqlTransaction.Rollback();
                }
                throw ex;
            }
        }



        /// <summary>
        /// Execute an SQL command using the parameters passed. Also takes an sql transaction to use
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        public void ExecuteCommand<T>(string query, object param, SqlTransaction sqlTransaction)
        {
            if (sqlTransaction == null) throw new SqlTransactionNotInitializedException(Core.Constants.ResponseMessages.SQlTransactionNotInitialized);
            try
            {
                sqlTransaction.Connection.Execute(query, param, transaction: sqlTransaction);
            }
            catch (SqlException ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw ex;
            }
        }

        /// <summary>
        /// Asynchronously execute an SQL command using the parameters passed. Also takes an sql transaction to use
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="param"></param>
        /// <param name="sqlTransaction"></param>
        /// <returns></returns>
        public async Task ExecuteCommandAsync<T>(string query, object param, SqlTransaction sqlTransaction)
        {
            if (sqlTransaction == null) throw new SqlTransactionNotInitializedException(Core.Constants.ResponseMessages.SQlTransactionNotInitialized);
            try
            {
                await sqlTransaction.Connection.ExecuteAsync(query, param, transaction: sqlTransaction);
            }
            catch (SqlException ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw ex;
            }
            catch (Exception ex)
            {
                if (sqlTransaction != null)
                {
                    sqlTransaction.Rollback();
                }
                throw ex;
            }
        }
    }
}