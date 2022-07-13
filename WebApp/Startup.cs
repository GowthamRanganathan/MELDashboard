using BusinessLayer.Implementations.Authentication;
using BusinessLayer.Implementations.Login;
using BusinessLayer.Implementations.PowerBI;
using BusinessLayer.Interfaces.Authentication;
using BusinessLayer.Interfaces.Login;
using BusinessLayer.Interfaces.PowerBI;
using BusinessLayer.Models.Auth;
using BusinessLayer.Models.PowerBiEmbed;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Models;
using RepositoryLayer.Implementations;
using RepositoryLayer.Interfaces;
using System;
using System.Text;
using BusinessLayer.Interfaces.Manage;
using BusinessLayer.Implementations.Manage;
using NLog;
using WebApp.Filters;

namespace MEL_Dashboard
{
    public class Startup
    {
        public Startup (IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services)
        {
            services.AddMvc();
            services.AddControllersWithViews();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.Configure<Jwt>(Configuration.GetSection("Jwt"));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
            });
            services.Configure<AzurePowerBi>(Configuration.GetSection("AzurePowerBi"));
            services.Configure<DbConnection>(Configuration.GetSection("DBSettings"));
            services.AddRazorPages();
            services.AddHttpContextAccessor();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
         .AddJwtBearer(options =>
         {
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["Jwt:Key"])),
                 ValidateIssuer = false,
                 ValidateAudience = false,
                 ClockSkew = TimeSpan.Zero,
                 ValidateLifetime = true
             };
         });
            RegisterDependencies(services);
            services.AddCors();
            services.AddHttpContextAccessor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env)
        {
            GlobalDiagnosticsContext.Set("connectionString", Configuration.GetConnectionString("DefaultConnection"));
            if (env.IsDevelopment())
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseSession();

            app.Use(async (context, next) =>
            {
                var JWToken = context.Session.GetString("AccessToken");
                if (!string.IsNullOrEmpty(JWToken))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + JWToken);
                }
                await next();
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Login}/{id?}");
            });
        }

        private void RegisterDependencies (IServiceCollection services)
        {
            services.AddScoped<IGetClient, GetClient>();
            services.AddScoped<IAzureAdService, AzureAdService>();
            services.AddScoped<IEmbedDashboard, EmbedDashboard>();
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IGetUserLoginDetailsRepository, GetUserLoginDetailsRepository>();
            services.AddScoped<ICreateAccessToken, CreateAccessToken>();
            services.AddScoped<ICreateRefreshToken, CreateRefreshToken>();
            services.AddScoped<ICreateClaims, CreateClaims>();
            services.AddScoped<IVerifyToken, VerifyToken>();
            services.AddScoped<IValidateLoginAndCreateToken, ValidateLoginAndCreateToken>();
            services.AddScoped<ISaveRefreshToken, SaveRefreshToken>();
            services.AddScoped<IGetGrantDetailsRepository, GetGrantDetailRepository>();
            services.AddScoped<ILoadDownloadDetailRespository, LoadDownloadDetailRespository>();
            services.AddScoped<IGetGrantService, GetGrantService>();
            services.AddScoped<ILoadDownloadDataService, LoadDownloadDataService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
