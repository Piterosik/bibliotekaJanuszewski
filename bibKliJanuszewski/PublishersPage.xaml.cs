using bibModelJanuszewski.Model;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliJanuszewski
{
    class DataGridDataSourcePublishers
    {
        public ObservableCollection<PublishersPublisher> Publishers { get; set; }
    }

    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class PublishersPage : Page
    {
        private DataGridDataSourcePublishers PublishersViewModel;

        private App app;
        private bool dataChanged = false;
        public PublishersPage()
        {
            this.InitializeComponent();
            app = (App) App.Current;

            PublishersViewModel = new DataGridDataSourcePublishers()
            {
                Publishers = new ObservableCollection<PublishersPublisher>((from publisher
                                                                           in app.BDLibraryUWP.publishersPublisherList
                                                                            orderby publisher.name
                                                                            select publisher).ToList())
            };
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (dataChanged)
            {
                SavePublishers();
            }
            base.OnNavigatingFrom(e);
        }

        void SavePublishers()
        {
            Publishers publishers = new Publishers()
            {
                Publisher = dataGrid.ItemsSource.Cast<PublishersPublisher>().ToArray(),
            };

            app.BDLibraryUWP.Serialize(publishers, BDLibraryUWP.fileNames.PUBLISHERS);
            app.BDLibraryUWP.publishersPublisherList = publishers.Publisher.ToList();
            dataChanged = false;
        }

        private void AddPublisherButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ulong idNext = PublishersViewModel.Publishers.Max(x => x.id) + 1;
            PublishersPublisher publisher = new PublishersPublisher() { id = idNext };

            PublishersViewModel.Publishers.Add(publisher);
            dataChanged = true;
        }

        private void DeletePublisherButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            int id = dataGrid.SelectedIndex;
            if (id != -1)
            {
                PublishersViewModel.Publishers.RemoveAt(id);
                dataChanged = true;
            }
        }
    }
}
