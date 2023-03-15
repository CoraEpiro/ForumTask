using ForumTask.DbContexts;
using ForumTask.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json;

namespace ForumTask.Controllers
{
    public class UserController : Controller, IAuthorizationFilter
    {
        private readonly BlogDataContext _context;

        public UserController(BlogDataContext context)
        {
            _context = context;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var isAuthorized = context
                .HttpContext
                .Request
                .Query
                .Any(u => u.Key == "role" && u.Value == "admin");
        }

        public IActionResult LogInPage()
        {
            return View();
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
            return RedirectToAction("Blogs", "Blog", user);
        }

        [HttpPost]
        public IActionResult LogInUser(string name, string password)
        {
            var user = _context.Users
            .FirstOrDefault(u => u.Name == name &&
                            u.Password == password);
            return RedirectToAction("Blogs", "Blog", user);
        }
        public IActionResult LogOutUser()
        {
            return RedirectToAction("Blogs", "Blog", null);
        }
    }
}
