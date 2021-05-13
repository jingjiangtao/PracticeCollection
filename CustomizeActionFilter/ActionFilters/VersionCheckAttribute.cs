using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomizeActionFilter.ActionFilters
{
    [Serializable, AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class VersionCheckAttribute : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (HasIgnoreVersionCheck(context))
            {
                return;
            }

            if (!context.HttpContext.Request.Headers.ContainsKey("version"))
            {
                context.Result = new BadRequestObjectResult("Error: incorrect version");
                return;
            }

            string headVersionStr = context.HttpContext.Request.Headers["version"].FirstOrDefault();
            var configuration = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            string confVersionStr = configuration.GetValue<string>("VersionFilter");
            if (headVersionStr != confVersionStr)
            {
                context.Result = new BadRequestObjectResult("Error: incorrect version");
                return;
            }
        }

        private bool HasIgnoreVersionCheck(ActionExecutingContext context)
        {
            IList<IFilterMetadata> filters = context.Filters;
            foreach (IFilterMetadata filter in filters)
            {
                if (filter is IgnoreVersionCheckAttribute)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
