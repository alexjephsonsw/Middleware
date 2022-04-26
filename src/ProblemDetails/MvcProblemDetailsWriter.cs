using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using MvcProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Hellang.Middleware.ProblemDetails
{
    public class MvcProblemDetailsWriter : IProblemDetailsWriter
    {
        private static readonly ActionDescriptor EmptyActionDescriptor = new();
        private static readonly RouteData EmptyRouteData = new();

        private readonly IActionResultExecutor<ObjectResult> _actionExecutor;
        private readonly IOptions<ProblemDetailsOptions> _options;

        public MvcProblemDetailsWriter(IActionResultExecutor<ObjectResult> actionExecutor, IOptions<ProblemDetailsOptions> options)
        {
            _actionExecutor = actionExecutor;
            _options = options;
        }

        public async Task WriteProblemDetailsAsync(HttpContext context, MvcProblemDetails details)
        {
            var routeData = context.GetRouteData() ?? EmptyRouteData;

            var actionContext = new ActionContext(context, routeData, EmptyActionDescriptor);

            var result = new ObjectResult(details)
            {
                StatusCode = details.Status ?? context.Response.StatusCode,
                ContentTypes = _options.Value.ContentTypes.Clone(),
            };

            await _actionExecutor.ExecuteAsync(actionContext, result);

            await context.Response.CompleteAsync();
        }
    }
}
