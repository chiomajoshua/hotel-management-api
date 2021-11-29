namespace hotel_management_api_identity.Core.Storage.QueryRepository
{
    public interface IDapperCommand<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task AddRangeAsync(List<TEntity> entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
    }
    public class DapperCommand<TEntity> : IDapperCommand<TEntity> where TEntity : class
    {
        private readonly IConfiguration _configuration;
        private readonly IExecuters _executers;
        private readonly IQueryUtilities _utilities;
        private readonly string _connStr;
        public DapperCommand(IConfiguration configuration, IExecuters executers, IQueryUtilities utilities)
        {
            _configuration = configuration;
            _executers = executers;
            _connStr = _configuration.GetConnectionString("DbConnectionString");            
            _utilities = utilities;
        }
        public Task AddAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task AddRangeAsync(List<TEntity> entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}