using System;
using System.Security.Claims;
using Api.Domain;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Api.Domain.Users;

namespace Api
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
            services.AddResponseCompression();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddCors();
            services.AddSingleton(Configuration);
            services.AddTransient<UserService>();
            services.AddMediatR(typeof(Startup));

            // Note in the docker files we are using __ instead of : because unix
            // doesn't support : in env var names so MS has made their Configuration
            // API resolve : too __ in unix environments. See: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration/?view=aspnetcore-3.1#environment-variables
            var connectionString = GetConfiguration("ConnectionStrings:ExchangeItConnectionString");
            var domain = GetConfiguration("Auth0:ExchangeItAuthDomain");
            var apiId = GetConfiguration("Auth0:ExchangeItAuthApiIdentifier");

            Console.WriteLine("Connecting to database: " + connectionString);

            services.AddDbContext<ViceContext>(options =>
            {
                options.UseSqlServer(connectionString);
#if DEBUG
                options.EnableSensitiveDataLogging();
#endif   
                options.EnableSensitiveDataLogging();
            });

            string authority = $"https://{domain}/";
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = authority;
                options.Audience = apiId;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Exchange It API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. 
                                Enter 'Bearer' [space] and then your token in the text input below. 
                                Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ViceContext context)
        {
            app.UseResponseCompression();

            /* app.UseCors(options => options
               .AllowAnyHeader()
               .AllowAnyOrigin()
               .WithMethods("OPTIONS", "GET", "PUT", "POST", "DELETE", "PATCH")); */

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "4473 V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(options => options
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .WithMethods("OPTIONS", "GET", "PUT", "POST", "DELETE", "PATCH"));

            app.UseCors(options =>
                options
                .WithOrigins(
                    "http://localhost:3000",
                    "https://localhost:3000")
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());

            app.UseAuthentication();

            app.UseAuthorization();

#if DEBUG
            DatabaseInitializer.Initialize(context);

            // app.UseExceptionHandler(errorApp =>
            // {
            //     errorApp.Run(async context =>
            //     {
            //         context.Response.StatusCode = 500; // or another Status accordingly to Exception Type
            //         context.Response.ContentType = "application/json";
            //
            //         var error = context.Features.Get<IExceptionHandlerFeature>();
            //         if (error != null)
            //         {
            //             var ex = error.Error;
            //
            //             await context.Response.WriteAsync(new ErrorDto()
            //             {
            //                 Code = StatusCodes.Status500InternalServerError,
            //                 Message = ex.Message // or your custom message
            //                 // other custom data
            //             }.ToString(), Encoding.UTF8);
            //         }
            //     });
            // });
#endif

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private string GetConfiguration(string name)
        {
            var configuration = Configuration[name];

            if (string.IsNullOrEmpty(configuration))
            {
                throw new Exception($"You must specify a valid configuration for {name} either as an environment variable or as an AppSetting.");
            }

#if DEBUG
            Console.WriteLine($"Read configuration \"{name}\" with value: {configuration}");
            // Uncomment to see full exceptions. I couldn't get this to work.
            // https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/wiki/PII
            IdentityModelEventSource.ShowPII = true;
#endif           
            return configuration;
        }
    }

    /*#if DEBUG

        public class ErrorDto
        {
            public int Code {get;set;}
            public string Message { get; set; }

        }
    #endif*/
}
