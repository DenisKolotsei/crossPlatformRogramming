using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml.Linq;

namespace lr11.Controllers
{
    public class HomeController : Controller
    {
        private readonly MyDataService myDataService;

        public HomeController(MyDataService my)
        {
            myDataService = my;
        }

        [HttpGet]
        public ActionResult Profile()
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (!userAuthed.HasValue) throw new Exception("Не авторизован.");
            if (userAuthed == 0) throw new Exception("Не авторизован.");

            string? userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId)) throw new Exception("Что-то не так.");

            User targetUser = myDataService.GetUserById(userId);

            return Json(new { success = true, name = targetUser.name, email = targetUser.email, phone = targetUser.phone});
        }
    }
}