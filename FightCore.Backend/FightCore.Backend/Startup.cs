using System.IO;
using AutoMapper;
using FightCore.Backend.Configuration;
using FightCore.Backend.Configuration.Mapping;
using FightCore.Backend.Configuration.Seeds;
using FightCore.Configuration;
using FightCore.Configuration.Models;
using FightCore.Data;
using FightCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using ConfigurationBuilder = Microsoft.Extensions.Configuration.ConfigurationBuilder;
using IdentityConstants = FightCore.Backend.Configuration.IdentityConstants;

namespace FightCore.Backend
{
    /// <summary>
    /// The class executed to start the application.
    /// Configures most of the DI and other services.
    /// </summary>
    public class Startup
    {
        /// <inheritdoc />
        public Startup(IConfiguration configuration)
        {
            FightCore.Configuration.ConfigurationBuilder.Build(Configuration);
            _customConfigurationObject = FightCore.Configuration.ConfigurationBuilder.Configuration;
        }

        /// <summary>
        /// The configuration created from the JSON files.
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        private ConfigurationObject _customConfigurationObject;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The service collection to be built up.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvcCore()
                .AddCors(options => options.AddPolicy("TestPolicy", policyBuilder =>
                    policyBuilder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()))
                .AddJsonFormatters()
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = _customConfigurationObject.Caching.Server;
                options.InstanceName = _customConfigurationObject.Caching.Instance;
            });

            services.AddIdentity<ApplicationUser, IdentityRole<long>>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddSwaggerGen(configuration =>
            {
                configuration.SwaggerDoc("v1", new OpenApiInfo { Title = "FightCore API", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "FightCore.Backend.xml");
                configuration.IncludeXmlComments(filePath);
            });

            services.AddAutoMapper(
                typeof(PostMapperProfile),
                typeof(GameMapperProfile),
                typeof(UserMapperProfile),
                typeof(GlobalMapperProfile),
                typeof(StageMapperProfile));

            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString(ConfigurationVariables.DefaultConnection),
                        sqlServerOptions =>
                            sqlServerOptions.MigrationsAssembly(ConfigurationVariables.MigrationAssembly)));

            // Add the Bearer authentication scheme as this is used by Identity Server.
            services.AddAuthentication(options =>
                {
                    // For some bizarre reason, if this isn't added, it doesn't work on Linux but does work on Windows.
                    // Please don't remove this line or it's another 7 hours of sadness.
                    options.DefaultAuthenticateScheme = IdentityConstants.AuthenticateScheme;
                    options.DefaultChallengeScheme = IdentityConstants.ChallengeScheme;
                })
                .AddJwtBearer(options =>
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
                var userManager = services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
                BackendSeed.ExecuteSeed(context, userManager);
            }
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builders.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("TestPolicy");
            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseExceptionHandler(applicationBuilder => applicationBuilder.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                var result = JsonConvert.SerializeObject(new { message = exception.Message });

                context.Response.ContentType = HttpContentTypes.ApplicationJson;

                await context.Response.WriteAsync(result);
            }));

            app.UseMvc();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseReDoc(configuration =>
                configuration.RoutePrefix = "swagger");
        }
    }
}
