using System;

namespace Library.DAL
{
    public class IsbnException : Exception
    {
        public IsbnException(string message) : base(message) { }
    }
}
