﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookAPI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using BookAPI.Interface;
using BookAPI.Services;
using System.Reflection;
using NSwag.AspNetCore;
using NJsonSchema;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BookAPI.Entities;
using Microsoft.AspNetCore.Identity;

namespace BookAPI
{
    public class Startup
    {
        //private object appSettingsSection;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddCors();
           

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = "Bokk API",
                    Description="Book application API",
                    TermsOfService="None",
                    Contact = new Swashbuckle.AspNetCore.Swagger.Contact
                    {
                        Name = "Cyber Interns",
                        Email="cyberinters@slack.com",
                        Url="cyberinterns.slack.com"
                    },
                    License = new License
                    {
                        Name = "",
                        Url = "#"
                    }
                });

                c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                {
                    Description = "Standard Authorization header using the Bearer scheme. Example:\"bearer {token}",
                    @In = "header",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> {
                { "Bearer", Enumerable.Empty<string>() },
                });
                //c.OperationFilter<SecurityRequirementsOperationFilter>();
            });



            var appSettings = Configuration.GetSection("AppSettings:Secret").Value;
            var key = Encoding.ASCII.GetBytes(appSettings);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            var connection = Configuration.GetConnectionString("Book");

            services.AddDbContext<BookApiDataContext>(options => options.UseSqlServer(connection));


            // identity configuration
            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddUserManager<UserManager<ApplicationUser>>()
                .AddRoleManager<RoleManager<ApplicationRole>>()
                .AddSignInManager<SignInManager<ApplicationUser>>()
                .AddEntityFrameworkStores<BookApiDataContext>();


            services.AddScoped<IAuthor, AuthorService>();
            services.AddScoped<IUser, UserService>();

            services.AddScoped<IAccount, AccountService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseCors(x => x
                           .AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials());

            app.UseSwagger();
            // Enable the Swagger UI middleware and the Swagger generator
            //app.UseSwaggerUI();
            app.UseSwaggerUi3(typeof(Startup).GetTypeInfo().Assembly, settings =>
            {
                settings.GeneratorSettings.DefaultPropertyNameHandling = PropertyNameHandling.CamelCase;


            });



            app.UseHttpsRedirection();
            
            app.UseMvc();
        }
    }
}
