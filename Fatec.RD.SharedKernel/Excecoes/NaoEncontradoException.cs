using System;

namespace Fatec.RD.SharedKernel.Excecoes
{
    [Serializable]
    public class NaoEncontradoException : Exception
    {
        public int Id { get; private set; }

        public NaoEncontradoException()
        {
        }

        public NaoEncontradoException(int id)
        {
            Id = id;
        }

        public NaoEncontradoException(string message, int id) : base(message)
        {
            Id = id;
        }
    }
}
