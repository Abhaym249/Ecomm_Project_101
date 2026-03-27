using System;
using System.Collections.Generic;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;

namespace Ecomm_Project_101.DataAccess.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _context;
        public CompanyRepository(ApplicationDbContext context): base(context) 
        {
            _context = context;
        }
    }
}
