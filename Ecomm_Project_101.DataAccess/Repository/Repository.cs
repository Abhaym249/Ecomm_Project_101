using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecomm_Project_101.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        public Repository(ApplicationDbContext context)
        {
            _context = context;
            dbSet = context.Set<T>();
        }
        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> filter = null, string IncludeProperties = null)
        {
           IQueryable<T> query = dbSet;
            if (filter != null)
                query=query.Where(filter);
            if(IncludeProperties != null)
            {
                foreach(var includeProp in IncludeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return query.FirstOrDefault();
        }

        public T Get(int id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T> ,IOrderedQueryable<T>> orderBy = null, string IncludeProperties = null)
        {
            IQueryable<T> query =dbSet;
            if(filter!=null)
                query=query.Where(filter);
            if (IncludeProperties != null)
            {
                foreach(var includeProp in IncludeProperties.Split(new char[] { ',' },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            if(orderBy!=null)
                return orderBy(query).ToList();
            return query.ToList();
          
        }

        public void Remove(int id)
        {
            dbSet.Remove(Get(id));
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }
    }
    }


