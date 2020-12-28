using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ABCSchool
{
    public sealed partial class SamplesPane : UserControl
    {
        public SamplesPane()
        {
            this.InitializeComponent();
        }
        private void NavigateToMasterDetailSelection(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(ManageStudentPage));
        }
        private void NavigateToHome(object sender, RoutedEventArgs e)
        {
            ((Frame)Window.Current.Content).Navigate(typeof(MainPage));
        }

    }
}
