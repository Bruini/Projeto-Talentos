using Fatec.RD.Bussiness;
using Fatec.RD.Bussiness.Inputs;
using Swashbuckle.Swagger.Annotations;

using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Fatec.RD.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Relatorio")]
    public class RelatorioController : ApiController
    {
        RelatorioNegocio _appRelatorio;

        /// <summary>
        /// 
        /// </summary>
        public RelatorioController()
        {
            _appRelatorio = new RelatorioNegocio();
        }

        /// <summary>
        /// Método que obtem uma lista de relatorio....
        /// </summary>
        /// <returns>Lista de Despesa</returns>
        /// <remarks>Obtem lista de depesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_appRelatorio.Selecionar());
        }

        /// <summary>
        /// Método que obtem, todas as despesas do relatório...
        /// </summary>
        /// <param name="id">Id do relatório</param>
        /// <returns>Lista de Despesas</returns>
        /// <remarks>Obtem lista de depesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>      
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [Route("{id}/Despesas")]
        [HttpGet]
        public IHttpActionResult GetDespesaRelatorio(int id)
        {
            return Ok(_appRelatorio.SelecionarDespesasPorRelatorio(id));
        }

        /// <summary>
        /// Método que obtem, todas as despesas sem relatório...
        /// </summary>
        /// <returns>Lista de Despesas</returns>
        /// <remarks>Obtem lista de depesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">NotFound</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [Route("Despesas")]
        [HttpGet]
        public IHttpActionResult GetDespesaRelatorio()
        {
            return Ok(_appRelatorio.SelecionarDespesasSemRelatorio());
        }


        /// <summary>
        /// Método que insere a relacao de relatório e despesa
        /// </summary>
        /// <param name="input">Input com lista da relação de relatório e despesa</param>
        /// <remarks>Insere vínculo entre as entidade</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [Route("{id}/Despesas")]
        [HttpPost]
        public HttpResponseMessage PostRelatorioDespesa(int id, RelatorioDespesaInput obj)
        {
            _appRelatorio.InserirRelatorioDespesa(id, obj);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }
    }
}
