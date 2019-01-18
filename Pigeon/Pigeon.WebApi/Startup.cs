using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NLog.Extensions.Logging;
using Pigeon.DataAccess.Entities;
using Pigeon.DataAccess.Repository;
using Pigeon.DataAccess.Repository.Implementation;
using Pigeon.Framework;
using Pigeon.WebApi.Models;
using Pigeon.WebApi.Service;
using Pigeon.WebApi.WebApi;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Pigeon.WebApi
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
            var connectionString = this.Configuration.GetConnectionString("DefaultConnection");


            services.AddDbContext<PigeonDbContext>(x =>
            {
                x.UseSqlServer(connectionString);
                x.UseLoggerFactory(_loggerFactory);
            });

            services.AddDbContext<SecurityDbContext>(x =>
            {
                x.UseSqlServer(connectionString);
                x.UseLoggerFactory(_loggerFactory);
            });


            services.AddIdentity<PigeonUser, IdentityRole>(x =>
            {
                x.Password.RequiredLength = 5;
                x.Password.RequireNonAlphanumeric = false;
                x.Password.RequireDigit = false;
            }).AddEntityFrameworkStores<SecurityDbContext>().AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                      .AddJwtBearer(options =>
                      {
                          options.RequireHttpsMetadata = false;
                          options.SaveToken = true;

                          options.TokenValidationParameters =
                               new TokenValidationParameters
                               {
                                   ValidIssuer = Configuration["Tokens:Issuer"],
                                   ValidAudience = Configuration["Tokens:Audience"],
                                   ValidateIssuerSigningKey = true,
                                   ValidateLifetime = true,
                                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"]))
                               };
                      })
                      .AddCookie(options =>
                      {
                          options.Events = new CookieAuthenticationEvents
                          {
                              OnRedirectToLogin = OnRedirectToLogin,
                              OnRedirectToAccessDenied = OnRedirectToAccessDenied
                          };
                      }); ;




            services.AddSingleton(Configuration);
            services.AddScoped<IChannelRepository, ChannelRepository>();
            services.AddScoped<IInstituteRepository, InstituteRepository>();
            services.AddScoped<INoticeRepository, NoticeRepository>();
            services.AddScoped<IChannelSubscribeRepository, ChannelSubscribeRepository>();
            services.AddScoped<INoticeVotingRepository, NoticeVotingRepository>();
            services.AddScoped<IUserInstituteRepository, UserInstituteRepository>();
            services.AddSingleton<IEmailClient, EmailClient>();

            services.AddScoped<IAccessControlService, AccessControlService>();
            services.AddScoped<IFileStorageService, AzureStorageService>();

            services.AddScoped<IInstituteCategoryRepository, InstituteCategoryRepository>();
            services.AddScoped<IInstituteTypeRepository, InstituteTypeRepository>();
            services.AddScoped<IChannelCategoryRepository, ChannelCategoryRepository>();
            services.AddScoped<IInstituteSubscribeRepository, InstituteSubscribeRepository>();


            // authorization
            services.AddAuthorization(options =>
            {
                // require user to have cookie auth or jwt bearer token
                options.AddPolicy("Authenticated",
                    policy => policy
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser());
            });

            // Add global authorization policy
            services.Configure<MvcOptions>(options =>
            {
                options.Filters.Add(new AuthorizeFilter("Authenticated"));
            });

            // Add framework services.
            services.AddCors();
            services.AddMvc();
        }


        private static Task OnRedirectToAccessDenied(RedirectContext<CookieAuthenticationOptions> ctx)
        {
            if (ctx.Request.Path.StartsWithSegments("/api") && ctx.Response.StatusCode == 200)
            {
                ctx.Response.StatusCode = 403;
            }

            return Task.CompletedTask;
        }

        private static Task OnRedirectToLogin(RedirectContext<CookieAuthenticationOptions> context)
        {
            if (context.Request.Path.StartsWithSegments("/api"))
            {
                // return 401 if not "logged in" from an API Call
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Task.CompletedTask;
            }

            // Redirect users to login page
            context.Response.Redirect(context.RedirectUri);
            return Task.CompletedTask;
        }

        private static ILoggerFactory _loggerFactory;
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            _loggerFactory = loggerFactory;
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en-US", false)
            //    {
            //        DateTimeFormat =
            //        {
            //            DateSeparator = "/",
            //            ShortDatePattern = "dd/MM/yyyy",
            //            LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
            //        }
            //    }
            //};
            //app.UseRequestLocalization(new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture("es-US"),
            //    SupportedCultures = supportedCultures,
            //    SupportedUICultures = supportedCultures
            //});


            var cInf = new CultureInfo("en-US", false)
            {
                DateTimeFormat =
                        {
                            DateSeparator = "/",
                            ShortDatePattern = "dd/MM/yyyy",
                            LongDatePattern = "dd/MM/yyyy hh:mm:ss tt"
                        }
            };

            System.Threading.Thread.CurrentThread.CurrentCulture = cInf;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cInf;


            // global cors policy
            app.UseCors(x => x
                .WithOrigins("http://localhost").AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
