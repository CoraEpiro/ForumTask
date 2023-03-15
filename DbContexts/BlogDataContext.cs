using ForumTask.DbModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace ForumTask.DbContexts
{
    public class BlogDataContext : DbContext
    {
        public BlogDataContext(DbContextOptions<BlogDataContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var stringListConverter = new StringListToStringConverter();

            modelBuilder.Entity<Blog>()
                .Property(p => p.Tags)
                .HasConversion(stringListConverter!);

            modelBuilder.Entity<Blog>()
                .Property(p => p.ContentImages)
                .HasConversion(stringListConverter!);

            modelBuilder.Entity<Blog>()
                .Property(p => p.LongContent)
                .HasConversion(stringListConverter!);
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
