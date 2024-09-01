using ElearningMVC.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ElearningMVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly ElearningContext db;

        public AuthController(ElearningContext db) 
        {
            this.db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [AcceptVerbs("Post", "Get")]
        public IActionResult CheckExistingEmailId(string email)
        {
            var data = db.UserAccounts.Where(x => x.UserEmail == email).SingleOrDefault();
            if (data != null)
            {
                return Json($"Email {email} already in used");
            }
            else
            {
                return Json(true);
            }
        }

        [HttpPost]
        public IActionResult SignUp(UserAccount user)
        {
            user.UserRole = "User";
            db.UserAccounts.Add(user);
            db.SaveChanges();
            return RedirectToAction("SignIn");
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignIn(UserAccount user)
        {
            var data = db.UserAccounts.Where(x => x.UserEmail.Equals(user.UserEmail)).SingleOrDefault();
            if (data != null)
            {
                bool v = data.UserEmail.Equals(user.UserEmail) && data.UserPass.Equals(user.UserPass);
                if (v)
                {
                    var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, user.UserEmail) },
                        CookieAuthenticationDefaults.AuthenticationScheme);

                    var principal = new ClaimsPrincipal(identity);

                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
                    HttpContext.Session.SetString("uemail", user.UserEmail);
                    return RedirectToAction("index", "home");
                }
                else
                {
                    TempData["errorpasword"] = "Invalid Password";
                }
            }
            else
            {
                TempData["erroremail"] = "Invalid Email";
            }
            return View();
        }

		public IActionResult ExternalLogin(string provider)
		{
			var redirectUrl = Url.Action("ExternalLoginCallback", "Auth");
			var properties = new AuthenticationProperties { RedirectUri = redirectUrl };
			return Challenge(properties, provider);
		}

		public async Task<IActionResult> ExternalLoginCallback()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			if (!result.Succeeded) return RedirectToAction("SignIn");

			var claims = result.Principal.Identities.FirstOrDefault().Claims;
			var email = claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

			var user = db.UserAccounts.SingleOrDefault(x => x.UserEmail == email);
			if (user == null)
			{
				user = new UserAccount { UserEmail = email, UserRole = "User" };
				db.UserAccounts.Add(user);
				db.SaveChanges();
			}

			var identity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, email) },
				CookieAuthenticationDefaults.AuthenticationScheme);
			var principal = new ClaimsPrincipal(identity);

			await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
			HttpContext.Session.SetString("uemail", email);
			return RedirectToAction("Index", "home");
		}


		public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var storedcookies = Request.Cookies.Keys;
            foreach (var cookie in storedcookies)
            {
                Response.Cookies.Delete(cookie);
            }
            return RedirectToAction("SignIn");
        }


    }
}

