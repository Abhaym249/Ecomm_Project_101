using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;

namespace Ecomm_Project_101.DataAccess.IRepository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private readonly IApplicationDbContext _context;
        public CoverTypeRepository(IApplicationDbContext context) : base(context)
        {
            _context=context;
        }

    }
}
