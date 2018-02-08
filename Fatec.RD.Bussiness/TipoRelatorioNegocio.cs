using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using System;
using System.Collections.Generic;

namespace Fatec.RD.Bussiness
{
    public sealed class TipoRelatorioNegocio
    {
        TipoRelatorioRepositorio _tipoRelatorioRepositorio;

        public TipoRelatorioNegocio()
        {
            _tipoRelatorioRepositorio = new TipoRelatorioRepositorio();
        }

        /// <summary>
        /// Método que adiciona um novo tipo de relatório
        /// </summary>
        /// <param name="obj">Objeto de Tipo de relatório</param>
        /// <returns>Um novo tipo de relatório</returns>
        public TipoRelatorio Adicionar(TipoInput obj)
        {
            var condicao = _tipoRelatorioRepositorio.Selecionar(obj.Descricao);

            if (condicao != null)
                throw new ConflitoException($"Já existe um tipo de despesa {condicao.Descricao}, cadastrado!");

            var novoObj = new TipoRelatorio()
            {
                Descricao = obj.Descricao,
                DataCriacao = DateTime.Now
            };

            novoObj.Validar();

            var retorno = _tipoRelatorioRepositorio.Inserir(novoObj);

            return _tipoRelatorioRepositorio.SelecionarPorId(retorno);
        }

        /// <summary>
        /// Método que seleciona um tipo de relatório pelo Id....
        /// </summary>
        /// <param name="id">Id do tipo de relatório</param>
        /// <returns>Objeto de Tipo de relatório</returns>
        public TipoRelatorio SelecionarPorId(int id)
        {
            var retorno = _tipoRelatorioRepositorio.SelecionarPorId(id);

            if (retorno.Id <= 0)
                throw new NaoEncontradoException("Tipo de Pagamento não encontrado", id);

            return retorno;
        }

        /// <summary>
        /// Método que altera um tipo de relatório...
        /// </summary>
        /// <param name="id">Id do tipo de relatório</param>
        /// <param name="input">Objeto de input de tipo de relatório</param>
        /// <returns>Objeto de tipo de relatório</returns>
        public TipoRelatorio Alterar(int id, TipoInput input)
        {
            var obj = this.SelecionarPorId(id);

            obj.Descricao = input.Descricao;
            obj.Validar();

            _tipoRelatorioRepositorio.Alterar(obj);

            return obj;
        }

        /// <summary>
        /// Método que deleta um tipo de relatório
        /// </summary>
        /// <param name="id">Id do tipo de relatório</param>
        public void Deletar(int id)
        {
            try
            {
                var obj = this.SelecionarPorId(id);
                _tipoRelatorioRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Aconteceu um erro ao deletar. Talvez esteja deletando ao que ainda esteja realcionado a um registro.");
            }
        }

        /// <summary>
        /// Método que seleciona uma lista com os tipos de relatório
        /// </summary>
        /// <returns>Lista de tipo de relatório</returns>
        public List<TipoRelatorio> Selecionar()
        {
            return _tipoRelatorioRepositorio.Selecionar();
        }
    }
}
