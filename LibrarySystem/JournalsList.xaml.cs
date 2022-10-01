using Library.DAL;
using Library.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;
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
    public sealed partial class JournalsList : Page
    {
        public JournalsList()
        {
            this.InitializeComponent();
            this.btnGoBackJournalsList.Click += btnGoBackJournalsList_Click;
            ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void btnGoBackJournalsList_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        private async void btnAddJournal_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddDeleteItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to add new journals").ShowAsync();
            }
            else
            {
                try
                {
                    if (JournalTitleTxt.Text == "" || EditorTxt.Text == "")
                        throw new EmptyTBException("You have to fill all the TextBoxes");
                    if (!Journal.JournalGenres.Contains(GenreTxt.Text))
                        throw new GenreException("The Genre you entered isn't one of the known genres");
                    if (comboFrequency.SelectedIndex == -1)
                        throw new EmptyComboBoxException("You didn't choose frequency");

                    string title = JournalTitleTxt.Text;
                    string pubDateString = CalendarDtPck.Date.ToString();
                    DateTime pubDate = DateTime.Parse(pubDateString);
                    double price = double.Parse(PriceTxt.Text);
                    var newJournal = new Journal(title, pubDate, price, 0);

                    newJournal.Editor = EditorTxt.Text;
                    newJournal.Genre = GenreTxt.Text;
                    newJournal.JournalEditionNumber = int.Parse(JournalEditionTxt.Text);

                    if (comboFrequency.SelectedIndex == 0)
                        newJournal.Frequency = JournalFrequency.Daily;
                    if (comboFrequency.SelectedIndex == 1)
                        newJournal.Frequency = JournalFrequency.Weekly;
                    if (comboFrequency.SelectedIndex == 2)
                        newJournal.Frequency = JournalFrequency.BiWeekly;
                    if (comboFrequency.SelectedIndex == 3)
                        newJournal.Frequency = JournalFrequency.Monthly;
                    if (comboFrequency.SelectedIndex == 4)
                        newJournal.Frequency = JournalFrequency.BiMonthly;
                    if (comboFrequency.SelectedIndex == 5)
                        newJournal.Frequency = JournalFrequency.Quarterly;
                    if (comboFrequency.SelectedIndex == 6)
                        newJournal.Frequency = JournalFrequency.SemiAnnually;
                    if (comboFrequency.SelectedIndex == 7)
                        newJournal.Frequency = JournalFrequency.Annually;
                    if (comboFrequency.SelectedIndex == 8)
                        newJournal.Frequency = JournalFrequency.BiAnnually;

                    DataMock.Instance.JournalItems.Add(newJournal);
                    UpdatingListView();
                }
                catch (FormatException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (GenreException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (EmptyTBException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (EmptyComboBoxException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
            }
        }
        private async void btnUpdateJournal_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddDeleteItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to update journals").ShowAsync();
            }
            else
            {
                try
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    for (int i = 0; i < DataMock.Instance.JournalItems.Count; i++)
                    {
                        if (DataMock.Instance.JournalItems[i].ID == objGuid)
                        {
                            string oldTitle = DataMock.Instance.JournalItems[i].Title;
                            string oldEditor = DataMock.Instance.JournalItems[i].Editor;
                            string oldGenre = DataMock.Instance.JournalItems[i].Genre;
                            int oldEditionNum = DataMock.Instance.JournalItems[i].JournalEditionNumber;
                            double oldPrice = DataMock.Instance.JournalItems[i].Price;
                            DateTime oldPubDate = DataMock.Instance.JournalItems[i].PublishDate;
                            Guid oldID = DataMock.Instance.JournalItems[i].ID;
                            JournalFrequency oldFrequency = DataMock.Instance.JournalItems[i].Frequency;
                            double oldDiscountLevel = DataMock.Instance.JournalItems[i].DiscountLevel;

                            string newTitle, newEditor, newGenre;
                            int newEditionNum;
                            double newPrice;
                            DateTime newPubDate;

                            if (JournalTitleTxt.Text != "")
                                newTitle = JournalTitleTxt.Text;
                            else
                                newTitle = oldTitle;
                            if (EditorTxt.Text != "")
                                newEditor = EditorTxt.Text;
                            else
                                newEditor = oldEditor;

                            if (GenreTxt.Text != "")
                            {
                                if (!Journal.JournalGenres.Contains(GenreTxt.Text))
                                    throw new GenreException("The Genre you entered isn't one of the known genres");
                                newGenre = GenreTxt.Text;
                            }
                            else
                                newGenre = oldGenre;

                            if (JournalEditionTxt.Text != "")
                                newEditionNum = int.Parse(JournalEditionTxt.Text);
                            else
                                newEditionNum = oldEditionNum;
                            if (PriceTxt.Text != "")
                                newPrice = double.Parse(PriceTxt.Text);
                            else
                                newPrice = oldPrice;
                            if (CalendarDtPck.Date != null)
                                newPubDate = DateTime.Parse(CalendarDtPck.Date.ToString());
                            else
                                newPubDate = oldPubDate;

                            var newJournal = new Journal(newTitle, newPubDate, newPrice, oldDiscountLevel);
                            newJournal.Editor = newEditor;
                            newJournal.Genre = newGenre;
                            newJournal.JournalEditionNumber = newEditionNum;
                            newJournal.ID = oldID;

                            if (comboFrequency.SelectedIndex == -1)
                                newJournal.Frequency = oldFrequency;
                            else if (comboFrequency.SelectedIndex == 0)
                                newJournal.Frequency = JournalFrequency.Daily;
                            else if (comboFrequency.SelectedIndex == 1)
                                newJournal.Frequency = JournalFrequency.Weekly;
                            else if (comboFrequency.SelectedIndex == 2)
                                newJournal.Frequency = JournalFrequency.BiWeekly;
                            else if (comboFrequency.SelectedIndex == 3)
                                newJournal.Frequency = JournalFrequency.Monthly;
                            else if (comboFrequency.SelectedIndex == 4)
                                newJournal.Frequency = JournalFrequency.BiMonthly;
                            else if (comboFrequency.SelectedIndex == 5)
                                newJournal.Frequency = JournalFrequency.Quarterly;
                            else if (comboFrequency.SelectedIndex == 6)
                                newJournal.Frequency = JournalFrequency.SemiAnnually;
                            else if (comboFrequency.SelectedIndex == 7)
                                newJournal.Frequency = JournalFrequency.Annually;
                            else if (comboFrequency.SelectedIndex == 8)
                                newJournal.Frequency = JournalFrequency.BiAnnually;

                            DataMock.Instance.JournalItems.RemoveAt(i);
                            DataMock.Instance.JournalItems.Add(newJournal);
                            UpdatingListView();
                            break;
                        }
                    }
                }
                catch (FormatException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (GenreException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
            }
        }
        private async void btnDeleteJournal_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddDeleteItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to delete journals").ShowAsync();
            }
            else
            {
                try
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    for (int i = 0; i < DataMock.Instance.JournalItems.Count; i++)
                    {
                        if (DataMock.Instance.JournalItems[i].ID == objGuid)
                        {
                            DataMock.Instance.JournalItems.RemoveAt(i);
                            UpdatingListView();
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
        private async void btnCreateDis_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToCreateDiscount == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to create a discount").ShowAsync();
            }
            else
            {
                try
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxIDDis.Text);
                    for (int i = 0; i < DataMock.Instance.JournalItems.Count; i++)
                    {
                        if (DataMock.Instance.JournalItems[i].ID == objGuid)
                        {
                            DataMock.Instance.JournalItems[i].DiscountLevel = int.Parse(txtDisLevel.Text);
                            double discountLevel = DataMock.Instance.JournalItems[i].DiscountLevel;
                            double price = DataMock.Instance.JournalItems[i].Price;

                            if (discountLevel == 0)
                                DataMock.Instance.JournalItems[i].ActualPrice = price;
                            else if (discountLevel < 0 || discountLevel > 99)
                            {
                                MessageDialog d1 = new MessageDialog("You entered invalid discount level number");
                                d1.ShowAsync();
                            }
                            else
                                DataMock.Instance.JournalItems[i].ActualPrice = price * ((100 - discountLevel) / 100);

                            var newJournal = DataMock.Instance.JournalItems[i];
                            DataMock.Instance.JournalItems.RemoveAt(i);
                            UpdatingListView();
                            DataMock.Instance.JournalItems.Add(newJournal);
                            UpdatingListView();
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
        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboSort.SelectedIndex == 0)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.Title));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
            if (comboSort.SelectedIndex == 1)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.Title));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
            if (comboSort.SelectedIndex == 2)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.PublishDate));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
            if (comboSort.SelectedIndex == 3)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.PublishDate));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
            if (comboSort.SelectedIndex == 4)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.ActualPrice));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
            if (comboSort.SelectedIndex == 5)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.ActualPrice));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
            if (comboSort.SelectedIndex == 6)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderBy(i => i.Frequency));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
            if (comboSort.SelectedIndex == 7)
            {
                DataMock.Instance.JournalItems = new ObservableCollection<Journal>(DataMock.Instance.JournalItems.OrderByDescending(i => i.Frequency));
                DataMock.Instance.JournalItemsFiltered = new ObservableCollection<Journal>(DataMock.Instance.JournalItems);
                ListView2.ItemsSource = DataMock.Instance.JournalItemsFiltered;
            }
        }
        private void UpdatingListView()
        {
            var filtered = DataMock.Instance.JournalItems.Where(journal => Filter(journal));
            Remove_NonMatching(filtered);
            AddBack_Journals(filtered);
        }
        private void OnFilterChanged(object sender, TextChangedEventArgs args)
        {
            var filtered = DataMock.Instance.JournalItems.Where(journal => Filter(journal));
            Remove_NonMatching(filtered);
            AddBack_Journals(filtered);
        }
        private bool Filter(Journal journal)
        {
            return journal.Title.Contains(FilterByTitle.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Editor.Contains(FilterByEditor.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Genre.Contains(FilterByGenre.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.JournalEditionNumber.ToString().StartsWith(FilterByEdition.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.JournalEditionNumber.ToString().Contains(FilterByEdition.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.JournalEditionNumber.ToString().EndsWith(FilterByEdition.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Frequency.ToString().StartsWith(FilterByFrequency.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Frequency.ToString().Contains(FilterByFrequency.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Frequency.ToString().EndsWith(FilterByFrequency.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Price.ToString().StartsWith(FilterByPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Price.ToString().Contains(FilterByPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.Price.ToString().EndsWith(FilterByPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.ActualPrice.ToString().StartsWith(FilterByActualPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.ActualPrice.ToString().Contains(FilterByActualPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.ActualPrice.ToString().EndsWith(FilterByActualPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.DiscountLevel.ToString().StartsWith(FilterByDiscountLevel.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.DiscountLevel.ToString().Contains(FilterByDiscountLevel.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   journal.DiscountLevel.ToString().EndsWith(FilterByDiscountLevel.Text, StringComparison.InvariantCultureIgnoreCase);
        }
        private void Remove_NonMatching(IEnumerable<Journal> filteredData)
        {
            for (int i = DataMock.Instance.JournalItemsFiltered.Count - 1; i >= 0; i--)
            {
                var item = DataMock.Instance.JournalItemsFiltered[i];
                if (!filteredData.Contains(item))
                {
                    DataMock.Instance.JournalItemsFiltered.Remove(item);
                }
            }
        }
        private void AddBack_Journals(IEnumerable<Journal> filteredData)
        {
            foreach (var item in filteredData)
            {
                if (!DataMock.Instance.JournalItemsFiltered.Contains(item))
                {
                    DataMock.Instance.JournalItemsFiltered.Add(item);
                }
            }
        }
        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tb = new TextBlock();

            string listViewContent = "";
            foreach (Journal journal in DataMock.Instance.JournalItemsFiltered)
            {
                listViewContent += $"{journal.Title}\t\t\t\t{journal.PublishDate:Y}\t{journal.Editor}\t{journal.Genre}\t{journal.JournalEditionNumber}\t{journal.Frequency}\t" +
                    $"{journal.Price}\t{journal.ActualPrice}\t{journal.DiscountLevel}\t{journal.ID}\t\n";
            }
            var savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            savePicker.SuggestedFileName = "ListView Content";

            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteTextAsync(file, listViewContent);
                FileUpdateStatus status = await CachedFileManager.CompleteUpdatesAsync(file);
                if (status == FileUpdateStatus.Complete)
                    tb.Text = "File " + file.Name + " was saved.";
                else
                    tb.Text = "File " + file.Name + " couldn't be saved.";
            }
            else
                tb.Text = "Operation cancelled.";
        }
    }
}
