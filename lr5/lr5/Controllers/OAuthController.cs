using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace lr5.Controllers
{
    public class OAuthController : Controller
    {
        private readonly MyDataService myDataService;

        public OAuthController(MyDataService my)
        {
            myDataService = my;
        }
        public ActionResult RedirectToOAuthServer()
        {
            var scopes = new[]{"https://www.googleapis.com/auth/userinfo.profile", "https://www.googleapis.com/auth/userinfo.email"};
            string scope = string.Join(" ", scopes);

            string redirectUrl = $"https://localhost:5001/OAuth/Code";
            string verCode = Guid.NewGuid().ToString();
            string code = ComputeHash(verCode);

            HttpContext.Session.SetString("codeVerifier", verCode);

            var url = GoogleAuth.GenerateOAuthReqUrl(scope, redirectUrl, code);
            return Redirect(url);
        }

        public async Task<ActionResult> Code(string code)
        {
            string redirectUrl = "https://localhost:5001/OAuth/Code";
            string verCode = HttpContext.Session.GetString("codeVerifier");
            TokenResult resToken = await GoogleAuth.ExchangeCodeToToken(code, verCode, redirectUrl);
            if (resToken != null)
            {
                UserInfo res = await GoogleAuth.GetUserInfo(resToken.access_token);
                if (res != null)
                {
                    User targetUser = myDataService.GetUserById(res.Id);

                    HttpContext.Session.SetString("userId", res.Id);
                    HttpContext.Session.SetString("accessToken", resToken.access_token);
                    HttpContext.Session.SetInt32("userAuthed", 0);

                    if (targetUser == null || string.IsNullOrEmpty(targetUser.phone))
                    {
                        if (targetUser == null)
                        {
                            myDataService.AddUser(new User() { id= res.Id, name=res.Name, email=res.Email});
                        }
                        return RedirectToAction("Reg", "Home");
                    }
                    else
                    {
                        HttpContext.Session.SetInt32("userAuthed", 1);
                        return RedirectToAction("Index", "Home");
                    }
                }
            } 
            
            return RedirectToAction("Login", "Home");
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
