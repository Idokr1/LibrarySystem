using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    /// <summary>
    /// class representing book handled by the library
    /// </summary>
    public class Book : LibraryItem
    {        
        public string ISBN { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Genre { get; set; }
        public int Edition { get; set; }
        public static List<string> BookGenres = new List<string>();


        /// <summary>
        /// create an instance of book
        /// </summary>
        /// <param name="title">the title of the new book</param>
        /// <param name="pubDate">the date the new book was published</param>
        /// <param name="price">the price of the new book</param>
        /// <param name="discountLevel">the discount level of the new book</param>
        public Book(string title, DateTime pubDate, double price, double discountLevel) : base(title, pubDate, price, discountLevel)
        {

        }
    }
}
