using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using sharepointecs.Services;
using Microsoft.SharePoint.Client;
using Microsoft.Extensions.Configuration;
using static System.Formats.Asn1.AsnWriter;
using System.Collections;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;
using Serilog;
using sharepointecs.DbContexts;

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

            Log.Logger = new LoggerConfiguration()
                 .WriteTo.Console()
                 .CreateLogger();

            using IHost host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var serverVersion = new MySqlServerVersion(new Version(5, 6));
                    services.AddDbContext<ControlDBContext>(dbContextOptions => dbContextOptions.
                    UseMySql("server=cityserver.mysql.uhserver.com;user=citymaster;password=City@key90;database=cityserver", serverVersion));

                    services.AddScoped<IDBRepository, DBRepository>();
                    services.AddTransient<ISharepointServices, SharepointServices>();
                    services.AddTransient<IFileGenerator, FileGenerator>();
                    services.AddTransient<ILogApplication, LogApplication>();
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
