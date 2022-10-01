using Library.DAL;
using Library.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using static Library.Model.LibraryItem;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibrarySystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IssueReturnJournals : Page
    {
        public const double FINE = 10;
        public IssueReturnJournals()
        {
            this.InitializeComponent();
            this.btnGoBackJournals.Click += btnGoBackJournals_Click;
            ListView3.ItemsSource = DataMock.Instance.JournalItems;
            ListView4.ItemsSource = DataMock.Instance.IssuedJournalsHistory;
            ListView5.ItemsSource = DataMock.Instance.Customers;
            ListView6.ItemsSource = DataMock.Instance.CurrentlyIssuedJournals;

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void btnGoBackJournals_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        private void ListView3_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listView = sender as ListView;
                Journal selectedJournal = listView.SelectedItem as Journal;
                if (selectedJournal != null)
                {
                    SelectedJournalIdTxt.Text = $"{selectedJournal.ID}";
                    SelectedJournalIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                    $"Full Details: \nTitle: {selectedJournal.Title} \nPublish Date: {selectedJournal.PublishDate:Y} \n" +
                    $"Editor: {selectedJournal.Editor} \nGenre: {selectedJournal.Genre} \nFrequency: {selectedJournal.Frequency} \n" +
                    $"Edition Number: {selectedJournal.JournalEditionNumber} \nPrice: {selectedJournal.Price:C} \nActual Price: {selectedJournal.ActualPrice:C} \n" +
                    $"Discount Level: {selectedJournal.DiscountLevel} \nID: {selectedJournal.ID}"
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
                Journal selectedJournal = listView.SelectedItem as Journal;
                if (selectedJournal != null)
                {
                    SelectedJournalIdTxt.Text = $"{selectedJournal.ID}";
                    SelectedJournalIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                    $"Full Details: \nTitle: {selectedJournal.Title} \nPublish Date: {selectedJournal.PublishDate:Y} \n" +
                    $"Editor: {selectedJournal.Editor} \nGenre: {selectedJournal.Genre} \nFrequency: {selectedJournal.Frequency} \n" +
                    $"Edition Number: {selectedJournal.JournalEditionNumber} \nPrice: {selectedJournal.Price:C} \nActual Price: {selectedJournal.ActualPrice:C} \n" +
                    $"Discount Level: {selectedJournal.DiscountLevel} \nID: {selectedJournal.ID}"
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
                Journal selectedJournal = listView.SelectedItem as Journal;
                if (selectedJournal != null)
                {
                    SelectedJournalIdTxt.Text = $"{selectedJournal.ID}";
                    SelectedCustIdTxt.Text = $"{selectedJournal.LenderID}";
                    SelectedJournalIdTxt.IsReadOnly = true;
                    SelectedCustIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                    $"Full Details: \nTitle: {selectedJournal.Title} \nPublish Date: {selectedJournal.PublishDate:Y} \n" +
                    $"Editor: {selectedJournal.Editor} \nGenre: {selectedJournal.Genre} \nFrequency: {selectedJournal.Frequency} \n" +
                    $"Edition Number: {selectedJournal.JournalEditionNumber} \nPrice: {selectedJournal.Price:C} \nActual Price: {selectedJournal.ActualPrice:C} \n" +
                    $"Discount Level: {selectedJournal.DiscountLevel} \nID: {selectedJournal.ID} \nLender Name: {selectedJournal.LenderName} \nLender ID: {selectedJournal.LenderID}"
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
        private async void btnIssueJournal_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToIssueItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to issue a journal").ShowAsync();
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

                    for (int i = 0; i < DataMock.Instance.JournalItems.Count; i++)
                    {
                        if (DataMock.Instance.JournalItems[i].ID == objGuid)
                        {
                            for (int j = 0; j < DataMock.Instance.Customers.Count; j++)
                            {
                                if (DataMock.Instance.Customers[j].ID == custGuid)
                                {
                                    if (DataMock.Instance.Customers[j].Balance - DataMock.Instance.JournalItems[i].ActualPrice < 0)
                                        throw new NegativeBalanceException("The customer doesn't have enough balance");

                                    DataMock.Instance.Customers[j].Balance -= DataMock.Instance.JournalItems[i].ActualPrice;
                                    var newCustomer = DataMock.Instance.Customers[j];
                                    DataMock.Instance.JournalItems[i].LenderName = DataMock.Instance.Customers[j].Name;
                                    DataMock.Instance.JournalItems[i].LenderID = DataMock.Instance.Customers[j].ID;
                                    DataMock.Instance.JournalItems[i].IssueDate = issueDate;
                                    DataMock.Instance.JournalItems[i].ReturnDate = returneDate;

                                    if (DateTime.Now > DataMock.Instance.JournalItems[i].ReturnDate)
                                        DataMock.Instance.JournalItems[i].isReturnLate = isLate.Late;
                                    else
                                        DataMock.Instance.JournalItems[i].isReturnLate = isLate.OnTime;

                                    DataMock.Instance.IssuedJournalsHistory.Add(DataMock.Instance.JournalItems[i]);
                                    DataMock.Instance.CurrentlyIssuedJournals.Add(DataMock.Instance.JournalItems[i]);
                                    DataMock.Instance.JournalItems.RemoveAt(i);
                                    DataMock.Instance.Customers.RemoveAt(j);
                                    DataMock.Instance.Customers.Add(newCustomer);
                                    matchIDs = true;
                                    break;
                                }
                            }
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
                catch (ArgumentException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
            }
        }
        private async void btnReturnJournal_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToIssueItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to return a journal").ShowAsync();
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
                    for (int i = 0; i < DataMock.Instance.CurrentlyIssuedJournals.Count; i++)
                    {
                        if (DataMock.Instance.CurrentlyIssuedJournals[i].ID == objGuid && DataMock.Instance.CurrentlyIssuedJournals[i].LenderID == custGuid)
                        {
                            if (DateTime.Now > DataMock.Instance.CurrentlyIssuedJournals[i].ReturnDate)
                            {
                                await new MessageDialog("You returned the journal in delay, you will be fined $10").ShowAsync();
                                for (int j = 0; j < DataMock.Instance.Customers.Count; j++)
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
                            DataMock.Instance.CurrentlyIssuedJournals[i].LenderName = null;
                            DataMock.Instance.CurrentlyIssuedJournals[i].LenderID = Guid.Empty;
                            DataMock.Instance.CurrentlyIssuedJournals[i].IssueDate = DateTime.MinValue;
                            DataMock.Instance.CurrentlyIssuedJournals[i].ReturnDate = DateTime.MinValue;
                            DataMock.Instance.CurrentlyIssuedJournals[i].isReturnLate = isLate.Default;
                            DataMock.Instance.JournalItems.Add(DataMock.Instance.CurrentlyIssuedJournals[i]);
                            DataMock.Instance.CurrentlyIssuedJournals.RemoveAt(i);
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
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.Title));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
            if (comboSort.SelectedIndex == 1)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.Title));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
            if (comboSort.SelectedIndex == 2)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.PublishDate));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
            if (comboSort.SelectedIndex == 3)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.PublishDate));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
            if (comboSort.SelectedIndex == 4)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.ActualPrice));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
            if (comboSort.SelectedIndex == 5)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.ActualPrice));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
            if (comboSort.SelectedIndex == 6)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.Frequency));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
            if (comboSort.SelectedIndex == 7)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.Frequency));
                ListView3.ItemsSource = DataMock.Instance.JournalItems;
            }
        }
    }
}
