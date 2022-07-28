using Library.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.DAL
{
    public class DataMock
    {
        public ObservableCollection<Book> BookItems { get; set; }
        public ObservableCollection<Journal> JournalItems { get; set; }
        public ObservableCollection<Book> IssuedBooksHistory { get; set; }
        public ObservableCollection<Journal> IssuedJournalsHistory { get; set; }
        public ObservableCollection<Book> CurrentlyIssuedBooks { get; set; }
        public ObservableCollection<Journal> CurrentlyIssuedJournals { get; set; }
        public ObservableCollection<Employee> Employees { get; set; }
        public ObservableCollection<Customer> Customers { get; set; }        
        public ObservableCollection<Book> BookItemsFiltered { get; set; }
        public ObservableCollection<Journal> JournalItemsFiltered { get; set; }


        public Employee loggedEmp { get; set; }


        private static DataMock _instance;
        public static DataMock Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new DataMock();
                return _instance;
            }
        }

        private DataMock()
        {
            BookItems = new ObservableCollection<Book>();
            JournalItems = new ObservableCollection<Journal>();
            IssuedBooksHistory = new ObservableCollection<Book>();
            IssuedJournalsHistory = new ObservableCollection<Journal>();
            Employees = new ObservableCollection<Employee>();
            Customers = new ObservableCollection<Customer>();
            CurrentlyIssuedBooks = new ObservableCollection<Book>();
            CurrentlyIssuedJournals = new ObservableCollection<Journal>();

            InitBooks();
            InitJournals();
            InitEmployees();
            InitCustomers();
            BookItemsFiltered = new ObservableCollection<Book>(BookItems);
            JournalItemsFiltered = new ObservableCollection<Journal>(JournalItems);
        }
        private void InitBooks()
        {
            Book.BookGenres.Add("Action");
            Book.BookGenres.Add("Art");
            Book.BookGenres.Add("Alternate History");
            Book.BookGenres.Add("Autobiography");
            Book.BookGenres.Add("Anthology");
            Book.BookGenres.Add("Biography");
            Book.BookGenres.Add("Business");
            Book.BookGenres.Add("Classic");
            Book.BookGenres.Add("Cookbook");
            Book.BookGenres.Add("Diary");
            Book.BookGenres.Add("Crime");
            Book.BookGenres.Add("Drama");
            Book.BookGenres.Add("Guide");
            Book.BookGenres.Add("Fairytale");
            Book.BookGenres.Add("Health");
            Book.BookGenres.Add("Fantasy");
            Book.BookGenres.Add("History");
            Book.BookGenres.Add("Historical fiction");
            Book.BookGenres.Add("Humor");
            Book.BookGenres.Add("Horror");
            Book.BookGenres.Add("Mystery");
            Book.BookGenres.Add("Novel");
            Book.BookGenres.Add("Romance");
            Book.BookGenres.Add("Science fiction");
            Book.BookGenres.Add("Review");
            Book.BookGenres.Add("Short story");
            Book.BookGenres.Add("Science");
            Book.BookGenres.Add("Thriller");            

            var book1 = new Book("Harry Potter and the Goblet of Fire", new DateTime(2001, 4, 8), 25, 0);
            book1.ISBN = "978-96-95002-01-8";
            book1.Author = "J. K. Rowling";
            book1.Publisher = "Bloomsbury";
            book1.Genre = "Fantasy";
            book1.Edition = 2;

            var book2 = new Book("Dark Horse", new DateTime(2022, 2, 8), 42, 0);
            book2.ISBN = "978-96-95055-02-1";
            book2.Author = "Gregg Hurwitz";
            book2.Publisher = "Minotaur Books";
            book2.Genre = "Thriller";
            book2.Edition = 213;

            var book3 = new Book("The Hobbit", new DateTime(1937, 9, 21), 50, 20);
            book3.ISBN = "978-96-94002-11-8";
            book3.Author = "J.R.R. Tolkien";
            book3.Publisher = "George Allen & Unwin";
            book3.Genre = "Novel";
            book3.Edition = 3;

            var book4 = new Book("Harry Potter and the Chamber of Secrets", new DateTime(1998, 7, 2), 30, 10);
            book4.ISBN = "978-96-95002-52-0";
            book4.Author = "J. K. Rowling";
            book4.Publisher = "Bloomsbury";
            book4.Genre = "Fantasy";
            book4.Edition = 24;

            var book5 = new Book("The 6:20 Man", new DateTime(2022, 7, 12), 20, 10);
            book5.ISBN = "978-96-90023-53-7";
            book5.Author = "David Baldacci";
            book5.Publisher = "Grand Central Publishing";
            book5.Genre = "Action";
            book5.Edition = 2;

            BookItems.Add(book1);
            BookItems.Add(book2);
            BookItems.Add(book3);
            BookItems.Add(book4);
            BookItems.Add(book5);
        }
        private void InitJournals()
        {
            Journal.JournalGenres.Add("Science");
            Journal.JournalGenres.Add("Medicine");
            Journal.JournalGenres.Add("Environment");

            var journal1 = new Journal("Nature", new DateTime(1869, 11, 5), 10, 0); 
            journal1.Editor = "John Maddox";
            journal1.Genre = "Science";
            journal1.JournalEditionNumber = 550;
            journal1.Frequency = JournalFrequency.Weekly;

            var journal2 = new Journal("The New England Journal of Medicine", new DateTime(1812, 5, 21), 15, 0);
            journal2.Editor = "Eric J. Rubin";
            journal2.Genre = "Medicine";
            journal2.JournalEditionNumber = 600;
            journal2.Frequency = JournalFrequency.Weekly;

            var journal3 = new Journal("Advanced Materials", new DateTime(1989, 1, 11), 20, 10);
            journal3.Editor = "Jos Lenders";
            journal3.Genre = "Science";
            journal3.JournalEditionNumber = 102;
            journal3.Frequency = JournalFrequency.Weekly;

            var journal4 = new Journal("Nucleic Acids Research", new DateTime(1974, 2, 4), 24, 0);
            journal4.Editor = "Julian Sale";
            journal4.Genre = "Science";
            journal4.JournalEditionNumber = 215;
            journal4.Frequency = JournalFrequency.BiWeekly;

            var journal5 = new Journal("Journal of Cleaner Production", new DateTime(1993, 5, 11), 21, 10);
            journal5.Editor = "Yutao Wang";
            journal5.Genre = "Environment";
            journal5.JournalEditionNumber = 125;
            journal5.Frequency = JournalFrequency.Monthly;

            JournalItems.Add(journal1);
            JournalItems.Add(journal2);
            JournalItems.Add(journal3);
            JournalItems.Add(journal4);
            JournalItems.Add(journal5);
        }
        private void InitEmployees()
        {
            var employee1 = new Employee("Ido", "054-8043789", "ido123", "123456");
            employee1.EmploymentDate = new DateTime(2021, 2, 4);
            employee1.AllowedToAddEmployee = true;
            employee1.AllowedToCreateDiscount = true;
            employee1.AllowedToAddDeleteItem = true;
            employee1.AllowedToIssueItem = true;

            var employee2 = new Employee("Dan", "052-8042189", "dan543", "987654321");
            employee2.EmploymentDate = new DateTime(2022, 5, 10);
            employee2.AllowedToAddEmployee = false;
            employee2.AllowedToCreateDiscount = true;
            employee2.AllowedToAddDeleteItem = true;
            employee2.AllowedToIssueItem = true;

            var employee3 = new Employee("Dana", "053-5324623", "dana234", "323232");
            employee3.EmploymentDate = new DateTime(2022, 6, 10);
            employee3.AllowedToAddEmployee = false;
            employee3.AllowedToCreateDiscount = false;
            employee3.AllowedToAddDeleteItem = true;
            employee3.AllowedToIssueItem = true;

            var employee4 = new Employee("Sami", "050-5322653", "samisami", "easypass");
            employee4.EmploymentDate = new DateTime(2022, 7, 10);
            employee4.AllowedToAddEmployee = false;
            employee4.AllowedToCreateDiscount = false;
            employee4.AllowedToAddDeleteItem = false;
            employee4.AllowedToIssueItem = true;

            Employees.Add(employee1);
            Employees.Add(employee2);
            Employees.Add(employee3);
            Employees.Add(employee4);
        }
        private void InitCustomers()
        {
            var customer1 = new Customer("Maya", "0548023549");
            customer1.Balance = 70;

            var customer2 = new Customer("Avi", "0544322649");
            customer2.Balance = 50;

            var customer3 = new Customer("Tamar", "0525492127");
            customer3.Balance = 100;
        
            Customers.Add(customer1);
            Customers.Add(customer2);
            Customers.Add(customer3);
        }
    }
}
