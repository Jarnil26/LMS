using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using System.Text.Json;
using LMS.Backend.DTOs;

namespace LMS.Backend.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            var response = new ErrorResponseDto
            {
                Success = false,
                Message = exception.Message,
                Errors = new[] { exception.StackTrace }
            };

            return context.Response.WriteAsJsonAsync(response);
        }
    }
}
