using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PRS.Repository
{
    public interface IRepository<T> where T: class
    {
        T Add(T t);
        T Update(T t);
        void Delete(T t);
        IEnumerable<T> GetAll();
        IQueryable<T> GetByQuery();
        T GetById(int id);
    }
}
