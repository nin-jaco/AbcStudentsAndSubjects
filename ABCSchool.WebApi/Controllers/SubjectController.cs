using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data.Interfaces;
using ABCSchool.WebApi.Base;
using ABCSchool.Data.Repositories;
using ABCSchool.WebApi.Interfaces;
using ABCSchool.Domain.Entities;

namespace ABCSchool.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : BaseController<Subject, SubjectRepository>, ISubjectController
    {
        private readonly SubjectRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SubjectController(IUnitOfWork unitOfWork, SubjectRepository repository) : base(unitOfWork, repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        
    }
}
