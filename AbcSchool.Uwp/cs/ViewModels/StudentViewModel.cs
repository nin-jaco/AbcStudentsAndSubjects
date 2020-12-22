using System;
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
    /// Provides a bindable wrapper for the Student model class, encapsulating various services for access by the UI.
    /// </summary>
    public class StudentViewModel : BindableBase, IEditableObject
    {
        /// <summary>
        /// Initializes a new instance of the StudentViewModel class that wraps a Student object.
        /// </summary>
        public StudentViewModel(Student model = null)
        {
            Model = model ?? new Student();
            CheckList = new ObservableCollection<CheckListItem>();
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

        /// <summary>
        /// Gets or sets the Student's last name.
        /// </summary>
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

        
        /// <summary>
        /// Gets or sets the Student's phone number. 
        /// </summary>
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

        /// <summary>
        /// Gets or sets the Student's email. 
        /// </summary>
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

        /// <summary>
        /// Gets or sets a value that indicates whether the underlying model has been modified. 
        /// </summary>
        /// <remarks>
        /// Used when sync'ing with the server to reduce load and only upload the models that have changed.
        /// </remarks>
        public bool IsModified { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to show a progress bar. 
        /// </summary>
        private bool _isLoading;
        public bool IsLoading
        {
            get => _isLoading;
            set => Set(ref _isLoading, value);
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new Student.
        /// </summary>
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
            await App.StudentService.DeleteAsync(Model.Id);
            App.ViewModel.Students.Remove(this);
        }

        /// <summary>
        /// Saves Student data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
            var subjects = this.CheckList.Where(p => p.IsSelected);
            IsInEdit = false;
            IsModified = false;
            if (IsNewStudent)
            {
                IsNewStudent = false;
                var response = await App.StudentService.PostAsJsonAsync(Model);
                if (response != null)
                {
                    this.Model = response;
                    App.ViewModel.Students.Add(this);
                }
            }
            else
            {
                await App.StudentService.PutAsJsonAsync(Model);
            }

            //var selectedSubjects = await App.StudentSubjectService.GetByStudentIdAsync(Model.Id);
            /*//todo
            foreach (var item in subjects)
            {
                var add = selectedSubjects.Where(p => p.s == )
                if(selectedSubjects.Contains)
                
                Model.StudentsSubjects.Add(new StudentsSubjects { Student = this.Model, SubjectId = item.Id, Subject = await App.SubjectService.GetByIdAsync(item.Id) });
            }*/
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
            Model = await App.StudentService.GetByIdAsync(Model.Id);
        }

        /// <summary>
        /// Gets the collection of the Student's subjects.
        /// </summary>

        //private ObservableCollection<SubjectViewModel> _selectedSubjects;
        //public ObservableCollection<SubjectViewModel> SelectedSubjects
        //{
        //    get => _selectedSubjects;
        //    set => Set(ref _selectedSubjects, value);
        //}

        private ObservableCollection<CheckListItem> _checkList;
        public ObservableCollection<CheckListItem> CheckList
        {
            get => _checkList;
            set => Set(ref _checkList, value);
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

            var allSubjects = await App.SubjectService.GetAllAsync();
            var selectedIds = StudentSubjects?.Select(p => p.SubjectId).ToList();

            await DispatcherHelper.ExecuteOnUIThreadAsync(() =>
            {
                CheckList?.Clear();
                if(CheckList ==null) CheckList = new ObservableCollection<CheckListItem>();

                foreach (var subject in allSubjects)
                {
                    
                    CheckList.Add(new CheckListItem
                    {
                        Id = subject.Id, Text = subject.Name,
                        IsSelected = selectedIds?.Contains(subject.Id) ?? false
                    });
                }

                IsLoading = false;
            });
        }

        
    }
}