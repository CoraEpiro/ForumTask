using ForumTask.DbContexts;
using ForumTask.DbModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Security.Claims;
using System.Text.Json;

namespace ForumTask.Controllers
{
    public class UserController : Controller
    {
        private readonly BlogDataContext _context;

        public UserController(BlogDataContext context)
        {
            _context = context;
        }
        public IActionResult LogInPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LogInUser(string name, string password)
        {
            var user = _context.Users
            .FirstOrDefault(u => u.Name == name &&
                            u.Password == password);
            if (user is not null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, name.ToLower()),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Role, user.Role.ToLower())
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var authProperties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60)
                };
                HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties
                );
                return RedirectToAction("Blogs", "Blog", user);
            }
            else
                return RedirectToAction("LogInPage", "User");
        }
        public IActionResult RegisterPage()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return LogInUser(user.Name, user.Password);
        }
        public IActionResult LogOutUser()
        {
            HttpContext.SignOutAsync();
            return RedirectToAction("Blogs", "Blog", null);
        }
    }
}