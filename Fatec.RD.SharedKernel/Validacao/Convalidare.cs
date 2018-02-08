using Fatec.RD.SharedKernel.Excecoes;
using Fatec.RD.SharedKernel.Util;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Fatec.RD.SharedKernel.Validacao
{
    public class Convalidare
    {
        private IList<ConvalidaCamposInfo> validacoes;

        public Convalidare()
        {
            validacoes = new List<ConvalidaCamposInfo>();
        }

        public void Validate(string message = "Ocorreram Erros")
        {
            if (validacoes.Any(x => !x.IsValid))
                throw new ValidacaoCamposException(message, validacoes.Where(c => !c.IsValid).ToList());
        }

        public Convalidare NotNull(string campo, object obj)
        {
            if (obj == null)
                validacoes.Add(new ConvalidaCamposInfo(campo, $"O campo {campo} é obrigatório", false));
            else
                validacoes.Add(ValidacaoOK());

            return this;
        }

        public Convalidare NotNullOrEmpty(string campo, string valor)
        {
            if (string.IsNullOrEmpty(valor))
                validacoes.Add(new ConvalidaCamposInfo(campo, $"O campo {campo} não pode ser vazio", false));
            else
                validacoes.Add(ValidacaoOK());

            return this;
        }

        public Convalidare GreaterThan(string campo, IComparable number, IComparable greaterThanNumber)
        {
            if (Comparar.GetComparisonResult(number, greaterThanNumber) > 0)
                validacoes.Add(ValidacaoOK());
            else
                validacoes.Add(new ConvalidaCamposInfo(campo, $"O campo {campo} deve ser maior do que {greaterThanNumber}.", false));

            return this;

        }

        public Convalidare LessThan(string field, IComparable number, IComparable lessThanNumber)
        {
            if (Comparar.GetComparisonResult(number, lessThanNumber) < 0)
                validacoes.Add(ValidacaoOK());
            else
                validacoes.Add(new ConvalidaCamposInfo(field, $"O campo {field} deve ser menor do que {lessThanNumber}.", false));

            return this;

        }

        private ConvalidaCamposInfo ValidacaoOK()
        {
            return new ConvalidaCamposInfo("", "", true);
        }
    }
}
