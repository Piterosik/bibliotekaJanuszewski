using bibModelJanuszewski.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliJanuszewski
{
    class DataGridDataSourceBooks
    {
        public ObservableCollection<BooksBook> Books { get; set; }
    }
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class BooksPage : Page
    {
        private DataGridDataSourceBooks BooksViewModel;


        private App app;
        private bool dataChanged = false;
        public BooksPage()
        {
            this.InitializeComponent();

            app = (App) App.Current;



            BooksViewModel = new DataGridDataSourceBooks()
            {
                Books = new ObservableCollection<BooksBook>((from book
                                                             in app.BDLibraryUWP.booksBookList
                                                             orderby book.title
                                                             select book).ToList())
            };


        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (dataChanged)
            {
                SaveBooks();
            }
            base.OnNavigatingFrom(e);
        }

        void SaveBooks()
        {
            Books books = new Books()
            {
                Book = dataGrid.ItemsSource.Cast<BooksBook>().ToArray(),
            };

            app.BDLibraryUWP.Serialize(books, BDLibraryUWP.fileNames.BOOKS);
            app.BDLibraryUWP.booksBookList = books.Book.ToList();
            dataChanged = false;
        }

        private void AddBookButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ulong idNext = BooksViewModel.Books.Max(x => x.id) + 1;
            BooksBook book = new BooksBook() { id = idNext };

            BooksViewModel.Books.Add(book);
            dataChanged = true;
        }

        private void DeleteBookButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int id = dataGrid.SelectedIndex;
            if (id != -1)
            {
                BooksViewModel.Books.RemoveAt(id);
                dataChanged = true;
            }
        }
    }
}

