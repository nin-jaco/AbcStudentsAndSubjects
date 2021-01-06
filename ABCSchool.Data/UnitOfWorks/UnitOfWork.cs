using ABCSchool.Data.Repositories;
using ABCSchool.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using ABCSchool.Data.Interfaces;

namespace ABCSchool.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AbcSchoolDbContext _context;
        public IStudentRepository StudentRepository { get; set; }
        public ISubjectRepository SubjectRepository { get; set; }

        public UnitOfWork(AbcSchoolDbContext context, StudentRepository studentRepository, SubjectRepository subjectRepository)
        {
            _context = context;
            StudentRepository = studentRepository;
            SubjectRepository = subjectRepository;
        }

        


        
        
        public int Complete()
        {
            return _context.SaveChanges();
        }

        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void BeginTransaction()
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool Commit()
        {
            throw new NotImplementedException();
        }

        public void Rollback()
        {
            throw new NotImplementedException();
        }
    }
}
