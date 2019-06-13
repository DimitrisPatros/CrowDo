using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseContext;
using DatabaseContext.SeedData;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CrowDo {
	public class Program {
		public static async Task Main(string[] args) {
			var host = CreateWebHostBuilder(args).Build();
			
			using (var scope = host.Services.CreateScope()) {
				await EnsureDataStorageIsReady(scope.ServiceProvider);
			}

			host.Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.ConfigureAppConfiguration((hostContext, config) => {
					config.SetBasePath(hostContext.HostingEnvironment.ContentRootPath);
					config.AddEnvironmentVariables();
					config.AddJsonFile("appsettings.json");
					config.AddJsonFile($"appsettings.{hostContext.HostingEnvironment.EnvironmentName}.json", true);
				});

		private static async Task EnsureDataStorageIsReady(IServiceProvider services) {
			await CoreEFStartup.InitializeDatabaseAsync(services);
			await SimpleContentEFStartup.InitializeDatabaseAsync(services);
		}
	}
}