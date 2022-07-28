using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    /// <summary>
    /// abstract class representing an item handled by the library
    /// </summary>
    public abstract class LibraryItem
    {
        /// <summary>
        /// the title of the item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// print or publish date of item
        /// </summary>
        public DateTime PublishDate { get; set; }

        /// <summary>
        /// the price of the item
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// the discount level of the item
        /// </summary>
        public double DiscountLevel { get; set; }

        /// <summary>
        /// the actual price of the item after discount
        /// </summary>
        public double ActualPrice { get; set; }

        /// <summary>
        /// the unique identifier of this item
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// the name of the lender of the item
        /// </summary>
        public string LenderName { get; set; }

        /// <summary>
        /// the ID of the lender of the item
        /// </summary>
        public Guid LenderID { get; set; }

        /// <summary>
        /// The issue date of item
        /// </summary>
        public DateTime IssueDate { get; set; }

        /// <summary>
        /// The return date of item
        /// </summary>
        public DateTime ReturnDate { get; set; }

        /// <summary>
        /// Determines whether the customer is late or not
        /// </summary>
        public isLate isReturnLate { get; set; }


        private double CalculateActualPrice()
        {
            double actualprice;
            if (DiscountLevel != 0)
            {
                actualprice = (Price * ((100 - DiscountLevel) / 100));
                return actualprice;
            }
            else
            {
                actualprice = Price;
                return actualprice;
            }
        }

        /// <summary>
        /// create an instance of library item
        /// </summary>
        /// <param name="title">the title of the new item</param>
        /// <param name="pubDate">the publish date of the new item</param>
        /// <param name="price">the price of the new item</param>
        /// <param name="discountLevel">the discount level of the new item</param>
        protected LibraryItem(string title, DateTime pubDate, double price, double discountLevel)
        {
            ID = Guid.NewGuid();
            Title = title;
            PublishDate = pubDate;
            Price = price;
            DiscountLevel = discountLevel;
            ActualPrice = CalculateActualPrice();
        }

        public enum isLate
        {
            Default,
            OnTime,
            Late           
        }
    }
}