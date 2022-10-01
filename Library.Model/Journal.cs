using System;
using System.Collections.Generic;

namespace Library.Model
{
    /// <summary>
    /// class representing journal handled by the library
    /// </summary>
    public class Journal : LibraryItem
    {
        public string Editor { get; set; }
        public string Genre { get; set; }
        public int JournalEditionNumber { get; set; }
        public JournalFrequency Frequency { get; set; }
        public static List<string> JournalGenres = new List<string>();


        /// <summary>
        /// create an instance of journal
        /// </summary>
        /// <param name="title">the title of the new journal</param>
        /// <param name="pubDate">the date the new journal was published</param>
        /// <param name="price">the price of the new journal</param>
        /// <param name="discountLevel">the discount level of the new journal</param>
        public Journal(string title, DateTime pubDate, double price, double discountLevel) : base(title, pubDate, price, discountLevel)
        {

        }
    }
    public enum JournalFrequency
    {
        Daily,
        Weekly,
        BiWeekly,
        Monthly,
        BiMonthly,
        Quarterly,
        SemiAnnually,
        Annually,
        BiAnnually
    }
}