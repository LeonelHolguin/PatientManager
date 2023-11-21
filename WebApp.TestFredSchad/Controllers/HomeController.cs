using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.PatientManager.Middlewares;
using WebApp.TestFredSchad.Models;

namespace WebApp.TestFredSchad.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ValidateUserSession _validateUserSession;

        public HomeController(ILogger<HomeController> logger, ValidateUserSession validateUserSession)
        {
            _logger = logger;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            return View();
        }
    }
}