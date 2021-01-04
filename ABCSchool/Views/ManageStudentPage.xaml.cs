using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ABCSchool.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ABCSchool.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManageStudentPage : Page
    {
        /// <summary>
        /// Gets the app-wide ViewModel instance.
        /// </summary>
        public MainViewModel ViewModel => App.ViewModel;

        public ManageStudentPage()
        {
            this.InitializeComponent();
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
                ViewModel.SelectedStudent = StudentListView.SelectedItem as StudentViewModel;
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
            ViewModel.SelectedStudent = new StudentViewModel();
            //MainViewModel.RefreshSubjectList();
            StudentListView.IsItemClickEnabled = false;
            ViewModel.SelectedStudent.IsInEdit = true;
            ViewModel.SelectedStudent.IsNewStudent = true;
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
            if (ViewModel.SelectedStudent != null)
            {
                ViewModel.SelectedStudent.DeleteAsync();
                if (ViewModel.Students.Remove(ViewModel.SelectedStudent))
                {
                    if (StudentListView.Items?.Count > 0)
                    {
                        StudentListView.SelectedIndex = 0;
                        ViewModel.SelectedStudent = StudentListView.SelectedItem as StudentViewModel;
                    }
                    else
                    {
                        // Details view is collapsed, in case there is not items.
                        DetailContentPresenter.Visibility = Visibility.Collapsed;
                        ViewModel.SelectedStudent = null;
                    }
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
            ViewModel.SelectedStudent.CancelEdit();
        }

        private void EditItemBtn_OnClickItem(object sender, RoutedEventArgs e)
        {
            if (StudentListView.SelectedIndex != -1)
            {
                if (StudentListView.Items?.Count > 0)
                {
                    ViewModel.SelectedStudent = StudentListView.SelectedItem as StudentViewModel;
                    ViewModel.SelectedStudent?.StartEdit();

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
            ViewModel?.SelectedStudent?.EndEdit();
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
