using ABCSchool.Data.Repositories;
using ABCSchool.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace ABCSchool.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AbcSchoolDbContext _context;
        public UnitOfWork(AbcSchoolDbContext context)
        {
            _context = context;
            StudentRepository = new StudentRepository(_context);
            SubjectRepository = new SubjectRepository(_context);
        }
        public IStudentRepository StudentRepository { get; private set; }
        public ISubjectRepository SubjectRepository { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
