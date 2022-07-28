using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Model
{
    /// <summary>
    /// class representing an employee
    /// </summary>
    public class Employee : Person
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime EmploymentDate { get; set; }
        public bool AllowedToAddEmployee { get; set; }
        public bool AllowedToCreateDiscount { get; set; }
        public bool AllowedToAddDeleteItem { get; set; }
        public bool AllowedToIssueItem { get; set; }        


        /// <summary>
        /// create an employee
        /// </summary>
        /// <param name="name">the employee's name</param>
        /// <param name="phoneNumber">the employee's phone number</param>
        /// <param name="username">the employee's username</param>
        /// <param name="password">the employee's password</param>
        public Employee(string name, string phoneNumber, string username, string password) : base(name, phoneNumber)
        {
            Username = username;
            Password = password;
        }
    }
}
