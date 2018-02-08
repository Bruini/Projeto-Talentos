using System.Collections.Generic;

namespace Fatec.RD.Dominio.Repositorio
{
    public interface IRepositorioBase<TEntidade> where TEntidade : class
    {
        List<TEntidade> Selecionar();
        TEntidade SelecionarPorId(int id); 
        int Inserir(TEntidade obj);
        void Alterar(TEntidade obj);
        void Delete(int id);
    }
}
