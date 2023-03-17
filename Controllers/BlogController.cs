using ForumTask.DbContexts;
using ForumTask.DbModels;
using ForumTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO.Compression;


namespace ForumTask.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDataContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BlogController(BlogDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
        }

        private void AddDefaultData()
        {
            var blog1 = new Blog()
            {
                Title = "Dolorum optio tempore voluptas dignissimos cumque fuga qui quibusdam quia",
                CoreImage = "~/img/blog/blog-1.jpg",
                ShortContent = "Similique neque nam consequuntur ad non maxime aliquam quas. Quibusdam animi praesentium. Aliquam et laboriosam eius aut nostrum quidem aliquid dicta.\r\n                  Et eveniet enim. Qui velit est ea dolorem doloremque deleniti aperiam unde soluta. Est cum et quod quos aut ut et sit sunt. Voluptate porro consequatur assumenda perferendis dolore.",
                LongContent = "smth",
                ContentOwner = "John Doe",
                ReleaseDate = DateTime.Now,
                Category = "Business",
                Tags = new List<string>()
                {
                    "Creative",
                    "Tips",
                    "Marketing"
                }
            };

            _context.Blogs.Add(blog1);
            _context.SaveChanges();
        }
        public async Task<IActionResult> Blogs(
            int page = 1,
            int pageSize = 2,
            User user = null,
            int userID = 0)
        {
            if (user.Role == null)
                user = _context.Users.Find(userID);

            var count = await _context.Blogs.CountAsync();
            var data = await _context.Blogs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var view = new PaginationModel<Blog>(data, page, pageSize, count);
            ViewData["User"] = user;
            return View(view);
        }

        public IActionResult SingleBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            return View(blog);
        }

        public void DeleteBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }
        public IActionResult EditBlog(int id)
        {
            var blog = _context.Blogs.Find(id);
            return View(blog);
        }
        public IActionResult SaveBlog(int id, string titleArea, string contentArea)
        {
            var blog = _context.Blogs.Find(id);
            blog.Title = titleArea;
            blog.LongContent = contentArea;
            _context.Blogs.Update(blog);
            _context.SaveChanges();
            return RedirectToAction("EditBlog", "Blog", blog);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(Blog blog)
        {
            blog.CoreImage = "~img/blog/blog-1.jpg";
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("SingleBlog", blog.Id);
        }
        public IActionResult CreateBlog(int id)
        {
            //ViewData["ContentOwner"] = _context.Blogs.Find(id).ContentOwner;
            return View();
        }
    }
}