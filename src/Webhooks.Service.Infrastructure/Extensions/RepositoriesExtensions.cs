using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Webhooks.Service.DataAccess.Contexts;
using Webhooks.Service.DataAccess.Interfaces;
using Webhooks.Service.DataAccess.Repositories;
using Webhooks.Service.Models.Configurations;

namespace Webhooks.Service.Infrastructure.Extensions
{
    public static class RepositoriesExtensions
    {
        public static void ConfigureRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            const string defaultConnectionParamName = "DefaultConnection";
            const string databaseConfigurationParamName = "DatabaseConfiguration";

            var defaultConnection = configuration.GetConnectionString(defaultConnectionParamName);

            var databaseConfiguration = configuration.GetSection(databaseConfigurationParamName).Get<DatabaseConfiguration>();
            services.Configure<DatabaseConfiguration>(configureOptions => configureOptions = databaseConfiguration);

            services.AddScoped<IMongoClient>(configure => new MongoClient(defaultConnection));
            services.AddScoped(configure => configure.GetService<IMongoClient>()!.GetDatabase(databaseConfiguration.Name));

            services.AddScoped<IWebhooksServiceDbContext, WebhooksServiceDbContext>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}
