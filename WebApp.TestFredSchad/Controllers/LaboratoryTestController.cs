using Microsoft.AspNetCore.Mvc;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.LaboratoryTest;
using WebApp.PatientManager.Middlewares;

namespace WebApp.PatientManager.Controllers
{
    public class LaboratoryTestController : Controller
    {
        private readonly ILaboratoryTestService _laboratoryTestService;
        private readonly ValidateUserSession _validateUserSession;

        public LaboratoryTestController(ILaboratoryTestService laboratoryTestService, ValidateUserSession validateUserSession)
        {
            _laboratoryTestService = laboratoryTestService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(await _laboratoryTestService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SaveLaboratoryTestViewModel vm = new SaveLaboratoryTestViewModel();

            return View("SaveLaboratoryTest", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveLaboratoryTestViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (!ModelState.IsValid)
            {
                return View("SaveLaboratoryTest", vm);

            }


            await _laboratoryTestService.Add(vm);
            return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" }); ;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            var vm = await _laboratoryTestService.GetByIdSaveViewModel(id);

            return View("SaveLaboratoryTest", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveLaboratoryTestViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (!ModelState.IsValid)
            {
                return View("SaveLaboratoryTest", vm);

            }


            await _laboratoryTestService.Update(vm);
            return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" }); ;
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });



            return View(await _laboratoryTestService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            await _laboratoryTestService.Delete(id);
            return RedirectToRoute(new { controller = "LaboratoryTest", action = "Index" });
        }
    }
}
