using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Ecomm_Project_Copy1.DataAcccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        internal DbSet<T> dbSet;
        {

        }
    }
}
