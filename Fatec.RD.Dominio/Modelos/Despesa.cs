using Fatec.RD.SharedKernel.Validacao;
using System;

namespace Fatec.RD.Dominio.Modelos
{
    public class Despesa
    {
        public int Id { get; set; }
        public int IdTipoDespesa { get; set; }
        public TipoDespesa TipoDespesa { get; set; }
        public int IdTipoPagamento { get; set; }
        public TipoPagamento TipoPagamento { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public string Comentario { get; set; }
        public DateTime DataCriacao { get; set; }

        public void Validar()
        {
            new Convalidare()
                .GreaterThan("IdTipoDespesa", this.IdTipoDespesa, 0)
                .GreaterThan("TipoPagamento", this.IdTipoPagamento, 0)
                .GreaterThan("Valor", this.Valor, 0)
                .Validate();
        }
    }
}
