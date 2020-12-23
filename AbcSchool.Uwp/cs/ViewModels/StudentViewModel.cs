using System;
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
    /// Provides a bindable wrapper for the Student model class, encapsulating various services for access by the UI.
    /// </summary>
    public class StudentViewModel : BindableBase, IEditableObject
    {
        public StudentViewModel(Student model = null)
        {
            Model = model ?? new Student();
            FirstName = "";
            LastName = "";
            Mobile = "";
            Email = "";
            StudentSubjects = new List<StudentSubject>();
        }
        
        private Student _model;
        public Student Model
        {
            get => _model;
            set
            {
                if (_model != value)
                {
                    _model = value;
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        public string FirstName
        {
            get => Model.FirstName;
            set
            {
                if (value != Model.FirstName)
                {
                    Model.FirstName = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        public string LastName
        {
            get => Model.LastName;
            set
            {
                if (value != Model.LastName)
                {
                    Model.LastName = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        public string Mobile
        {
            get => Model.Mobile;
            set
            {
                if (value != Model.Mobile)
                {
                    Model.Mobile = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        public string Email
        {
            get => Model.Email;
            set
            {
                if (value != Model.Email)
                {
                    Model.Email = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        public IList<StudentSubject> StudentSubjects
        {
            get => Model.StudentSubjects;
            set
            {
                if (value != Model.StudentSubjects)
                {
                    Model.StudentSubjects = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }


        public bool IsModified { get; set; }

        
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        
        private bool _isNewStudent;
        public bool IsNewStudent
        {
            get => _isNewStudent;
            set => Set(ref _isNewStudent, value);
        }

        private bool _isInEdit = false;
        public bool IsInEdit
        {
            get => _isInEdit;
            set => Set(ref _isInEdit, value);
        }

        public void BeginEdit(){}

        public void StartEdit() => IsInEdit = true;

        /// <summary>
        /// Called when a bound DataGrid control commits the edits that have been made to a Student.
        /// </summary>
        public async void EndEdit() => await SaveAsync();

        public async void DeleteAsync()
        {
            if(await App.StudentService.DeleteAsync(Model.Id)) App.MainViewModel.Students.Remove(this);
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
                var response = await App.StudentService.PostAsJsonAsync(Model);
                if (response != null)
                {
                    this.Model = response;
                    App.MainViewModel.Students.Add(this);
                }
            }

            var subjects = App.MainViewModel.Subjects.Where(p => p.IsSelected)?.ToList();
            foreach (var p in subjects)
            {
                Model.StudentSubjects.Add(new StudentSubject{ StudentId = Model.Id, SubjectId = p.Model.Id });
            }
            await App.StudentService.PutAsJsonAsync(Model);
        }
        
        /// <summary>
        /// Raised when the user cancels the changes they've made to the Student data.
        /// </summary>
        public event EventHandler AddNewStudentCanceled;

        public async void CancelEdit() => await CancelEditsAsync();
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

        public async Task RefreshStudentAsync()
        {
            Model = await App.StudentService.GetByIdAsync(Model.Id);
        }

        

        

        

        
    }
}