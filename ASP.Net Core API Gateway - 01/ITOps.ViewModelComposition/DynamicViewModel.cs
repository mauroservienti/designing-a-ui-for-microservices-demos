using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace ITOps.ViewModelComposition.Engine
{
    class DynamicViewModel : DynamicObject, IViewModel
    {
        RouteData routeData;
        IQueryCollection query;
        IDictionary<string, object> properties = new Dictionary<string, object>();

        public DynamicViewModel(HttpContext context)
        {
            this.routeData = context.GetRouteData();
            this.query = context.Request.Query;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result) => properties.TryGetValue(binder.Name, out result);

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            properties[binder.Name] = value;
            return true;
        }

        public override IEnumerable<string> GetDynamicMemberNames()
        {
            foreach (var item in properties.Keys)
            {
                yield return item;
            }
        }
    }
}
