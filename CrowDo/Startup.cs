﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrowDoServices;
using CrowDoServices.Interfaces;
using CrowDoServices.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CrowDo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CrowDoDbContext>(options =>options
            .UseSqlServer(@"Server=localhost;Database=CrowDoDataBase; Trusted_Connection = True; ConnectRetryCount = 0;"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IProjectServices, ProjectServices>();
            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<ISearchServices, SearchServices>();
            services.AddScoped<ISerializer, ExcelSerializer>();
            services.AddSwaggerDocument();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }


            app.UseSwagger();
            app.UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
