using lr5.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace lr5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDataService myDataService;

        public HomeController(ILogger<HomeController> logger, MyDataService my)
        {
            myDataService = my;
            _logger = logger;
        }

        public IActionResult Index()
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (userAuthed.HasValue)
            {
                if(userAuthed == 1)
                {
                    return View();
                }
            }
            return RedirectToAction("Login", "Home");
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Reg()
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (userAuthed.HasValue)
            {
                if (userAuthed == 1)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            string? userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Home");
            }
            return View();
        }

        public ActionResult HandleReg(string phone)
        {
            string? userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                return Json(new { Success = false});
            }

            if(string.IsNullOrEmpty(phone)) return Json(new { Success = false });
            if(phone.Length != 9) return Json(new { Success = false });


            User targetUser = myDataService.GetUserById(userId);

            if (targetUser != null)
            {
                targetUser.phone = phone;
                myDataService.UpdateUser(userId, targetUser);
                HttpContext.Session.SetInt32("userAuthed", 1);
                return Json(new { Success = true });
            }
            return Json(new { Success = false });
        }

        public async Task<IActionResult> Profile()
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (!userAuthed.HasValue) return RedirectToAction("Login", "Home");
            if (userAuthed == 0) return RedirectToAction("Login", "Home");

            string? userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId)) return RedirectToAction("Login", "Home");

            User targetUser = myDataService.GetUserById(userId);
            ViewBag.name = targetUser.name;
            ViewBag.email = targetUser.email;
            ViewBag.phone = "+380" + targetUser.phone;
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