using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MyPizza.ApplicationCore.Attributes.Filter
{
    public sealed class AppExceptionFilter : IExceptionFilter
    {
        private readonly ILogger<AppExceptionFilter> _logger;
        public AppExceptionFilter(ILogger<AppExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var result = new ViewResult { ViewName = "Error" };
            _logger.LogError(context.Exception.StackTrace);
            context.Result = result;
        }
    }
}
