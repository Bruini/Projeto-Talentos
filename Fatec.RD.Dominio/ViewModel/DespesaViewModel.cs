using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fatec.RD.Dominio.ViewModel
{
    public class DespesaViewModel
    {
        public int Id { get; set; }
        public string TipoPagamento { get; set; }
        public string TipoDespesa { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Comentario { get; set; }
    }
}
