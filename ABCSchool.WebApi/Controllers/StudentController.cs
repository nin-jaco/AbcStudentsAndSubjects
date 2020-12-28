using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data.Repositories;
using ABCSchool.WebApi.Base;
using ABCSchool.Domain.Entities;

namespace ABCSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController<Student, StudentRepository>
    {
        private readonly StudentRepository _repository;
        public StudentController(StudentRepository repository) : base(repository)
        {
            _repository = repository;
        }

        
    }
}
