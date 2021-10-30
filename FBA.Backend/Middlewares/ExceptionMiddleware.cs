using System;
using System.Threading.Tasks;
using FBA.CrossCutting.Contract.Exceptions;
using FBA.CrossCutting.Contract.Logging;
using Microsoft.AspNetCore.Http;

namespace FBA.Backend.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext context, ILogger logger)
        {
            try
            {
                await _next.Invoke(context);
            }
            catch (BusinessException ex)
            {
                context.Response.StatusCode = 400;
                logger.LogError(ex, "Global exception middleware");
                await context.Response.WriteAsJsonAsync(new {Message = ex.Message});
            }
            catch (ValidationException validationException)
            {
                context.Response.StatusCode = 422;
                logger.LogError(validationException, "Global exception middleware");
                await context.Response.WriteAsJsonAsync(validationException.Fields);
            }
            catch(Exception exception)
            {
                context.Response.StatusCode = 500;
                logger.LogError(exception, "Global exception middleware");
                await context.Response.WriteAsJsonAsync(new {Message = $"{exception.Message} {exception.StackTrace}"});
            }
        }
    }
}