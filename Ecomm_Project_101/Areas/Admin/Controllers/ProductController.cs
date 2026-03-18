using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

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

            return View();
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
