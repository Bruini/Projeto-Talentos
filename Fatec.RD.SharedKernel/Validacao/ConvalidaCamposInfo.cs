namespace Fatec.RD.SharedKernel.Validacao
{
    public sealed class ConvalidaCamposInfo
    {
        public string Campo { get; private set; }
        public string Mensagem { get; private set; }
        public bool IsValid { get; private set; }

        public ConvalidaCamposInfo(string campo, string mensagem, bool isValid)
        {
            Campo = campo;
            IsValid = isValid;
            Mensagem = mensagem;
        }
    }
}
