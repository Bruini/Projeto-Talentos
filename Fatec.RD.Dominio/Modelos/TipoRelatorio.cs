using Fatec.RD.SharedKernel.Validacao;
using System;

namespace Fatec.RD.Dominio.Modelos
{
    public class TipoRelatorio
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }

        public void Validar()
        {
            new Convalidare()
                .NotNullOrEmpty("Descricao", this.Descricao)
                .Validate();
        }
    }
}
