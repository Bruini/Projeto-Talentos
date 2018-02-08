using Dapper;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.Repositorio;
using Fatec.RD.Infra.Repositorio.Contexto;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Fatec.RD.Infra.Repositorio.Base
{
    public sealed class TipoPagamentoRepositorio : IRepositorioBase<TipoPagamento>
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;

        public TipoPagamentoRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Alterar(TipoPagamento obj)
        {
            var sqlCommand = @"UPDATE TipoPagamento 
                                      SET Descricao = @Descricao
                                        WHERE Id = @Id";

            _connection.Execute(sqlCommand, obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM TipoPagamento WHERE Id = @Id", new { Id = id });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int Inserir(TipoPagamento obj)
        {
            return _connection.Query<int>(@"INSERT TipoPagamento (Descricao, DataCriacao)
                                                    VALUES (@Descricao, @DataCriacao)
                                                        SELECT CAST (SCOPE_IDENTITY() as int)", obj).First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TipoPagamento> Selecionar()
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoPagamento";

            return _connection.Query<TipoPagamento>(sqlCommand).ToList();
        }

        /// <summary>
        /// Método que seleciona um Tipo de pagamento, pela descrição
        /// </summary>
        /// <returns>Lista de tipo de despesa</returns>
        public TipoPagamento Selecionar(string descricao)
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoPagamento
                                        WHERE Descricao like @Descricao";

            return _connection.Query<TipoPagamento>(sqlCommand, new { Descricao = $"%{descricao}%" }).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TipoPagamento SelecionarPorId(int id)
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoPagamento
                                        WHERE id = @id";

            return _connection.Query<TipoPagamento>(sqlCommand, new { id }).FirstOrDefault();
        }
    }
}
