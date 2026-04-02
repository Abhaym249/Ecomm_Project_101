using System;
using System.Collections.Generic;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository;
using Ecomm_Project_101.Models;
using Ecomm_Project_101.DataAccess.Repository.IRepository;

namespace Ecomm_Project_101.DataAccess.IRepository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly IApplicationDbContext _context;
        public CategoryRepository(IApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
