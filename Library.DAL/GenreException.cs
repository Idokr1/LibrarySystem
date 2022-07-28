using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class GenreException : Exception
    {
        public GenreException(string message) : base(message) { }
    }
}
