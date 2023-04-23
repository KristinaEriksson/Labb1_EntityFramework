using Labb1_EntityFramework.Data;
using Microsoft.AspNetCore.Mvc;

namespace Labb1_EntityFramework.Controllers
{
    public class AdminController : Controller
    {
        private readonly ForzaDbContext _context;
        public AdminController(ForzaDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {

            bool admin = false;

            foreach (var item in _context.Employees)
            {
                if (item.Role.ToLower() == "admin")
                {
                    admin = true;
                }
            }
            if (admin)
            {

                return RedirectToAction("SearchMonth", "LeaveList");
            }
            else
            {
                return View();
            }
        }
    }
}
