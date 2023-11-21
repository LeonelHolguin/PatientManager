using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Appointments;
using PatientManager.Core.Application.ViewModels.Patients;
using PatientManager.Core.Application.ViewModels.Medics;
using PatientManager.Core.Domain.Entities;


namespace PatientManager.Core.Application.Services
{
    public class AppointmentService : IGenericService<SaveAppointmentViewModel, AppointmentViewModel>, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<SaveAppointmentViewModel> Add(SaveAppointmentViewModel appointmentToSave)
        {
            Appointment appointment = new();

            appointment.Id = appointmentToSave.Id;
            appointment.AppointmentDate = (DateOnly)appointmentToSave.AppointmentDate;
            appointment.AppointmentTime = (TimeOnly)appointmentToSave.AppointmentTime;
            appointment.Reason = appointmentToSave.Reason;
            appointment.State = appointmentToSave.State;
            appointment.PatientId = appointmentToSave.PatientId;
            appointment.MedicId = appointmentToSave.MedicId;

            appointment = await _appointmentRepository.AddAsync(appointment);

            SaveAppointmentViewModel viewModel = new();
            viewModel.Id = appointmentToSave.Id;
            viewModel.AppointmentDate = (DateOnly)appointment.AppointmentDate;
            viewModel.AppointmentTime = (TimeOnly)appointment.AppointmentTime;
            viewModel.Reason = appointment.Reason;
            viewModel.State = appointment.State;
            viewModel.PatientId = appointment.PatientId;
            viewModel.MedicId = appointment.MedicId;

            return viewModel;
        }   

        public async Task Update(SaveAppointmentViewModel appointmentToSave)
        {
            Appointment appointment = new();

            appointment.Id = appointmentToSave.Id;
            appointment.AppointmentDate = (DateOnly)appointmentToSave.AppointmentDate;
            appointment.AppointmentTime = (TimeOnly)appointmentToSave.AppointmentTime;
            appointment.Reason = appointmentToSave.Reason;
            appointment.State = appointmentToSave.State;
            appointment.PatientId = appointmentToSave.PatientId;
            appointment.MedicId = appointmentToSave.MedicId;
            appointment.Created = (DateTime)appointmentToSave.Created;
            appointment.CreatedBy = appointmentToSave.CreatedBy;

            await _appointmentRepository.UpdateAsync(appointment);
        }

        public async Task Delete(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            await _appointmentRepository.DeleteAsync(appointment);
        }

        public async Task<List<AppointmentViewModel>> GetAllViewModel()
        {
            var appointmentList = await _appointmentRepository.GetAllAsync();

            return appointmentList.Select(appointment => new AppointmentViewModel
            {
                Id = appointment.Id,
                AppointmentDate = appointment.AppointmentDate,
                AppointmentTime = appointment.AppointmentTime,
                Reason = appointment.Reason,
                State = appointment.State,
                Patient = new PatientViewModel
                {
                    Id = appointment.Patient!.Id,
                    Name = appointment.Patient.Name,
                    LastName = appointment.Patient.LastName,
                },
                Medic = new MedicViewModel
                {
                    Id = appointment.Medic!.Id,
                    Name= appointment.Medic.Name,
                    LastName= appointment.Medic.LastName,
                },
            }).ToList();
        }

        public async Task<AppointmentViewModel> GetById(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            AppointmentViewModel appointmentVM = new();
            appointmentVM.Id = appointment.Id;
            appointmentVM.AppointmentDate = appointment.AppointmentDate;
            appointmentVM.AppointmentTime = appointment.AppointmentTime;
            appointmentVM.Reason = appointment.Reason;
            appointmentVM.State = appointment.State;
            appointmentVM.Patient = new PatientViewModel
            {
                Id = appointment.Patient!.Id,
                Name = appointment.Patient.Name,
                LastName = appointment.Patient.LastName,
            };
            appointmentVM.Medic = new MedicViewModel
            {
                Id = appointment.Medic!.Id,
                Name = appointment.Medic.Name,
                LastName = appointment.Medic.LastName,
            };

            return appointmentVM;
        }

        public async Task<SaveAppointmentViewModel> GetByIdSaveViewModel(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);

            SaveAppointmentViewModel appointmentVM = new();
            appointmentVM.Id = appointment.Id;
            appointmentVM.AppointmentDate = appointment.AppointmentDate;
            appointmentVM.AppointmentTime = appointment.AppointmentTime;
            appointmentVM.Reason = appointment.Reason;
            appointmentVM.State = appointment.State;
            appointmentVM.PatientId = appointment.PatientId;
            appointmentVM.MedicId = appointment.MedicId;
            appointmentVM.Created = appointment.Created;
            appointmentVM.CreatedBy = appointment.CreatedBy;

            return appointmentVM;
        }
    }
}
