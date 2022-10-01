using System;

namespace Library.DAL
{
    public class EmptyTBException : Exception
    {
        public EmptyTBException(string message) : base(message) { }
    }
}
