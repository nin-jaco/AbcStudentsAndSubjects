using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ABCSchool.Data.Base;
using ABCSchool.Data.Interfaces;
using ABCSchool.Models;
using Microsoft.EntityFrameworkCore;

namespace ABCSchool.Data.Repositories
{
    public class StudentsSubjectsRepository : RepositoryBase<StudentsSubjects, AbcSchoolDbContext>, IStudentsSubjectsRepository
    {
        private readonly AbcSchoolDbContext _context;
        public StudentsSubjectsRepository(AbcSchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<StudentsSubjects>> GetAllSubjectsForStudentId(int studentId)
        {
            return await _context.Set<StudentsSubjects>().Where(p => p.StudentId == studentId).ToListAsync();
        }

    }
}
