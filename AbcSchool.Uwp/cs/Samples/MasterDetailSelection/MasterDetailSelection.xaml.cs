using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ABCSchool.Models;
using ABCSchool.Uwp.Services;
using ABCSchool.Uwp.ViewModels;

namespace ABCSchool.Uwp.Samples.MasterDetailSelection
{
    public sealed partial class MasterDetailSelection : Page
    {
        //public MainViewModel ViewModel => App.ViewModel;

        public MasterDetailSelection()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Details view can remove an item from the list.
            if (e.Parameter as string == "Delete")
            {
                DeleteItem(null, null);
            }
            base.OnNavigatedTo(e);
        }
        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            MasterListView.SelectionMode = ListViewSelectionMode.Single;
            MasterListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            RelativePanel.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Collapsed;
            DeleteItemBtn.Visibility = Visibility.Collapsed;
            CancelSelectionBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
        }

        
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MasterListView.SelectedItems.Count == 1)
            {
                MainViewModel.StudentViewModel = MasterListView.SelectedItem as StudentViewModel;
                MainViewModel.StudentViewModel.RefreshStudentsSubjects();
                MasterListView.SelectionMode = ListViewSelectionMode.Single;
                MasterListView.IsItemClickEnabled = true;
                DetailContentPresenter.Visibility = Visibility.Visible;
                RelativePanel.Visibility = Visibility.Collapsed;
                AddItemBtn.Visibility = Visibility.Visible;
                EditItemBtn.Visibility = Visibility.Visible;
                DeleteItemBtn.Visibility = Visibility.Visible;
                CancelSelectionBtn.Visibility = Visibility.Visible;
                SaveBtn.Visibility = Visibility.Collapsed;
            }
        }
        

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            MainViewModel.StudentViewModel = e.ClickedItem as StudentViewModel;
            MainViewModel.StudentViewModel.RefreshStudentsSubjects();
            MasterListView.SelectionMode = ListViewSelectionMode.Single;
            MasterListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Visible;
            RelativePanel.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Visible;
            DeleteItemBtn.Visibility = Visibility.Visible;
            CancelSelectionBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Collapsed;

        }
        

        #region Commands
        private void AddItem(object sender, RoutedEventArgs e)
        {
            MasterListView.IsItemClickEnabled = false;
            MainViewModel.StudentViewModel.IsInEdit = true;
            MainViewModel.StudentViewModel.IsNewStudent = true;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            RelativePanel.Visibility = Visibility.Visible;
            AddItemBtn.Visibility = Visibility.Collapsed;
            EditItemBtn.Visibility = Visibility.Collapsed;
            DeleteItemBtn.Visibility = Visibility.Collapsed;
            CancelSelectionBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Visible;
        }
        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (MainViewModel.StudentViewModel != null)
            {
                MainViewModel.StudentViewModel.DeleteAsync();
                MainViewModel.Students.Remove(MainViewModel.StudentViewModel);

                if (MasterListView.Items.Count > 0)
                {
                    MasterListView.SelectedIndex = 0;
                    MainViewModel.StudentViewModel = MasterListView.SelectedItem as StudentViewModel;
                }
                else
                {
                    // Details view is collapsed, in case there is not items.
                    DetailContentPresenter.Visibility = Visibility.Collapsed;
                    MainViewModel.StudentViewModel = null;
                }
            }
        }
        
        private void CancelSelection(object sender, RoutedEventArgs e)
        {
            MasterListView.SelectionMode = ListViewSelectionMode.Single;
            MasterListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            RelativePanel.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Collapsed;
            DeleteItemBtn.Visibility = Visibility.Collapsed;
            CancelSelectionBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
            MainViewModel.StudentViewModel.CancelEdit();
        }
        
        

        private void EditItemBtn_OnClickItem(object sender, RoutedEventArgs e)
        {
            if (MasterListView.SelectedIndex != -1)
            {
                if (MasterListView.Items.Count > 0)
                {
                    MainViewModel.StudentViewModel = MasterListView.SelectedItem as StudentViewModel;
                    MainViewModel.StudentViewModel.StartEdit();

                    //MasterListView.SelectionMode = ListViewSelectionMode.None;
                    MasterListView.IsItemClickEnabled = false;
                    DetailContentPresenter.Visibility = Visibility.Collapsed;
                    RelativePanel.Visibility = Visibility.Visible;
                    AddItemBtn.Visibility = Visibility.Collapsed;
                    EditItemBtn.Visibility = Visibility.Collapsed;
                    DeleteItemBtn.Visibility = Visibility.Visible;
                    CancelSelectionBtn.Visibility = Visibility.Visible;
                    SaveBtn.Visibility = Visibility.Visible;
                    
                }
            }
        }

        private void SaveBtn_OnClickSelection(object sender, RoutedEventArgs e)
        {
            if (MainViewModel != null) MainViewModel.StudentViewModel.EndEdit();
            //else
            //{
            //    ViewModel.StudentViewModel = new StudentViewModel(new Student{Email = RelativePanel.Children[0].});
            //}

            MasterListView.SelectionMode = ListViewSelectionMode.Single;
            MasterListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            RelativePanel.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Collapsed;
            DeleteItemBtn.Visibility = Visibility.Collapsed;
            CancelSelectionBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
        }

        #endregion
    }
}
