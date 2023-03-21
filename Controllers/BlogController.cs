using ForumTask.DbContexts;
using ForumTask.DbModels;
using ForumTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.IO.Compression;
using System.Security.Claims;

namespace ForumTask.Controllers
{
    public static class Userduser
    {
        public static User user;
    }
    public class BlogController : Controller
    {
        private readonly BlogDataContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        private User _user;

        public BlogController(BlogDataContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
            if(_context.Blogs.Count() < 5)
            {
                AddDefaultData(10);
            }
        }

        private void AddDefaultData(int count)
        {
            for (int i = 0; i < count; i++)
            {

                var blog = new Blog()
                {
                    Title = "Why everyone should try GPT-4, even the CEO",
                    CoreImage = "~/img/chat-gpt-image-1.png",
                    LongContent = "One week ago, on Mar 14, OpenAI released GPT-4. I’ll admit to being one of the users who rushed to pay my $20/month to play with it in ChatGPT Plus and I’m impressed. Let me take this opportunity to congratulate OpenAI on a tremendous achievement in democratizing AI. Bravo!\r\n\r\nToday, Google released Bard, giving the public access to its alternative to GPT-4, LaMDA, for free via a waitlist. Another bravo!\r\n\r\nWhat I’m far less impressed with is some of the online chatter on the subject. Too many hot takes seem to be coming from people who’ve never used it themselves and have only heard about GPT-4 from their cousin’s daughter’s yoga teacher’s prize cabbage.\r\n\r\nI could join the fray and write all about how GPT-4 and LaMDA aren’t perfect and they still makes plenty of factual errors (though they’re less gaudy than the mistakes we’ve been poking fun at for the last few months) but here’s what I’ll say instead:\r\n\r\nFirstly, never trust anything you haven’t tested thoroughly. Secondly…\r\n\r\nStop reading about GPT-4 and LaMDA and go spend that time getting your hands dirty with them.\r\n\r\nI feel personally compelled to point you in the direction of self-improvement: right now, GPT-4 and LaMDA are the most cutting edge AI productivity tools released to the general public, so please invest some time in becoming a more AI-informed citizen — stop reading about them and go spend the time getting your hands dirty with them. (Feel free to abandon this article too, I’ll survive.)\r\n\r\nThat’s how you’ll know what they are and whether they’re useful to you. I promise you that you don’t need any special training to interact with them, all you do is you type your text in a text box, just like using a text messaging app:\r\n\r\nTo try GPT-4, you just sign up for ChatGPT Plus and off you go. (Costs $20/month.)\r\nTo try LaMDA, you just sign up for Bard and off you go. (Free, but there might a waitlist.)\r\nDive right in! You can’t do any harm with these things if you keep your (un-fact-checked) output to yourself. Avoid putting confidential/sensitive info in there, though.\r\n\r\nThe fact that there’s two of them means my advice doubles: try them both! Copy-paste from one to the other. Feel free to drop the same text into both and see what comes out.",
                    ContentOwner = "Chat GPT",
                    ReleaseDate = DateTime.Now,
                    Category = "Business",
                    Tags = new List<string>()
                    {
                        "Artificial Intelligence",
                        "Technology",
                        "Machine Learning"
                    }
                };

                blog.ShortContent = blog.LongContent.Length < 300 ? blog.LongContent : blog.LongContent.Substring(0, 300) + "...";

                _context.Blogs.Add(blog);
                _context.SaveChanges();
            }
        }
        public async Task<IActionResult> Blogs(
            int page = 1,
            int pageSize = 2,
            User user = null,
            int userID = 0)
        {
            _user = user;
            if (userID != 0)
                FindUser(userID);

            var count = await _context.Blogs.CountAsync();
            var data = await _context.Blogs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var view = new PaginationModel<Blog>(data, page, pageSize, count);
            ViewData["User"] = _user;

            return View(view);
        }

        public IActionResult SingleBlog(int blogID, int userID)
        {
            FindUser(userID);
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == blogID);
            ViewData["User"] = _user;
            return View(blog);
        }

        public void DeleteBlog(int id)
        {
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == id);
            _context.Blogs.Remove(blog);
            _context.SaveChanges();
        }

        public IActionResult EditBlog(int blogID, int userID)
        {
            FindUser(userID);
            var blog = _context.Blogs.FirstOrDefault(b => b.Id == blogID);
            ViewData["User"] = _user;
            return View(blog);
        }
        public IActionResult SaveBlog(int id, string titleArea, string contentArea)
        {
            var blog = _context.Blogs.Find(id);
            blog.Title = titleArea;
            blog.LongContent = contentArea;
            blog.ShortContent = blog.LongContent.Length < 300 ? blog.LongContent : blog.LongContent.Substring(0, 300) + "...";
            _context.Blogs.Update(blog);
            _context.SaveChanges();
            return RedirectToAction("Blogs", "Blog");
        }

        [HttpPost]
        public async Task<IActionResult> CreateBlog(Blog blog)
        {
            blog.CoreImage = "~/img/chat-gpt-image-1.png";
            // Blog id comes with the id of the user, therefore, we are setting it 0 for making enable to generate automatically.
            blog.Id = 0;
            blog.ShortContent = blog.LongContent.Length < 300 ? blog.LongContent : blog.LongContent.Substring(0, 300) + "...";
            _context.Blogs.Add(blog);
            _context.SaveChanges();
            return RedirectToAction("Blogs");
        }

        [Authorize(Roles = "admin,client")]
        public IActionResult CreateBlog(int id)
        {
            FindUser(id);
            ViewData["User"] = _user;
            ViewData["ContentOwner"] = _user.Name;
            return View();
        }

        private void FindUser(int id)
        {
            _user = _context.Users.FirstOrDefault(x => x.Id == id)!;
        }
    }
}