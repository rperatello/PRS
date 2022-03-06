using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRS.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {

        private PRSContext context = null;

        public Repository(PRSContext context)
        {
            this.context = context;
        }

        public T Add(T t)
        {
            context.Set<T>().Add(t);
            return t;
        }

        public void Delete(T t)
        {
            context.Set<T>().Remove(t);
        }

        public IEnumerable<T> GetAll()
        {
            return context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return context.Set<T>().Find(id);
        }

        public IQueryable<T> GetByQuery()
        {
            return context.Set<T>().AsQueryable();
        }

        public T Update(T t)
        {
            context.Set<T>().Update(t);
            return t;
        }
    }
}
