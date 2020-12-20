using System;
using System.Collections.Generic;
using System.Text;
using ABCSchool.Data.Base;
using ABCSchool.Data.Interfaces;
using ABCSchool.Models;

namespace ABCSchool.Data.Repositories
{
    public class SubjectRepository : RepositoryBase<Subject, AbcSchoolDbContext>, ISubjectRepository
    {
        public SubjectRepository(AbcSchoolDbContext context) : base(context)
        {

        }
    }
}
