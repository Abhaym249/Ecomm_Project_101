using System;
using System.Collections.Generic;
using System.Text;

namespace Ecomm_Project_101.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        void Save();//save method 
    }
}
