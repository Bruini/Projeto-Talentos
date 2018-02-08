using Fatec.RD.SharedKernel.Validacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fatec.RD.SharedKernel.Excecoes
{
    public class ValidacaoCamposException : Exception
    {
        public IList<ConvalidaCamposInfo> FieldsValidation { get; private set; }

        public ValidacaoCamposException(string message) : base(message)
        {
            FieldsValidation = new List<ConvalidaCamposInfo>();
            FieldsValidation.Add(new ConvalidaCamposInfo("", message, false));
        }

        public ValidacaoCamposException(string message, IList<ConvalidaCamposInfo> fieldsValidation) : base(message)
        {
            this.FieldsValidation = fieldsValidation;
        }

        public override string Message => GetErrorSummary();

        private string GetErrorSummary()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Os seguintes campos informados não são válidos:\n");

            foreach (var val in FieldsValidation.Where(c => !c.IsValid))
            {
                sb.Append($"{val.Campo} : {val.Mensagem}\n");
            }
            return sb.ToString();
        }
    }
}
