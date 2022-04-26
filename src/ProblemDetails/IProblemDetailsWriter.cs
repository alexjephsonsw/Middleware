using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using MvcProblemDetails = Microsoft.AspNetCore.Mvc.ProblemDetails;

namespace Hellang.Middleware.ProblemDetails
{
    public interface IProblemDetailsWriter
    {
        Task WriteProblemDetailsAsync(HttpContext context, MvcProblemDetails details);
    }
}
