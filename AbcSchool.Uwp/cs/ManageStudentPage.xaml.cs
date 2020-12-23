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

namespace ABCSchool
{
    public sealed partial class ManageStudentPage : Page
    {
        public ManageStudentPage()
        {
            this.InitializeComponent();
            MainViewModel = App.
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
            StudentListView.SelectionMode = ListViewSelectionMode.Single;
            StudentListView.IsItemClickEnabled = true;
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
            if (StudentListView.SelectedItems.Count == 1)
            {
                App.MainViewModel.StudentViewModel = StudentListView.SelectedItem as StudentViewModel;
                //MainViewModel.RefreshSubjectList();
                StudentListView.SelectionMode = ListViewSelectionMode.Single;
                StudentListView.IsItemClickEnabled = true;
                DetailContentPresenter.Visibility = Visibility.Visible;
                RelativePanel.Visibility = Visibility.Collapsed;
                AddItemBtn.Visibility = Visibility.Visible;
                EditItemBtn.Visibility = Visibility.Visible;
                DeleteItemBtn.Visibility = Visibility.Visible;
                CancelSelectionBtn.Visibility = Visibility.Visible;
                SaveBtn.Visibility = Visibility.Collapsed;
            }
        }

        #region Commands
        private void AddItem(object sender, RoutedEventArgs e)
        {
            App.MainViewModel.StudentViewModel = new StudentViewModel();
            //MainViewModel.RefreshSubjectList();
            StudentListView.IsItemClickEnabled = false;
            App.MainViewModel.StudentViewModel.IsInEdit = true;
            App.MainViewModel.StudentViewModel.IsNewStudent = true;
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
            if (App.MainViewModel.StudentViewModel != null)
            {
                App.MainViewModel.StudentViewModel.DeleteAsync();
                App.MainViewModel.Students.Remove(App.MainViewModel.StudentViewModel);

                if (StudentListView.Items.Count > 0)
                {
                    StudentListView.SelectedIndex = 0;
                    App.MainViewModel.StudentViewModel = StudentListView.SelectedItem as StudentViewModel;
                }
                else
                {
                    // Details view is collapsed, in case there is not items.
                    DetailContentPresenter.Visibility = Visibility.Collapsed;
                    App.MainViewModel.StudentViewModel = null;
                }
            }
        }
        
        private void CancelSelection(object sender, RoutedEventArgs e)
        {
            StudentListView.SelectionMode = ListViewSelectionMode.Single;
            StudentListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            RelativePanel.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Collapsed;
            DeleteItemBtn.Visibility = Visibility.Collapsed;
            CancelSelectionBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
            App.MainViewModel.StudentViewModel.CancelEdit();
        }
        
        private void EditItemBtn_OnClickItem(object sender, RoutedEventArgs e)
        {
            if (StudentListView.SelectedIndex != -1)
            {
                if (StudentListView.Items.Count > 0)
                {
                    App.MainViewModel.StudentViewModel = StudentListView.SelectedItem as StudentViewModel;
                    if (App.MainViewModel.StudentViewModel != null) App.MainViewModel.StudentViewModel.StartEdit();

                    //MasterListView.SelectionMode = ListViewSelectionMode.None;
                    StudentListView.IsItemClickEnabled = false;
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
            if (App.MainViewModel?.StudentViewModel != null) App.MainViewModel.StudentViewModel.EndEdit();
            
            StudentListView.SelectionMode = ListViewSelectionMode.Single;
            StudentListView.IsItemClickEnabled = true;
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
