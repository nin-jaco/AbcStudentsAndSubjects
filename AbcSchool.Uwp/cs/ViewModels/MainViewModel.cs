using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Models;
using ABCSchool.Uwp.Model;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ABCSchool.Uwp.ViewModels
{
    /// <summary>
    /// Provides data and commands accessible to the entire app.  
    /// </summary>
    public class MainViewModel : BindableBase
    {
        /// <summary>
        /// Creates a new MainViewModel.
        /// </summary>
        public MainViewModel()
        {
            Task.Run(GetStudentListAsync);
            //Task.Run(GetSubjectListAsync);
            StudentViewModel = new StudentViewModel(new Student());
            StudentViewModel.CheckList = new List<CheckListItem>();
        }

        

        /// <summary>
        /// The collection of students in the list. 
        /// </summary>
        public ObservableCollection<StudentViewModel> Students { get; }
            = new ObservableCollection<StudentViewModel>();


        //private StudentViewModel _selectedStudent;
        //public StudentViewModel SelectedStudent
        //{
        //    get => _selectedStudent;
        //    set => Set(ref _selectedStudent , value);
        //}

        private StudentViewModel _studentViewModel;
        public StudentViewModel StudentViewModel
        {
            get => _studentViewModel;
            set => Set(ref _studentViewModel, value);
        }



        private ObservableCollection<SubjectViewModel> _selectedSubjects;
        public ObservableCollection<SubjectViewModel> SelectedSubjects
        {
            get => _selectedSubjects;
            set => Set(ref _selectedSubjects, value);
        }

        //private ObservableCollection<SubjectViewModel> _allSubjects;
        //public ObservableCollection<SubjectViewModel> AllSubjects
        //{
        //    get => _allSubjects;
        //    set => Set(ref _allSubjects, value);
        //}

        private bool _isLoading = false;

        /// <summary>
        /// Gets or sets a value indicating whether the Students list is currently being updated. 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        /// <summary>
        /// Gets the complete list of Students from the database.
        /// </summary>
        public async Task GetStudentListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            var students = await App.StudentService.GetAllAsync();
            if (students == null)
            {
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Students.Clear();
                foreach (var c in students)
                {
                    Students.Add(new StudentViewModel(c));
                }
                IsLoading = false;
            });
        }

        //public async Task GetSubjectListAsync()
        //{
        //    await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

        //    var subjects = await App.SubjectService.GetAllAsync();
        //    if (subjects == null)
        //    {
        //        return;
        //    }

        //    await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
        //    {
        //        AllSubjects.Clear();
        //        foreach (var c in subjects)
        //        {
        //            AllSubjects.Add(new SubjectViewModel(c));
        //        }
        //        IsLoading = false;
        //    });
        //}

        public StudentViewModel CreateEmptyModel()
        {
            return new StudentViewModel(new Student()) { IsNewStudent = true };
        }

        /// <summary>
        /// Saves any modified Students and reloads the student list from the database.
        /// </summary>
        public void Sync()
        {
            Task.Run(async () =>
            {
                IsLoading = true;
                foreach (var modifiedStudent in Students
                    .Where(customer => customer.IsModified).Select(student => student.Model))
                {
                    await App.StudentService.PutAsJsonAsync(modifiedStudent);
                }

                await GetStudentListAsync();
                IsLoading = false;
            });
        }
    }
}