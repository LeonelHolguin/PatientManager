using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Medics;
using PatientManager.Core.Domain.Entities;


namespace PatientManager.Core.Application.Services
{
    internal class MedicService : IMedicService
    {
        private readonly IMedicRepository _medicRepository;
        public MedicService(IMedicRepository medicRepository)
        {
            _medicRepository = medicRepository;
        }

        public async Task<SaveMedicViewModel> Add(SaveMedicViewModel medicToSave)
        {
            Medic medic = new();

            medic.Id = medicToSave.Id;
            medic.Name = medicToSave.Name;
            medic.LastName = medicToSave.LastName;
            medic.Email = medicToSave.Email;
            medic.IdentityCard = medicToSave.IdentityCard;
            medic.Phone = medicToSave.Phone;
            medic.Photo = medicToSave.Photo;

            medic = await _medicRepository.AddAsync(medic);

            SaveMedicViewModel viewModel = new();

            viewModel.Id = medic.Id;
            viewModel.Name = medic.Name;
            viewModel.LastName = medic.LastName;
            viewModel.Email = medic.Email;
            viewModel.IdentityCard = medic.IdentityCard;
            viewModel.Phone = medic.Phone;
            viewModel.Photo = medic.Photo;

            return viewModel;
        }

        public async Task Update(SaveMedicViewModel medicToSave)
        {
            Medic medic = await _medicRepository.GetByIdAsync(medicToSave.Id);

            medic.Id = medicToSave.Id;
            medic.Name = medicToSave.Name;
            medic.LastName = medicToSave.LastName;
            medic.Email = medicToSave.Email;
            medic.IdentityCard = medicToSave.IdentityCard;
            medic.Phone = medicToSave.Phone;
            medic.Photo = medicToSave.Photo;

            await _medicRepository.UpdateAsync(medic);
        }

        public async Task Delete(int id)
        {
            var medic = await _medicRepository.GetByIdAsync(id);
            await _medicRepository.DeleteAsync(medic);
        }

        public async Task<List<MedicViewModel>> GetAllViewModel()
        {
            var medicList = await _medicRepository.GetAllAsync();

            return medicList.Select(medic => new MedicViewModel
            {
                Id = medic.Id,
                Name = medic.Name,
                LastName = medic.LastName,
                Email = medic.Email,
                IdentityCard = medic.IdentityCard,
                Phone = medic.Phone,
                Photo = medic.Photo,

            }).ToList();
        }

        public async Task<MedicViewModel> GetById(int id)
        {
            var medic = await _medicRepository.GetByIdAsync(id);

            MedicViewModel medicVM = new();
            medicVM.Id = medic.Id;
            medicVM.Name = medic.Name;
            medicVM.LastName = medic.LastName;
            medicVM.Email = medic.Email;
            medicVM.IdentityCard = medic.IdentityCard;
            medicVM.Phone = medic.Phone;
            medicVM.Photo = medic.Photo;

            return medicVM;
        }

        public async Task<SaveMedicViewModel> GetByIdSaveViewModel(int id)
        {
            var medic = await _medicRepository.GetByIdAsync(id);

            SaveMedicViewModel medicVM = new();
            medicVM.Id = medic.Id;
            medicVM.Name = medic.Name;
            medicVM.LastName = medic.LastName;
            medicVM.Email = medic.Email;
            medicVM.IdentityCard = medic.IdentityCard;
            medicVM.Phone = medic.Phone;
            medicVM.Photo = medic.Photo;
            medicVM.Created = medic.Created;
            medicVM.CreatedBy = medic.CreatedBy;

            return medicVM;
        }
    }
}
