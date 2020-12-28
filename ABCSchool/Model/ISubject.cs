using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABCSchool.Model
{
    public interface ISubject
    {
        IsSelected = false;
        this.IsModified = false;
        this.Name = "";
        this.IsLoading = false;
        this.IsNewSubject = true;
        this.IsInEdit = false;
        Students = new List<Student>();
    }
}
