using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Ecomm_Project_101.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);//save
        void Update(T entity);//update
        T Get(int id);
        IEnumerable<T> GetAll(Expression<Func<T,bool>>filter =null,Func<IQueryable<T>> orderBy = null,String IncludeProperties = null);
        T FirstOrDefault(Expression<Func<T, bool>> filter = null, String IncludeProperties = null);
        void Remove(int id);
        void Remove(T entity);  
        void RemoveRange(IEnumerable<T> entity);


    }
}
