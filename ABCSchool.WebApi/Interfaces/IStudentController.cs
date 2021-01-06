using ABCSchool.Domain.Entities;
using ABCSchool.Domain.Interfaces;
using ABCSchool.WebApi.Base;

namespace ABCSchool.WebApi.Interfaces
{
    public interface IStudentController : IBaseController<Student>
    {
    }
}