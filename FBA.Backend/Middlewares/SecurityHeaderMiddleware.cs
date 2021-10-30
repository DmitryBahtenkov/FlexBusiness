using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FBA.Backend.Middlewares
{
    public class SecurityHeaderMiddleware
    {
        private readonly RequestDelegate _next;
        
        public SecurityHeaderMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        
        public async Task Invoke(HttpContext context)
        {
            context.Response.Headers.Add("Access-Control-Allow-Origin", new StringValues("*"));
            await _next(context);
        }
    }
}