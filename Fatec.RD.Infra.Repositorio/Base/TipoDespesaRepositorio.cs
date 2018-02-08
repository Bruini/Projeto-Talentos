using Dapper;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.Repositorio;
using Fatec.RD.Infra.Repositorio.Contexto;

using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Fatec.RD.Infra.Repositorio.Base
{
    public sealed class TipoDespesaRepositorio : IRepositorioBase<TipoDespesa>
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;
        public TipoDespesaRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        /// <summary>
        /// Método para alterar um tipo de despesa
        /// </summary>
        /// <param name="obj">Objeto de Tipo de Despesa</param>
        public void Alterar(TipoDespesa obj)
        {
            var sqlCommand = @"UPDATE TipoDespesa 
                                      SET Descricao = @Descricao
                                        WHERE Id = @Id";

            _connection.Execute(sqlCommand, obj);
        }

        /// <summary>
        /// Método que deleta um tipo de despesa
        /// </summary>
        /// <param name="id">Id do tipo de despesa</param>
        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM TipoDespesa WHERE Id = @Id", new { Id = id });
        }

        /// <summary>
        /// Método que insere um tipo de despesa
        /// </summary>
        /// <param name="obj">Objeto de tipo de despesa</param>
        /// <returns>Id do tipo de despesa</returns>
        public int Inserir(TipoDespesa obj)
        {
            return _connection.Query<int>(@"INSERT TipoDespesa (Descricao, DataCriacao)
                                                    VALUES (@Descricao, @DataCriacao)
                                                        SELECT CAST (SCOPE_IDENTITY() as int)", obj).First();
        }

        /// <summary>
        /// Método que seleciona uma lista de tipo de despesa
        /// </summary>
        /// <returns>Lista de tipo de despesa</returns>
        public List<TipoDespesa> Selecionar()
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoDespesa";

            return _connection.Query<TipoDespesa>(sqlCommand).ToList();
        }

        /// <summary>
        /// Método que seleciona um Tipo de despesa, pela descrição
        /// </summary>
        /// <returns>Lista de tipo de despesa</returns>
        public TipoDespesa Selecionar(string descricao)
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoDespesa
                                        WHERE Descricao like @Descricao";

            return _connection.Query<TipoDespesa>(sqlCommand, new { Descricao = $"%{descricao}%" }).FirstOrDefault();
        }

        /// <summary>
        /// Método que seleciona um tipo de despesa
        /// </summary>
        /// <param name="id">id do tipo de despesa</param>
        /// <returns>Objeto de tipo de despesa</returns>
        public TipoDespesa SelecionarPorId(int id)
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoDespesa
                                        WHERE id = @id";

            return _connection.Query<TipoDespesa>(sqlCommand, new { id }).FirstOrDefault();
        }
    }
}
