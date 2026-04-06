using Ecomm_project_101.Utility;
using Ecomm_Project_101.DataAccess.Data;
using Ecomm_Project_101.DataAccess.Repository.IRepository;
using Ecomm_Project_101.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecomm_Project_101.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _context;

        public UserController(IUnitOfWork unitOfWork, ApplicationDbContext context)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            return View();
        }

        #region Apis

        [HttpGet]
        public IActionResult GetAll()
        {
            var userList = _context.ApplicationUsers.ToList();
            var roleList = _context.Roles.ToList();
            var userRoleList = _context.UserRoles.ToList();

            foreach (var user in userList)
            {
                var roleId = userRoleList.FirstOrDefault(u => u.UserId == user.Id).RoleId;
                user.Role = roleList.FirstOrDefault(r => r.Id == roleId).Name;

                if (user.CompanyId == null)
                {
                    user.Company = new Company()
                    {
                        Name = ""
                    };
                }
                if (user.CompanyId != null)
                {
                    user.Company = new Company()
                    {
                        Name = _unitOfWork.Company.Get(Convert.ToInt32(user.CompanyId)).Name
                    };
                }
            }
            // remove admin role user
            var adminUser = userList.FirstOrDefault(u => u.Role == SD.Role_Admin);
            if (adminUser != null)
                userList.Remove(adminUser);

            return Json(new { data = userList });
        }
        [HttpPost]
        public IActionResult LockUnlock([FromBody] string id)
        {
            bool isLocked = false;
            var UserInDb = _unitOfWork.ApplicationUsers.FirstOrDefault(u => u.Id == id);
            if (UserInDb == null)
            {
                return Json(new
                {
                    success = false,
                    message = "Something went wrong while lock Unlock user !!!"
                });
               
            }
            if (UserInDb != null && UserInDb.LockoutEnd > DateTime.Now)
            {
                UserInDb.LockoutEnd = DateTime.Now;
                isLocked = false;
            }
            else
            {
                UserInDb.LockoutEnd = DateTime.Now.AddYears(100);
                isLocked = true;
            }
            _context.SaveChanges();
            return Json(new { success = true, message = isLocked == true ? "User successfully locked " : "USer Successfully Unlocked" });
        }
        #endregion
    }
}