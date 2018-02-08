using Fatec.RD.Bussiness.Inputs;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Base;
using Fatec.RD.SharedKernel.Excecoes;
using System.Collections.Generic;

namespace Fatec.RD.Bussiness
{
    public sealed class RelatorioNegocio
    {
        RelatorioRepositorio _relatorioRepositorio;
        RelatorioDespesaRepositorio _relatorioDespesaRepositorio;

        public RelatorioNegocio()
        {
            _relatorioRepositorio = new RelatorioRepositorio();
            _relatorioDespesaRepositorio = new RelatorioDespesaRepositorio();
        }

        /// <summary>
        /// Método que seleciona uma lista de relatorio...
        /// </summary>
        /// <returns></returns>
        public List<RelatorioViewModel> Selecionar()
        {
            return _relatorioRepositorio.Selecionar();
        }

        /// <summary>
        /// Método que seleciona despesas pelo relatorio...
        /// </summary>
        /// <param name="idRelatorio">Id do relatório</param>
        /// <returns></returns>
        public List<DespesaViewModel> SelecionarDespesasPorRelatorio(int idRelatorio)
        {
            this.SelecionarPorId(idRelatorio);

            return _relatorioRepositorio.SelecionarPorRelatorio(idRelatorio);
        }

        /// <summary>
        /// Método que seleciona despesas sem ser atrelado com o relatorio
        /// </summary>
        /// <returns></returns>
        public List<DespesaViewModel> SelecionarDespesasSemRelatorio()
        {
            return _relatorioRepositorio.SelecionarDespesasSemRelatorio();
        }

        /// <summary>
        /// Método que seleciona um relatorio pelo Id
        /// </summary>
        /// <param name="id">ID do relatório</param>
        /// <returns>Objeto de relatorio</returns>
        public Relatorio SelecionarPorId(int id)
        {
            var retorno = _relatorioRepositorio.SelecionarPorId(id);

            if (retorno.Id <= 0)
                throw new NaoEncontradoException("Relatório não encontrado", id);

            return retorno;
        }

        /// <summary>
        /// Método que insere a relação de Despesa com relatório...
        /// </summary>
        /// <param name="obj">Obj de Input</param>
        public void InserirRelatorioDespesa(int idRelatorio, RelatorioDespesaInput obj)
        {
            foreach(var item in obj.Chave)
            {
                _relatorioDespesaRepositorio.Inserir(item.IdDespesa, idRelatorio);
            }
        }
    }
}
