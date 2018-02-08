using Fatec.RD.SharedKernel.Validacao;
using System;

namespace Fatec.RD.Dominio.Modelos
{
    public class Relatorio
    {
        public int Id { get; set; }
        public int IdTipoRelatorio { get; set; }
        public TipoRelatorio TipoRelatorio { get; set; }
        public string Descricao { get; set; }
        public string Comentario { get; set; }
        public DateTime DataCriacao { get; set; }

        public void Validar()
        {
            new Convalidare()
                .GreaterThan("IdTipoRelatorio", this.IdTipoRelatorio, 0)
                .NotNullOrEmpty("Descricao", this.Descricao)
                .Validate();
        }
    }
}
