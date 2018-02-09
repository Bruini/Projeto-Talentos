using Dapper;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.Repositorio;
using Fatec.RD.Infra.Repositorio.Contexto;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Fatec.RD.Infra.Repositorio.Base
{
    public sealed class DespesaRepositorio : IRepositorioBase<Despesa>
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;

        public DespesaRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        /// <summary>
        /// Método que altera uma Despesa
        /// </summary>
        /// <param name="obj">Objeto Despesa</param>
        public void Alterar(Despesa obj)
        {
            var sqlCommand = @"UPDATE Despesa 
                                      SET IdTipoDespesa = @IdTipoDespesa, IdTipoPagamento = @IdTipoPagamento, Data = @Data, Valor = @Valor, Comentario = @Comentario
                                        WHERE Id = @Id";

            _connection.Execute(sqlCommand, obj);
        }

        /// <summary>
        /// Método que deleta uma Despesa
        /// </summary>
        /// <param name="id">Id da Despesa</param>
        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM Despesa WHERE Id = @Id", new { Id = id });
        }

        /// <summary>
        /// Método que insere uma nova despesa
        /// </summary>
        /// <param name="obj">Objerto Despesa</param>
        /// <returns></returns>
        public int Inserir(Despesa obj)
        {
            return _connection.Query<int>(@"INSERT Despesa (IdTipoDespesa, IdTipoPagamento, Data, Valor, Comentario, DataCriacao)
                                                    VALUES (@IdTipoDespesa, @IdTipoPagamento, @Data, @Valor, @Comentario, @DataCriacao)
                                                        SELECT CAST (SCOPE_IDENTITY() as int)", obj).First();
        }

        /// <summary>
        /// Método que lista todas as Despesas
        /// </summary>
        /// <returns>Uma lista de Despesas</returns>
        public List<Despesa> Selecionar()
        {
            var sqlCommand = @"SELECT D.Id, D.IdTipoDespesa, P.Descricao as [TipoPagamento], D.IdTipoPagamento,T.Descricao as [TipoDespesa] ,D.Data, D.Valor, D.Comentario, D.DataCriacao
                                    FROM Despesa AS D
                                        INNER JOIN TipoDespesa AS T ON D.IdTipoDespesa = T.Id
                                        INNER JOIN TipoPagamento As P ON D.IdTipoPagamento = P.Id";

            return _connection.Query<Despesa>(sqlCommand).ToList();
        }

        /// <summary>
        /// Método que seleciona uma Despesa
        /// </summary>
        /// <param name="id">Id da Despesa</param>
        /// <returns>Uma Despesa</returns>
        public Despesa SelecionarPorId(int id)
        {
            var sqlCommand = @"SELECT D.Id, D.IdTipoDespesa, P.Descricao as [TipoPagamento], D.IdTipoPagamento,T.Descricao as [TipoDespesa] ,D.Data, D.Valor, D.Comentario, D.DataCriacao
                                    FROM Despesa AS D
                                        INNER JOIN TipoDespesa AS T ON D.IdTipoDespesa = T.Id
                                        INNER JOIN TipoPagamento As P ON D.IdTipoPagamento = P.Id
                                        WHERE D.id = @id";

            return _connection.Query<Despesa>(sqlCommand, new { id }).FirstOrDefault();
        }

     
    }
}
