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

namespace ABCSchool.Uwp.Samples.MasterDetailSelection
{
    public sealed partial class MasterDetailSelection : Page
    {
        private Student _selectedStudent;
        private StudentService _studentService { get; } = new StudentService();

        private ObservableCollection<Student> _students;

        public MasterDetailSelection()
        {
            this.InitializeComponent();
            this.Loaded += OnLoaded;
            List<Student> students = new List<Student>(); 
            Task task = Task.Run(async () =>
            {
                students = await _studentService.GetAllAsync();
            });

            task.Wait();
            _students = new ObservableCollection<Student>(students);
            if (_students.Count > 0)
            {
                MasterListView.ItemsSource = _students;
            }
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
            if (_selectedStudent == null && _students.Count > 0)
            {
                _selectedStudent = _students[0];
                MasterListView.SelectedIndex = 0;
            }
            // If the app starts in narrow mode - showing only the Master listView - 
            // it is necessary to set the commands and the selection mode.
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                VisualStateManager.GoToState(this, MasterState.Name, true);
            }
            else if (PageSizeStatesGroup.CurrentState == WideState)
            {
                // In this case, the app starts is wide mode, Master/Details view, 
                // so it is necessary to set the commands and the selection mode.
                VisualStateManager.GoToState(this, MasterDetailsState.Name, true);
                MasterListView.SelectionMode = ListViewSelectionMode.Extended;
                MasterListView.SelectedItem = _selectedStudent;
            }
            else
            {
                new InvalidOperationException();
            }
        }
        private void OnCurrentStateChanged(object sender, VisualStateChangedEventArgs e)
        {
            bool isNarrow = e.NewState == NarrowState;
            if (isNarrow)
            {
                Frame.Navigate(typeof(DetailsPage), _selectedStudent, new SuppressNavigationTransitionInfo());
            }
            else
            {
                VisualStateManager.GoToState(this, MasterDetailsState.Name, true);
                MasterListView.SelectionMode = ListViewSelectionMode.Extended;
                MasterListView.SelectedItem = _selectedStudent;
            }

            EntranceNavigationTransitionInfo.SetIsTargetElement(MasterListView, isNarrow);
            if (DetailContentPresenter != null)
            {
                EntranceNavigationTransitionInfo.SetIsTargetElement(DetailContentPresenter, !isNarrow);
            }
        }
        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PageSizeStatesGroup.CurrentState == WideState)
            {
                if (MasterListView.SelectedItems.Count == 1)
                {
                    _selectedStudent = MasterListView.SelectedItem as Student;
                    EnableContentTransitions();
                }
                // Entering in Extended selection
                else if (MasterListView.SelectedItems.Count > 1
                     && MasterDetailsStatesGroup.CurrentState == MasterDetailsState)
                {
                    VisualStateManager.GoToState(this, ExtendedSelectionState.Name, true);
                }
            }
            // Exiting Extended selection
            if (MasterDetailsStatesGroup.CurrentState == ExtendedSelectionState &&
                MasterListView.SelectedItems.Count == 1)
            {
                VisualStateManager.GoToState(this, MasterDetailsState.Name, true);
            }
        }
        // ItemClick event only happens when user is a Master state. In this state, 
        // selection mode is none and click event navigates to the details view.
        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            // The clicked item it is the new selected Student
            _selectedStudent = e.ClickedItem as Student;
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                // Go to the details page and display the item 
                Frame.Navigate(typeof(DetailsPage), _selectedStudent, new DrillInNavigationTransitionInfo());
            }
            //else
            {
                // Play a refresh animation when the user switches detail items.
                //EnableContentTransitions();
            }
        }
        private void EnableContentTransitions()
        {
            DetailContentPresenter.ContentTransitions.Clear();
            DetailContentPresenter.ContentTransitions.Add(new EntranceThemeTransition());
        }
        #region Commands
        private void AddItem(object sender, RoutedEventArgs e)
        {
            //Student c = new Student();
            //_students.Add(c);
            Frame.Navigate(typeof(AddItemPage), new Student(), new SuppressNavigationTransitionInfo());

            // Select this item in case that the list is empty
            if (MasterListView.SelectedIndex == -1)
            {
                MasterListView.SelectedIndex = 0;
                _selectedStudent = MasterListView.SelectedItem as Student;
                // Details view is collapsed, in case there is not items.
                // You should show it just in case. 
                DetailContentPresenter.Visibility = Visibility.Visible;
            }
        }
        private void DeleteItem(object sender, RoutedEventArgs e)
        {
            if (_selectedStudent != null)
            {
                _students.Remove(_selectedStudent);

                if (MasterListView.Items.Count > 0)
                {
                    MasterListView.SelectedIndex = 0;
                    _selectedStudent = MasterListView.SelectedItem as Student;
                }
                else
                {
                    // Details view is collapsed, in case there is not items.
                    DetailContentPresenter.Visibility = Visibility.Collapsed;
                    _selectedStudent = null;
                }
            }
        }
        private void DeleteItems(object sender, RoutedEventArgs e)
        {
            if (MasterListView.SelectedIndex != -1)
            {
                List<Student> selectedItems = new List<Student>();
                foreach (Student student in MasterListView.SelectedItems)
                {
                    selectedItems.Add(student);
                }
                foreach (Student student in selectedItems)
                {
                    _students.Remove(student);
                }
                if (MasterListView.Items.Count > 0)
                {
                    MasterListView.SelectedIndex = 0;
                    _selectedStudent = MasterListView.SelectedItem as Student;
                }
                else
                {
                    DetailContentPresenter.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void SelectItems(object sender, RoutedEventArgs e)
        {
            if (MasterListView.Items.Count > 0)
            {
                VisualStateManager.GoToState(this, MultipleSelectionState.Name, true);
            }
        }
        private void CancelSelection(object sender, RoutedEventArgs e)
        {
            if (PageSizeStatesGroup.CurrentState == NarrowState)
            {
                VisualStateManager.GoToState(this, MasterState.Name, true);
            }
            else
            {
                VisualStateManager.GoToState(this, MasterDetailsState.Name, true);
            }
        }
        private void ShowSplitView(object sender, RoutedEventArgs e)
        { 
            // Clearing the cache
            int cacheSize = ((Frame)Parent).CacheSize;
            ((Frame)Parent).CacheSize = 0;
            ((Frame)Parent).CacheSize = cacheSize;

            MySamplesPane.SamplesSplitView.IsPaneOpen = !MySamplesPane.SamplesSplitView.IsPaneOpen;
        }
        #endregion

        private void EditItemBtn_OnClickItem(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AddItemPage), _selectedStudent, new SuppressNavigationTransitionInfo());
        }
    }
}
