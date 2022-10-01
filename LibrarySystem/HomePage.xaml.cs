using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace LibrarySystem
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {
        public HomePage()
        {
            this.InitializeComponent();
            this.BtnBooks.Click += BtnBooks_Click;
            this.BtnJournals.Click += BtnJournals_Click;
            this.BtnIssueReturnBooks.Click += BtnIssueReturnBooks_Click;
            this.BtnIssueReturnJournals.Click += BtnIssueReturnJournals_Click;
            this.BtnEmployees.Click += BtnEmployees_Click;
            this.BtnCustomers.Click += BtnCustomers_Click;
            this.BtnLogout.Click += BtnLogout_Click;
        }
        private void BtnBooks_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(BooksList));
        }
        private void BtnJournals_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(JournalsList));
        }
        private void BtnIssueReturnBooks_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(IssueReturnBook));
        }
        private void BtnIssueReturnJournals_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(IssueReturnJournals));
        }
        private void BtnEmployees_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(EmployeesList));
        }
        private void BtnCustomers_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(CustomersList));
        }
        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
