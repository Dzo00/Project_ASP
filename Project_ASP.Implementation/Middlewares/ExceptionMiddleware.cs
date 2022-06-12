using FluentValidation;
using Microsoft.AspNetCore.Http;
using Project_ASP.Application.Exceptions;
using Project_ASP.Application.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Project_ASP.Implementation.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try {
                await next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex) {
            context.Response.ContentType = "application/json";
            var errorDetails = new ErrorDetails();

            errorDetails.Message = ex.Message;

            switch (ex)
            {
                case UnauthorizedException unauthorizedException:
                    errorDetails.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                case NotFoundException notFoundException:
                    errorDetails.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case FluentValidation.ValidationException validationException:
                    errorDetails.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    errorDetails.Errors = validationException.Errors.Select(x => new { PropertyName = x.PropertyName, ErrorMessage = x.ErrorMessage });
                    break;
                case Application.Exceptions.ValidationException customValidationException:
                    errorDetails.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    errorDetails.Errors = customValidationException.Failures;
                    break;
                default:
                    errorDetails.StatusCode = (int)HttpStatusCode.InternalServerError;
                    errorDetails.Message = "Internal Server Error, please contact your administrator!";
                    break;
            }

            context.Response.StatusCode = errorDetails.StatusCode;
            await context.Response.WriteAsync(errorDetails.ToString());
        }
    }
}
