using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Domain.Entities;

namespace ABCSchool.ViewModels
{
    public class StudentSubjectViewModel: BindableBase
    {
        public StudentSubjectViewModel(StudentSubject studentSubject = null)
        {
            Model = studentSubject ?? new StudentSubject();
        }
        
        private StudentSubject _model;
        public StudentSubject Model
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

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                if (_isSelected != value)
                {
                    _isSelected = value;
                    OnPropertyChanged(string.Empty);
                }
            }
        }

        private int _studentId;
        public int StudentId
        {
            get => _studentId;
            set => Set(ref _studentId, value);
        }

        private Student _student;
        public Student Student
        {
            get => _student;
            set => Set(ref _student, value);
        }

        private Subject _subject;
        public Subject Subject
        {
            get => _subject;
            set => Set(ref _subject, value);
        }
    }
}
