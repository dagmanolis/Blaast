using Blaast.Services;
using Castle.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;

namespace Blaast
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBlaastService<T>( this IServiceCollection services ) where T : class
        {
            ProxyGenerator generator = new ProxyGenerator();
            var blaastService = new BlaastService();
            var blastInterceptor = new BlaastInterceptor( blaastService );
            var appProperties = generator.CreateClassProxy<T>( blastInterceptor );

            services.AddSingleton( blaastService );
            services.AddSingleton( appProperties );
        }
    }
}
