using System.IO;
using AutoMapper;
using FightCore.Backend.Configuration;
using FightCore.Backend.Configuration.Mapping;
using FightCore.Backend.Configuration.Seeds;
using FightCore.Configuration;
using FightCore.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

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
                        .AllowAnyOrigin()))
                .AddAuthorization()
                .AddJsonFormatters()
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSwaggerGen(configuration =>
            {
                configuration.SwaggerDoc("v1", new OpenApiInfo {Title = "FightCore API", Version = "v1"});
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "FightCore.Backend.xml");
                configuration.IncludeXmlComments(filePath);
            });

            services.AddAutoMapper(typeof(PostMapperProfile), typeof(GameMapperProfile), typeof(UserMapperProfile));

            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString(ConfigurationVariables.DefaultConnection), 
                        sqlServerOptions =>
                            sqlServerOptions.MigrationsAssembly(ConfigurationVariables.MigrationAssembly)));

            // Add the Bearer authentication scheme as this is used by Identity Server.
            services.AddAuthentication("Bearer")
                .AddJwtBearer("Bearer", options =>
                {
                    options.Authority = Configuration["IdentityServer"];
                    options.RequireHttpsMetadata = false;

                    options.Audience = "fightcore-backend";
                });

            services.AddPatterns(Configuration);

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString(ConfigurationVariables.DefaultConnection));

            using (var context = new ApplicationDbContext(optionsBuilder.Options))
            {
                BackendSeed.ExecuteSeed(context);
            }

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
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseReDoc(configuration=>
                configuration.RoutePrefix = "swagger");
        }
    }
}
