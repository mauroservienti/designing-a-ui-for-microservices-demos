using System.Collections.Generic;

namespace Catalog.ViewModelComposition.Events
{
    public class AvailableProductsLoaded
    {
        public IDictionary<int, dynamic> AvailableProductsViewModel { get; set; }
    }
}
