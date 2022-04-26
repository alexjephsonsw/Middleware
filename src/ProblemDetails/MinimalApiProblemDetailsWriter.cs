using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;
using MvcProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Hellang.Middleware.ProblemDetails
{
    public class MinimalApiProblemDetailsWriter : IProblemDetailsWriter
    {
        public Task WriteProblemDetailsAsync(HttpContext context, MvcProblemDetails details)
        {
#if NET6_0_OR_GREATER
            var problem = Results.Problem(details);

            return problem.ExecuteAsync(context);
#else
            throw new NotImplementedException();
#endif
        }
    }
}
