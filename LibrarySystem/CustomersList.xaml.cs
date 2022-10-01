using Library.DAL;
using Library.Model;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibrarySystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CustomersList : Page
    {
        public CustomersList()
        {
            this.InitializeComponent();
            this.btnGoBackCustList.Click += btnGoBackCustList_Click;
            ListView2.ItemsSource = DataMock.Instance.Customers;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void btnGoBackCustList_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        private void btnAddCust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CustNameTxt.Text == "")
                    throw new EmptyTBException("You have to fill all the TextBoxes");
                if (CustPhoneTxt.Text.Length != 10)
                    throw new PhoneNumberException("A phone number has 10 numbers");

                string name = CustNameTxt.Text;
                string phoneNum = CustPhoneTxt.Text;
                double balance = double.Parse(CustBalanceTxt.Text);

                var newCust = new Customer(name, phoneNum);
                newCust.Balance = balance;
                DataMock.Instance.Customers.Add(newCust);
            }
            catch (FormatException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
            catch (EmptyTBException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
            catch (PhoneNumberException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
        private void btnUpdateCust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid objGuid = Guid.Empty;
                objGuid = Guid.Parse(txtBoxID.Text);
                for (int i = 0; i < DataMock.Instance.Customers.Count; i++)
                {
                    if (DataMock.Instance.Customers[i].ID == objGuid)
                    {
                        string oldName = DataMock.Instance.Customers[i].Name;
                        string oldPhone = DataMock.Instance.Customers[i].PhoneNumber;
                        double oldBalance = DataMock.Instance.Customers[i].Balance;
                        Guid oldID = DataMock.Instance.Customers[i].ID;

                        string newName, newPhone;
                        double newBalance;

                        if (CustNameTxt.Text != "")
                            newName = CustNameTxt.Text;
                        else
                            newName = oldName;

                        if (CustPhoneTxt.Text != "")
                        {
                            if (CustPhoneTxt.Text.Length != 10)
                                throw new PhoneNumberException("A phone number has 10 numbers");
                            newPhone = CustPhoneTxt.Text;
                        }
                        else
                            newPhone = oldPhone;

                        if (CustBalanceTxt.Text != "")
                            newBalance = double.Parse(CustBalanceTxt.Text);
                        else
                            newBalance = oldBalance;

                        var newCust = new Customer(newName, newPhone);
                        newCust.Balance = newBalance;
                        newCust.ID = oldID;

                        DataMock.Instance.Customers.RemoveAt(i);
                        DataMock.Instance.Customers.Add(newCust);
                        break;
                    }
                }
            }
            catch (FormatException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
            catch (PhoneNumberException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
        private void btnDeleteCust_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid objGuid = Guid.Empty;
                objGuid = Guid.Parse(txtBoxID.Text);
                for (int i = 0; i < DataMock.Instance.Customers.Count; i++)
                {
                    if (DataMock.Instance.Customers[i].ID == objGuid)
                    {
                        DataMock.Instance.Customers.RemoveAt(i);
                        break;
                    }
                }
            }
            catch (FormatException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
        private void ListView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboSort.SelectedIndex == 0)
            {
                DataMock.Instance.Customers = new ObservableCollection<Customer>(DataMock.Instance.Customers.OrderBy(i => i.Name));
                ListView2.ItemsSource = DataMock.Instance.Customers;
            }
            if (comboSort.SelectedIndex == 1)
            {
                DataMock.Instance.Customers = new ObservableCollection<Customer>(DataMock.Instance.Customers.OrderByDescending(i => i.Name));
                ListView2.ItemsSource = DataMock.Instance.Customers;
            }
            if (comboSort.SelectedIndex == 2)
            {
                DataMock.Instance.Customers = new ObservableCollection<Customer>(DataMock.Instance.Customers.OrderBy(i => i.Balance));
                ListView2.ItemsSource = DataMock.Instance.Customers;
            }
            if (comboSort.SelectedIndex == 3)
            {
                DataMock.Instance.Customers = new ObservableCollection<Customer>(DataMock.Instance.Customers.OrderByDescending(i => i.Balance));
                ListView2.ItemsSource = DataMock.Instance.Customers;
            }
        }
    }
}
