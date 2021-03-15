using System;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Autopal.BrainBay.RickandMorty.WebApp.Controllers
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class FromDatabaseActionFilter : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            context.HttpContext.Response.Headers.Add("Name", "from-database");
            base.OnActionExecuted(context);
        }
    }
}