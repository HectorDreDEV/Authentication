using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Basic.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Basic.Controllers;

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
    
    [Authorize]
    public IActionResult Secret()
    {
        return View();
    }

    public IActionResult Authenticate()
    {
        var grandClaims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "Bob"),
            new Claim(ClaimTypes.Email, "Bob@gmail.com"),
            new Claim("ClaimTypes.Custom", "Customizing")
        };
        
        var licenseClaims = new List<Claim>
        {
            new Claim("License.Type", "A2")
        };

        var grandIdentity = new ClaimsIdentity(grandClaims, "Grand Identity");
        var licenseIdentity = new ClaimsIdentity(licenseClaims, "License Identity");

        var userPrincipal = new ClaimsPrincipal(new[] {grandIdentity, licenseIdentity});

        HttpContext.SignInAsync(userPrincipal);
        
        return RedirectToAction("Index");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}