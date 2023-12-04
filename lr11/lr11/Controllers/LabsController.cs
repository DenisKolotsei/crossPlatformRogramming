using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lr11;
using Microsoft.AspNetCore.Authorization;

namespace lr11.Controllers
{
    public class LabsController : Controller
    {
        [HttpGet]
        public ActionResult Lab1(string str1, string str2)
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (!userAuthed.HasValue) throw new Exception("Не авторизован.");
            if (userAuthed == 0) throw new Exception("Не авторизован.");

            // Ваш код для обработки нажатия кнопки
            if (str1 == null || str2 == null) throw new Exception("Не правильні параметри.");

            int res1 = lr1.Solve(str1, str2);
            return Json(new { res1 });
        }

        [HttpGet]
        public ActionResult Lab2(int number)
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (!userAuthed.HasValue) throw new Exception("Не авторизован.");
            if (userAuthed == 0) throw new Exception("Не авторизован.");
            try
            {
                int res1 = lr2.Resolve(number);
                return Json(new { res1 });
            }
            catch(Exception ex)
            {
                throw new Exception("Не правильні параметри.");
            }
        }

        [HttpGet]
        public ActionResult Lab3(int N, int x1, int y1, int x2, int y2)
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (!userAuthed.HasValue) throw new Exception("Не авторизован.");
            if (userAuthed == 0) throw new Exception("Не авторизован.");
            try
            {
                int res1 = lr3.Search(N, x1, y1, x2, y2);
                return Json(new { res1 });
            }
            catch (Exception ex)
            {
                throw new Exception("Не правильні параметри.");
            }
        }
    }
}
