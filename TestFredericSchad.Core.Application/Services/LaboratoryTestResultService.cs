using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.LaboratoryTestResults;
using PatientManager.Core.Application.ViewModels.Appointments;
using PatientManager.Core.Application.ViewModels.Patients;
using PatientManager.Core.Application.ViewModels.LaboratoryTest;
using PatientManager.Core.Domain.Entities;


namespace PatientManager.Core.Application.Services
{
    public class LaboratoryTestResultService : IGenericService<SaveLaboratoryTestResultViewModel, LaboratoryTestResultViewModel>, ILaboratoryTestResultService
    {
        private readonly ILaboratoryTestResultRepository _laboratoryTestResultRepository;
        public LaboratoryTestResultService(ILaboratoryTestResultRepository laboratoryTestResultRepository)
        {
            _laboratoryTestResultRepository = laboratoryTestResultRepository;
        }

        public async Task<SaveLaboratoryTestResultViewModel> Add(SaveLaboratoryTestResultViewModel laboratoryTestResultToSave)
        {
            LaboratoryTestResult laboratoryTestResult = new();

            laboratoryTestResult.Id = laboratoryTestResultToSave.Id;
            laboratoryTestResult.Description = laboratoryTestResultToSave.Description;
            laboratoryTestResult.State = laboratoryTestResultToSave.State;
            laboratoryTestResult.PatientId = laboratoryTestResultToSave.PatientId;
            laboratoryTestResult.LaboratoryTestId = laboratoryTestResultToSave.LaboratoryTestId;
            laboratoryTestResult.AppointmentId = laboratoryTestResultToSave.AppointmentId;

            laboratoryTestResult = await _laboratoryTestResultRepository.AddAsync(laboratoryTestResult);

            SaveLaboratoryTestResultViewModel viewModel = new();

            viewModel.Id = laboratoryTestResult.Id;
            viewModel.Description = laboratoryTestResult.Description;
            viewModel.State = laboratoryTestResult.State;
            viewModel.PatientId = laboratoryTestResult.PatientId;
            viewModel.LaboratoryTestId = laboratoryTestResult.LaboratoryTestId;
            viewModel.AppointmentId = laboratoryTestResult.AppointmentId;

            return viewModel;

        }

        public async Task Update(SaveLaboratoryTestResultViewModel laboratoryTestResultToSave)
        {
            LaboratoryTestResult laboratoryTestResult = new();

            laboratoryTestResult.Id = laboratoryTestResultToSave.Id;
            laboratoryTestResult.Description = laboratoryTestResultToSave.Description;
            laboratoryTestResult.State = laboratoryTestResultToSave.State;
            laboratoryTestResult.PatientId = laboratoryTestResultToSave.PatientId;
            laboratoryTestResult.LaboratoryTestId = laboratoryTestResultToSave.LaboratoryTestId;
            laboratoryTestResult.AppointmentId = laboratoryTestResultToSave.AppointmentId;
            laboratoryTestResult.Created = (DateTime)laboratoryTestResultToSave.Created;
            laboratoryTestResult.CreatedBy = laboratoryTestResultToSave.CreatedBy;

            await _laboratoryTestResultRepository.UpdateAsync(laboratoryTestResult);
        }

        public async Task Delete(int id)
        {
            var laboratoryTestResult = await _laboratoryTestResultRepository.GetByIdAsync(id);
            await _laboratoryTestResultRepository.DeleteAsync(laboratoryTestResult);
        }

        public async Task BulkDeleteByAppointment(int id)
        {
            await _laboratoryTestResultRepository.BulkDeleteByAppointmentAsync(id);
        }

        public async Task<List<LaboratoryTestResultViewModel>> GetAllViewModel()
        {
            var laboratoryTestResultList = await _laboratoryTestResultRepository.GetAllAsync();

            return laboratoryTestResultList.Select(laboratoryTestResult => new LaboratoryTestResultViewModel
            {
                Id = laboratoryTestResult.Id,
                Description = laboratoryTestResult.Description,
                State = laboratoryTestResult.State,
                Patient = new PatientViewModel
                {
                    Id = laboratoryTestResult.Patient.Id,
                    Name = laboratoryTestResult.Patient.Name,
                    LastName = laboratoryTestResult.Patient.LastName,
                    IdentityCard = laboratoryTestResult.Patient.IdentityCard,

                },
                LaboratoryTest = new LaboratoryTestViewModel
                {
                    Id = laboratoryTestResult.LaboratoryTest!.Id,
                    Name = laboratoryTestResult.LaboratoryTest.Name,
                },
                Appointment = laboratoryTestResult.Appointment == null ? null : new AppointmentViewModel
                {
                    Id = laboratoryTestResult.Appointment.Id,
                }
            }).ToList();
        }

        public async Task<List<LaboratoryTestResultViewModel>> GetAllByIdentityCardPatient(string? identityCardPatient)
        {
            var laboratoryTestResultList = await _laboratoryTestResultRepository.GetAllAsync();

            if(identityCardPatient != null)
            {
                laboratoryTestResultList = laboratoryTestResultList
                    .Where(laboratoryTestResult => laboratoryTestResult.Patient.IdentityCard
                    .ToLower()
                    .Contains(identityCardPatient.ToLower())).ToList();
            }



            return laboratoryTestResultList.Select(laboratoryTestResult => new LaboratoryTestResultViewModel
            {
                Id = laboratoryTestResult.Id,
                Description = laboratoryTestResult.Description,
                State = laboratoryTestResult.State,
                Patient = new PatientViewModel
                {
                    Id = laboratoryTestResult.Patient!.Id,
                    Name = laboratoryTestResult.Patient.Name,
                    LastName = laboratoryTestResult.Patient.LastName,
                    IdentityCard = laboratoryTestResult.Patient.IdentityCard,

                },
                LaboratoryTest = new LaboratoryTestViewModel
                {
                    Id = laboratoryTestResult.LaboratoryTest!.Id,
                    Name = laboratoryTestResult.LaboratoryTest.Name,
                },
                Appointment = laboratoryTestResult.Appointment == null ? null : new AppointmentViewModel
                {
                    Id = laboratoryTestResult.Appointment.Id,
                }
            }).ToList();
        }

        public async Task<LaboratoryTestResultViewModel> GetById(int id)
        {
            var laboratoryTestResult = await _laboratoryTestResultRepository.GetByIdAsync(id);

            LaboratoryTestResultViewModel laboratoryTestResultVM = new();
            laboratoryTestResultVM.Id = laboratoryTestResult.Id;
            laboratoryTestResultVM.Id = laboratoryTestResult.Id;
            laboratoryTestResultVM.Description = laboratoryTestResult.Description;
            laboratoryTestResultVM.State = laboratoryTestResult.State;
            laboratoryTestResultVM.Patient = new PatientViewModel
            {
                Id = laboratoryTestResult.Patient!.Id,
                Name = laboratoryTestResult.Patient.Name,
                LastName = laboratoryTestResult.Patient.LastName,
                IdentityCard = laboratoryTestResult.Patient.IdentityCard,

            };
            laboratoryTestResultVM.LaboratoryTest = new LaboratoryTestViewModel
            {
                Id = laboratoryTestResult.LaboratoryTest!.Id,
                Name = laboratoryTestResult.LaboratoryTest.Name,
            };
            laboratoryTestResultVM.Appointment = laboratoryTestResult.Appointment == null ? null : new AppointmentViewModel
            {
                Id = laboratoryTestResult.Appointment.Id,
            };
            return laboratoryTestResultVM;
        }

        public async Task<SaveLaboratoryTestResultViewModel> GetByIdSaveViewModel(int id)
        {
            var laboratoryTestResult = await _laboratoryTestResultRepository.GetByIdAsync(id);

            SaveLaboratoryTestResultViewModel laboratoryTestResultVM = new();
            laboratoryTestResultVM.Id = laboratoryTestResult.Id;
            laboratoryTestResultVM.Description = laboratoryTestResult.Description;
            laboratoryTestResultVM.State = laboratoryTestResult.State;
            laboratoryTestResultVM.PatientId = laboratoryTestResult.PatientId;
            laboratoryTestResultVM.LaboratoryTestId = laboratoryTestResult.LaboratoryTestId;
            laboratoryTestResultVM.AppointmentId = laboratoryTestResult.AppointmentId;
            laboratoryTestResultVM.Created = laboratoryTestResult.Created;
            laboratoryTestResultVM.CreatedBy = laboratoryTestResult.CreatedBy;

            return laboratoryTestResultVM;
        }
    }
}
