using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using static Library.Model.LibraryItem;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibrarySystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IssueReturnBook : Page
    {
        public const double FINE = 10;
        public IssueReturnBook()
        {
            this.InitializeComponent();
            this.btnGoBackBooks.Click += btnGoBackBooks_Click;
            ListView3.ItemsSource = DataMock.Instance.BookItems;
            ListView4.ItemsSource = DataMock.Instance.IssuedBooksHistory;
            ListView5.ItemsSource = DataMock.Instance.Customers;
            ListView6.ItemsSource = DataMock.Instance.CurrentlyIssuedBooks;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void btnGoBackBooks_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        private void ListView3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listView = sender as ListView;
                Book selectedBook = listView.SelectedItem as Book;
                if (selectedBook != null)
                {
                    SelectedBookIdTxt.Text = $"{selectedBook.ID}";
                    SelectedBookIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                    $"Full Details: \nTitle: {selectedBook.Title} \nPublish Date: {selectedBook.PublishDate:Y} \n" +
                    $"ISBN: {selectedBook.ISBN} \nAuthor: {selectedBook.Author} \nPublisher: {selectedBook.Publisher} \n" +
                    $"Edition: {selectedBook.Edition} \nGenre: {selectedBook.Genre} \nPrice: {selectedBook.Price:C} \nActual Price: {selectedBook.ActualPrice:C} \n" +
                    $"Discount Level: {selectedBook.DiscountLevel}% \nID: {selectedBook.ID}"
                    );
                    dialog.ShowAsync();
                }
                listView.SelectedItem = null;
            }
            catch (NullReferenceException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
        private void ListView4_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listView = sender as ListView;
                Book selectedBook = listView.SelectedItem as Book;
                if (selectedBook != null)
                {
                    SelectedBookIdTxt.Text = $"{selectedBook.ID}";
                    SelectedBookIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                    $"Full Details: \nTitle: {selectedBook.Title} \nPublish Date: {selectedBook.PublishDate:Y} \n" +
                    $"ISBN: {selectedBook.ISBN} \nAuthor: {selectedBook.Author} \nPublisher: {selectedBook.Publisher} \n" +
                    $"Edition: {selectedBook.Edition} \nGenre: {selectedBook.Genre} \nPrice: {selectedBook.Price:C} \nActual Price: {selectedBook.ActualPrice:C} \n" +
                    $"Discount Level: {selectedBook.DiscountLevel}% \nID: {selectedBook.ID}"
                    );
                    dialog.ShowAsync();
                }
                listView.SelectedItem = null;
            }
            catch (NullReferenceException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
        private void ListView5_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listView = sender as ListView;
                Customer selectedCust = listView.SelectedItem as Customer;
                if (selectedCust != null)
                {
                    SelectedCustIdTxt.Text = $"{selectedCust.ID}";
                    SelectedCustIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                   $"Full Details: \nName: {selectedCust.Name} \nPhone Number: {selectedCust.PhoneNumber} \n" +
                   $"Balance: {selectedCust.Balance:C} \nID: {selectedCust.ID}"
                   );
                    dialog.ShowAsync();
                }
                listView.SelectedItem = null;
            }
            catch (NullReferenceException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
        private void ListView6_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listView = sender as ListView;
                Book selectedBook = listView.SelectedItem as Book;
                if (selectedBook != null)
                {
                    SelectedBookIdTxt.Text = $"{selectedBook.ID}";
                    SelectedCustIdTxt.Text = $"{selectedBook.LenderID}";
                    SelectedBookIdTxt.IsReadOnly = true;                    
                    SelectedCustIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                    $"Full Details: \nTitle: {selectedBook.Title} \nPublish Date: {selectedBook.PublishDate:Y} \n" +
                    $"ISBN: {selectedBook.ISBN} \nAuthor: {selectedBook.Author} \nPublisher: {selectedBook.Publisher} \n" +
                    $"Edition: {selectedBook.Edition} \nGenre: {selectedBook.Genre} \nPrice: {selectedBook.Price:C} \nActual Price: {selectedBook.ActualPrice:C} \n" +
                    $"Discount Level: {selectedBook.DiscountLevel}% \nID: {selectedBook.ID} \nLender Name: {selectedBook.LenderName} \nLender ID: {selectedBook.LenderID}"
                    );
                    dialog.ShowAsync();
                }
                listView.SelectedItem = null;
            }
            catch (NullReferenceException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
        private async void btnIssueBook_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToIssueItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to issue a book").ShowAsync();
            }
            else
            {
                try
                {
                    bool matchIDs = false;
                    Guid objGuid = Guid.Empty;
                    Guid custGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    custGuid = Guid.Parse(txtBoxIDCust.Text);

                    string issueDateString = CalendarDtPck.Date.ToString();
                    DateTime issueDate = DateTime.Parse(issueDateString);

                    string returnDateString = CalendarDtPckReturn.Date.ToString();
                    DateTime returneDate = DateTime.Parse(returnDateString);

                    if (issueDate >= returneDate)
                        throw new ArgumentException("The return date you chose is the same or earlier than the issue date");

                    for (int i = 0; i < DataMock.Instance.BookItems.Count; i++)
                    {
                        if (DataMock.Instance.BookItems[i].ID == objGuid)
                        {
                            for (int j = 0; j < DataMock.Instance.Customers.Count; j++)
                            {
                                if (DataMock.Instance.Customers[j].ID == custGuid)
                                {
                                    if (DataMock.Instance.Customers[j].Balance - DataMock.Instance.BookItems[i].ActualPrice < 0)
                                        throw new NegativeBalanceException("The customer doesn't have enough balance");                                    

                                    DataMock.Instance.Customers[j].Balance -= DataMock.Instance.BookItems[i].ActualPrice;
                                    var newCustomer = DataMock.Instance.Customers[j];
                                    DataMock.Instance.BookItems[i].LenderName = DataMock.Instance.Customers[j].Name;
                                    DataMock.Instance.BookItems[i].LenderID = DataMock.Instance.Customers[j].ID;
                                    DataMock.Instance.BookItems[i].IssueDate = issueDate;
                                    DataMock.Instance.BookItems[i].ReturnDate = returneDate;

                                    if (DateTime.Now > DataMock.Instance.BookItems[i].ReturnDate)
                                        DataMock.Instance.BookItems[i].isReturnLate = isLate.Late;
                                    else
                                        DataMock.Instance.BookItems[i].isReturnLate = isLate.OnTime;

                                    DataMock.Instance.IssuedBooksHistory.Add(DataMock.Instance.BookItems[i]);
                                    DataMock.Instance.CurrentlyIssuedBooks.Add(DataMock.Instance.BookItems[i]);                                    
                                    DataMock.Instance.BookItems.RemoveAt(i);
                                    DataMock.Instance.Customers.RemoveAt(j);
                                    DataMock.Instance.Customers.Add(newCustomer);
                                    matchIDs = true;
                                    break;                                    
                                }
                            }                         
                        }
                    }
                    if(matchIDs == false)
                        await new MessageDialog("There is no match between those ID's").ShowAsync();
                }
                catch (FormatException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (NegativeBalanceException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (ArgumentException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
            }                       
        }
        private async void btnReturnBook_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToIssueItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to return a book").ShowAsync();
            }
            else
            {
                try
                {
                    bool matchIDs = false;                    
                    Guid objGuid = Guid.Empty;
                    Guid custGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    custGuid = Guid.Parse(txtBoxIDCust.Text);
                    for (int i = 0; i < DataMock.Instance.CurrentlyIssuedBooks.Count; i++)
                    {
                        if (DataMock.Instance.CurrentlyIssuedBooks[i].ID == objGuid && DataMock.Instance.CurrentlyIssuedBooks[i].LenderID == custGuid)
                        {
                            if (DateTime.Now > DataMock.Instance.CurrentlyIssuedBooks[i].ReturnDate)
                            {
                                await new MessageDialog("You returned the book in delay, you will be fined $10").ShowAsync();
                                for(int j = 0; j < DataMock.Instance.Customers.Count; j++)
                                {
                                    if (DataMock.Instance.Customers[j].ID == custGuid)
                                    {
                                        if (DataMock.Instance.Customers[j].Balance < FINE)
                                            throw new NegativeBalanceException("You don't have enough money to pay the fine, please contact an employee");
                                        else
                                        {
                                            DataMock.Instance.Customers[j].Balance -= FINE;
                                            var newCustomer = DataMock.Instance.Customers[j];
                                            DataMock.Instance.Customers.RemoveAt(j);
                                            DataMock.Instance.Customers.Add(newCustomer);
                                        }
                                    }
                                }                                
                            }                            
                            DataMock.Instance.CurrentlyIssuedBooks[i].LenderName = null;
                            DataMock.Instance.CurrentlyIssuedBooks[i].LenderID = Guid.Empty;
                            DataMock.Instance.CurrentlyIssuedBooks[i].IssueDate = DateTime.MinValue;
                            DataMock.Instance.CurrentlyIssuedBooks[i].ReturnDate = DateTime.MinValue;
                            DataMock.Instance.CurrentlyIssuedBooks[i].isReturnLate = isLate.Default;
                            DataMock.Instance.BookItems.Add(DataMock.Instance.CurrentlyIssuedBooks[i]);
                            DataMock.Instance.CurrentlyIssuedBooks.RemoveAt(i);                            
                            matchIDs = true;
                            break;                                                         
                        }
                    }
                    if (matchIDs == false)
                        await new MessageDialog("There is no match between those ID's").ShowAsync();
                }
                
                catch (FormatException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (NegativeBalanceException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
            }            
        }
        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboSort.SelectedIndex == 0)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderBy(i => i.Title));
                ListView3.ItemsSource = DataMock.Instance.BookItems;
            }
            if (comboSort.SelectedIndex == 1)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderByDescending(i => i.Title));
                ListView3.ItemsSource = DataMock.Instance.BookItems;
            }
            if (comboSort.SelectedIndex == 2)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderByDescending(i => i.PublishDate));
                ListView3.ItemsSource = DataMock.Instance.BookItems;
            }
            if (comboSort.SelectedIndex == 3)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderBy(i => i.PublishDate));
                ListView3.ItemsSource = DataMock.Instance.BookItems;
            }
            if (comboSort.SelectedIndex == 4)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderBy(i => i.ActualPrice));
                ListView3.ItemsSource = DataMock.Instance.BookItems;
            }
            if (comboSort.SelectedIndex == 5)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderByDescending(i => i.ActualPrice));
                ListView3.ItemsSource = DataMock.Instance.BookItems;
            }
        }
    }
}
