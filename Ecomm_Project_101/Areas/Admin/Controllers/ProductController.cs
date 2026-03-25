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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
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

                CategoryList = _unitOfWork.Category.GetAll().Select(cl => new SelectListItem()
                {
                    Text = cl.Name,
                    Value = cl.Id.ToString()

                }),
                CoverTypeList = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem()
                {
                    Text = ct.Name,
                    Value = ct.Id.ToString()

                })
            };
            if (id == null || id == 0)
            {
                return View(productVM);
            }
            productVM.Product = _unitOfWork.Product.Get(id.GetValueOrDefault());
            if (productVM.Product == null)
            {
                return NotFound();
            }
            return View(productVM);

        }
        [HttpPost]
        public IActionResult Upsert(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if (files.Count() > 0)
                {
                    var fileName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(files[0].FileName);
                    var uploads = Path.Combine(webRootPath, @"images\Products");

                    if (productVM.Product.Id != 0) //edit
                    {
                        var ImageExists = _unitOfWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = ImageExists;
                        if (productVM.Product.ImageUrl != null)
                        {
                            var ImagePath = Path.Combine(webRootPath, productVM.Product.ImageUrl.Trim('\\'));
                            if (System.IO.File.Exists(ImagePath))
                            {
                                System.IO.File.Delete(ImagePath);
                            }
                        }
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productVM.Product.ImageUrl = @"\images\Products\" + fileName + extension;
                    }
                    else
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                        {
                            files[0].CopyTo(fileStream);
                        }
                        productVM.Product.ImageUrl = @"\images\Products\" + fileName + extension;
                    }
                }
                else
                {
                    if (productVM.Product.Id != 0)
                    {
                        var imageExists = _unitOfWork.Product.Get(productVM.Product.Id).ImageUrl;
                        productVM.Product.ImageUrl = imageExists;
                    }
                }

                if (productVM.Product.Id == 0)
                    _unitOfWork.Product.Add(productVM.Product);
                else
                    _unitOfWork.Product.Update(productVM.Product);

                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVM = new ProductVM()
                {
                    Product = new Product(),
                    CategoryList = _unitOfWork.Category.GetAll().Select(cl => new SelectListItem()
                    {
                        Text = cl.Name,
                        Value = cl.Id.ToString()
                    }),
                    CoverTypeList = _unitOfWork.CoverType.GetAll().Select(ct => new SelectListItem()
                    {
                        Text = ct.Name,
                        Value = ct.Id.ToString()
                    })
                };
                if (productVM.Product.Id != 0)
                {
                    productVM.Product = _unitOfWork.Product.Get(productVM.Product.Id);
                    if (productVM.Product == null)
                    {
                        return NotFound();
                    }
                }
            }

            return View(productVM);
        }


        #region Apis
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.Product.GetAll() });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var productInDb = _unitOfWork.Product.Get(id);
            if (productInDb == null)
            {
                return Json(new { success = false, message = "Unable to delete data !!!" });
            }
                //Image delete
                var WebRootPath = _webHostEnvironment.WebRootPath;
                var ImagePath = Path.Combine(WebRootPath, productInDb.ImageUrl).Trim('\\');
            
                if (System.IO.File.Exists(ImagePath))
                {
                    System.IO.File.Delete(ImagePath);
                }
                //database se delete
                _unitOfWork.Product.Remove(productInDb);
                _unitOfWork.Save();
                return Json(new
                {
                    success = true,
                    message = "data deleted successfully!!!"
                });
            

        }
        #endregion
    }
}