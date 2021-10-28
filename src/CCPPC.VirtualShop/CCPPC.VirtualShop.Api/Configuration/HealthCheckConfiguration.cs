using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace CCPPC.VirtualShop.Api.Configuration
{
    public static class HealthCheckConfiguration
    {
        private const string UrlCheck = "/health-check";

        public static void AddHealthCheckConfiguration(this IServiceCollection services, IConfiguration configuration) 
        {


            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("SqlServer"), name: "VirtualStore")
                .AddMongoDb(mongodbConnectionString: configuration.GetConnectionString("MongoDb"),
                    name: configuration.GetValue<string>("MongoDbSettings:DatabaseName"),
                    failureStatus: HealthStatus.Unhealthy);
        }

        public static void UseHealthCheckConfiguration(this IApplicationBuilder app)
        {
            app.UseHealthChecks(UrlCheck, new HealthCheckOptions()
            {
                ResponseWriter = async (context, report) =>
                {
                    var result = JsonConvert.SerializeObject(
                        new
                        {
                            statusApplication = report.Status.ToString(),
                            healthChecks = report.Entries.Select(entry => new
                            {
                                check = entry.Key,
                                status = Enum.GetName(typeof(HealthStatus), entry.Value.Status)
                            })
                        });
                }
            });
        }
    }
}
