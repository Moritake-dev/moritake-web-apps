using AutoMapper;
using MGAuthentication.Data;
using MGAuthentication.Data.User;
using MGAuthentication.Respositories.Common.DepartmentRepositories;
using MGAuthentication.Respositories.Common.InformationBoardRepositories;
using MGAuthentication.Respositories.Common.JobPositionRepositories;
using MGAuthentication.Respositories.CurrentLocationRepositories;
using MGAuthentication.Respositories.UserRepositories;
using MGAuthentication.Services;
using MGAuthentication.Services.CommonServices;
using MGAuthentication.Services.CommonServices.CurrentUserServices;
using MGAuthentication.Services.CommonServices.DepartmentServices;
using MGAuthentication.Services.CommonServices.InformationBoardServices;
using MGAuthentication.Services.CommonServices.JobPositionServices;
using MGAuthentication.Services.LocationServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Logging;
using System;
using System.Linq;
using Newtonsoft.Json.Serialization;

namespace MGAuthentication
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private void CheckSameSite(HttpContext httpContext, CookieOptions options)
        {
            if (options.SameSite == SameSiteMode.None)
            {
                var userAgent = httpContext.Request.Headers["User-Agent"].ToString();
                // TODO: Use your User Agent library of choice here.
                // For .NET Core < 3.1 set SameSite = (SameSiteMode)(-1)
                options.SameSite = SameSiteMode.Unspecified;
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {
            IdentityModelEventSource.ShowPII = true; //Add this line

            //services.ConfigureNonBreakingSameSiteCookies();

            // Adding db to the application
            services.AddDbContext<ApplicationDbContext>(config =>
            {
                config.UseSqlServer(Configuration.GetConnectionString("MGAuthenticationConnection"), options => options.EnableRetryOnFailure());
            });

            // for accessing the HttpContext outside of controller
            services.AddHttpContextAccessor();

            // adding CORS support to the application
            // we are allowing all for this application from our specific client
            services.AddCors(options =>
            {
                options.AddPolicy("AllowMyOrigins",
                    builder =>
                    {
                        builder.AllowCredentials()
                        .AllowAnyHeader()
                        .SetIsOriginAllowedToAllowWildcardSubdomains()
                        .AllowAnyMethod()
                        // we are basically allowing our angular application in development
                        .WithOrigins("http://localhost:4200");
                    });
            });

            // adding Identity services to the application. this is responsible for the tables in the database ex.- ASPUsers
            services.AddIdentity<ApplicationUser, IdentityRole>(config =>
            {
                config.Password.RequiredLength = 6;
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // FOR LOGIN ISSUE
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Unspecified;
                options.OnAppendCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
                options.OnDeleteCookie = cookieContext =>
                    CheckSameSite(cookieContext.Context, cookieContext.CookieOptions);
            });

            // configuring the cookie of the identityserver. we will also configure the connection point of the application
            services.ConfigureApplicationCookie(config =>
            {
                // Fixes for Login Issue
                // REFERENCE : https://stackoverflow.com/questions/60757016/identity-server-4-post-login-redirect-not-working-in-chrome-only
                // YET TO TRY : https://www.thinktecture.com/en/identity/samesite/prepare-your-identityserver/
                //
                config.Cookie.IsEssential = true;
                //config.Cookie.SameSite = SameSiteMode.Unspecified;
                //config.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;

                config.Cookie.Name = "MG.Auth.Cookie";
                config.LoginPath = "/Auth/login";
                config.LogoutPath = "/Auth/logout";
                config.AccessDeniedPath = "/Auth/AccessDenied";
                config.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
            });

            // configure identityserver
            services.AddIdentityServer()
                // .AddApiAuthorization<ApplicationUser, ApplicationDbContext>()
                .AddAspNetIdentity<ApplicationUser>()
                .AddInMemoryApiScopes(Config.GetApiScopes())
                .AddProfileService<IdentityWithAdditionalClaimsProfileService>()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients())
                .AddDeveloperSigningCredential();

            // Adds an authentication handler for an API that co-exists with Authorization Server (Identity Server)
            services.AddAuthentication()
                .AddIdentityServerJwt();

            // Enables the api of the identity server itself for the outer world.
            services.AddLocalApiAuthentication();
            // adding authorization to the application

            services.AddAuthorization(options =>
            {
                // Policy for ROLE: Admin
                options.AddPolicy("RequireAdmin",
                    policy => policy.RequireRole("Admin"));

                // Policy for ROLE: Manager && FEATURE: location
                options.AddPolicy("location_manager", policy => policy.RequireRole("Manager").RequireClaim("feature_access", "location"));
            });

            // Adding Swagger and automapper to the application
            services.AddSwaggerGen();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            // ~~~~~~~~~~~ Adding Application Specific Services ~~~~~~~~~~~~~~
            services.AddTransient<ICurrentUserService, CurrentUserService>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IDepartmentRepository, DepartmentRespository>();

            services.AddScoped<IJobPositionService, JobPositionService>();
            services.AddScoped<IJobPositionRepository, JobPositionRepository>();

            services.AddScoped<ICurrentLocationRepository, CurrentLocationRepository>();
            services.AddScoped<ILocationService, LocationService>();

            services.AddScoped<IInformationBoardService, InformationBoardService>();
            services.AddScoped<IInformationBoardRepository, InformationBoardRepository>();

            // ~~~~~~~~~~~ Adding Application Specific Services ~~~~~~~~~~~~~~

            // Adding runtime compilation by the application. let you customize and visualize views in runtime
            services.AddRazorPages()
                .AddRazorRuntimeCompilation();

            services.AddControllers(setupAction =>
                setupAction.ReturnHttpNotAcceptable = true
                ).AddXmlDataContractSerializerFormatters().AddNewtonsoftJson(setupAction =>
                setupAction.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver());
            // Setting up newtonsoft Json
            services.AddControllersWithViews(options =>
            {
                options.InputFormatters.Insert(0, GetJsonPatchInputFormatter());
            });
        }

        private static NewtonsoftJsonPatchInputFormatter GetJsonPatchInputFormatter()
        {
            // JSON Patch support in aspnet core 3.0 still has JSON.Net dependency - https://docs.microsoft.com/en-us/aspnet/core/web-api/jsonpatch?view=aspnetcore-3.0
            var jsonPatchServices = new ServiceCollection();
            jsonPatchServices.AddLogging().AddMvc().AddNewtonsoftJson();
            var jsonPatchServiceProvider = jsonPatchServices.BuildServiceProvider();
            var jsonPatchOptions = jsonPatchServiceProvider.GetRequiredService<IOptions<MvcOptions>>().Value;
            return jsonPatchOptions.InputFormatters.OfType<NewtonsoftJsonPatchInputFormatter>().First();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext context,
            UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCookiePolicy();

            // ***************************************************************************************

            app.UseCors("AllowMyOrigins");

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSwagger();
            app.UseSwaggerUI(config =>
            {
                config.SwaggerEndpoint("/swagger/v1/swagger.json", "Moritake-Gumi Authentication");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();

            // We are seeding the database here
            ApplicationDbInitializer.SeedData(context, userManager, roleManager);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}