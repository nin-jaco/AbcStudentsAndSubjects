using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using ABCSchool.Data.Interfaces;
using ABCSchool.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ABCSchool.Data.Repositories.Base
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : class
    {
        protected readonly AbcSchoolDbContext Context;

        protected GenericRepository(AbcSchoolDbContext context)
        {
            this.Context = context;
        }

        public Task<TEntity> GetFirst(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task<TEntity> Add(TEntity entity)
        {
            await Context.Set<TEntity>().AddAsync(entity);
            await Context.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> Delete(int id)
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity == null)
            {
                throw new InvalidOperationException($@"Item with id {id} not found.");
            }

            Context.Set<TEntity>().Remove(entity);
            await Context.SaveChangesAsync();

            return entity;
        }


        public virtual async Task<TEntity> Get(int id)
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }

        public Task<IEnumerable<TEntity>> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<TEntity>> GetAll()
        {
            return await Context.Set<TEntity>().ToListAsync();
        }

        public virtual async Task<TEntity> Update(TEntity entity)
        {
            Context.Entry<TEntity>(entity).State = EntityState.Modified;
            //context.Update<TEntity>(entity);
            await Context.SaveChangesAsync();
            return entity;
        }
    }
}
