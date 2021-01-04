using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Domain.Entities;

namespace ABCSchool.Model
{
    public interface ISubject
    {
        bool IsSelected { get; set; }
        bool IsModified { get; set; }
        string Name { get; set; }
        bool IsLoading { get; set; }
        bool IsNewSubject { get; set; }
        bool IsInEdit { get; set; }
        ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}
