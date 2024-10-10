using DataLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Environment.EnvironmentName = "Development";

// Add services to the container.
builder.Services.AddControllersWithViews();

string connectionString = builder.Configuration.GetConnectionString("Development")!;
//builder.Services.AddTransient(services =>
//{
//    var options = new DbContextOptionsBuilder();
//    options.UseSqlServer(connectionString);
//    return new AppDbContext(options.Options, false);
//});
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connectionString));

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
    name: "main",
    pattern: "{controller=Home}/{action=Index}/{id:int?}");

app.MapControllerRoute(
    name: "users",
    pattern: "{controller=Users}/{action=Index}/{id:int?}");

// further stuff is equal to [HttpGetAttribute(template: "/questions/{questionId:int}", Name = "questions")] for QuestionsController.Index(int questionId)
app.MapControllerRoute(
    name: "questions",
    pattern: "questions/{questionId:int}",
    defaults: new { controller = "Questions", action = "Index" }
    );

app.Run();
