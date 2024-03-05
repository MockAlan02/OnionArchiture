using WebApi.Middlewares;

namespace WebApi.Extensions
{
    public static class AppExtensions
    {
        public static void UseErrorHandleMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}
