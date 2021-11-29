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
        /// Get all Entities asynchronously
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync(int offset, int rows);

        /// <summary>
        /// Find an Entity by ID asynchronously
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(Guid id);


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
        /// <returns></returns>
        Task<int> GetCountAsync();
    }


    public class DapperQuery<TEntity> : IDapperQuery<TEntity> where TEntity : class
    {
        public DapperQuery()
        {

        }

        public Task<TEntity> FindByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TEntity>> GetAllAsync(int offset, int rows)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TEntity>> GetByAsync(Dictionary<string, string> criteria, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsExistAsync(Dictionary<string, string> criteria)
        {
            throw new NotImplementedException();
        }
    }
}