using System;
using System.Security.Cryptography.X509Certificates;
using FightCore.Configuration;
using FightCore.Data;
using FightCore.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace FightCore.Identity
{
    public class Startup
    {
        public IWebHostEnvironment Environment { get; }

        public IConfiguration Configuration { get; }

        public Startup(IWebHostEnvironment environment, IConfiguration configuration)
        {
            Environment = environment;
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
            services
                .AddCors(options => options.AddPolicy("TestPolicy", policyBuilder =>
                        policyBuilder.AllowAnyHeader()
                            .AllowAnyMethod()
                            .AllowAnyOrigin()))
                .AddMvc()
                .SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_3_0);

            services.AddIdentity<ApplicationUser, IdentityRole<long>>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var connectionString = Configuration.GetConnectionString(ConfigurationVariables.DefaultConnection);
            var migrationsAssembly = ConfigurationVariables.MigrationAssembly;

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            var builder = services.AddIdentityServer()
                // Stores the configuration in the database.
                // NOTE: This uses the import ConfigurationDbContext.
                .AddConfigurationStore(options =>
                    options.ConfigureDbContext = efBuilder =>
                        efBuilder.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly("FightCore.Identity"))
                )
                // Stores the users, tokens, etc in the database.
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = b =>
                        b.UseSqlServer(connectionString,
                            sql => sql.MigrationsAssembly(migrationsAssembly));

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                })
                .AddAspNetIdentity<ApplicationUser>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                var certificate = new X509Certificate2("bundle.pfx", "PASSWORD");
                builder.AddSigningCredential(certificate);
            }
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseRouting();


            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            });

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseCors("TestPolicy");

            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseIdentityServer();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

            });
        }
    }
}
