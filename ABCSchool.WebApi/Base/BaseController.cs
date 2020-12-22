using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data.Interfaces;
using ABCSchool.Models;

namespace ABCSchool.WebApi.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<TEntity, TRepository> : ControllerBase
        where TEntity : class, IEntity
        where TRepository : IRepository<TEntity>
    {
        private readonly TRepository _repository;

        public BaseController(TRepository repository)
        {
            this._repository = repository;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get()
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
        public async Task<IActionResult> Put(int id, TEntity item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }
            await _repository.Update(item);
            return NoContent();
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
