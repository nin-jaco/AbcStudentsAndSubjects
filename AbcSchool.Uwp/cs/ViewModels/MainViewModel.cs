using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Models;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ABCSchool.Uwp.ViewModels
{
    /// <summary>
    /// Provides data and commands accessible to the entire app.  
    /// </summary>
    public class MainViewModel : BindableBase, IEditableObject
    {
        public MainViewModel()
        {
            this.Students = new ObservableCollection<StudentViewModel>();
            this.Subjects = new ObservableCollection<SubjectViewModel>();
            this.StudentViewModel = new StudentViewModel(new Student());
            this.SubjectViewModel = new SubjectViewModel(new Subject());
        }


        public ObservableCollection<StudentViewModel> Students { get; } 

        public ObservableCollection<SubjectViewModel> Subjects { get; } 


        private StudentViewModel _studentViewModel;
        public StudentViewModel StudentViewModel
        {
            get => _studentViewModel;
            set => Set(ref _studentViewModel, value);
        }

        private SubjectViewModel _subjectViewModel;
        public SubjectViewModel SubjectViewModel
        {
            get => _subjectViewModel ?? new SubjectViewModel(new Subject());
            set => Set(ref _subjectViewModel, value);
        }



        private bool _isLoading = false;
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        public async void GetAllStudents() => await GetAllStudentsAsync();
        public async Task GetAllStudentsAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
            {
                IsLoading = true;
                var students = await App.StudentService.GetAllAsync();
                if (students != null)
                {
                    Students.Clear();
                    foreach (var c in students)
                    {
                        Students.Add(new StudentViewModel(c));
                    }
                }
                
                IsLoading = false;
            });

        }

        public async void GetAllSubjects() => await GetAllSubjectsAsync();
        public async Task GetAllSubjectsAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(async () =>
            {
                IsLoading = true;
                var subjects = await App.SubjectService.GetAllAsync();
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
        /*public void Sync()
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
        }*/
        public void BeginEdit()
        {
            throw new System.NotImplementedException();
        }

        public void CancelEdit()
        {
            throw new System.NotImplementedException();
        }

        public void EndEdit()
        {
            throw new System.NotImplementedException();
        }
    }
}