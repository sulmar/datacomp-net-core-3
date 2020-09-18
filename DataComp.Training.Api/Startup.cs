using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using DataComp.Training.Api.AuthenticationHandlers;
using DataComp.Training.Api.AuthorizationHandlers;
using DataComp.Training.Api.Extensions;
using DataComp.Training.Api.Requriments;
using DataComp.Training.Api.Services;
using DataComp.Training.Fakers;
using DataComp.Training.FakeServices;
using DataComp.Training.IServices;
using DataComp.Training.Models;
using DataComp.Training.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Serilog;



// dotnet add package FluentValidation.AspNetCore

namespace DataComp.Training.Api
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
            // dotnet add package MediatR.Extensions.Microsoft.DependencyInjection
            services.AddMediatR(typeof(Startup).Assembly);

            services.AddControllers()
                .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<UserValidator>());

            // services.AddTransient<IValidator<User>, UserValidator>();

            services.AddSingleton<IUserService, FakeUserService>();
            services.AddSingleton<Faker<User>, UserFaker>();

            services.AddSingleton<IProductServiceAsync, FakeProductServiceAsync>();
            services.AddSingleton<Faker<Product>, ProductFaker>();

            services.AddSingleton<IMessageService, EmailMessageService>();

            // services.AddScoped<IUserService, DbUserService>();

            // Rejestracja konfiguracji z u¿yciem interfejsu IOptions
            services.Configure<FakeEntityServiceOptions>(Configuration.GetSection("FakeEntityServiceOptions"));

            // Rejestracja konfiguracji bez u¿ycia interfejsu IOptions
            //var emailMessageServiceOptions = new EmailMessageServiceOptions();
            //Configuration.GetSection("EmailMessageService").Bind(emailMessageServiceOptions);
            //services.AddSingleton(emailMessageServiceOptions);

            // z u¿yciem w³asnej klasy rozszerzaj¹cej
            services.ConfigurePOCO<EmailMessageServiceOptions>(Configuration.GetSection("EmailMessageService"));

            services.AddSingleton<IAuthenticateService, FakeUserService>();

            services.AddSingleton<ITokenService, JwtTokenService>();
            services.AddSingleton<IClaimService, ClaimService>();

            var secretkey = Encoding.ASCII.GetBytes(Configuration["Secret-Key"]);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(cfg =>
                 {
                     cfg.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(secretkey),
                         ValidateIssuer = false,
                         ValidateAudience = false
                     };
                 });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("AtLeast18", policy => policy.Requirements.Add(new MinimumAgeRequirement(18)));
            });

            services.AddSingleton<IAuthorizationHandler, MinimumAgeHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string environment = env.EnvironmentName; // ASPNETCORE_ENVIRONMENT

            if (env.EnvironmentName == "Production")
            {
                Console.WriteLine("Uwaga Produkcja!");
            }

            string smtp = Configuration["EmailMessageService:Smtp"];
            int port = int.Parse(Configuration["EmailMessageService:Port"]);

            string googleMapsSecretKey = Configuration["GoogleMapsApiKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSerilogRequestLogging();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
