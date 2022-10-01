using System;

namespace Library.DAL
{
    public class NegativeBalanceException : Exception
    {
        public NegativeBalanceException(string message) : base(message) { }
    }
}
