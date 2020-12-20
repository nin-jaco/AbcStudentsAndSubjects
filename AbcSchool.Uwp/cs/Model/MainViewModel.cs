using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Models;
using ABCSchool.Uwp.Services;
using ABCSchool.Uwp.ViewModels;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ABCSchool.Uwp.Model
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
            Task.Run(GetSubjectListAsync);
        }

        

        /// <summary>
        /// The collection of students in the list. 
        /// </summary>
        public ObservableCollection<StudentViewModel> Students { get; }
            = new ObservableCollection<StudentViewModel>();

        public ObservableCollection<SubjectViewModel> Subjects { get; }
            = new ObservableCollection<SubjectViewModel>();

        private StudentViewModel _selectedStudent;
        /// <summary>
        /// Gets or sets the selected customer, or null if no customer is selected. 
        /// </summary>
        public StudentViewModel SelectedStudent
        {
            get => _selectedStudent;
            set => Set(ref _selectedStudent, value);
        }

        private StudentViewModel _newEditStudent;
        /// <summary>
        /// Gets or sets the selected customer, or null if no customer is selected. 
        /// </summary>
        public StudentViewModel NewEditStudent
        {
            get => _newEditStudent ?? SelectedStudent ?? new StudentViewModel(new Student());
            set => Set(ref _newEditStudent, value);
        }

        private SubjectViewModel _selectedSubject;
        public SubjectViewModel SelectedSubject
        {
            get => _selectedSubject;
            set => Set(ref _selectedSubject, value);
        }

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

        public async Task GetSubjectListAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() => IsLoading = true);

            var subjects = await App.SubjectService.GetAllAsync();
            if (subjects == null)
            {
                return;
            }

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                Subjects.Clear();
                foreach (var c in subjects)
                {
                    Subjects.Add(new SubjectViewModel(c));
                }
                IsLoading = false;
            });
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
                    .Where(customer => customer.IsModified).Select(student => student.StudentModel))
                {
                    await App.StudentService.PutAsJsonAsync(modifiedStudent);
                }

                await GetStudentListAsync();
                IsLoading = false;
            });
        }
    }
}