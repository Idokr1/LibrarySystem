using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    /// <summary>
    /// class representing a person
    /// </summary>
    public class Person
    {
        /// <summary>
        /// the person's name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// the person's phone number
        /// </summary>
        public string PhoneNumber { get; set; } 

        /// <summary>
        /// the unique identifier of each person
        /// </summary>
        public Guid ID { get; set; }

        public Person(string name, string phoneNumber)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            ID = Guid.NewGuid();
        }
    }
}
