
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;
using System.Text;
using TaskManagementAPI.Context;
using TaskManagementAPI.Repositories;
using TaskManagementAPI.Services;

namespace TaskManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost",
                    builder => builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                //.WithOrigins(["",""])
                );
            });

            // Add DbContext configuration USING DbContextOptions
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddControllers();
            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<IUserRepository, userRepository>();
            builder.Services.AddScoped<ITaskRepository, TaskRepository>();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskManagementAPI", Version = "v1" });

                // Add JWT Authentication
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by your JWT token"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] {}
                    }
                });
            });


            //extract the secret string
            var JwtKey = builder.Configuration.GetValue<string>("Jwt:Secret");


            //this sets up the authentication service for your application
            _ = builder.Services.AddAuthentication(x =>
            {
                /*tells the app which scheme to use when authenticating incoming requests.
                Here, it's set to JWT Bearer meaning it will look for a JWT in the request's Authorization header
                */
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

                /*specifies the scheme to use when a user is unauthenticated or authorized.
                it returns a 401 unauthorized  response, and the JWT bearer scheme handles that process
                */
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                /*this is where you configure the specfic behavior of the JWT Bearer authentication handler.*/
                .AddJwtBearer(x =>
                {
                    /*set to true to ensure HTTPS connection(false to avoid certificate issues)*/
                    x.RequireHttpsMetadata = true;
                    /*saves the incoming token in the HttpContext after it has been validated.This allows you to access the token later in your app logic
                      */
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        //ensures that token was signed with the correct secret key.Without this,anyone could forge a token
                        ValidateIssuerSigningKey = true,

                        //this is where you pass the secret key. the code gets the key from your app's config and converts it into a symmetricsecuritykey object. the token's signature will be verified against this key
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtKey)),

                        //the issuer is the entity that created the token,provides the exprected issuer's name to add another layer of security
                        ValidateIssuer = true,

                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        /* intended recipient of the token,settings it to false means the application won't check who the token is for.
                        */
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["Jwt:Audience"],

                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero

                    };
                });


            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("EmployeeOnly", policy => policy.RequireRole("Employee"));
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
