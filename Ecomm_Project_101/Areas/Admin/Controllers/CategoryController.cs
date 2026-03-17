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
        [HttpPost]
        public IActionResult Upsert(Category category)
        {
            if(category==null) return BadRequest();
            if (!ModelState.IsValid) return View(category);
            if (category.Id == 0)
            {
                _unitOfWork.Category.Add(category);
            }
            else
            {
                _unitOfWork.Category.Update(category);
              
            }
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }
    #region Apis
    [HttpGet] //endpoint
        public IActionResult GetAll()
        {
            var CategoryList = _unitOfWork.Category.GetAll();
            return Json(new { data = CategoryList });
        }
        [HttpDelete]
        public IActionResult Delete(int id)  
        { 
            var CategoryInDb =_unitOfWork.Category.Get(id);
            if(CategoryInDb == null)
            {
                return Json(new {success=false,message="Unable to Delete Data !!! "});               
            }
            _unitOfWork.Category.Remove(CategoryInDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = " Data Deleted Successfully !!!!" });
            
        }
    #endregion
    }
}
