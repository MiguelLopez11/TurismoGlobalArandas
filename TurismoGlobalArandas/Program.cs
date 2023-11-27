using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;
using TurismoGlobalArandas.Context;
using Microsoft.OpenApi.Models;
using UConnect.Entities;
using Newtonsoft.Json;
using Microsoft.Extensions.FileProviders;

namespace TurismoGlobalArandas
{
    public class Program
    {
        private IConfiguration _configuration;

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
            ConfigurationManager configuration = builder.Configuration;
            
            
            
            //CONECTION DATABASE
            builder.Services.AddDbContext<TurismoGlobalContext>(options =>
            {
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString("DefaultConnection")
                );
            });
            //IDENTITY CONFIGURATION
            builder.Services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<TurismoGlobalContext>()
                .AddDefaultTokenProviders();

            // Adding Authentication
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                // Adding Jwt Bearer
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidAudience = configuration["JWT:ValidAudience"],
                        ValidIssuer = configuration["JWT:ValidIssuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(configuration["JWT:Secret"])
                        )
                    };
                });
            //CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(
                    name: MyAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:8080");
                        policy.WithOrigins("https://turismoglobalarandas.onrender.com");
                        policy.AllowAnyHeader();
                        policy.AllowCredentials();
                        policy.AllowAnyMethod();
                    }
                );
            });
            //SWAGGER User configuration
            builder.Services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(
                    "v1",
                    new OpenApiInfo { Title = "TurismoGlobalArandas_WebServices", Version = "v1" }
                );
                option.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter a valid token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "Bearer"
                    }
                );
                option.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            new string[] { }
                        }
                    }
                );
            });
           
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            var app = builder.Build();
            var env = app.Environment;
            //SERVICE FILES UPLOAD
            if (app.Environment != null)
            {
                app.UseStaticFiles();
                app.UseStaticFiles(new StaticFileOptions
                {
                    FileProvider = new PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath, "Resource")),
                    RequestPath = "/Resource/Files"
                });
            }

            if (app.Environment.IsDevelopment()) { }
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCors(MyAllowSpecificOrigins);

            app.MapControllers();

            app.Run();
        }
    }
}
