using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Patients;
using PatientManager.Core.Domain.Entities;

namespace PatientManager.Core.Application.Services
{
    public class PatientService : IGenericService<SavePatientViewModel, PatientViewModel>, IPatientService
    {
        private readonly IPatientRepository _patientRepository;
        public PatientService(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<SavePatientViewModel> Add(SavePatientViewModel patientToSave)
        {
            Patient patient = new();

            patient.Id = patientToSave.Id;
            patient.Name = patientToSave.Name;
            patient.LastName = patientToSave.LastName;
            patient.Phone = patientToSave.Phone;
            patient.Address = patientToSave.Address;
            patient.BirthDate = (DateOnly)patientToSave.BirthDate;
            patient.HasAllergy = patientToSave.HasAllergy;
            patient.IsSmoker = patientToSave.IsSmoker;
            patient.IdentityCard = patientToSave.IdentityCard;
            patient.Photo = patientToSave.Photo;

            patient = await _patientRepository.AddAsync(patient);

            SavePatientViewModel viewModel = new();

            viewModel.Id = patient.Id;
            viewModel.Name = patient.Name;
            viewModel.LastName = patient.LastName;
            viewModel.Phone = patient.Phone;
            viewModel.Address = patient.Address;
            viewModel.BirthDate = patient.BirthDate;
            viewModel.HasAllergy = patient.HasAllergy;
            viewModel.IsSmoker = patient.IsSmoker;
            viewModel.Created = patient.Created;
            viewModel.CreatedBy = patient.CreatedBy;
            viewModel.IdentityCard = patient.IdentityCard;
            viewModel.Photo = patient.Photo;

            return viewModel;
        }

        public async Task Update(SavePatientViewModel patientToSave)
        {
            Patient patient = await _patientRepository.GetByIdAsync(patientToSave.Id);

            patient.Id = patientToSave.Id;
            patient.Name = patientToSave.Name;
            patient.LastName = patientToSave.LastName;
            patient.Phone = patientToSave.Phone;
            patient.Address = patientToSave.Address;
            patient.BirthDate = (DateOnly)patientToSave.BirthDate!;
            patient.HasAllergy = patientToSave.HasAllergy;
            patient.IsSmoker = patientToSave.IsSmoker;
            patient.IdentityCard = patientToSave.IdentityCard;
            patient.Photo = patientToSave.Photo;
            patient.Created = (DateTime)patientToSave.Created!;
            patient.CreatedBy = patientToSave.CreatedBy!;

            await _patientRepository.UpdateAsync(patient);
        }

        public async Task Delete(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);
            await _patientRepository.DeleteAsync(patient);
        }

        public async Task<List<PatientViewModel>> GetAllViewModel()
        {
            var patientList = await _patientRepository.GetAllAsync();

            return patientList.Select(patient => new PatientViewModel
            {
                Id = patient.Id,
                Name = patient.Name,
                LastName = patient.LastName,
                Phone = patient.Phone,
                Address = patient.Address,
                BirthDate = patient.BirthDate,
                HasAllergy = patient.HasAllergy,
                IsSmoker = patient.IsSmoker,
                IdentityCard = patient.IdentityCard,
                Photo = patient.Photo,
        }).ToList();
        }

        public async Task<PatientViewModel> GetById(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            PatientViewModel patientVM = new();
            patientVM.Id = patient.Id;
            patientVM.Name = patient.Name;
            patientVM.Name = patient.Name;
            patientVM.LastName = patient.LastName;
            patientVM.Phone = patient.Phone;
            patientVM.Address = patient.Address;
            patientVM.BirthDate = patient.BirthDate;
            patientVM.HasAllergy = patient.HasAllergy;
            patientVM.IsSmoker = patient.IsSmoker;
            patientVM.IdentityCard = patient.IdentityCard;
            patientVM.Photo = patient.Photo;

            return patientVM;
        }

        public async Task<SavePatientViewModel> GetByIdSaveViewModel(int id)
        {
            var patient = await _patientRepository.GetByIdAsync(id);

            SavePatientViewModel patientVM = new();
            patientVM.Id = patient.Id;
            patientVM.Name = patient.Name;
            patientVM.Name = patient.Name;
            patientVM.Name = patient.Name;
            patientVM.LastName = patient.LastName;
            patientVM.Phone = patient.Phone;
            patientVM.Address = patient.Address;
            patientVM.BirthDate = (DateOnly?)patient.BirthDate;
            patientVM.HasAllergy = patient.HasAllergy;
            patientVM.IsSmoker = patient.IsSmoker;
            patientVM.IdentityCard = patient.IdentityCard;
            patientVM.Photo = patient.Photo;
            patientVM.Created = patient.Created;
            patientVM.CreatedBy = patient.CreatedBy;

            return patientVM;
        }
    }
}
