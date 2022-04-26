using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Hellang.Middleware.ProblemDetails
{
    public class ProblemDetailsWriterFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ProblemDetailsWriterFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IProblemDetailsWriter Create()
        {
#if NET6_0_OR_GREATER
            var actionResultExecutor = _serviceProvider.GetService<IActionResultExecutor<ObjectResult>>();
            if (actionResultExecutor == null)
            {
                return _serviceProvider.GetRequiredService<MinimalApiProblemDetailsWriter>();
            }
#endif
            return _serviceProvider.GetRequiredService<MvcProblemDetailsWriter>();
        }
    }
}
