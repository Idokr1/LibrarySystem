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


    public sealed partial class BooksList : Page
    {
        public BooksList()
        {
            this.InitializeComponent();
            this.btnGoBackBooksList.Click += btnGoBackBooksList_Click;
            ListView1.ItemsSource = DataMock.Instance.BookItemsFiltered;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void btnGoBackBooksList_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame.CanGoBack)
                this.Frame.GoBack();
        }
        private async void btnAddBook_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddDeleteItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to add new books").ShowAsync();
            }
            else
            {
                try
                {
                    if (BookTitleTxt.Text == "" || AuthorTxt.Text == "" || PublisherTxt.Text == "")
                        throw new EmptyTBException("You have to fill all the TextBoxes");
                    if (ISBNTxt.Text.Length != 17)
                        throw new IsbnException("ISBN contains 17 characters - 13 numbers and 4 dashes");
                    if (!Book.BookGenres.Contains(GenreTxt.Text))
                        throw new GenreException("The Genre you entered isn't one of the known genres");

                    string title = BookTitleTxt.Text;
                    string pubDateString = CalendarDtPck.Date.ToString();
                    DateTime pubDate = DateTime.Parse(pubDateString);
                    double price = double.Parse(PriceTxt.Text);
                    var newBook = new Book(title, pubDate, price, 0);
                    newBook.ISBN = ISBNTxt.Text;
                    newBook.Author = AuthorTxt.Text;
                    newBook.Publisher = PublisherTxt.Text;
                    newBook.Edition = int.Parse(EditionTxt.Text);
                    newBook.Genre = GenreTxt.Text;

                    DataMock.Instance.BookItems.Add(newBook);
                    UpdatingListView();
                }
                catch (FormatException e1)
                {
                    MessageDialog d1 = new MessageDialog(e1.Message);
                    d1.ShowAsync();
                }
                catch (IsbnException e1)
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
            }
        }
        private async void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddDeleteItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to update books").ShowAsync();
            }
            else
            {
                try
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    for (int i = 0; i < DataMock.Instance.BookItems.Count; i++)
                    {
                        if (DataMock.Instance.BookItems[i].ID == objGuid)
                        {
                            string oldTitle = DataMock.Instance.BookItems[i].Title;
                            string oldISBN = DataMock.Instance.BookItems[i].ISBN;
                            string oldAuthor = DataMock.Instance.BookItems[i].Author;
                            string oldPublisher = DataMock.Instance.BookItems[i].Publisher;
                            int oldEdition = DataMock.Instance.BookItems[i].Edition;
                            string oldGenre = DataMock.Instance.BookItems[i].Genre;
                            double oldPrice = DataMock.Instance.BookItems[i].Price;
                            DateTime oldPubDate = DataMock.Instance.BookItems[i].PublishDate;
                            Guid oldID = DataMock.Instance.BookItems[i].ID;
                            double oldDiscountLevel = DataMock.Instance.BookItems[i].DiscountLevel;

                            string newTitle, newISBN, newAuthor, newPublisher, newGenre;
                            int newEdition;
                            double newPrice;
                            DateTime newPubDate;

                            if (BookTitleTxt.Text != "")
                                newTitle = BookTitleTxt.Text;
                            else
                                newTitle = oldTitle;

                            if (ISBNTxt.Text != "")
                            {
                                if (ISBNTxt.Text.Length != 17)
                                    throw new IsbnException("ISBN contains 17 characters - 13 numbers and 4 dashes");
                                newISBN = ISBNTxt.Text;
                            }
                            else
                                newISBN = oldISBN;

                            if (AuthorTxt.Text != "")
                                newAuthor = AuthorTxt.Text;
                            else
                                newAuthor = oldAuthor;
                            if (PublisherTxt.Text != "")
                                newPublisher = PublisherTxt.Text;
                            else
                                newPublisher = oldPublisher;

                            if (GenreTxt.Text != "")
                            {
                                if (!Book.BookGenres.Contains(GenreTxt.Text))
                                    throw new GenreException("The Genre you entered isn't one of the known genres");
                                newGenre = GenreTxt.Text;
                            }
                            else
                                newGenre = oldGenre;

                            if (EditionTxt.Text != "")
                                newEdition = int.Parse(EditionTxt.Text);
                            else
                                newEdition = oldEdition;
                            if (PriceTxt.Text != "")
                                newPrice = double.Parse(PriceTxt.Text);
                            else
                                newPrice = oldPrice;
                            if (CalendarDtPck.Date != null)
                                newPubDate = DateTime.Parse(CalendarDtPck.Date.ToString());
                            else
                                newPubDate = oldPubDate;

                            var newBook = new Book(newTitle, newPubDate, newPrice, oldDiscountLevel);
                            newBook.ISBN = newISBN;
                            newBook.Author = newAuthor;
                            newBook.Publisher = newPublisher;
                            newBook.Edition = newEdition;
                            newBook.Genre = newGenre;
                            newBook.ID = oldID;

                            DataMock.Instance.BookItems.RemoveAt(i);
                            DataMock.Instance.BookItems.Add(newBook);
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
                catch (IsbnException e1)
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
        private async void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (DataMock.Instance.loggedEmp.AllowedToAddDeleteItem == false)
            {
                await new MessageDialog($"{DataMock.Instance.loggedEmp.Name}, you are not allowed to delete books").ShowAsync();
            }
            else
            {
                try
                {
                    Guid objGuid = Guid.Empty;
                    objGuid = Guid.Parse(txtBoxID.Text);
                    for (int i = 0; i < DataMock.Instance.BookItems.Count; i++)
                    {
                        if (DataMock.Instance.BookItems[i].ID == objGuid)
                        {
                            DataMock.Instance.BookItems.RemoveAt(i);
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
                    for (int i = 0; i < DataMock.Instance.BookItems.Count; i++)
                    {
                        if (DataMock.Instance.BookItems[i].ID == objGuid)
                        {
                            DataMock.Instance.BookItems[i].DiscountLevel = int.Parse(txtDisLevel.Text);
                            double discountLevel = DataMock.Instance.BookItems[i].DiscountLevel;
                            double price = DataMock.Instance.BookItems[i].Price;

                            if (discountLevel == 0)
                                DataMock.Instance.BookItems[i].ActualPrice = price;
                            else if (discountLevel < 0 || discountLevel > 99)
                            {
                                MessageDialog d1 = new MessageDialog("You entered invalid discount level number");
                                d1.ShowAsync();
                            }
                            else
                                DataMock.Instance.BookItems[i].ActualPrice = price * ((100 - discountLevel) / 100);

                            var newBook = DataMock.Instance.BookItems[i];
                            DataMock.Instance.BookItems.RemoveAt(i);
                            UpdatingListView();
                            DataMock.Instance.BookItems.Add(newBook);
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
        private void ListView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void comboSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboSort.SelectedIndex == 0)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderBy(i => i.Title));
                DataMock.Instance.BookItemsFiltered = new ObservableCollection<Book>(DataMock.Instance.BookItems);
                ListView1.ItemsSource = DataMock.Instance.BookItemsFiltered;
            }
            if (comboSort.SelectedIndex == 1)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderByDescending(i => i.Title));
                DataMock.Instance.BookItemsFiltered = new ObservableCollection<Book>(DataMock.Instance.BookItems);
                ListView1.ItemsSource = DataMock.Instance.BookItemsFiltered;
            }
            if (comboSort.SelectedIndex == 2)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderByDescending(i => i.PublishDate));
                DataMock.Instance.BookItemsFiltered = new ObservableCollection<Book>(DataMock.Instance.BookItems);
                ListView1.ItemsSource = DataMock.Instance.BookItemsFiltered;
            }
            if (comboSort.SelectedIndex == 3)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderBy(i => i.PublishDate));
                DataMock.Instance.BookItemsFiltered = new ObservableCollection<Book>(DataMock.Instance.BookItems);
                ListView1.ItemsSource = DataMock.Instance.BookItemsFiltered;
            }
            if (comboSort.SelectedIndex == 4)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderBy(i => i.ActualPrice));
                DataMock.Instance.BookItemsFiltered = new ObservableCollection<Book>(DataMock.Instance.BookItems);
                ListView1.ItemsSource = DataMock.Instance.BookItemsFiltered;
            }
            if (comboSort.SelectedIndex == 5)
            {
                DataMock.Instance.BookItems = new ObservableCollection<Book>(DataMock.Instance.BookItems.OrderByDescending(i => i.ActualPrice));
                DataMock.Instance.BookItemsFiltered = new ObservableCollection<Book>(DataMock.Instance.BookItems);
                ListView1.ItemsSource = DataMock.Instance.BookItemsFiltered;
            }
        }
        private void UpdatingListView()
        {
            var filtered = DataMock.Instance.BookItems.Where(book => Filter(book));
            Remove_NonMatching(filtered);
            AddBack_Books(filtered);
        }
        private void OnFilterChanged(object sender, TextChangedEventArgs args)
        {
            var filtered = DataMock.Instance.BookItems.Where(book => Filter(book));
            Remove_NonMatching(filtered);
            AddBack_Books(filtered);
        }
        private bool Filter(Book book)
        {
            return book.Title.Contains(FilterByTitle.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.ISBN.Contains(FilterByISBN.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Author.Contains(FilterByAuthor.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Publisher.Contains(FilterByPublisher.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Genre.Contains(FilterByGenre.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Edition.ToString().StartsWith(FilterByEdition.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Edition.ToString().Contains(FilterByEdition.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Edition.ToString().EndsWith(FilterByEdition.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Price.ToString().StartsWith(FilterByPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Price.ToString().Contains(FilterByPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.Price.ToString().EndsWith(FilterByPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.ActualPrice.ToString().StartsWith(FilterByActualPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.ActualPrice.ToString().Contains(FilterByActualPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.ActualPrice.ToString().EndsWith(FilterByActualPrice.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.DiscountLevel.ToString().StartsWith(FilterByDiscountLevel.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.DiscountLevel.ToString().Contains(FilterByDiscountLevel.Text, StringComparison.InvariantCultureIgnoreCase) &&
                   book.DiscountLevel.ToString().EndsWith(FilterByDiscountLevel.Text, StringComparison.InvariantCultureIgnoreCase);
        }
        private void Remove_NonMatching(IEnumerable<Book> filteredData)
        {
            for (int i = DataMock.Instance.BookItemsFiltered.Count - 1; i >= 0; i--)
            {
                var item = DataMock.Instance.BookItemsFiltered[i];
                if (!filteredData.Contains(item))
                {
                    DataMock.Instance.BookItemsFiltered.Remove(item);
                }
            }
        }
        private void AddBack_Books(IEnumerable<Book> filteredData)
        {
            foreach (var item in filteredData)
            {
                if (!DataMock.Instance.BookItemsFiltered.Contains(item))
                {
                    DataMock.Instance.BookItemsFiltered.Add(item);
                }
            }
        }
        private async void btnExport_Click(object sender, RoutedEventArgs e)
        {
            TextBlock tb = new TextBlock();

            string listViewContent = "";
            foreach (Book book in DataMock.Instance.BookItemsFiltered)
            {
                listViewContent += $"{book.Title}\t{book.PublishDate:Y}\t\t{book.ISBN}\t\t{book.Author}\t{book.Publisher}\t{book.Edition}\t{book.Genre}\t{book.Price}\t" +
                    $"{book.ActualPrice}\t{book.DiscountLevel}\t{book.ID}\t\n";
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
