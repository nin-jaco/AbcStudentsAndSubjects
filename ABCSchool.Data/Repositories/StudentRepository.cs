using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Data.Base;
using ABCSchool.Data.Interfaces;
using ABCSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCSchool.Data.Repositories
{
    public class StudentRepository : RepositoryBase<Student, AbcSchoolDbContext>, IStudentRepository
    {
        private readonly AbcSchoolDbContext _context;
        public StudentRepository(AbcSchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Student> Get(int id)
        {
            return await _context.Students.Include("StudentsSubjects").FirstOrDefaultAsync(p => p.Id ==id);
        }
    }
}
