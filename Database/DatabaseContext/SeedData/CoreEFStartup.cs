using System;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace DatabaseContext.SeedData {
	public class CoreEFStartup {
		public static async Task InitializeDatabaseAsync(IServiceProvider services) {
			var context = services.GetRequiredService<CrowDoDbContext>();
			await context.Database.EnsureCreatedAsync();
			await context.Database.MigrateAsync();
		}
	}
}