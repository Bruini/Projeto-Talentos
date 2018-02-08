using System;

namespace Fatec.RD.SharedKernel.Excecoes
{
    public class ConflitoException : Exception
    {
        public int Id { get; private set; }

        public ConflitoException()
        {
        }

        public ConflitoException(string message) : base(message)
        {

        }
    }
}
