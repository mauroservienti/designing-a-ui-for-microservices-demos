using Newtonsoft.Json;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Threading.Tasks;

namespace JsonUtils
{
    public static class HttpContentExtensions
    {
        public static async Task<ExpandoObject> AsExpando(this HttpContent content)
            => JsonConvert.DeserializeObject<ExpandoObject>(await content.ReadAsStringAsync());
    }
}