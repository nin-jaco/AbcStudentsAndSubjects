﻿using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ABCSchool
{
    /// <summary>
    /// This is the landing page used to navigate to all different scenarios.  See Solution Explorer
    /// for the code of disctinct scenarios.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        private void ShowSplitView(object sender, RoutedEventArgs e)
        {
            SamplesPane.SamplesSplitView.IsPaneOpen = !SamplesPane.SamplesSplitView.IsPaneOpen;
        }
    }
}