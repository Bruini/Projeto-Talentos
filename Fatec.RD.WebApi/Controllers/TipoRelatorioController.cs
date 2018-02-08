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
    [RoutePrefix("api/TipoRelatorio")]
    public class TipoRelatorioController : ApiController
    {
        TipoRelatorioNegocio _appTipoRelatorio;

        /// <summary>
        /// 
        /// </summary>
        public TipoRelatorioController()
        {
            _appTipoRelatorio = new TipoRelatorioNegocio();
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
        [ResponseType(typeof(TipoRelatorio))]
        [HttpPost]
        public IHttpActionResult Post(TipoInput input)
        {
            var obj = _appTipoRelatorio.Adicionar(input);
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
        [ResponseType(typeof(TipoRelatorio))]
        [Route("{id}")]
        [HttpPut]
        public IHttpActionResult Put(int id, TipoInput input)
        {
            var obj = _appTipoRelatorio.Alterar(id, input);
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
        [ResponseType(typeof(TipoRelatorio))]
        [Route("{id}")]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            _appTipoRelatorio.Deletar(id);
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
        [ResponseType(typeof(TipoRelatorio))]
        [Route("{id}")]
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            return Ok(_appTipoRelatorio.SelecionarPorId(id));
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
            return Ok(_appTipoRelatorio.Selecionar());
        }
    }
}
