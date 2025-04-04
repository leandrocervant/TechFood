using Microsoft.AspNetCore.Builder;

namespace TechFood.Application
{
    public static class RequestPipeline
    {
        public static IApplicationBuilder UseApplication(this IApplicationBuilder app)
        {
            app.UseMiddleware<Common.EventualConsistency.Middleware>();

            return app;
        }
    }
}
