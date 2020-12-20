using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using ABCSchool.Models;
using ABCSchool.Uwp.ViewModels;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ABCSchool.Uwp.Model
{
    /// <summary>
    /// Provides a bindable wrapper for the Student model class, encapsulating various services for access by the UI.
    /// </summary>
    public class StudentViewModel : BindableBase, IEditableObject
    {
        /// <summary>
        /// Initializes a new instance of the StudentViewModel class that wraps a Student object.
        /// </summary>
        public StudentViewModel(Student studentModel = null)
        {
            StudentModel = studentModel ?? new Student
            {
                Email = "", FirstName = "", Id = 0, LastName = "", Mobile = "",
                StudentsSubjects = new List<StudentsSubjects>()
            };
        }

        private Student _studentModel;

        /// <summary>
        /// Gets or sets the underlying Student object.
        /// </summary>
        public Student StudentModel
        {
            get => _studentModel;
            set
            {
                if (_studentModel != value)
                {
                    _studentModel = value;
                    RefreshStudentsSubjects();

                    // Raise the PropertyChanged event for all properties.
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the Student's first name.
        /// </summary>
        public string FirstName
        {
            get => StudentModel.FirstName;
            set
            {
                if (value != StudentModel.FirstName)
                {
                    StudentModel.FirstName = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Student's last name.
        /// </summary>
        public string LastName
        {
            get => StudentModel.LastName;
            set
            {
                if (value != StudentModel.LastName)
                {
                    StudentModel.LastName = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        
        /// <summary>
        /// Gets or sets the Student's phone number. 
        /// </summary>
        public string Mobile
        {
            get => StudentModel.Mobile;
            set
            {
                if (value != StudentModel.Mobile)
                {
                    StudentModel.Mobile = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Student's email. 
        /// </summary>
        public string Email
        {
            get => StudentModel.Email;
            set
            {
                if (value != StudentModel.Email)
                {
                    StudentModel.Email = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the underlying model has been modified. 
        /// </summary>
        /// <remarks>
        /// Used when sync'ing with the server to reduce load and only upload the models that have changed.
        /// </remarks>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets the collection of the Student's subjects.
        /// </summary>
        public ObservableCollection<StudentsSubjects> StudentsSubjects { get; } = new ObservableCollection<StudentsSubjects>();

        private StudentsSubjects _selectedStudentsSubjects;

        /// <summary>
        /// Gets or sets the currently selected subject.
        /// </summary>
        public StudentsSubjects SelectedStudentsSubjects
        {
            get => _selectedStudentsSubjects;
            set => Set(ref _selectedStudentsSubjects, value);
        }

        private bool _isLoading;

        /// <summary>
        /// Gets or sets a value that indicates whether to show a progress bar. 
        /// </summary>
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        private bool _isNewStudent;

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new Student.
        /// </summary>
        public bool IsNewStudent
        {
            get => _isNewStudent;
            set => Set(ref _isNewStudent, value);
        }

        private bool _isInEdit = false;

        /// <summary>
        /// Gets or sets a value that indicates whether the Student data is being edited.
        /// </summary>
        public bool IsInEdit
        {
            get => _isInEdit;
            set => Set(ref _isInEdit, value);
        }

        /// <summary>
        /// Saves Student data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;
            if (IsNewStudent)
            {
                IsNewStudent = false;
                App.ViewModel.Students.Add(this);
                await App.StudentService.PostAsJsonAsync(StudentModel);
                return;
            }

            await App.StudentService.PutAsJsonAsync(StudentModel);
        }

        /// <summary>
        /// Raised when the user cancels the changes they've made to the Student data.
        /// </summary>
        public event EventHandler AddNewStudentCanceled;

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
        public async Task CancelEditsAsync()
        {
            if (IsNewStudent)
            {
                AddNewStudentCanceled?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                await RevertChangesAsync();
            }
        }

        /// <summary>
        /// Discards any edits that have been made, restoring the original values.
        /// </summary>
        public async Task RevertChangesAsync()
        {
            IsInEdit = false;
            if (IsModified)
            {
                await RefreshStudentAsync();
                IsModified = false;
            }
        }

        /// <summary>
        /// Enables edit mode.
        /// </summary>
        public void StartEdit() => IsInEdit = true;

        /// <summary>
        /// Reloads all of the Student data.
        /// </summary>
        public async Task RefreshStudentAsync()
        {
            RefreshStudentsSubjects();
            StudentModel = await App.StudentService.GetByIdAsync(StudentModel.Id);
        }

        /// <summary>
        /// Resets the Student detail fields to the current values.
        /// </summary>
        public void RefreshStudentsSubjects() => Task.Run(LoadStudentsSubjectsAsync);

        /// <summary>
        /// Loads the subject data for the Student.
        /// </summary>
        public async Task LoadStudentsSubjectsAsync()
        {
            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                IsLoading = true;
            });

            var studentSubjects = await App.StudentSubjectService.GetByStudentIdAsync(StudentModel.Id);

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                StudentsSubjects.Clear();
                foreach (var subject in studentSubjects)
                {
                    StudentsSubjects.Add(subject);
                }

                IsLoading = false;
            });
        }

        /// <summary>
        /// Called when a bound DataGrid control causes the Student to enter edit mode.
        /// </summary>
        public void BeginEdit()
        {
            // Not used.
        }

        /// <summary>
        /// Called when a bound DataGrid control cancels the edits that have been made to a Student.
        /// </summary>
        public async void CancelEdit() => await CancelEditsAsync();

        /// <summary>
        /// Called when a bound DataGrid control commits the edits that have been made to a Student.
        /// </summary>
        public async void EndEdit() => await SaveAsync();

        public async void DeleteAsync()
        {
            await App.StudentService.DeleteAsync(StudentModel.Id);
            App.ViewModel.Students.Remove(this);
        }
    }
}