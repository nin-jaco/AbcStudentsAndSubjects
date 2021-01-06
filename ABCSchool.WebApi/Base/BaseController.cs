using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data.Interfaces;
using ABCSchool.Domain.Entities;
using ABCSchool.Domain.Interfaces;
using ABCSchool.WebApi.Interfaces;

namespace ABCSchool.WebApi.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TRepository> : ControllerBase, IBaseController<TEntity>
        where TEntity : class, IEntity
        where TRepository : IGenericRepository<TEntity>
    {
        private readonly TRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        protected BaseController(IUnitOfWork unitOfWork,TRepository repository)
        {
            this._repository = repository;
            _unitOfWork = unitOfWork;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IList<TEntity>>> Get()
        {
            return await _repository.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public virtual async Task<ActionResult<TEntity>> Get(int id)
        {
            var item = await _repository.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public virtual async Task<ActionResult<TEntity>> Put(int id, TEntity item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            return await _repository.Update(item);
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity item)
        {
            await _repository.Add(item);
            return CreatedAtAction("Get", new { id = item.Id }, item);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id)
        {
            var item = await _repository.Delete(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

    }
}
