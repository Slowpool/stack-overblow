using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using stackoverblow.Models;
using StackOverblow.Models;

namespace StackOverblow.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
    public async Task<IActionResult> Index(string researchText)
    {
        if (researchText.Length == 0)
            return View();

        var researchModel = new ResearchModel(researchText);
        return View(researchModel);
    }
}