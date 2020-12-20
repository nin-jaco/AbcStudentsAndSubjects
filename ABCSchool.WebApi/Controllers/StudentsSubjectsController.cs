using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data.Repositories;
using ABCSchool.WebApi.Base;
using ABCSchool.Models;

namespace ABCSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsSubjectsController : BaseController<StudentsSubjects, StudentsSubjectsRepository>
    {
        private StudentsSubjectsRepository _repository;
        public StudentsSubjectsController(StudentsSubjectsRepository repository) : base(repository)
        {
            _repository = repository;
        }

        // GET: api/[controller]/studentid/5
        [HttpGet("studentId/{studentId}")]
        public async Task<List<StudentsSubjects>> GetAllSubjectsForStudentId(int studentId)
        {
            return await _repository.GetAllSubjectsForStudentId(studentId);
        }
    }
}
