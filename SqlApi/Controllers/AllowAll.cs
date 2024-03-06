using Microsoft.AspNetCore.Mvc.Filters;
using System.Web.Http.Filters;

namespace SqlApi.Controllers
{
    public class AllowAll : System.Web.Http.Filters.ActionFilterAttribute
    {
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
