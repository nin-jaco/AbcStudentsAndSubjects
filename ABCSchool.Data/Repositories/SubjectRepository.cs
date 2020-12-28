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
    public class SubjectRepository : GenericRepository<Subject, AbcSchoolDbContext>, ISubjectRepository
    {
        private readonly AbcSchoolDbContext _context;

        public SubjectRepository(AbcSchoolDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Subject> Get(int id)
        {
            return await _context.Subjects.FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
