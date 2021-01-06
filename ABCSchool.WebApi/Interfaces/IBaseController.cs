using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ABCSchool.WebApi.Interfaces
{
    public interface IBaseController<TEntity>
    {
        Task<ActionResult<IList<TEntity>>> Get();
        Task<ActionResult<TEntity>> Get(int id);
        Task<ActionResult<TEntity>> Put(int id, TEntity item);
        Task<ActionResult<TEntity>> Post(TEntity item);
        Task<ActionResult<TEntity>> Delete(int id);
    }
}
