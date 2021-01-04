using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Data.Base;
using ABCSchool.Domain.Entities;
using ABCSchool.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ABCSchool.Data.Repositories
{
    public class StudentRepository : GenericRepository<Student, AbcSchoolDbContext>, IStudentRepository
    {
        private readonly AbcSchoolDbContext _context;
        public StudentRepository(AbcSchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Student> Get(int id)
        {
            return await _context.Students.Include("StudentSubjects").FirstOrDefaultAsync(p => p.Id ==id);
        }

        public override async Task<List<Student>> GetAll()
        {
            return await _context.Set<Student>().Include("StudentSubjects").ToListAsync();
        }
    }
}
