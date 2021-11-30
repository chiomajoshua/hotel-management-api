using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace hotel_management_api_identity.Core.Storage
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<HMSDbContext>
    {
        public HMSDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@Directory.GetCurrentDirectory() + "/../hotel-management-api-identity/appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<HMSDbContext>();
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connectionString);
            return new HMSDbContext(builder.Options);
        }
    }
}