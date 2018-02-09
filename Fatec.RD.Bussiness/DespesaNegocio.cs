using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using System;
using System.Collections.Generic;




namespace Fatec.RD.Bussiness
{
    public sealed class DespesaNegocio
    {
        DespesaRepositorio _despesaRepositorio;

        public DespesaNegocio()
        {
            _despesaRepositorio = new DespesaRepositorio();
        }

        /// <summary>
        /// Método que adiciona uma nova despesa
        /// </summary>
        /// <param name="obj">Objeto de Despesa</param>
        /// <returns>Uma nova despesa</returns>
        public Despesa Adicionar(DespesaInput obj)
        {

            var novoObj = new Despesa()
            {
                IdTipoDespesa = obj.IdTipoDespesa,
                IdTipoPagamento = obj.IdTipoPagamento,
                Data = obj.Data,
                Valor = obj.Valor,
                Comentario = obj.Comentario,
                DataCriacao = DateTime.Now
            };

            novoObj.Validar();

            var retorno = _despesaRepositorio.Inserir(novoObj);

            return _despesaRepositorio.SelecionarPorId(retorno);
        }

        /// <summary>
        /// Método que seleciona uma despesa pelo Id....
        /// </summary>
        /// <param name="id">Id da despesa</param>
        /// <returns>Objeto de Despesa</returns>
        public Despesa SelecionarPorId(int id)
        {
            var retorno = _despesaRepositorio.SelecionarPorId(id);

            if (retorno.Id <= 0)
                throw new NaoEncontradoException("Despesa não encontrada", id);

            return retorno;
        }

        /// <summary>
        /// Método que altera um tipo de despesa...
        /// </summary>
        /// <param name="id">Id do tipo de depsesa</param>
        /// <param name="input">Objeto de input de tipo de despesa</param>
        /// <returns>Objeto de tipo de despesa</returns>
        public Despesa Alterar(int id, Despesa input)
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
        /// Método que deleta um tipo de despsa
        /// </summary>
        /// <param name="id">Id do tipo de despesa</param>
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
        /// Método que seleciona uma lista de despesas...
        /// </summary>
        /// <returns></returns>
        public List<DespesaViewModel> Selecionar()
        {
            return _despesaRepositorio.Selecionar();
        }
    }
}
