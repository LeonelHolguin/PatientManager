using Microsoft.AspNetCore.Mvc;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Users;
using WebApp.PatientManager.Middlewares;

namespace WebApp.PatientManager.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ValidateUserSession _validateUserSession;

        public UserController(IUserService userService, ValidateUserSession validateUserSession) 
        { 
            _userService = userService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if(!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(await _userService.GetAllViewModel());
        }

        public IActionResult Create() 
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SaveUserViewModel vm = new SaveUserViewModel();

            return View("SaveUser", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);

            }

            if (await _userService.UserNameExists(vm.UserName))
            {
                ModelState.AddModelError("UserName", "El nombre de usuario ya existe, por favor escriba otro.");
                return View("SaveUser", vm);
            }


            await _userService.Add(vm);
            return RedirectToRoute(new { controller = "User", action = "Index" });;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            var vm = await _userService.GetByIdSaveViewModel(id);

            return View("SaveUser", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveUserViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (!ModelState.IsValid)
            {
                return View("SaveUser", vm);

            }

            var possibleMatch = await _userService.GetById(vm.Id);

            if(possibleMatch.UserName.ToLower() != vm.UserName.ToLower()) 
            {
                if (await _userService.UserNameExists(vm.UserName))
                {
                    ModelState.AddModelError("UserName", "El nombre de usuario ya existe, por favor escriba otro.");
                    return View("SaveUser", vm);
                }
            }

            await _userService.Update(vm);
            return RedirectToRoute(new { controller = "User", action = "Index" }); ;
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });



            return View(await _userService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            await _userService.Delete(id);
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }
    }
}
