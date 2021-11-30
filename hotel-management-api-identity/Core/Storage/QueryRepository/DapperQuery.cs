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
    }


    public class DapperQuery<TEntity> : IDapperQuery<TEntity> where TEntity : class
    {
        private readonly IConfiguration _configuration;
        private readonly IExecuters _executers;
        private readonly IQueryUtilities _utilities;
        private readonly string _connStr;
        public DapperQuery(IConfiguration configuration, IExecuters executers, IQueryUtilities utilities)
        {
            _configuration = configuration;
            _executers = executers;
            _connStr = _configuration.GetConnectionString("DbConnectionString");
            _utilities = utilities;
        }

        public async Task<TEntity> GetByDefaultAsync(Dictionary<string, string> criteria)
        {
            string query = _utilities.GenerateSelectQuery<TEntity>(criteria);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(_connStr, query, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8603 // Possible null reference return.
            return entityObject.FirstOrDefault();
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<IQueryable<TEntity>> GetByAsync(Dictionary<string, string> criteria, int pageSize, int pageNumber)
        {
            string query = _utilities.GeneratePaginatedSelectQuery<TEntity>(criteria, pageSize, pageNumber);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(_utilities.GetConnectionString(), query, null);
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
            return entityObject.AsQueryable();
        }

        public async Task<bool> IsExistAsync(Dictionary<string, string> criteria)
        {
            string query = _utilities.GenerateSingleRecordQuery<TEntity>(criteria);

            var entityObject = await _executers.ExecuteReaderAsync<TEntity>(_connStr, query, null);
            return entityObject.Count() > 0;
        }
    }
}