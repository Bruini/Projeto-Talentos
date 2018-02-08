using Dapper;
using Fatec.RD.Infra.Repositorio.Contexto;
using System.Data;

namespace Fatec.RD.Infra.Repositorio.Base
{
    public class RelatorioDespesaRepositorio
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;
        public RelatorioDespesaRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        /// <summary>
        /// Método que insere a relação entre Despesa e Relatorio...
        /// </summary>
        /// <param name="idDespesa">IdDespesa</param>
        /// <param name="idRelatorio">IdRelatorio</param>
        public void Inserir(int idDespesa, int idRelatorio)
        {
            _connection.Execute(@"INSERT RelatorioDespesa (IdDespesa, IdRelatorio)
                                   VALUES (@IdDespesa, @IdRelatorio)", new { IdDespesa = idDespesa, IdRelatorio = idRelatorio });
        }
    }
}
