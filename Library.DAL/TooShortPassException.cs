using System;

namespace Library.DAL
{
    public class TooShortPassException : Exception
    {
        public TooShortPassException(string message) : base(message) { }
    }
}
