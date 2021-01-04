using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Domain.Entities;

namespace ABCSchool.Model
{
    public interface IStudent
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
        bool IsModified { get; set; }
        bool IsLoading { get; set; }
        bool IsNewStudent { get; set; }
        bool IsInEdit { get; set; }
    }
}
