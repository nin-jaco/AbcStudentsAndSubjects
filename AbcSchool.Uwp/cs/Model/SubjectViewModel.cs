using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Models;
using ABCSchool.Uwp.ViewModels;
using Microsoft.Toolkit.Uwp.Helpers;

namespace ABCSchool.Uwp.Model
{
    public class SubjectViewModel : BindableBase, IEditableObject
    {
        public SubjectViewModel(Subject model = null) => SubjectModel = model ?? new Subject();

        private Subject _subjectModel;

        /// <summary>
        /// Gets or sets the underlying Student object.
        /// </summary>
        public Subject SubjectModel
        {
            get => _subjectModel;
            set
            {
                if (_subjectModel != value)
                {
                    _subjectModel = value;
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        /// <summary>
        /// Gets or sets the subject's name.
        /// </summary>
        public string Name
        {
            get => SubjectModel.Name;
            set
            {
                if (value != SubjectModel.Name)
                {
                    SubjectModel.Name = value;
                    IsModified = true;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsModified { get; set; }

        private bool _isLoading;

        /// <summary>
        /// Gets or sets a value that indicates whether to show a progress bar. 
        /// </summary>
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
                App.ViewModel.Subjects.Add(this);
                await App.SubjectService.PostAsync(SubjectModel);
                return;
            }

            await App.SubjectService.PutAsync(SubjectModel);
        }

        /// <summary>
        /// Raised when the user cancels the changes they've made to the Student data.
        /// </summary>
        public event EventHandler AddNewSubjectCanceled;

        /// <summary>
        /// Cancels any in progress edits.
        /// </summary>
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
                await RefreshSubjectsAsync();
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
        public async Task RefreshSubjectsAsync()
        {
            SubjectModel = await App.SubjectService.GetByIdAsync(SubjectModel.Id);
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
    }
}
