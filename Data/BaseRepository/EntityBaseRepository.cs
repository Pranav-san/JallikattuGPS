using Jallikattu.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace Jallikattu.Data.BaseRepository
{
    public class EntityBaseRepository<T>: IEntityBaseRepository<T> where T : class
    {
        protected readonly Entities _context;
        protected readonly DbSet<T> _dbSet;

        public EntityBaseRepository(Entities context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<T> GetAll()
        {
            throw new NotImplementedException();
        }

        public T GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(int id, T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();

        }

        
    }
}