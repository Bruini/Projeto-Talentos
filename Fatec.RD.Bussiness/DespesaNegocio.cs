using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using System;
using System.Collections.Generic;

namespace Fatec.RD.Bussiness
{
    public class DespesaNegocio
    {
        DespesaRepositorio _despesaRepositorio;

        public DespesaNegocio()
        {
            _despesaRepositorio = new DespesaRepositorio();
        }

        /// <summary>
        /// Método que adiciona uma despesa
        /// </summary>
        /// <param name="obj">Objeto de Despesa</param>
        /// <returns>Uma nova despesa</returns>
        public Despesa Adicionar(DespesaInput input)
        {
            var novoObj = new Despesa()
            {
                IdTipoDespesa = input.IdTipoDespesa,
                IdTipoPagamento = input.IdTipoPagamento,
                Data = input.Data,
                Valor = input.Valor,
                Comentario = input.Comentario,
                DataCriacao = DateTime.Now
            };

            novoObj.Validar();
            var retorno = _despesaRepositorio.Inserir(novoObj);
            return _despesaRepositorio.SelecionarPorId(retorno);
        }

        /// <summary>
        /// Método que altera uma despesa...
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <param name="input">Objeto de input de despesa</param>
        /// <returns>Objeto de tipo de pagamento</returns>
        public Despesa Alterar(int id, DespesaInput input)
        {
            var obj = this.SelecionarPorId(id);


            obj.IdTipoDespesa = input.IdTipoDespesa;
            obj.IdTipoPagamento = input.IdTipoPagamento;
            obj.Data = input.Data;
            obj.Valor = input.Valor;
            obj.Comentario = input.Comentario;
            obj.Validar();

            _despesaRepositorio.Alterar(obj);

            return obj;
        }

        /// <summary>
        /// Método que seleciona uma despesa
        /// </summary>
        /// <param name="obj">Id de Despesa</param>
        /// <returns>Uma Despesa</returns>
        public Despesa SelecionarPorId(int id)
        {
            var retorno = _despesaRepositorio.SelecionarPorId(id);

            if (retorno.Id <= 0)
                throw new NaoEncontradoException("Despesa não encontrada", id);

            return retorno;
        }

        /// <summary>
        /// Método que deleta uma Despesa
        /// </summary>
        /// <param name="id">Id da Despesa</param>
        public void Deletar(int id)
        {
            try
            {
                var obj = this.SelecionarPorId(id);
                _despesaRepositorio.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Aconteceu um erro ao deletar. Talvez esteja deletando ao que ainda esteja realcionado a um registro.");
            }
        }

        /// <summary>
        /// Método que seleciona uma lista com as Despesas
        /// </summary>
        /// <returns>Lista de Despesas</returns>
        public List<Despesa> Selecionar()
        {
            return _despesaRepositorio.Selecionar();
        }

        /// <summary>
        /// Método que soma valores da Despesas
        /// </summary>
        /// <returns>Valor total</returns>

        public decimal SomarDespesa()
        {

            var obj = this.Selecionar();
            decimal somaTotal = 0;

            foreach(var item in obj)
            {
                somaTotal += item.Valor;
            }

            return somaTotal;

        }

        public decimal SomarDespesaRelatorio(int id)
        {
            RelatorioNegocio relatorio = new RelatorioNegocio();
            var obj = relatorio.SelecionarDespesasPorRelatorio(id);
            decimal somaTotal = 0;

            foreach (var item in obj)
            {
                somaTotal += item.Valor;
            }

            return somaTotal;
            
        }
    }
}
