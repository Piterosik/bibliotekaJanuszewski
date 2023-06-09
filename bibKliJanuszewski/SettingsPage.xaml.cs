using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

//Szablon elementu Pusta strona jest udokumentowany na stronie https://go.microsoft.com/fwlink/?LinkId=234238

namespace bibKliJanuszewski
{
    /// <summary>
    /// Pusta strona, która może być używana samodzielnie lub do której można nawigować wewnątrz ramki.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        private void ThemeRB_Checked(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (Window.Current.Content is FrameworkElement rootElement && sender is RadioButton rb)
            {
                switch (rb.Name)
                {
                    case "SystemThemeRB":
                        rootElement.RequestedTheme = ElementTheme.Default;
                        break;
                    case "DarkThemeRB":
                        rootElement.RequestedTheme = ElementTheme.Dark;
                        break;
                    case "LightThemeRB":
                        rootElement.RequestedTheme = ElementTheme.Light;
                        break;
                }

                ApplicationData.Current.LocalSettings.Values["Theme"] = (int) rootElement.RequestedTheme;

            }
        }


    }
}
