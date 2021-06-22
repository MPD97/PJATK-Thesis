using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Thesis.Application.Common.Models;
using ValidationException = Thesis.Application.Common.Exceptions.ValidationException;

namespace Thesis.Application.Common.Behaviours
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context )
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }


        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            // Log issues and handle exception response

            if (exception.GetType() == typeof(ValidationException))
            {
                var code = HttpStatusCode.BadRequest;
                var result = JsonConvert.SerializeObject(ApiResult<ValidationException>.Error(((ValidationException)exception).Errors));
                return WriteResponse(context, code, result);
            }
            else
            {
                var code = HttpStatusCode.InternalServerError;
                var result = JsonConvert.SerializeObject(ApiResult<ValidationException>.Error(new Dictionary<string, string[]>()));
                return WriteResponse(context, code, result);
            }
        }

        private static Task WriteResponse(HttpContext context, HttpStatusCode code, string result)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
