using hotel_management_api_identity.Core.Helpers.Extension;

namespace hotel_management_api_identity.Core.Storage.QueryRepository
{
    public interface IDapperQuery<TEntity> where TEntity : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(Dictionary<string, string> criteria);

            

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetByAsync(Dictionary<string, string> criteria, int pageSize, int pageNumber);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<TEntity> GetByDefaultAsync(Dictionary<string, string> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<TEntity> ValidateTokenAsync(Dictionary<string, string> criteria);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<TEntity> GetByDefaultAsync(Dictionary<string, Guid> criteria);

        /// <summary>
        /// Get Multiple Rows
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetByAsync(int pageSize, int pageNumber);


        /// <summary>
        /// Is Exist By DateRimeOffset
        /// </summary>
        /// <param name="criteria"></param>
        /// <param name="roomId"></param>
        /// <returns></returns>
        Task<bool> IsExistAsync(Dictionary<string, DateTimeOffset> criteria, Guid roomId);

        /// <summary>
        /// Get By Date Range
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetByDateRangeAsync(Dictionary<string, DateTimeOffset> criteria);

        /// <summary>
        /// Get By Async Without Pagination
        /// </summary>
        /// <param name="criteria"></param>
        /// <returns></returns>
        Task<IQueryable<TEntity>> GetByAsync(Dictionary<string, string> criteria);
    }


    public class DapperQuery<TEntity> : IDapperQuery<TEntity> where TEntity : class
    {
        private readonly IExecuters _executers;
        private readonly IQueryUtilities _utilities;
        public DapperQuery(IExecuters executers, IQueryUtilities utilities)
        {
            _executers = executers;
            _utilities = utilities;
        }

        public async Task<TEntity> GetByDefaultAsync(Dictionary<string, string> criteria)
        {
            string query = _utilities.GenerateSelectQuery<TEntity>(criteria);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.FirstOrDefault();
        }

        public async Task<TEntity> GetByDefaultAsync(Dictionary<string, Guid> criteria)
        {
            string query = _utilities.GenerateSingleRecordQuery<TEntity>(criteria);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.FirstOrDefault();
        }

        public async Task<IQueryable<TEntity>> GetByAsync(Dictionary<string, string> criteria, int pageSize, int pageNumber)
        {
            string query = _utilities.GeneratePaginatedSelectQuery<TEntity>(criteria, pageSize, pageNumber);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.AsQueryable();
        }

        public async Task<IQueryable<TEntity>> GetByAsync(Dictionary<string, string> criteria)
        {
            string query = _utilities.GeneratePaginatedSelectQuery<TEntity>(criteria);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.AsQueryable();
        }

        public async Task<IQueryable<TEntity>> GetByDateRangeAsync(Dictionary<string, DateTimeOffset> criteria)
        {
            string query = _utilities.GeneratePaginatedSelectQuery<TEntity>(criteria);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.AsQueryable();
        }

        public async Task<bool> IsExistAsync(Dictionary<string, string> criteria)
        {
            string query = _utilities.GenerateSingleRecordQuery<TEntity>(criteria);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.Any();
        }

        public async Task<bool> IsExistAsync(Dictionary<string, DateTimeOffset> criteria, Guid roomId)
        {
            string query = _utilities.GenerateCheckIfRoomIsEmptyQuery<TEntity>(criteria, roomId);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.Any();
        }

        public async Task<IQueryable<TEntity>> GetByAsync(int pageSize, int pageNumber)
        {
            string query = _utilities.GeneratePaginatedSelectQuery<TEntity>(pageSize,pageNumber );
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.AsQueryable();            
        }

        public async Task<TEntity> ValidateTokenAsync(Dictionary<string, string> criteria)
        {
            string query = _utilities.GenerateTokenValidationQuery<TEntity>(criteria);
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(query, null);
            return entityObject.FirstOrDefault();
        }        
    }
}