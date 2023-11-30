using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using lr5;
using Microsoft.AspNetCore.Authorization;

namespace lr5.Controllers
{
    public class LabsController : Controller
    {
        private readonly MyDataService myDataService;
        public LabsController(MyDataService my)
        {
            myDataService = my;
        }
        public ActionResult RunLab1()
        {
            return View();
        }
        public ActionResult RunLab2()
        {
            return View();
        }
        public ActionResult RunLab3()
        {
            return View();
        }

        [HttpPost]
        public ActionResult HandleRunLab1(string str1, string str2)
        {
            // Ваш код для обработки нажатия кнопки
            if(str1 == null || str2 == null)
            {
                return Json(new { success = false, error = "Не правильні параметри."});
            }

            int res1 = lr1.Solve(str1, str2);
            return Json(new { res1 });
        }

        [HttpPost]
        public ActionResult HandleRunLab2(int number)
        {
            try
            {
                int res1 = lr2.Resolve(number);
                return Json(new { res1 });
            }
            catch(Exception ex)
            {
                return Json(new { success = false, error = "Не правильні параметри." });
            }
        }

        [HttpPost]
        public ActionResult HandleRunLab3(int N, int x1, int y1, int x2, int y2)
        {
            try
            {
                int res1 = lr3.Search(N, x1, y1, x2, y2);
                return Json(new { res1 });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = "Не правильні параметри." });
            }
        }
    }
}
