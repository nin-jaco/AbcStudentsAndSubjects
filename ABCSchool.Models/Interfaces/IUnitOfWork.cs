using System;
using System.Collections.Generic;
using System.Text;

namespace ABCSchool.Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IStudentRepository StudentRepository { get; }
        ISubjectRepository SubjectRepository { get; }
        int Complete();
    }
}
