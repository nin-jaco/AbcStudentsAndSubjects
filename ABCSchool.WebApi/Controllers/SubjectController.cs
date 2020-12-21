using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data.Interfaces;
using ABCSchool.WebApi.Base;
using ABCSchool.Data.Repositories;
using ABCSchool.Models;

namespace ABCSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : BaseController<Subject, SubjectRepository>, ISubjectController
    {
        private readonly StudentRepository _repository;

        public SubjectController(SubjectRepository repository) : base(repository)
        {

        }

        
    }

    public interface ISubjectController 
    {
    }
}
