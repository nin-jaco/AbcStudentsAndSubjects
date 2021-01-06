using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data.Interfaces;
using ABCSchool.Data.Repositories;
using ABCSchool.Data.UnitOfWorks;
using ABCSchool.WebApi.Base;
using ABCSchool.Domain.Entities;
using ABCSchool.Domain.Interfaces;
using ABCSchool.WebApi.Interfaces;

namespace ABCSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : BaseController<Student, StudentRepository>, IStudentController
    {
        private IUnitOfWork _unitOfWork;
        private readonly StudentRepository _repository;

        public StudentController(IUnitOfWork unitOfWork, StudentRepository repository) : base(unitOfWork,repository)
        {
            _unitOfWork = unitOfWork;
            _repository = repository;
        }

        [HttpPut("{id}")]
        public override async Task<ActionResult<Student>> Put(int id, Student item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _repository.Update(item);
                return result;
            }

            return null;
        }
    }
}
