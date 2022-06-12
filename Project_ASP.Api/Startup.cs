using AutoMapper;
using FluentValidation.Results;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Project_ASP.Api.Core;
using Project_ASP.Api.Extensions;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.Application.Emails;
using Project_ASP.Application.Exceptions;
using Project_ASP.Application.Logger;
using Project_ASP.DataAccess;
using Project_ASP.Implementation.BusinessLogic;
using Project_ASP.Implementation.Emails;
using Project_ASP.Implementation.Extensions;
using Project_ASP.Implementation.Logger;
using Project_ASP.Implementation.Profiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Project_ASP.Api
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
            var settings = new AppSettings();

            Configuration.Bind(settings);

            services.AddSingleton(settings);
            services.AddTransient<ProjectContext>();
            services.AddAllAppServices(settings);
            services.AddControllers();
            services.AddTransient<ILogger, ConsoleLogger>();
            services.AddTransient<IUseCaseLogger, DbLogger>();
            services.AddTransient<BaseHandler>();
            services.AddTransient<IEmailSender>(x =>
             new SmtpEmailSender(settings.EmailOptions.FromEmail,
                                 settings.EmailOptions.Password,
                                 settings.EmailOptions.Port,
                                 settings.EmailOptions.Host));

            services.AddControllers();
            services.AddHttpContextAccessor();

            // AutoMapper
            services.AddAutoMapper(typeof(EfGetDietsQuery).Assembly);
            services.AddAutoMapper(typeof(EfGetIngredientTypesQuery).Assembly);
            services.AddAutoMapper(typeof(EfGetIngredientsQuery).Assembly);
            services.AddAutoMapper(typeof(EfGetRecipesQuery).Assembly);
            services.AddAutoMapper(typeof(EfGetUsersQuery).Assembly);
            //services.AddAutoMapper(typeof(EfGetLogsQuery).Assembly);


            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project_ASP.Api", Version = "v1" });
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project_ASP.Api v1"));
            }

            app.UseRouting();
            app.ConfigureExceptionMiddleware();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
