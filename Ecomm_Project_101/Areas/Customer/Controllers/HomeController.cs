using System.Diagnostics;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Ecomm_Project_101.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitfOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitfOfWork = unitOfWork;
        }

        public IActionResult Index(int page = 1)
        {
            int pageSize = 4;

            var products = _unitfOfWork.Product.GetAll();

            var totalProducts = products.Count();

            var pagedProducts = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            ViewBag.CurrentPage = page;
            ViewBag.TotalPages = (int)Math.Ceiling((double)totalProducts / pageSize);

            return View(pagedProducts);
        }
        public IActionResult Details(int id)
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
