using bibModelJanuszewski.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliJanuszewski
{
    class DataGridDataSourceAuthors
    {
        public ObservableCollection<AuthorsAuthor> Authors { get; set; }
    }


    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class AuthorsPage : Page
    {
        private DataGridDataSourceAuthors AuthorsViewModel;

        private App app;
        private bool dataChanged = false;

        public AuthorsPage()
        {
            this.InitializeComponent();

            app = (App) App.Current;


            AuthorsViewModel = new DataGridDataSourceAuthors()
            {
                Authors = new ObservableCollection<AuthorsAuthor>((from author
                                                                   in app.BDLibraryUWP.authorsAuthorList
                                                                   orderby author.lastName
                                                                   select author
                                                                   ).ToList())

            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (dataChanged)
            {
                SaveAuthors();
            }
            base.OnNavigatingFrom(e);
        }

        void SaveAuthors()
        {
            Authors authors = new Authors()
            {
                Author = dataGrid.ItemsSource.Cast<AuthorsAuthor>().ToArray(),
            };

            app.BDLibraryUWP.Serialize(authors, BDLibraryUWP.fileNames.AUTHORS);
            app.BDLibraryUWP.authorsAuthorList = authors.Author.ToList();
            dataChanged = false;
        }

        private void AddAuthorButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ulong idNext = AuthorsViewModel.Authors.Max(x => x.id) + 1;
            AuthorsAuthor author = new AuthorsAuthor() { id = idNext };

            AuthorsViewModel.Authors.Add(author);
            dataChanged = true;
        }

        private void DeleteAuthorButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int id = dataGrid.SelectedIndex;
            if (id != -1)
            {
                AuthorsViewModel.Authors.RemoveAt(id);
                dataChanged = true;
            }
        }
    }
}
