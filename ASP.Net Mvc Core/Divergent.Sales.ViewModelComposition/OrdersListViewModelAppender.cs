using Divergent.Sales.ViewModelComposition.Events;
using ServiceComposer.AspNetCore;
using JsonUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Divergent.Sales.ViewModelComposition
{
    public class OrdersListViewModelAppender : IHandleRequests
    {
        public bool Matches(RouteData routeData, string httpVerb, HttpRequest request)
        {
            /*
             * matching is a bit weak in this sample, it's designed 
             * this way to satisfy both the Gateway and the Mvc sample
             */
            var controller = (string)routeData.Values["controller"];

            return HttpMethods.IsGet(httpVerb)
                && controller.ToLowerInvariant() == "orders"
                && !routeData.Values.ContainsKey("id");
        }

        public async Task Handle(string requestId, dynamic vm, RouteData routeData, HttpRequest request)
        {
            var pageIndex = (string)request.Query["pageindex"] ?? "0";
            var pageSize = (string)request.Query["pageSize"] ?? "10";

            var url = $"http://localhost:20295/api/orders?pageSize={pageSize}&pageIndex={pageIndex}";
            var client = new HttpClient();
            var response = await client.GetAsync(url);

            dynamic[] orders = await response.Content.AsExpandoArray();

            var ordersViewModel = MapToDictionary(orders);

            await vm.RaiseEvent(new OrdersLoaded()
            {
                OrdersViewModel = ordersViewModel
            });

            vm.Orders = ordersViewModel.Values.ToArray();
        }

        IDictionary<dynamic, dynamic> MapToDictionary(dynamic[] orders)
        {
            var ordersViewModel = new Dictionary<dynamic, dynamic>();

            foreach (dynamic order in orders)
            {
                dynamic vm = new ExpandoObject();
                vm.OrderNumber = order.Number;
                vm.OrderItemsCount = order.ItemsCount;

                ordersViewModel[order.Id] = vm;
            }

            return ordersViewModel;
        }
    }
}
