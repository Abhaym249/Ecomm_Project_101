using System;
using System.Collections.Generic;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;

namespace Ecomm_Project_101.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>,IProductRepository
    {
        private readonly IApplicationDbContext _context;
        public ProductRepository(IApplicationDbContext context) : base(context) 
        {
            _context = context;
                
        }
        
            
    }
}

