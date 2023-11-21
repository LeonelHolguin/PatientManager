using Microsoft.AspNetCore.Mvc;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.Services;
using PatientManager.Core.Application.ViewModels.LaboratoryTestResults;
using WebApp.PatientManager.Middlewares;

namespace WebApp.PatientManager.Controllers
{
    public class LaboratoryTestResultController : Controller
    {
        private readonly ILaboratoryTestResultService _laboratoryTestResultService;
        private readonly ValidateUserSession _validateUserSession;

        public LaboratoryTestResultController(ILaboratoryTestResultService laboratoryTestResultService, ValidateUserSession validateUserSession)
        {
            _laboratoryTestResultService = laboratoryTestResultService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            var laboratoryTestResultList = await _laboratoryTestResultService.GetAllViewModel();

            laboratoryTestResultList = laboratoryTestResultList.Where(LBR => LBR.State != Constants.LaboratoryTestState.Completed).ToList();

            return View(laboratoryTestResultList);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string IdentityCardPatient)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            var resultsForPatient = await _laboratoryTestResultService.GetAllByIdentityCardPatient(IdentityCardPatient);

            resultsForPatient = resultsForPatient.Where(results => results.State != Constants.LaboratoryTestState.Completed).ToList();

            return View(resultsForPatient);
        }

        public async Task<IActionResult> SubmitResults(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            var vm = await _laboratoryTestResultService.GetByIdSaveViewModel(id);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> SubmitResults(SaveLaboratoryTestResultViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (vm.Description == null)
            {
                ModelState.AddModelError("Description", "Debe digitar el resultado de la prueba.");
                return View("SaveLaboratoryTestResult", vm);

            }
            
            await _laboratoryTestResultService.Update(vm);
            return RedirectToRoute(new { controller = "LaboratoryTestResult", action = "Index" }); ;
        }
    }
}
