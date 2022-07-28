using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class IsbnException : Exception
    {
        public IsbnException(string message) : base(message) { }
    }
}
