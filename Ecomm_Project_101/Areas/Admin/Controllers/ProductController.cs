using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;
using Ecomm_Project_101.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Ecomm_Project_101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                Product = new Product(),

                CategoryList = _unitOfWork.Category.GetAll().Select(cl => new SelectListItem {
                    Text = cl.Name,
                    Value = cl.Id.ToString()

                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()

                })
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            productVM.Product=_unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null) 
            { 
                return NotFound();
            }
            return View(productVM);
        }

           
        
        #region Apis
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Product.GetAll() });
        }

        #endregion
    }
}
