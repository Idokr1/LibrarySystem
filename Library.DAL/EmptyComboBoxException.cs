using System;

namespace Library.DAL
{
    public class EmptyComboBoxException : Exception
    {
        public EmptyComboBoxException(string message) : base(message) { }
    }
}
