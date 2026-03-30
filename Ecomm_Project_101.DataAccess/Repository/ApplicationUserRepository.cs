using System;
using System.Collections.Generic;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository.IRepository;

namespace Ecomm_Project_101.DataAccess.Repository
{
    public class ApplicationUserRepository:Repository<ApplicationUserRepository>,IApplicationUserRepository
    {
        private readonly ApplicationDbContext _context;
        public ApplicationUserRepository(ApplicationDbContext context):base(context) 
        {
            _context = context; 
        }
    }
}
