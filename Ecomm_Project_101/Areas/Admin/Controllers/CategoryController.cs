using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project_101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Category category = new Category();
            if (id == null) return View(category);//Create
            //Edit
            category = _unitOfWork.Category.Get(id.GetValueOrDefault());
            if (category == null) return NotFound();
            return View(category);
        }
    #region Apis
    [HttpGet] //endpoint
        public IActionResult GetAll()
        {
            var CategoryList = _unitOfWork.Category.GetAll();
            return Json(new { data = CategoryList });
        }   
    #endregion
    }
}
