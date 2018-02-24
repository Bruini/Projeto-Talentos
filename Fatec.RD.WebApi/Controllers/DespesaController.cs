using Fatec.RD.Bussiness;
using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Swashbuckle.Swagger.Annotations;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;

namespace Fatec.RD.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [RoutePrefix("api/Despesa")]
    public class DespesaController : ApiController
    {

        DespesaNegocio _appDespesa;

        /// <summary>
        /// 
        /// </summary>
        public DespesaController()
        {
            _appDespesa = new DespesaNegocio();
        }

        /// <summary>
        /// Método que insere uma nova despesa
        /// </summary>
        /// <param name="input">Input de Despesa</param>
        /// <remarks>Insere uma nova Despesa</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Despesa))]
        [HttpPost]
        public IHttpActionResult Post([FromBody] DespesaInput input)
        {
            var obj = _appDespesa.Adicionar(input);
            return Created($"{Request?.RequestUri}/{obj.Id}", obj);
        }


        /// <summary>
        /// Método que altera uma despesa
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <returns></returns>
        /// <remarks>Altera uma despesa</remarks>
        /// <response code="202">Accepted</response>
        /// <response code="404">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Accepted)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Despesa))]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] DespesaInput input)
        {
            var obj = _appDespesa.Alterar(id, input);
            return Content(HttpStatusCode.Accepted, obj);
        }

        /// <summary>
        /// Método que obtem uma despesa
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <returns></returns>
        /// <remarks>obtem uma despesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Despesa))]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_appDespesa.SelecionarPorId(id));
        }


        /// <summary>
        /// Método que obtem uma lista de despesas
        /// </summary>
        /// <returns>Lista Despesa</returns>
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
            return Ok(_appDespesa.Selecionar());
        }

        /// <summary>
        /// Método que exclui uma Despesa....
        /// </summary>
        /// <param name="id">Id da Despesa</param>
        /// <returns></returns>
        /// <remarks>Deleta uma Despesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(Despesa))]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _appDespesa.Deletar(id);
            return Ok();
        }


        /// <summary>
        /// Método que soma valor das despesas
        /// </summary>
        /// <returns>Valor total</returns>
        /// <remarks>Valor total das depesas</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [Route("SomaTotal")]
        [HttpGet]
        public IHttpActionResult GetSomaTotal()
        {
            return Ok(_appDespesa.SomarDespesa());
        }

        /// <summary>
        /// Método que soma valor das despesas de um relatorio
        /// </summary>
        /// <returns>Valor total</returns>
        /// <remarks>Valor total das depesas de um relatorio</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [Route("SomaTotal/{id}")]
        [HttpGet]
        public IHttpActionResult GetSomaTotalRelaorio(int id)
        {
            return Ok(_appDespesa.SomarDespesaRelatorio(id));
        }


    }
}
