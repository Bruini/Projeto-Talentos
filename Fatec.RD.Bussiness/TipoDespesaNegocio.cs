using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using System;
using System.Collections.Generic;

namespace Fatec.RD.Bussiness
{
    public sealed class TipoDespesaNegocio
    {
        TipoDespesaRepositorio _tipoDespesaRepositorio;

        public TipoDespesaNegocio()
        {
            _tipoDespesaRepositorio = new TipoDespesaRepositorio();
        }

        /// <summary>
        /// Método que adiciona um novo tipo de despesa
        /// </summary>
        /// <param name="obj">Objeto de Tipo de Despesa</param>
        /// <returns>Um novo tipo de despesa</returns>
        public TipoDespesa Adicionar(TipoInput obj)
        {
            var condicao = _tipoDespesaRepositorio.Selecionar(obj.Descricao);

            if (condicao != null)
                throw new ConflitoException($"Já existe um tipo de despesa {condicao.Descricao}, cadastrado!");

            var novoObj = new TipoDespesa()
            {
                Descricao = obj.Descricao,
                DataCriacao = DateTime.Now
            };

            novoObj.Validar();

            var retorno = _tipoDespesaRepositorio.Inserir(novoObj);

            return _tipoDespesaRepositorio.SelecionarPorId(retorno);
        }

        /// <summary>
        /// Método que seleciona um tipo de despesa pelo Id....
        /// </summary>
        /// <param name="id">Id do tipo de despesa</param>
        /// <returns>Objeto de Tipo de despesa</returns>
        public TipoDespesa SelecionarPorId(int id)
        {
            var retorno = _tipoDespesaRepositorio.SelecionarPorId(id);

            if (retorno.Id <= 0)
                throw new NaoEncontradoException("Tipo de Despesa não encontrado", id);

            return retorno;
        }

        /// <summary>
        /// Método que altera um tipo de despesa...
        /// </summary>
        /// <param name="id">Id do tipo de depsesa</param>
        /// <param name="input">Objeto de input de tipo de despesa</param>
        /// <returns>Objeto de tipo de despesa</returns>
        public TipoDespesa Alterar(int id, TipoInput input)
        {
            var obj = this.SelecionarPorId(id);

            obj.Descricao = input.Descricao;
            obj.Validar();

            _tipoDespesaRepositorio.Alterar(obj);

            return obj;
        }

        /// <summary>
        /// Método que deleta um tipo de despsa
        /// </summary>
        /// <param name="id">Id do tipo de despesa</param>
        public void Deletar(int id)
        {
            try
            {
                var obj = this.SelecionarPorId(id);
                _tipoDespesaRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Aconteceu um erro ao deletar. Talvez esteja deletando ao que ainda esteja realcionado a um registro.");
            }
        }

        /// <summary>
        /// Método que seleciona uma lista com os tipos de despesa
        /// </summary>
        /// <returns>Lista de tipo de despesa</returns>
        public List<TipoDespesa> Selecionar()
        {
            return _tipoDespesaRepositorio.Selecionar();
        }
    }
}
