using System.Data.SqlClient;

namespace hotel_management_api_identity.Core.Storage.QueryRepository
{
    public interface IDapperCommand<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddBatchAsync(TEntity entity, List<Dictionary<string, object>> columnValuePairs, SqlTransaction sqlTransaction);
        Task UpdateAsync(TEntity entity);
        Task UpdateBatchAsync(TEntity entity, List<Dictionary<string, object>> columnValuePairs, SqlTransaction sqlTransaction);
    }
    public class DapperCommand<TEntity> : IDapperCommand<TEntity> where TEntity : class
    {
        private readonly IExecuters _executers;
        private readonly IQueryUtilities _utilities;
        public DapperCommand(IExecuters executers, IQueryUtilities utilities)
        {
            _executers = executers;
            _utilities = utilities;
        }
        public async Task AddAsync(TEntity entity)
        {
            await _executers.ExecuteCommandAsync<TEntity>(_utilities.GenerateInsertQuery(entity), _utilities.GetObjectParams(entity));
        }

        public async Task AddBatchAsync(TEntity entity, List<Dictionary<string, object>> columnValuePairs, SqlTransaction sqlTransaction)
        {
            string query = _utilities.GenerateBulkInsertQuery<TEntity>(columnValuePairs);
            await _executers.ExecuteCommandAsync<TEntity>(query, new { }, sqlTransaction);
        }
        
        public async Task UpdateAsync(TEntity entity)
        {
            string query = $"{_utilities.GenerateUpdateQuery(entity)}";
            await _executers.ExecuteCommandAsync<TEntity>(query, _utilities.GetObjectParams(entity));
        }

        public async Task UpdateBatchAsync(TEntity entity, List<Dictionary<string, object>> columnValuePairs, SqlTransaction sqlTransaction)
        {
            string query = _utilities.GenerateBulkUpdateQuery<TEntity>(columnValuePairs);
            await _executers.ExecuteCommandAsync<TEntity>(query, new { }, sqlTransaction);
        }
    }
}