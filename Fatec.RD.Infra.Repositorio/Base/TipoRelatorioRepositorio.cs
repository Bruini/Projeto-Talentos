using Dapper;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.Repositorio;
using Fatec.RD.Infra.Repositorio.Contexto;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Fatec.RD.Infra.Repositorio.Base
{
    public sealed class TipoRelatorioRepositorio : IRepositorioBase<TipoRelatorio>
    {
        readonly DapperContexto _db;
        readonly IDbConnection _connection;

        public TipoRelatorioRepositorio()
        {
            _db = new DapperContexto();
            _connection = _db.Connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public void Alterar(TipoRelatorio obj)
        {
            var sqlCommand = @"UPDATE TipoRelatorio 
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
            _connection.Execute("DELETE FROM TipoRelatorio WHERE Id = @Id", new { Id = id });
        }

        public int Inserir(TipoRelatorio obj)
        {
            return _connection.Query<int>(@"INSERT TipoRelatorio (Descricao, DataCriacao)
                                                    VALUES (@Descricao, @DataCriacao)
                                                        SELECT CAST (SCOPE_IDENTITY() as int)", obj).First();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<TipoRelatorio> Selecionar()
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoRelatorio";

            return _connection.Query<TipoRelatorio>(sqlCommand).ToList();
        }

        /// <summary>
        /// Método que seleciona um Tipo de despesa, pela descrição
        /// </summary>
        /// <returns>Lista de tipo de despesa</returns>
        public TipoRelatorio Selecionar(string descricao)
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoRelatorio
                                        WHERE Descricao like @Descricao";

            return _connection.Query<TipoRelatorio>(sqlCommand, new { Descricao = $"%{descricao}%" }).FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TipoRelatorio SelecionarPorId(int id)
        {
            var sqlCommand = @"SELECT Id, Descricao, DataCriacao
                                    FROM TipoRelatorio
                                        WHERE id = @id";

            return _connection.Query<TipoRelatorio>(sqlCommand, new { id }).FirstOrDefault();
        }
    }
}
