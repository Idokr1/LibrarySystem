using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class TooShortPassException : Exception
    {
        public TooShortPassException(string message) : base(message) { }
    }
}
