using System.Diagnostics;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using stackoverblow.Models;
using StackOverblow.Models;

namespace StackOverblow.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly AppDbContext _context;

    public HomeController(ILogger<HomeController> logger, AppDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpPost]
    public async Task<IActionResult> Index(string q)
    {
        if (q.Length == 0)
            return View();

        var researchModel = new ResearchModel(q);
        return View(researchModel);
    }
}
