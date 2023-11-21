using Microsoft.AspNetCore.Mvc;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Users;
using PatientManager.Core.Application.Helpers;
using WebApp.PatientManager.Middlewares;

namespace WebApp.PatientManager.Controllers
{
    public class UserActionsController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public UserActionsController(IUserService userService, ValidateUserSession validateUserSession)
        {
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public IActionResult Index()
        {
            if (_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel userToSignIn)
        {
            if (_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            if (!ModelState.IsValid)
            {
                return View(userToSignIn);

            }

            UserViewModel userVM = await _userService.Login(userToSignIn);

            if (userVM != null)
            {
                HttpContext.Session.Set<UserViewModel>("user", userVM);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }
            else
            {
                ModelState.AddModelError("userValidation", "Usuario o contraseña incorrectos");
            }

            return View(userToSignIn);
        }


        public IActionResult Logout() 
        {
            HttpContext.Session.Remove("user");

            return RedirectToRoute(new { controller = "UserActions", action = "Index" });
        }
    }
}
