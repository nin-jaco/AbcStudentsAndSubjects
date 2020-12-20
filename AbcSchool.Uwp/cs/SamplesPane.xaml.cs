using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using ABCSchool.Uwp.Samples.MasterDetailSelection;

namespace ABCSchool.Uwp
{
    public sealed partial class SamplesPane : UserControl
    {
        public SamplesPane()
        {
            this.InitializeComponent();
        }
        private void NavigateToMasterDetailSelection(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MasterDetailSelection));
        }
        private void NavigateToHome(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

    }
}
