using System;
using System.Collections.Generic;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.IRepository;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;

namespace Ecomm_Project_101.DataAccess.Repository
{
    public class UnitOfWork:IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(context);
            CoverType= new CoverTypeRepository(context);
            Product = new ProductRepository(context);
            Company = new CompanyRepository(context);
        }
        public ICategoryRepository Category { private set; get; }

        public ICoverTypeRepository CoverType { private set; get; }
        public IProductRepository Product { private set; get; }
        public ICompanyRepository Company { private set; get; }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
