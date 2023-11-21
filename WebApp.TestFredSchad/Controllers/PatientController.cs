using Microsoft.AspNetCore.Mvc;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Medics;
using PatientManager.Core.Application.ViewModels.Patients;
using WebApp.PatientManager.Middlewares;

namespace WebApp.PatientManager.Controllers
{
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ValidateUserSession _validateUserSession;

        public PatientController(IPatientService patientService, ValidateUserSession validateUserSession)
        {
            _patientService = patientService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(await _patientService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SavePatientViewModel vm = new SavePatientViewModel();

            return View("SavePatient", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SavePatientViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (!ModelState.IsValid)
            {
                if(vm.BirthDate == null)
                    ModelState.AddModelError("BirthDate", "Debe colocar una fecha.");
                return View("SavePatient", vm);

            }


            SavePatientViewModel viewModel = await _patientService.Add(vm);
            if (viewModel != null && viewModel.Id != 0)
            {
                viewModel.Photo = UploadFile(vm.File, viewModel.Id);
                await _patientService.Update(viewModel);
            }

            return RedirectToRoute(new { controller = "Patient", action = "Index" }); ;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            var vm = await _patientService.GetByIdSaveViewModel(id);


            return View("SavePatient", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SavePatientViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            SavePatientViewModel viewModel = await _patientService.GetByIdSaveViewModel(vm.Id);

            if (!ModelState.IsValid)
            {
                if (vm.Name != null && vm.LastName != null && vm.Phone != null && vm.IdentityCard != null)
                {
                    vm.Photo = UploadFile(vm.File, viewModel.Id, true, viewModel.Photo);

                    await _patientService.Update(vm);
                    return RedirectToRoute(new { controller = "Patient", action = "Index" }); ;
                }

                return View("SavePatient", vm);

            }

            vm.Photo = UploadFile(vm.File, viewModel.Id, true, viewModel.Photo);

            await _patientService.Update(vm);
            return RedirectToRoute(new { controller = "Patient", action = "Index" }); ;
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });



            return View(await _patientService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            await _patientService.Delete(id);

            string basePath = $"/Images/Patients/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach (FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach (DirectoryInfo folder in directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }

            return RedirectToRoute(new { controller = "Patient", action = "Index" });
        }



        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string photo = "")
        {
            if (isEditMode && file == null)
            {
                return photo;
            }

            string basePath = $"/Images/Patients/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new FileInfo(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename);

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if (isEditMode)
            {
                string[] oldImagePart = photo.Split("/");
                string oldImageName = oldImagePart[oldImagePart.Length - 1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if (System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{filename}";
        }

    }
}
