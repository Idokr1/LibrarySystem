using Library.DAL;
using System;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace LibrarySystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }
        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool LoggedIn = false;
                string username = UsernameTxt.Text;
                string password = PassTxt.Password;

                if (UsernameTxt.Text.Length < 6)
                    throw new TooShortPassException("Too short username - At least 6 characters");
                if (PassTxt.Password.Length < 6)
                    throw new TooShortPassException("Too short password - At least 6 characters");

                for (int i = 0; i < DataMock.Instance.Employees.Count; i++)
                {
                    if (DataMock.Instance.Employees[i].Username == username && DataMock.Instance.Employees[i].Password == password)
                    {
                        DataMock.Instance.loggedEmp = DataMock.Instance.Employees[i];
                        LoggedIn = true;
                        this.Frame.Navigate(typeof(HomePage));
                    }
                }
                if (LoggedIn == false)
                {
                    await new MessageDialog("Your Details are wrong").ShowAsync();
                }
            }
            catch (TooShortPassException e1)
            {
                MessageDialog d1 = new MessageDialog(e1.Message);
                d1.ShowAsync();
            }
        }
    }
}
