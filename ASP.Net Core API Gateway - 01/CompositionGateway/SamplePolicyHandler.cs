using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace CompositionGateway
{
    internal class SamplePolicyHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            context.Succeed(context.Requirements.First());

            return Task.CompletedTask;
        }
    }
}