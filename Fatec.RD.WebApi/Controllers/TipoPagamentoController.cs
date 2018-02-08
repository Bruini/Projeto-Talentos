﻿using Fatec.RD.Bussiness;
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
    [RoutePrefix("api/TipoPagamento")]
    public class TipoPagamentoController : ApiController
    {
        TipoPagamentoNegocio _appTipoPagamento;

        /// <summary>
        /// 
        /// </summary>
        public TipoPagamentoController()
        {
            _appTipoPagamento = new TipoPagamentoNegocio();
        }

        /// <summary>
        /// Método que insere um novo tipo de despesa
        /// </summary>
        /// <param name="input">Input de Tipo de Despesa</param>
        /// <remarks>Insere um novo usuário</remarks>
        /// <response code="201">Created</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Created)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(TipoPagamento))]
        [HttpPost]
        public IHttpActionResult Post([FromBody] TipoInput input)
        {
            var obj = _appTipoPagamento.Adicionar(input);
            return Created($"{Request?.RequestUri}/{obj.Id}", obj);
        }

        /// <summary>
        /// Método que altera um tipo de despsa....
        /// </summary>
        /// <param name="id">Id do tipo de despesa</param>
        /// <returns></returns>
        /// <remarks>Deleta um aluno</remarks>
        /// <response code="202">Accepted</response>
        /// <response code="404">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.Accepted)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(TipoPagamento))]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, [FromBody] TipoInput input)
        {
            var obj = _appTipoPagamento.Alterar(id, input);
            return Content(HttpStatusCode.Accepted, obj);
        }


        /// <summary>
        /// Método que exclui um tipo de despesa....
        /// </summary>
        /// <param name="id">Id do tipo de despesa</param>
        /// <returns></returns>
        /// <remarks>Deleta um tipo de despesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="404">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.NotFound, "NotFound")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(TipoPagamento))]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _appTipoPagamento.Deletar(id);
            return Ok();
        }

        /// <summary>
        /// Método que obtem um tipo de despesa....
        /// </summary>
        /// <param name="id">Id do tipo de despesa</param>
        /// <returns></returns>
        /// <remarks>obtem um tipo de despesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [ResponseType(typeof(TipoPagamento))]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_appTipoPagamento.SelecionarPorId(id));
        }

        /// <summary>
        /// Método que obtem uma lista de tipo de despesa....
        /// </summary>
        /// <returns>Lista de Tipo de Despesa</returns>
        /// <remarks>Obtem lista de tipo de depesa</remarks>
        /// <response code="200">Ok</response>
        /// <response code="400">BadRequest</response>
        /// <response code="500">InternalServerError</response>
        [SwaggerResponse(HttpStatusCode.OK)]
        [SwaggerResponse(HttpStatusCode.BadRequest, "BadRequest")]
        [SwaggerResponse(HttpStatusCode.InternalServerError, "InternalServerError")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok(_appTipoPagamento.Selecionar());
        }
    }
}
