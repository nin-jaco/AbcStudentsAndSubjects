﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;
using ABCSchool.Models;
using ABCSchool.Uwp.Model;
using ABCSchool.Uwp.Services;

namespace ABCSchool.Uwp.Samples.MasterDetailSelection
{
    public sealed partial class MasterDetailSelection : Page
    {
        //public MainViewModel SelectedStudent { get; set; }
        //private StudentService StudentService { get; } = new StudentService();

        //public ObservableCollection<Student> Students { get; }

        public MainViewModel ViewModel => App.ViewModel;

        public MasterDetailSelection()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            /*List<Student> students = new List<Student>(); 
            Task task = Task.Run(async () =>
            {
                students = await StudentService.GetAllAsync();
            });

            task.Wait();
            Students = new ObservableCollection<Student>(students);
            if (Students.Count > 0)
            {
                MasterListView.ItemsSource = Students;
            }*/
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
            EditContentPresenter.Visibility = Visibility.Collapsed;
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
                ViewModel.SelectedStudent = MasterListView.SelectedItem as StudentViewModel;
                MasterListView.SelectionMode = ListViewSelectionMode.Single;
                MasterListView.IsItemClickEnabled = true;
                DetailContentPresenter.Visibility = Visibility.Visible;
                EditContentPresenter.Visibility = Visibility.Collapsed;
                AddItemBtn.Visibility = Visibility.Visible;
                EditItemBtn.Visibility = Visibility.Visible;
                DeleteItemBtn.Visibility = Visibility.Visible;
                CancelSelectionBtn.Visibility = Visibility.Visible;
                SaveBtn.Visibility = Visibility.Collapsed;
            }
        }
        

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            ViewModel.SelectedStudent = e.ClickedItem as StudentViewModel;
            MasterListView.SelectionMode = ListViewSelectionMode.Single;
            MasterListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Visible;
            EditContentPresenter.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Visible;
            DeleteItemBtn.Visibility = Visibility.Visible;
            CancelSelectionBtn.Visibility = Visibility.Visible;
            SaveBtn.Visibility = Visibility.Collapsed;

        }
        

        #region Commands
        private void AddItem(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedStudent = new StudentViewModel();
            ViewModel.SelectedStudent.StartEdit();
            ViewModel.SelectedStudent.IsNewStudent = true;
            MasterListView.IsItemClickEnabled = false;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            EditContentPresenter.Visibility = Visibility.Visible;
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
                ViewModel.SelectedStudent.
                ViewModel.Students.Remove(ViewModel.SelectedStudent);

                if (MasterListView.Items.Count > 0)
                {
                    MasterListView.SelectedIndex = 0;
                    ViewModel.SelectedStudent = MasterListView.SelectedItem as StudentViewModel;
                }
                else
                {
                    // Details view is collapsed, in case there is not items.
                    DetailContentPresenter.Visibility = Visibility.Collapsed;
                    ViewModel.SelectedStudent = null;
                }
            }
        }
        
        private void CancelSelection(object sender, RoutedEventArgs e)
        {
            MasterListView.SelectionMode = ListViewSelectionMode.Single;
            MasterListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            EditContentPresenter.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Collapsed;
            DeleteItemBtn.Visibility = Visibility.Collapsed;
            CancelSelectionBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
            ViewModel.SelectedStudent.CancelEdit();
        }
        
        #endregion

        private void EditItemBtn_OnClickItem(object sender, RoutedEventArgs e)
        {
            if (MasterListView.SelectedIndex != -1)
            {
                if (MasterListView.Items.Count > 0)
                {
                    ViewModel.SelectedStudent = MasterListView.SelectedItem as StudentViewModel;
                    ViewModel.SelectedStudent.StartEdit();

                    //MasterListView.SelectionMode = ListViewSelectionMode.None;
                    MasterListView.IsItemClickEnabled = false;
                    DetailContentPresenter.Visibility = Visibility.Collapsed;
                    EditContentPresenter.Visibility = Visibility.Visible;
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
            //var a = ViewModel.SelectedStudent;
            //ViewModel.Sync();
            ViewModel.SelectedStudent.EndEdit();
            MasterListView.SelectionMode = ListViewSelectionMode.Single;
            MasterListView.IsItemClickEnabled = true;
            DetailContentPresenter.Visibility = Visibility.Collapsed;
            EditContentPresenter.Visibility = Visibility.Collapsed;
            AddItemBtn.Visibility = Visibility.Visible;
            EditItemBtn.Visibility = Visibility.Collapsed;
            DeleteItemBtn.Visibility = Visibility.Collapsed;
            CancelSelectionBtn.Visibility = Visibility.Collapsed;
            SaveBtn.Visibility = Visibility.Collapsed;
        }
    }
}