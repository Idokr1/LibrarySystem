﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    /// <summary>
    /// class representing a customer
    /// </summary>
    public class Customer : Person
    {
        public double Balance { get; set; }

        /// <summary>
        /// create a customer
        /// </summary>
        /// <param name="name">the employee's name</param>
        /// <param name="phoneNumber">the employee's phone number</param>
        public Customer(string name, string phoneNumber) : base(name, phoneNumber)
        {
            
        }
    }
}
