using ForumTask.DbContexts;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<BlogDataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDataContext")
    , settings =>
    {
        settings.CommandTimeout(30);
        settings.MigrationsHistoryTable("EF_TABLE_MIGRATION");
        settings.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
    }));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Blog}/{action=Blogs}/{id?}");
app.Run();