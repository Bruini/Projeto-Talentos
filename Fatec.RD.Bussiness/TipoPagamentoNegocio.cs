using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using System;
using System.Collections.Generic;

namespace Fatec.RD.Bussiness
{
    public class TipoPagamentoNegocio
    {
        TipoPagamentoRepositorio _tipoPagamentoRepositorio;

        public TipoPagamentoNegocio()
        {
            _tipoPagamentoRepositorio = new TipoPagamentoRepositorio();
        }

        /// <summary>
        /// Método que adiciona um novo tipo de pagamento
        /// </summary>
        /// <param name="obj">Objeto de Tipo de pagamento</param>
        /// <returns>Um novo tipo de despesa</returns>
        public TipoPagamento Adicionar(TipoInput obj)
        {
            var condicao = _tipoPagamentoRepositorio.Selecionar(obj.Descricao);

            if (condicao != null)
                throw new ConflitoException($"Já existe um tipo de despesa {condicao.Descricao}, cadastrado!");

            var novoObj = new TipoPagamento()
            {
                Descricao = obj.Descricao,
                DataCriacao = DateTime.Now
            };

            novoObj.Validar();

            var retorno = _tipoPagamentoRepositorio.Inserir(novoObj);

            return _tipoPagamentoRepositorio.SelecionarPorId(retorno);
        }

        /// <summary>
        /// Método que seleciona um tipo de pagamento pelo Id....
        /// </summary>
        /// <param name="id">Id do tipo de pagamento</param>
        /// <returns>Objeto de Tipo de pagamento</returns>
        public TipoPagamento SelecionarPorId(int id)
        {
            var retorno = _tipoPagamentoRepositorio.SelecionarPorId(id);

            if (retorno.Id <= 0)
                throw new NaoEncontradoException("Tipo de Pagamento não encontrado", id);

            return retorno;
        }

        /// <summary>
        /// Método que altera um tipo de pagamento...
        /// </summary>
        /// <param name="id">Id do tipo de pagamento</param>
        /// <param name="input">Objeto de input de tipo de pagamento</param>
        /// <returns>Objeto de tipo de pagamento</returns>
        public TipoPagamento Alterar(int id, TipoInput input)
        {
            var obj = this.SelecionarPorId(id);

            obj.Descricao = input.Descricao;
            obj.Validar();

            _tipoPagamentoRepositorio.Alterar(obj);

            return obj;
        }

        /// <summary>
        /// Método que deleta um tipo de pagamento
        /// </summary>
        /// <param name="id">Id do tipo de pagamento</param>
        public void Deletar(int id)
        {
            try
            {
                var obj = this.SelecionarPorId(id);
                _tipoPagamentoRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Aconteceu um erro ao deletar. Talvez esteja deletando ao que ainda esteja realcionado a um registro.");
            }
        }

        /// <summary>
        /// Método que seleciona uma lista com os tipos de pagamento
        /// </summary>
        /// <returns>Lista de tipo de pagamento</returns>
        public List<TipoPagamento> Selecionar()
        {
            return _tipoPagamentoRepositorio.Selecionar();
        }
    }
}
