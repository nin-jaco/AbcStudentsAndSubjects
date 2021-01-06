using System;
using Microsoft.EntityFrameworkCore;

namespace ABCSchool.Data.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository StudentRepository { get; set; }
        ISubjectRepository SubjectRepository { get; set; }

        void BeginTransaction();
        void SaveChanges();
        bool Commit();
        void Rollback();
    }

    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext Context { get; }
    }
}
