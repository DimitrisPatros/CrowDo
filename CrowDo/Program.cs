using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DatabaseContext;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace CrowDo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var a = CreateWebHostBuilder(args).Build();
            //_context.Database.EnsureCreated();
            using (IServiceScope scope = a.Services.CreateScope())
            {
                Init(scope.ServiceProvider);
            }
            a.Run();
        }

        private static void Init(IServiceProvider serviceProvider)
        {
            CrowDoDbContext a = serviceProvider.GetRequiredService<CrowDoDbContext>();
            a.Database.EnsureCreated();            
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
