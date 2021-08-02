using Application.Helper;
using Application.Implementations;
using Application.Interfaces;
using DataAccessLayer.DatabaseContext;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Secure.Hash.Algorithm.SDK.Controllers;
using Services.Auth;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Application.Extensions
{
    public static class ServiceDependencyInjection
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser, ApplicationUser>();
            services.AddTransient<ITokenization, Tokenization>();
            services.AddTransient<IAccountUserDAO, AccountUserDAO>();
            services.AddSingleton(typeof(SecureData));
            services.AddSingleton(typeof(PasswordHasherHelper));
            services.AddHttpContextAccessor();
            return services;
        }

        public static IServiceCollection AddServiceInjections(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SmartAppDBContext>(options => options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
            services.AddScoped<IDbConnection>((s) => new SqlConnection(config.GetConnectionString("DefaultConnection")));
            services.Configure<JWTSettings>(config.GetSection(nameof(JWTSettings)));
            return services;
        }
        public static IServiceCollection AddServiceAuthentication(this IServiceCollection services, IConfiguration Configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, opt => 
            {
                opt.Cookie.HttpOnly = true;
                opt.SlidingExpiration = true;
                opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                opt.LoginPath = "/Account/SignIn";
                opt.LogoutPath = "/Account/LogOut";
                opt.AccessDeniedPath = opt.LogoutPath;
                opt.ReturnUrlParameter = "returnUrl";
            })
            .AddJwtBearer(opt =>
            {
                opt.Authority = Configuration["Auth0:Domain"];
                opt.Audience = Configuration["Auth0:Audience"];
                opt.SaveToken = true;
                opt.RequireHttpsMetadata = true;
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    ValidIssuer = Configuration["JWTSettings:Issuer"],
                    ValidAudience = Configuration["JWTSettings:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWTSettings:SecretKey"])),
                    ClockSkew = TimeSpan.Zero,
                    AuthenticationType = JwtBearerDefaults.AuthenticationScheme
                };
                opt.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        context.Token = context.Request.Cookies["X-Access-Token"];
                        return Task.CompletedTask;
                    }
                };
            });

            return services;
        }
    }
}
