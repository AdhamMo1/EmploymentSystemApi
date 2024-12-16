using EmploymentSystemApplication.DTOs.Authentication;
using EmploymentSystemApplication.Services;
using EmploymentSystemApplication.ServicesContract;
using EmploymentSystemApplication.UnitOfWorkContract;
using EmploymentSystemDomain.Entities;
using EmploymentSystemInfrastructure.Data;
using EmploymentSystemInfrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

namespace EmploymentSystemApi.Extensions;

public static class ServicesExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
    {
        //config dbContext
        services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));
        //config identity
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();
        //config authentication - Jwt BearerToken
        services.Configure<JWT>(config.GetSection("JWT"));
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidIssuer = config["JWT:Issuer"],
                ValidAudience = config["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Key"])),
                ClockSkew = TimeSpan.Zero
            };
        });
        // config swagger
        services.AddSwaggerGen(c =>
        {
            var securitySchema = new OpenApiSecurityScheme
            {
                Description = "JWT Auth Bearer Scheme",
                Name = "Authorisation",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer",
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            };

            c.AddSecurityDefinition("Bearer", securitySchema);

            var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securitySchema, new[] {"Bearer"}
                    }
                };

            c.AddSecurityRequirement(securityRequirement);

        });
        // config AutoMapper
        services.AddAutoMapper(typeof(AuthenticationResponse));
        // config meditor
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AuthenticationResponse).Assembly));
        // config Dependancy injection
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IUnitOfWork,UnitOfWork>();
        services.AddScoped<IVacancyService, VacancyService>();
        services.AddScoped<IApplicantApplicationService,ApplicantApplicationService>();
        return services;
    }
}