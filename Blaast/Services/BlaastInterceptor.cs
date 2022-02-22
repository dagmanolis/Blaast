using Castle.DynamicProxy;

namespace Blaast.Services
{
    internal class BlaastInterceptor : IInterceptor
    {
        private BlaastService _blaastService;
        public BlaastInterceptor( BlaastService blaastService)
        {
            _blaastService = blaastService;
        }

        public void Intercept( IInvocation invocation )
        {
            if ( invocation.Method.Name.StartsWith( "get_" ) )
            {
            }
            invocation.Proceed();
            if ( invocation.Method.Name.StartsWith( "set_" ) )
            {
                _blaastService.HasChangedInvoker();
            }
        }
    }
}
