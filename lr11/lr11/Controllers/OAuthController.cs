using IdentityModel;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Numerics;
using System.Security.Cryptography;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace lr11.Controllers
{
    public class OAuthController : Controller
    {
        private readonly MyDataService myDataService;

        static string codver;

        public OAuthController(MyDataService my)
        {
            myDataService = my;
        }

        [HttpGet]
        public IActionResult GetUrl()
        {
            try
            {
                var scopes = new[] { "https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email" };
                string scope = string.Join(" ", scopes);

                string redirectUrl = $"http://localhost:3000/code";
                string verCode = Guid.NewGuid().ToString();
                string code = ComputeHash(verCode);

                codver = verCode;

                HttpContext.Session.SetString("codeVerifier", verCode);
                Console.WriteLine(HttpContext.Session.Id);
                Console.WriteLine(Request.HttpContext);

                string url = GoogleAuth.GenerateOAuthReqUrl(scope, redirectUrl, code);
                return Json(new { url });
            }
            catch(Exception ex)
            {
                throw new Exception("Что-то не так.");
            }           
        }

        [HttpPost]
        public async Task<IActionResult> Code(string code)
        {
            Console.WriteLine(HttpContext.Session.Id);
            string redirectUrl = "http://localhost:3000/code";
            string verCode = HttpContext.Session.GetString("codeVerifier");
            TokenResult resToken = await GoogleAuth.ExchangeCodeToToken(code, verCode, redirectUrl);
            if (resToken != null)
            {
                UserInfo res = await GoogleAuth.GetUserInfo(resToken.access_token);
                if (res != null)
                {
                    User targetUser = myDataService.GetUserById(res.Id);

                    HttpContext.Session.SetString("userId", res.Id);
                    HttpContext.Session.SetInt32("userAuthed", 0);

                    if (targetUser == null || string.IsNullOrEmpty(targetUser.phone))
                    {
                        if (targetUser == null)
                        {
                            myDataService.AddUser(new User() { id= res.Id, name=res.Name, email=res.Email});
                        }
                        return Json(new { success = true, registered = false});
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("userAuthed", 1);
                        return Json(new { success = true, registered = true });
                    }
                }
            }
            throw new Exception("Что-то не так.");
        }

        [HttpPost]
        public ActionResult Reg(string phone)
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (userAuthed.HasValue)
            {
                if (userAuthed == 1)
                {
                    throw new Exception("Вы вже зареэстровані");
                }
            }

            string? userId = HttpContext.Session.GetString("userId");
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Увійдіть через гугл акаунт.");
            }

            if (string.IsNullOrEmpty(phone)) throw new Exception("Не правильні параметри");
            if (phone.Length != 9) throw new Exception("Не правильні параметри");

            User targetUser = myDataService.GetUserById(userId);

            if (targetUser != null)
            {
                targetUser.phone = phone;
                myDataService.UpdateUser(userId, targetUser);
                HttpContext.Session.SetInt32("userAuthed", 1);
                return Json(new { Success = true });
            }
            throw new Exception("Щось не так");
        }

        [HttpGet]
        public IActionResult isAuted()
        {
            int? userAuthed = HttpContext.Session.GetInt32("userAuthed");
            if (userAuthed.HasValue)
            {
                if (userAuthed == 1)
                {
                    return Json(new { authed = true });
                }
            }
            return Json(new { authed = false });
        }

        public static string ComputeHash(string codeVerifier)
        {
            using var sha256 = SHA256.Create();
            var challengeBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(codeVerifier));
            var codeChallenge = Base64Url.Encode(challengeBytes);
            return codeChallenge;
        }
    }
}
