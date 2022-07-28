using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class NegativeBalanceException : Exception
    {
        public NegativeBalanceException(string message) : base(message) { }
    }
}
