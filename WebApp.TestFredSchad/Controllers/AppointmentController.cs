using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Appointments;
using PatientManager.Core.Application.ViewModels.LaboratoryTestResults;
using WebApp.PatientManager.Middlewares;

namespace WebApp.PatientManager.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        private readonly ILaboratoryTestResultService _laboratoryTestResultService;
        private readonly ILaboratoryTestService _laboratoryTestService;
        private readonly IPatientService _patientService;
        private readonly IMedicService _medicService;
        private readonly ValidateUserSession _validateUserSession;

        public AppointmentController(IAppointmentService appointmentService, 
                                     ILaboratoryTestResultService laboratoryTestResultService,
                                     ILaboratoryTestService LaboratorytestService,
                                     IPatientService patientService,
                                     IMedicService medicService,
                                     ValidateUserSession validateUserSession)
        {
            _appointmentService = appointmentService;
            _laboratoryTestResultService = laboratoryTestResultService;
            _laboratoryTestService = LaboratorytestService;
            _patientService = patientService;
            _medicService = medicService;
            _validateUserSession = validateUserSession;
        }

        public async Task<IActionResult> Index()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });  

            return View(await _appointmentService.GetAllViewModel());
        }

        public async Task<IActionResult> Create()
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });



            SaveAppointmentViewModel vm = new SaveAppointmentViewModel();
            vm.Patients = await _patientService.GetAllViewModel();
            vm.Medics = await _medicService.GetAllViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Create(SaveAppointmentViewModel vm)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            if (!ModelState.IsValid)
            {
                vm.Patients = await _patientService.GetAllViewModel();
                vm.Medics = await _medicService.GetAllViewModel();

                if (vm.AppointmentDate == null)
                    ModelState.AddModelError("AppointmentDate", "Debe colocar una fecha.");
                if(vm.AppointmentTime == null)
                    ModelState.AddModelError("AppointmentTime", "Debe colocar una hora.");
                return View(vm);
            }


            await _appointmentService.Add(vm);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> ReportResult(string ids)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SaveLaboratoryTestResultComplements complements = new SaveLaboratoryTestResultComplements();

            var idList = ids.Split(',').Select(int.Parse).ToList();

            complements.ListLaboratoryTest = await _laboratoryTestService.GetAllViewModel();
            complements.AppointmentId = idList[0];
            complements.PatientId = idList[1];


            return View(complements);
        }

        [HttpPost]
        public async Task<IActionResult> ReportResult(SaveLaboratoryTestResultComplements complements)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            SaveLaboratoryTestResultViewModel vm = new SaveLaboratoryTestResultViewModel();

            var appointmentViewModel = await _appointmentService.GetByIdSaveViewModel(complements.AppointmentId);

            vm.AppointmentId = complements.AppointmentId;
            vm.PatientId = complements.PatientId;
            vm.State = Constants.LaboratoryTestState.Pending;

            foreach(int laboratoryTestId in complements.LaboratoryTestsId)
            {
                vm.LaboratoryTestId = laboratoryTestId;
                await _laboratoryTestResultService.Add(vm);
            }

            appointmentViewModel.State = Constants.AppointmentState.PendingResult;
            await _appointmentService.Update(appointmentViewModel);

            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }


        public async Task<IActionResult> ConsultationResults(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });


            var resultsByAppointment = await _laboratoryTestResultService.GetAllViewModel();
            resultsByAppointment = resultsByAppointment.Where(result => result.Appointment.Id == id).ToList();

            return View(resultsByAppointment);
        }

        public async Task<IActionResult> CompleteAppointment(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            var appointmentToComplete = await _appointmentService.GetByIdSaveViewModel(id);
            appointmentToComplete.State = Constants.AppointmentState.Completed;
            await _appointmentService.Update(appointmentToComplete);

            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }

        public async Task<IActionResult> ViewResults(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            var resultsList = await _laboratoryTestResultService.GetAllViewModel();
            resultsList = resultsList
                .Where(result => 
                result.State == Constants.LaboratoryTestState.Completed && 
                result.Appointment.Id == id).ToList();
   

            return View(resultsList);
        }



        public async Task<IActionResult> Delete(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });



            return View(await _appointmentService.GetByIdSaveViewModel(id));
        }

        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!_validateUserSession.HasUser())
                return RedirectToRoute(new { controller = "UserActions", action = "Index" });

            if (!_validateUserSession.IsAssistant())
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            await _laboratoryTestResultService.BulkDeleteByAppointment(id);

            await _appointmentService.Delete(id);
            return RedirectToRoute(new { controller = "Appointment", action = "Index" });
        }
    }
}
