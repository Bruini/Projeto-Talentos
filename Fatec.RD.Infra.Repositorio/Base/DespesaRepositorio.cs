using Dapper;
using Fatec.RD.Dominio.Modelos;
using Fatec.RD.Dominio.Repositorio;
using Fatec.RD.Dominio.ViewModel;
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

        public void Alterar(Despesa obj)
        {
            var sqlCommand = @"UPDATE Despesa 
                                      SET IdTipoDespesa = @IdTipoDespesa,
                                          IdTipoPagamento = @IdTipoPagamento,
                                          Data= @Data,
                                          Valor = @Valor,
                                          Comentario = @Comentario
                                        WHERE Id = @Id";


            _connection.Execute(sqlCommand, obj);
        }

        public void Delete(int id)
        {
            _connection.Execute("DELETE FROM Despesa WHERE Id = @Id", new { Id = id });
        }

        public int Inserir(Despesa obj)
        {
            return _connection.Query<int>(@"INSERT Despesa (Comentario,IdTipoDespesa, IdTipoPagamento, Data, Valor , DataCriacao)
                                                    VALUES (@Comentario, @IdTipoDespesa, @IdTipoPagamento, @Data, @Valor, @DataCriacao)
                                                        SELECT CAST (SCOPE_IDENTITY() as int)", obj).First();
        }

        public List<DespesaViewModel> Selecionar()
        {
            var sqlCommand = @"SELECT d.Id, td.Descricao as TipoDespesa, tp.Descricao as TipoPagamento, d.Data, d.Valor, d.Comentario
                                    FROM Despesa d
                                        INNER JOIN TipoPagamento tp ON d.IdTipoPagamento = tp.Id
                                        INNER JOIN TipoDespesa td ON d.IdTipoDespesa = td.Id";

            return _connection.Query<DespesaViewModel>(sqlCommand).ToList();
        }

        public Despesa SelecionarPorId(int id)
        {

            var sqlCommand = @"SELECT Id, Comentario, DataCriacao, IdTipoDespesa, IdTipoPagamento, Data, Valor 
                                    FROM Despesa
                                        WHERE id = @id";

            return _connection.Query<Despesa>(sqlCommand, new { id }).FirstOrDefault();
        }

        List<Despesa> IRepositorioBase<Despesa>.Selecionar()
        {
            var sqlCommand = @"SELECT Id, Comentario, DataCriacao, IdTipoDespesa, IdTipoPagamento, Data, Valor 
                                    FROM Despesa";

            return _connection.Query<Despesa>(sqlCommand).ToList();
        }
    }
}
