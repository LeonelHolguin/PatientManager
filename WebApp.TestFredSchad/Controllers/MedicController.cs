using Microsoft.AspNetCore.Mvc;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Medics;
using WebApp.PatientManager.Middlewares;

namespace WebApp.PatientManager.Controllers
{
    public class MedicController : Controller
    {
        private readonly IMedicService _medicService;
        private readonly ValidateUserSession _validateUserSession;

        public MedicController(IMedicService medicService, ValidateUserSession validateUserSession)
        {
            _medicService = medicService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            return View(await _medicService.GetAllViewModel());
        }

        public IActionResult Create()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SaveMedicViewModel vm = new SaveMedicViewModel();

            return View("SaveMedic", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveMedicViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (!ModelState.IsValid)
            {
                return View("SaveMedic", vm);

            }


            SaveMedicViewModel viewModel = await _medicService.Add(vm);
            if (viewModel != null && viewModel.Id != 0)
            {
                viewModel.Photo = UploadFile(vm.File, viewModel.Id);
                await _medicService.Update(viewModel);
            }
            return RedirectToRoute(new { controller = "Medic", action = "Index" }); ;
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            var vm = await _medicService.GetByIdSaveViewModel(id);

            return View("SaveMedic", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SaveMedicViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SaveMedicViewModel viewModel = await _medicService.GetByIdSaveViewModel(vm.Id);

            if (!ModelState.IsValid)
            {
                if (vm.Name != null && vm.LastName != null && vm.Phone != null && vm.IdentityCard != null && vm.Email != null)
                {
                    vm.Photo = UploadFile(vm.File, viewModel.Id, true, viewModel.Photo);

                    await _medicService.Update(vm);
                    return RedirectToRoute(new { controller = "Medic", action = "Index" }); ;
                }

                return View("SaveMedic", vm);

            }

            vm.Photo = UploadFile(vm.File, viewModel.Id, true, viewModel.Photo);

            await _medicService.Update(vm);
            return RedirectToRoute(new { controller = "Medic", action = "Index" }); ;
        }

        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });



            return View(await _medicService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAdministrator())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            await _medicService.Delete(id);


            string basePath = $"/Images/Medics/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            if (Directory.Exists(path))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                foreach(FileInfo file in directoryInfo.GetFiles())
                {
                    file.Delete();
                }

                foreach(DirectoryInfo folder in  directoryInfo.GetDirectories())
                {
                    folder.Delete(true);
                }

                Directory.Delete(path);
            }


            return RedirectToRoute(new { controller = "Medic", action = "Index" });
        }

        private string UploadFile(IFormFile file, int id, bool isEditMode = false, string photo = "")
        {
            if(isEditMode && file == null)
            {
                return photo;
            }

            string basePath = $"/Images/Medics/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");

            if(!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            Guid guid = Guid.NewGuid();
            FileInfo fileInfo = new FileInfo(file.FileName);
            string filename = guid + fileInfo.Extension;

            string fileNameWithPath = Path.Combine(path, filename);

            using(var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            if(isEditMode)
            {
                string[] oldImagePart = photo.Split("/");
                string oldImageName = oldImagePart[oldImagePart.Length-1];
                string completeImageOldPath = Path.Combine(path, oldImageName);

                if(System.IO.File.Exists(completeImageOldPath))
                {
                    System.IO.File.Delete(completeImageOldPath);
                }
            }

            return $"{basePath}/{filename}";
        }
    }
}
