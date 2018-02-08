using Dapper;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.ViewModel;
using Fatec.RD.Infra.Repositorio.Contexto;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Fatec.RD.Infra.Repositorio.Base
{
    public sealed class RelatorioRepositorio
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;

        public RelatorioRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        /// <summary>
        /// Método que seleciona uma lista de relatórios..
        /// </summary>
        /// <returns>Lista de relatórios</returns>
        public List<RelatorioViewModel> Selecionar()
        {
            var sqlCommand = @"SELECT r.Id, tp.Descricao as TipoRelatorio, r.Descricao, r.Comentario, r.DataCriacao
                                    FROM Relatorio r
                                    INNER JOIN TipoRelatorio tp ON r.IdTipoRelatorio = tp.Id";

            return _connection.Query<RelatorioViewModel>(sqlCommand).ToList();
        }

        /// <summary>
        /// Método que seleciona um relatorio pelo Id...
        /// </summary>
        /// <param name="id">Id do relatorio</param>
        /// <returns>Objeto de relatorio</returns>
        public Relatorio SelecionarPorId(int id)
        {
            var sqlCommand = @"SELECT *
                                    FROM Relatorio
                                        WHERE a.id = @id";

            return _connection.Query<Relatorio>(sqlCommand, new { id }).FirstOrDefault();
        }

        /// <summary>
        /// Método que retorna as despesas de um relatorio...
        /// </summary>
        /// <param name="idRelatorio">Id do relatorio</param>
        /// <returns>Uma lista de despesas</returns>
        public List<DespesaViewModel> SelecionarPorRelatorio(int idRelatorio)
        {
            var sqlCommand = @"SELECT d.Id, td.Descricao as TipoDespesa, tp.Descricao as TipoPagamento, d.Data, d.Valor, d.Comentario
                                    FROM Despesa d
                                        INNER JOIN RelatorioDespesa rd ON rd.IdDespesa = d.Id
                                        INNER JOIN Relatorio r ON rd.IdRelatorio = r.Id
                                        INNER JOIN TipoPagamento tp ON d.IdTipoPagamento = tp.Id
                                        INNER JOIN TipoDespesa td ON d.IdTipoDespesa = td.Id
                                    WHERE r.Id = @IdRelatorio";

            return _connection.Query<DespesaViewModel>(sqlCommand, new { IdRelatorio = idRelatorio }).ToList();
        }

        /// <summary>
        /// Método que retr=orna, despesas que não estão atreladas a algum relatorio...
        /// </summary>
        /// <returns>Lista de despesas</returns>
        public List<DespesaViewModel> SelecionarDespesasSemRelatorio()
        {
            var sqlCommand = @"SELECT d.Id, td.Descricao as TipoDespesa, tp.Descricao as TipoPagamento, d.Data, d.Valor, d.Comentario
                                    FROM Despesa d
                                        LEFT JOIN RelatorioDespesa rd ON rd.IdDespesa = d.Id
                                        INNER JOIN TipoPagamento tp ON d.IdTipoPagamento = tp.Id
                                        INNER JOIN TipoDespesa td ON d.IdTipoDespesa = td.Id
                                    WHERE rd.IdRelatorio is null";

            return _connection.Query<DespesaViewModel>(sqlCommand).ToList();
        }
    }
}
