using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var conectionString = configuration.GetConnectionString("Database");
            ////Add Service to the container
            //services.AddDbContext<ApplicationDbContext>(opt => opt.UserSqlServer(conectionString));
            return services;
        }
    }
}
