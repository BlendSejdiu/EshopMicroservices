using BuildingBlocks.Exceptions.Handler;

namespace OrderingAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddAPIServices(this IServiceCollection services)
        {
            services.AddCarter();
            services.AddExceptionHandler<CustomExceptionHandler>();
            return services;
        }

        public static WebApplication UseAPIServices(this WebApplication app)
        { 
            app.MapCarter();

            app.UseExceptionHandler(options => { });

            return app;
        }
    }
}
