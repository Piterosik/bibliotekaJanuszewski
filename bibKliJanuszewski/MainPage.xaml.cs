using System;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x415

namespace bibKliJanuszewski
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            if (Window.Current.Content is FrameworkElement rootElement)
            {
                ElementTheme elementTheme = ApplicationData.Current.LocalSettings.Values["Theme"] != null ? (ElementTheme) ApplicationData.Current.LocalSettings.Values["Theme"] : ElementTheme.Default;
                rootElement.RequestedTheme = elementTheme;

                App app = (App) App.Current;
                app.BDLibraryUWP.TestData();
            }

        }

        private void btAuthors_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }

        private async void btWebsite_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://ukw.edu.pl"));
        }

        private void btSettings_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (frmMain.CurrentSourcePageType != typeof(SettingsPage))
                frmMain.Navigate(typeof(SettingsPage));
        }

        private void btHelp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (frmMain.CurrentSourcePageType != typeof(HelpPage))
                frmMain.Navigate(typeof(HelpPage));
        }

        private void NavView_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (frmMain.CanGoBack)
                frmMain.GoBack();
        }

        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                frmMain.Navigate(typeof(SettingsPage));
            }
            else
                switch (args.InvokedItemContainer.Name)
                {
                    case "AuthorListMenuItem":
                        frmMain.Navigate(typeof(AuthorsPage));
                        break;
                    case "PublisherListMenuItem":
                        frmMain.Navigate(typeof(PublishersPage));
                        break;
                    case "BookListMenuItem":
                        frmMain.Navigate(typeof(BooksPage));
                        break;
                    default: break;
                }
        }
    }
}
