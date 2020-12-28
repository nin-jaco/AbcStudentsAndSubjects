using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Domain.Entities;

namespace ABCSchool.Model
{
    public class SubjectItem : Subject
    {
        public bool IsSelected { get; set; } = false;
    }
}
