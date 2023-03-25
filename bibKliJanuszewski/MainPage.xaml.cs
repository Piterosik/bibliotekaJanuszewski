using System;
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
            Frame.Navigate(typeof(SettingsPage));
        }

        private void btHelp_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
        }
    }
}
