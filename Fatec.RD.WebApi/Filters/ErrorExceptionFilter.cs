using Fatec.RD.SharedKernel.Excecoes;
using Newtonsoft.Json;

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace Fatec.RD.WebApi.Filters
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorExceptionFilter : ExceptionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contexto"></param>
        public override void OnException(HttpActionExecutedContext contexto)
        {
            HttpStatusCode status;
            var exception = contexto.Exception;

            if (exception is NaoEncontradoException)
                status = HttpStatusCode.NotFound;
            else if (exception is ValidacaoCamposException)
                status = HttpStatusCode.BadRequest;
            else if (exception is ConflitoException)
                status = HttpStatusCode.Conflict;
            else
                status = HttpStatusCode.InternalServerError;

            var retorno = JsonConvert.SerializeObject(new { error = contexto.Exception.Message, inner = contexto.Exception.InnerException });

            contexto.Response = new HttpResponseMessage()
            {
                Content = new StringContent(retorno, System.Text.Encoding.UTF8, "application/json"),
                StatusCode = status
            };

            LogRequest(contexto, retorno);
            base.OnException(contexto);
        }

        private void LogRequest(HttpActionExecutedContext contexto, string retorno)
        {
            var parametros = contexto.ActionContext.ActionArguments;
            var json = string.Empty;

            if (parametros.Count == 1)
                json = JsonConvert.SerializeObject(new { obj = parametros.FirstOrDefault().Value });
            else
                json = null;

            var statusCode = (int)contexto.Response.StatusCode;
            var verb = contexto.Request.Method.Method;
            var endPoint = contexto.Request.RequestUri.AbsoluteUri;
        }
    }
}