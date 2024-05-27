using System.Reflection;


namespace ExchangeHttpWorker.DependencyInjection
{
    public static class MediatRConfigure
    {
        public static object AddAddMediatRService(this IServiceCollection services)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); 
            });
            
            return services;
        }
        
    }
}
