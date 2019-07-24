using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Bartdebever.Patterns.Services;
using FightCore.Backend.Configuration.Mapping;
using FightCore.Data;
using FightCore.Repositories.Fakes.Posts;
using FightCore.Repositories.Posts;
using FightCore.Services.Posts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FightCore.Backend
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
            services
                .AddMvcCore()
                .AddCors(options => options.AddPolicy("TestPolicy", policyBuilder =>
                    policyBuilder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()
                        .AllowCredentials()))
                .AddAuthorization()
                .AddJsonFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(typeof(PostMapperProfile));

            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"], 
                        sqlServerOptions =>
                            sqlServerOptions.MigrationsAssembly("FightCore.Data")));

            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = "http://localhost:5000";
                    options.RequireHttpsMetadata = false;

                    options.Audience = "fightcore-backend";
                });

            services = ServicesAndRepositoryInjection(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("TestPolicy");

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private IServiceCollection ServicesAndRepositoryInjection(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IPostService, PostService>();
            serviceCollection.AddScoped<DbContext, ApplicationDbContext>();

            var parsingSuccess = bool.TryParse(Configuration["UseMocking"], out var mocking);
            if (parsingSuccess && mocking)
            {
                 // Add the Mocks
                 serviceCollection.AddScoped<IPostRepository, FakePostRepository>();
            }
            else
            {
                // Add the EF Core repositories
                serviceCollection.AddScoped<IPostRepository, PostRepository>();
            }

            return serviceCollection;
        }
    }
}
