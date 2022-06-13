using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Project_ASP.Api.Core;
using Project_ASP.Application.BusinessLogic;
using Project_ASP.DataAccess;
using Project_ASP.Domain.Interfaces;
using Project_ASP.Implementation.BusinessLogic;
using Project_ASP.Implementation.Validators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Project_ASP.Api.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAllAppServices(this IServiceCollection services, AppSettings settings)
        {
            services.AddAppUser();
            services.AddJwt(settings);
            services.AddAppUseCases();
            services.AddAppFluentValidation();

            return services;
        }

        public static IServiceCollection AddAppFluentValidation(this IServiceCollection services)
        {
            services.AddTransient<CreateDietValidator>();
            services.AddTransient<CreateIngredientTypeValidator>();
            services.AddTransient<CreateIngredientValidator>();
            services.AddTransient<UpdateIngredientValidator>();
            services.AddTransient<CreateRecipeValidator>();
            services.AddTransient<UpdateRecipeValidator>();
            services.AddTransient<RegisterUserValidator>();
            services.AddTransient<UpdateAccountValidator>();
            return services;
        }

        public static IServiceCollection AddAppUseCases(this IServiceCollection services)
        {
            // Users
            services.AddTransient<IRegisterUserCommand, EfRegisterUserCommand>();
            //
            // Diets
            services.AddTransient<ICreateDietCommand, EfCreateDietCommand>();
            services.AddTransient<IGetDietsQuery, EfGetDietsQuery>();
            services.AddTransient<IGetDietQuery, EfGetDietQuery>();
            services.AddTransient<IUpdateDietCommand,EfUpdateDietCommand>();
            services.AddTransient<IDeleteDietCommand,EfDeleteDietCommand>();
            //
            // IngredientTypes
            services.AddTransient<ICreateIngredientTypeCommand, EfCreateIngredientTypeCommand>();
            services.AddTransient<IGetIngredientTypesQuery, EfGetIngredientTypesQuery>();
            services.AddTransient<IGetIngredientTypeQuery, EfGetIngredientTypeQuery>();
            services.AddTransient<IUpdateIngredientTypeCommand, EfUpdateIngredientTypeCommand>();
            services.AddTransient<IDeleteIngredientTypeCommand, EfDeleteIngredientTypeCommand>();
            //

            // Ingredients
            services.AddTransient<ICreateIngredientCommand, EfCreateIngredientCommand>();
            services.AddTransient<IGetIngredientsQuery, EfGetIngredientsQuery>();
            services.AddTransient<IGetIngredientQuery, EfGetIngredientQuery>();
            services.AddTransient<IUpdateIngredientCommand, EfUpdateIngredientCommand>();
            services.AddTransient<IDeleteIngredientCommand, EfDeleteIngredientCommand>();
            //

            // Recipes
            services.AddTransient<ICreateRecipeCommand, EfCreateRecipeCommand>();
            services.AddTransient<IGetRecipesQuery, EfGetRecipesQuery>();
            services.AddTransient<IGetRecipeQuery, EfGetRecipeQuery>();
            services.AddTransient<IUpdateRecipeCommand, EfUpdateRecipeCommand>();
            services.AddTransient<IDeleteRecipeCommand, EfDeleteRecipeCommand>();
            //

            // Admin
            services.AddTransient<IAddNewAdminCommand, EfCreateNewAdminCommand>();
            services.AddTransient<IGetUsersQuery, EfGetUsersQuery>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            //

            // User and Admin
            services.AddTransient<IDeleteAccountCommand, EfDeleteAccountCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            //

            return services;
        }

        public static void AddAppUser(this IServiceCollection services)
        {
            services.AddTransient<IApplicationUser>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();
                var header = accessor.HttpContext.Request.Headers["Authorization"];

                var claims = accessor.HttpContext.User;

                if (claims == null || claims.FindFirst("UserId") == null)
                {
                    return new AnonimousUser();
                }

                var actor = new JwtUser
                {
                    Email = claims.FindFirst("Email").Value,
                    Id = Int32.Parse(claims.FindFirst("UserId").Value),
                    Identity = claims.FindFirst("Email").Value,
                    PermissionIds = JsonConvert.DeserializeObject<List<int>>(claims.FindFirst("Permissions").Value)
                };

                return actor;
            });
        }

        public static void AddJwt(this IServiceCollection services, AppSettings settings)
        {
            services.AddTransient(x =>
            {
                var context = x.GetService<ProjectContext>();
                var settings = x.GetService<AppSettings>();

                return new JwtManager(context, settings.JwtSettings);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = settings.JwtSettings.Issuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.JwtSettings.SecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
