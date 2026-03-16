using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project_101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }
    public IActionResult Upsert(int? id)
        {CoverType coverType = new CoverType();
            if (id == null) return View(coverType);//create
            //edit
             coverType = _unitOfWork.CoverType.Get(id.GetValueOrDefault());
            if(coverType == null) return NotFound();
            return View();
        }
        #region Apis
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.CoverType.GetAll() });
        }

        #endregion
    }
}
