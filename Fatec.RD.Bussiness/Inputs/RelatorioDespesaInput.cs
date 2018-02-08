using System.Collections.Generic;

namespace Fatec.RD.Bussiness.Inputs
{
    public class RelatorioDespesaInput
    {
        public RelatorioDespesaInput()
        {
            Chave = new List<ChaveRelatorioDespesa>();
        }
        public List<ChaveRelatorioDespesa> Chave { get; set; }
    }

    public class ChaveRelatorioDespesa
    {
        public int IdDespesa { get; set; }
    }
}
