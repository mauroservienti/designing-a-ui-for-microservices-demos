using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition
{
    public interface IRouteInterceptor
    {
        bool Matches(RouteData routeData, string httpVerb);
    }

    public interface IViewModelAppender : IRouteInterceptor
    {
        Task Append(dynamic vm, RouteData routeData, IQueryCollection query);
    }
}
