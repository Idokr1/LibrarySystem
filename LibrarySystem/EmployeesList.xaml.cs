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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibrarySystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EmployeesList : Page
    {
        public EmployeesList()
        {
            this.InitializeComponent();
            this.btnGoBackEmployeesList.Click += btnGoBackEmployeesList_Click;
            ListView2.ItemsSource = DataMock.Instance.Employees;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void btnGoBackEmployeesList_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        private async void btnAddEmp_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddEmployee == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to add a new employee").ShowAsync();
            }
            else
            {
                try
                {
                    if (EmpNameTxt.Text == "")
                        throw new EmptyTBException("You have to fill all the TextBoxes");
                    if (EmpPhoneTxt.Text.Length != 10)
                        throw new PhoneNumberException("A phone number has 10 numbers");
                    if (EmpUsernameTxt.Text.Length < 6)
                        throw new TooShortPassException("Too short username - At least 6 characters");
                    if (EmpPassTxt.Text.Length < 6)
                        throw new TooShortPassException("Too short password - At least 6 characters");
                    if (comboAllowedAddEmp.SelectedIndex == -1 || comboAllowedDis.SelectedIndex == -1 || comboAllowedDToAddDelete.SelectedIndex == -1 || comboAllowedDToIssue.SelectedIndex == -1)
                        throw new EmptyComboBoxException("You didn't set permissions");

                    string name = EmpNameTxt.Text;
                    string phoneNum = EmpPhoneTxt.Text;
                    string username = EmpUsernameTxt.Text;
                    string pass = EmpPassTxt.Text;
                    string employementDateString = CalendarDtPck.Date.ToString();
                    DateTime employementDate = DateTime.Parse(employementDateString);

                    var newEmp = new Employee(name, phoneNum, username, pass);
                    newEmp.EmploymentDate = employementDate;

                    if (comboAllowedAddEmp.SelectedIndex == 0)
                        newEmp.AllowedToAddEmployee = true;
                    else if (comboAllowedAddEmp.SelectedIndex == 1)
                        newEmp.AllowedToAddEmployee = false;

                    if (comboAllowedDis.SelectedIndex == 0)
                        newEmp.AllowedToCreateDiscount = true;
                    else if (comboAllowedDis.SelectedIndex == 1)
                        newEmp.AllowedToCreateDiscount = false;

                    if (comboAllowedDToAddDelete.SelectedIndex == 0)
                        newEmp.AllowedToAddDeleteItem = true;
                    else if (comboAllowedDToAddDelete.SelectedIndex == 1)
                        newEmp.AllowedToAddDeleteItem = false;

                    if (comboAllowedDToIssue.SelectedIndex == 0)
                        newEmp.AllowedToIssueItem = true;
                    else if (comboAllowedDToIssue.SelectedIndex == 1)
                        newEmp.AllowedToIssueItem = false;

                    DataMock.Instance.Employees.Add(newEmp);
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
                catch (EmptyComboBoxException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (TooShortPassException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
            }            
        }
        private async void btnUpdateEmp_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddEmployee == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to update an employee").ShowAsync();
            }
            else
            {
                try
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    for (int i = 0; i < DataMock.Instance.Employees.Count; i++)
                    {
                        if (DataMock.Instance.Employees[i].ID == objGuid)
                        {
                            string oldName = DataMock.Instance.Employees[i].Name;
                            string oldPhone = DataMock.Instance.Employees[i].PhoneNumber;
                            string oldUsername = DataMock.Instance.Employees[i].Username;
                            string oldPass = DataMock.Instance.Employees[i].Password;
                            bool oldAllowedAdd = DataMock.Instance.Employees[i].AllowedToAddEmployee;
                            bool oldAllowedDis = DataMock.Instance.Employees[i].AllowedToCreateDiscount;
                            bool oldAllowedAddDel = DataMock.Instance.Employees[i].AllowedToAddDeleteItem;
                            bool oldAllowedIssue = DataMock.Instance.Employees[i].AllowedToIssueItem;
                            DateTime oldEmployementDate = DataMock.Instance.Employees[i].EmploymentDate;
                            Guid oldID = DataMock.Instance.Employees[i].ID;


                            string newName, newPhone, newUsername, newPass;
                            DateTime newEmployementDate;

                            if (EmpNameTxt.Text != "")
                                newName = EmpNameTxt.Text;
                            else
                                newName = oldName;

                            if (EmpPhoneTxt.Text != "")
                            {
                                if (EmpPhoneTxt.Text.Length != 10)
                                    throw new PhoneNumberException("A phone number has 10 numbers");
                                newPhone = EmpPhoneTxt.Text;
                            }                                
                            else
                                newPhone = oldPhone;

                            if (EmpUsernameTxt.Text != "")
                            {
                                if (EmpUsernameTxt.Text.Length < 6)
                                    throw new TooShortPassException("Too short username - At least 6 characters");
                                newUsername = EmpUsernameTxt.Text;
                            }                                
                            else
                                newUsername = oldUsername;

                            if (EmpPassTxt.Text != "")
                            {
                                if (EmpPassTxt.Text.Length < 6)
                                    throw new TooShortPassException("Too short password - At least 6 characters");
                                newPass = EmpPassTxt.Text;
                            }                                
                            else
                                newPass = oldPass;

                            if (CalendarDtPck.Date != null)
                                newEmployementDate = DateTime.Parse(CalendarDtPck.Date.ToString());
                            else
                                newEmployementDate = oldEmployementDate;

                            var newEmp = new Employee(newName, newPhone, newUsername, newPass);
                            newEmp.EmploymentDate = newEmployementDate;
                            newEmp.ID = oldID;

                            if (comboAllowedAddEmp.SelectedIndex == 0)
                                newEmp.AllowedToAddEmployee = true;
                            else if (comboAllowedAddEmp.SelectedIndex == 1)
                                newEmp.AllowedToAddEmployee = false;

                            if (comboAllowedDis.SelectedIndex == 0)
                                newEmp.AllowedToCreateDiscount = true;
                            else if (comboAllowedDis.SelectedIndex == 1)
                                newEmp.AllowedToCreateDiscount = false;

                            if (comboAllowedDToAddDelete.SelectedIndex == 0)
                                newEmp.AllowedToAddDeleteItem = true;
                            else if (comboAllowedDToAddDelete.SelectedIndex == 1)
                                newEmp.AllowedToAddDeleteItem = false;

                            if (comboAllowedDToIssue.SelectedIndex == 0)
                                newEmp.AllowedToIssueItem = true;
                            else if (comboAllowedDToIssue.SelectedIndex == 1)
                                newEmp.AllowedToIssueItem = false;

                            DataMock.Instance.Employees.RemoveAt(i);
                            DataMock.Instance.Employees.Add(newEmp);
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
                catch (TooShortPassException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
            }            
        }
        private async void btnDeleteEmp_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddEmployee == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to delete an employee").ShowAsync();
            }
            else
            {
                try
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    for (int i = 0; i < DataMock.Instance.Employees.Count; i++)
                    {
                        if (DataMock.Instance.Employees[i].ID == objGuid)
                        {
                            DataMock.Instance.Employees.RemoveAt(i);
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
        }
        private void ListView2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ListView listView = sender as ListView;
                Employee selectedEmployee = listView.SelectedItem as Employee;
                if (selectedEmployee != null)
                {
                    SelectedEmpIdTxt.Text = $"{selectedEmployee.ID}";
                    SelectedEmpIdTxt.IsReadOnly = true;
                    MessageDialog dialog = new MessageDialog(
                   $"Full Details: \nName: {selectedEmployee.Name} \nPhone Number: {selectedEmployee.PhoneNumber} \n" +
                   $"Username: {selectedEmployee.Username} \nPassword: {selectedEmployee.Password} \nEmployement Date: {selectedEmployee.EmploymentDate:Y} \n" +
                   $"Allowed to Add Employee: {selectedEmployee.AllowedToAddEmployee} \nAllowed to Create Discount: {selectedEmployee.AllowedToCreateDiscount} \n" +
                   $"Allowed to Add/Delete/Update Items: {selectedEmployee.AllowedToAddDeleteItem} \nAllowed to Issue Item: {selectedEmployee.AllowedToIssueItem}"
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
                DataMock.Instance.Employees = new ObservableCollection<Employee>(DataMock.Instance.Employees.OrderBy(i => i.Name));
                ListView2.ItemsSource = DataMock.Instance.Employees;
            }
            if (comboSort.SelectedIndex == 1)
            {
                DataMock.Instance.Employees = new ObservableCollection<Employee>(DataMock.Instance.Employees.OrderByDescending(i => i.Name));
                ListView2.ItemsSource = DataMock.Instance.Employees;
            }
            if (comboSort.SelectedIndex == 2)
            {
                DataMock.Instance.Employees = new ObservableCollection<Employee>(DataMock.Instance.Employees.OrderByDescending(i => i.EmploymentDate));
                ListView2.ItemsSource = DataMock.Instance.Employees;
            }
            if (comboSort.SelectedIndex == 3)
            {
                DataMock.Instance.Employees = new ObservableCollection<Employee>(DataMock.Instance.Employees.OrderBy(i => i.EmploymentDate));
                ListView2.ItemsSource = DataMock.Instance.Employees;
            }
        }
    }
}
