using Dapper.Data.Mappers.EmployeeMappers;
using Dapper.Data.Repositories;
using Dapper.Data.Repositories.Abstracts;
using Dapper.FluentMap;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace Dapper.Data.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection RegisterRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // 
            FluentMapper.Initialize(config =>
            {
                config.AddMap(new EmployeeMapper());
            });


            services.AddScoped((_) => new SqlConnection(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IDbTransaction>(ctx =>
            {
                var connection = ctx.GetRequiredService<SqlConnection>();
                connection.Open();
                return connection.BeginTransaction();
            });

            // register repo 
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            return services;
        }
    }
}
