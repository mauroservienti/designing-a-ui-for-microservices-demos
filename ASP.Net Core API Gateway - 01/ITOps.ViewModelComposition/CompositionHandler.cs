using ITOps.ViewModelComposition.Engine;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition.Gateway
{
    public class CompositionHandler
    {
        public static async Task<(dynamic ViewModel, int StatusCode)> HandleGetRequest(HttpContext context)
        {
            var vm = new DynamicViewModel(context);
            var pending = new List<Task>();
            var routeData = context.GetRouteData();
            var interceptors = context.RequestServices.GetServices<IRouteInterceptor>();

            //matching interceptors could be cached by URL
            var matching = interceptors
                .Where(a => a.Matches(context.GetRouteData(), HttpMethods.Get))
                .ToArray();

            foreach (var appender in matching.OfType<IViewModelAppender>())
            {
                pending.Add
                (
                    appender.Append(vm, routeData, context.Request.Query)
                );
            }

            if (pending.Count == 0)
            {
                return (null, StatusCodes.Status404NotFound);
            }
            else
            {
                await Task.WhenAll(pending);

                //result transformer? e.g. to change from vm.OrdersViewModel to orders[]

                return (vm, StatusCodes.Status200OK);
            }
        }
    }
}
