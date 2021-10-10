using System.IO;
using FightCore.Backend.Configuration;
using FightCore.Backend.Configuration.Mapping;
using FightCore.Backend.Middleware;
using FightCore.Configuration;
using FightCore.Configuration.Models;
using FightCore.Data;
using FightCore.FrameData;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Serilog;
using IdentityConstants = FightCore.Backend.Configuration.IdentityConstants;

namespace FightCore.Backend
{
    /// <summary>
    /// The class executed to start the application.
    /// Configures most of the DI and other services.
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            FightCore.Configuration.ConfigurationBuilder.Build(Configuration);
            _customConfigurationObject = FightCore.Configuration.ConfigurationBuilder.Configuration;
            Log.Information("Launching FightCore API.");
            Log.Information("Caching: {0}.\nItems will be cached and a Redis instance will be made.\n" +
                            "Please be aware of this during testing.", _customConfigurationObject.Caching.Enabled);
            Log.Information("Encryption Algorithm: {0}.", _customConfigurationObject.Encryption.Algorithm);
        }

        /// <summary>
        /// The configuration created from the JSON files.
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        private readonly ConfigurationObject _customConfigurationObject;

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The service collection to be built up.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllersWithViews();
            services
                .AddMvcCore()
                .AddCors(options => options.AddPolicy("TestPolicy", policyBuilder =>
                    policyBuilder.AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowAnyOrigin()))
                .AddNewtonsoftJson()
                .AddApiExplorer()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddDistributedRedisCache(options =>
            {
                options.Configuration = _customConfigurationObject.Caching.Server;
                options.InstanceName = _customConfigurationObject.Caching.Instance;
            });

            services.AddSwaggerGen(configuration =>
            {
                configuration.SwaggerDoc("v1", new OpenApiInfo { Title = "FightCore API", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "FightCore.Backend.xml");
                configuration.IncludeXmlComments(filePath);
            });

            services.AddAutoMapper(
                typeof(PostMapperProfile),
                typeof(GameMapperProfile),
                typeof(EditMapperProfile),
                typeof(UserMapperProfile),
                typeof(GlobalMapperProfile),
                typeof(StageMapperProfile));

            services.AddDbContext<ApplicationDbContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString(ConfigurationVariables.DefaultConnection),
                        sqlServerOptions =>
                            sqlServerOptions.MigrationsAssembly(ConfigurationVariables.MigrationAssembly)));

            services.AddDbContext<FrameDataContext>(
                options =>
                    options.UseSqlServer(Configuration.GetConnectionString("FrameDataContext"),
                        sqlServerOptions =>
                            sqlServerOptions.MigrationsAssembly("FightCore.FrameData")));

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = IdentityConstants.AuthenticateScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/fightcore";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/fightcore",
                        ValidateAudience = true,
                        ValidAudience = "fightcore",
                        ValidateLifetime = true
                    };
                });

            services.AddPatterns(Configuration);

            //var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //optionsBuilder.UseSqlServer(Configuration.GetConnectionString(ConfigurationVariables.DefaultConnection));

            //using (var context = new ApplicationDbContext(optionsBuilder.Options))
            //{
            //	//context.Database.Migrate();
            //	var userManager = services.BuildServiceProvider().GetService<UserManager<ApplicationUser>>();
            //	BackendSeed.ExecuteSeed(context, userManager);
            //}
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application builders.</param>
        /// <param name="env">The hosting environment.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Log.Information("Using developer exception pages.");
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios
                // see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseRouting();
            app.UseCors("TestPolicy");

            app.UseSerilogRequestLogging();

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseUserMiddleware();

            app.UseExceptionHandler(applicationBuilder => applicationBuilder.Run(async context =>
            {
                var feature = context.Features.Get<IExceptionHandlerPathFeature>();
                var exception = feature.Error;

                var result = JsonConvert.SerializeObject(new { message = exception.Message, errorCode = "internalServerError" });
                context.Response.ContentType = HttpContentTypes.ApplicationJson;
                context.Response.StatusCode = 500;

                await context.Response.WriteAsync(result);
            }));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "FightCore API V1");
                c.RoutePrefix = string.Empty;
            });
        }
    }
}
