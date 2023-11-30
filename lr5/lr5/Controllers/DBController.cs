using lr5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace lr5.Controllers
{
    public class DBController : Controller
    {
        private readonly DBContext dbcontext;
        public DBController(DBContext context)
        {
            dbcontext = context;
        }

        public ActionResult CenterTable()
        {
            return View();
        }
    }
}
