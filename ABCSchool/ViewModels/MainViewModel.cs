﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Common;
using ABCSchool.Domain.Entities;
using Microsoft.Toolkit.Uwp.Helpers;
using static ABCSchool.App;

namespace ABCSchool.ViewModels
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
            Task.Run(GetAllStudents);
            Task.Run(GetAllSubjects);
        }

        public ObservableCollection<StudentViewModel> Students { get; } = new ObservableCollection<StudentViewModel>();

        public ObservableCollection<SubjectViewModel> Subjects { get; } = new ObservableCollection<SubjectViewModel>();


        private StudentViewModel _selectedStudent;
        public StudentViewModel SelectedStudent
        {
            get => _selectedStudent;
            set => Set(ref _selectedStudent, value);
        }

        private SubjectViewModel _selectedSubject;
        public SubjectViewModel SelectedSubject
        {
            get => _selectedSubject ?? new SubjectViewModel(new Subject());
            set => Set(ref _selectedSubject, value);
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
                var students = await StudentService.GetAllAsync();
                if (students != null)
                {
                    Students.Clear();
                    foreach (var c in students)
                    {
                        var model = new StudentViewModel(c);
                        var selectedSubjectIds = c.StudentSubjects?.Select(p => p.SubjectId)?.ToList();
                        foreach (var p in App.ViewModel.Subjects)
                        {
                            if (selectedSubjectIds != null && selectedSubjectIds.Contains(p.Model.Id)) p.IsSelected = true;
                        }
                        Students.Add(model);
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
                var subjects = await SubjectService.GetAllAsync();
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
        
    }
}