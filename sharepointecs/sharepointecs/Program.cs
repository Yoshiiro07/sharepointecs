using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using sharepointecs.Services;
using Microsoft.SharePoint.Client;
using Microsoft.Extensions.Configuration;
using static System.Formats.Asn1.AsnWriter;
using sharepointecs.DbContexts;
using System.Collections;

namespace sharepointecs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Set up app
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);
            IConfiguration config = builder.Build();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddTransient<IControlDBRepository, ControlDBRepository>();
                    services.AddTransient<IGetPageSharepoint, GetPageSharepoint>();
                    services.AddSingleton<AppRun>();
                }).Build();

            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            services.GetRequiredService<AppRun>().Run();
        }

        static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
                .AddEnvironmentVariables();
        }
    }
}
