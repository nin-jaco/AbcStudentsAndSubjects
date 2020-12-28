using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using ABCSchool.Common;
using ABCSchool.Domain.Entities;
using ABCSchool.Model;

namespace ABCSchool.ViewModels
{
    public class SubjectViewModel : BindableBase, IEditableObject
    {
        public SubjectViewModel()
        {
            Model = new Subject();
            this.IsSelected = false;
            this.IsModified = false;
            this.Name = "";
            this.IsLoading = false;
            this.IsNewSubject = true;
            this.IsInEdit = false;
            Students = new List<Student>();
        }

        public SubjectViewModel(Subject model)
        {
            Model = model;
            this.IsSelected = false;
            this.IsModified = false;
            this.Name = model.Name;
            this.IsLoading = false;
            this.IsNewSubject = false;
            this.IsInEdit = false;
            Students = model.Students;
        }

        private Subject _model;
        public Subject Model
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

        /// <summary>
        /// Gets or sets the subject's name.
        /// </summary>
        public string Name
        {
            get => Model.Name;
            set
            {
                if (value != Model.Name)
                {
                    Model.Name = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
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

        private bool _isNewSubject;

        /// <summary>
        /// Gets or sets a value that indicates whether this is a new Student.
        /// </summary>
        public bool IsNewSubject
        {
            get => _isNewSubject;
            set => Set(ref _isNewSubject, value);
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

        public ICollection<Student> Students
        {
            get => Model.Students;
            set
            {
                if (value != Model.Students)
                {
                    Model.Students = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        /*public IList<StudentSubject> StudentSubjects
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
        }*/

        /// <summary>
        /// Saves Student data that has been edited.
        /// </summary>
        public async Task SaveAsync()
        {
            IsInEdit = false;
            IsModified = false;
            if (IsNewSubject)
            {
                IsNewSubject = false;
                App.MainViewModel.Subjects.Add(this);
                await App.SubjectService.PostAsJsonAsync(Model);
                return;
            }

            await App.SubjectService.PutAsJsonAsync(Model);
        }

        /// <summary>
        /// Raised when the user cancels the changes they've made to the Student data.
        /// </summary>
        public event EventHandler AddNewSubjectCanceled;

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
        

        /// <summary>
        /// Enables edit mode.
        /// </summary>
        public void StartEdit() => IsInEdit = true;

        
        public async Task RefreshSubjectAsync()
        {
            Model = await App.SubjectService.GetByIdAsync(Model.Id);
        }

        /// <summary>
        /// Called when a bound DataGrid control causes the Student to enter edit mode.
        /// </summary>
        public void BeginEdit()
        {
            // Not used.
        }

        public async void CancelEdit() => await CancelEditsAsync();
        public async Task CancelEditsAsync()
        {
            if (IsNewSubject)
            {
                AddNewSubjectCanceled?.Invoke(this, EventArgs.Empty);
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
                await RefreshSubjectAsync();
                IsModified = false;
            }
        }
        

        public async void EndEdit() => await SaveAsync();
    }
}
