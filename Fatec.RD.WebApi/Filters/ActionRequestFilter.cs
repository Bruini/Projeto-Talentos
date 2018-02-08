using Newtonsoft.Json;
using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Fatec.RD.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionRequestFilter : ActionFilterAttribute
    {
        private string SendJson { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var parametros = actionContext.ActionArguments;
            var itens = string.Empty;

            if (parametros.Count == 1)
                SendJson = JsonConvert.SerializeObject(new { obj = parametros.FirstOrDefault().Value });
            else
                SendJson = null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionExecutedContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Response != null)
            {
                var statusCode = (int)actionExecutedContext.Response.StatusCode;
                var verb = actionExecutedContext.Request.Method.Method;
                var endPoint = actionExecutedContext.Request.RequestUri.AbsoluteUri;
                var json = SendJson;
            }
        }
    }
}