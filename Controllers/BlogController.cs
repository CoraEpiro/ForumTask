using ForumTask.DbContexts;
using ForumTask.DbModels;
using ForumTask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace ForumTask.Controllers
{
    public class BlogController : Controller
    {
        private readonly BlogDataContext _context;

        public BlogController(BlogDataContext context)
        {
            _context = context;
        }

        public void AddDefaultData()
        {
            var blog1 = new Blog()
            {
                Title = "Dolorum optio tempore voluptas dignissimos cumque fuga qui quibusdam quia",
                CoreImage = "~/img/blog/blog-1.jpg",
                ShortContent = "Similique neque nam consequuntur ad non maxime aliquam quas. Quibusdam animi praesentium. Aliquam et laboriosam eius aut nostrum quidem aliquid dicta.\r\n                  Et eveniet enim. Qui velit est ea dolorem doloremque deleniti aperiam unde soluta. Est cum et quod quos aut ut et sit sunt. Voluptate porro consequatur assumenda perferendis dolore.",
                LongContent = new List<string>() {
                    "Similique neque nam consequuntur ad non maxime aliquam quas. Quibusdam animi praesentium. Aliquam et laboriosam eius aut nostrum quidem aliquid dicta. Et eveniet enim. Qui velit est ea dolorem doloremque deleniti aperiam unde soluta. Est cum et quod quos aut ut et sit sunt. Voluptate porro consequatur assumenda perferendis dolore.\r\n\r\n",
                    "Sit repellat hic cupiditate hic ut nemo. Quis nihil sunt non reiciendis. Sequi in accusamus harum vel aspernatur. Excepturi numquam nihil cumque odio. Et voluptate cupiditate.\r\n\r\n",
                    "Et vero doloremque tempore voluptatem ratione vel aut. Deleniti sunt animi aut. Aut eos aliquam doloribus minus autem quos.\r\n\r\n",
                    "Sed quo laboriosam qui architecto. Occaecati repellendus omnis dicta inventore tempore provident voluptas mollitia aliquid. Id repellendus quia. Asperiores nihil magni dicta est suscipit perspiciatis. Voluptate ex rerum assumenda dolores nihil quaerat. Dolor porro tempora et quibusdam voluptas. Beatae aut at ad qui tempore corrupti velit quisquam rerum. Omnis dolorum exercitationem harum qui qui blanditiis neque. Iusto autem itaque. Repudiandae hic quae aspernatur ea neque qui. Architecto voluptatem magni. Vel magnam quod et tempora deleniti error rerum nihil tempora.\r\n\r\n",
                    "Et quae iure vel ut odit alias.\r\n",
                    "Officiis animi maxime nulla quo et harum eum quis a. Sit hic in qui quos fugit ut rerum atque. Optio provident dolores atque voluptatem rem excepturi molestiae qui. Voluptatem laborum omnis ullam quibusdam perspiciatis nulla nostrum. Voluptatum est libero eum nesciunt aliquid qui. Quia et suscipit non sequi. Maxime sed odit. Beatae nesciunt nesciunt accusamus quia aut ratione aspernatur dolor. Sint harum eveniet dicta exercitationem minima. Exercitationem omnis asperiores natus aperiam dolor consequatur id ex sed. Quibusdam rerum dolores sint consequatur quidem ea. Beatae minima sunt libero soluta sapiente in rem assumenda. Et qui odit voluptatem. Cum quibusdam voluptatem voluptatem accusamus mollitia aut atque aut.\r\n\r\n",
                    "Ut repellat blanditiis est dolore sunt dolorum quae.\r\n",
                    "Rerum ea est assumenda pariatur quasi et quam. Facilis nam porro amet nostrum. In assumenda quia quae a id praesentium. Quos deleniti libero sed occaecati aut porro autem. Consectetur sed excepturi sint non placeat quia repellat incidunt labore. Autem facilis hic dolorum dolores vel. Consectetur quasi id et optio praesentium aut asperiores eaque aut. Explicabo omnis quibusdam esse. Ex libero illum iusto totam et ut aut blanditiis. Veritatis numquam ut illum ut a quam vitae.\r\n\r\n",
                    "Alias quia non aliquid. Eos et ea velit. Voluptatem maxime enim omnis ipsa voluptas incidunt. Nulla sit eaque mollitia nisi asperiores est veniam.\r\n\r\n"
                },
                ContentOwner = "John Doe",
                ReleaseDate = DateTime.Now,
                Category = "Business",
                Tags = new List<string>()
                {
                    "Creative",
                    "Tips",
                    "Marketing"
                },
                ContentImages = new List<string>()
                {
                    "~/img/blog/blog-inside-post.jpg"
                }
            };

            _context.Blogs.Add(blog1);
            _context.SaveChanges();
        }
        public async Task<IActionResult> Blogs(
            int page = 1,
            int pageSize = 2,
            User user = null)
        {
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
    }
}
