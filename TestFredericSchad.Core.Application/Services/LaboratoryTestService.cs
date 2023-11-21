using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.LaboratoryTest;
using PatientManager.Core.Domain.Entities;


namespace PatientManager.Core.Application.Services
{
    public class LaboratoryTestService : ILaboratoryTestService
    {
        private readonly ILaboratoryTestRepository _laboratoryTestRepository;
        public LaboratoryTestService(ILaboratoryTestRepository laboratoryTestRepository)
        {
            _laboratoryTestRepository = laboratoryTestRepository;
        }

        public async Task<SaveLaboratoryTestViewModel> Add(SaveLaboratoryTestViewModel laboratoryTestToSave)
        {
            LaboratoryTest laboratoryTest = new();

            laboratoryTest.Id = laboratoryTestToSave.Id;
            laboratoryTest.Name = laboratoryTestToSave.Name;

            laboratoryTest = await _laboratoryTestRepository.AddAsync(laboratoryTest);

            SaveLaboratoryTestViewModel viewModel = new();

            viewModel.Id = laboratoryTest.Id;
            viewModel.Name = laboratoryTest.Name;

            return viewModel;
        }

        public async Task Update(SaveLaboratoryTestViewModel laboratoryTestToSave)
        {
            LaboratoryTest laboratoryTest = new();

            laboratoryTest.Id = laboratoryTestToSave.Id;
            laboratoryTest.Name = laboratoryTestToSave.Name;
            laboratoryTest.Created = (DateTime)laboratoryTestToSave.Created;
            laboratoryTest.CreatedBy = laboratoryTestToSave.CreatedBy;

            await _laboratoryTestRepository.UpdateAsync(laboratoryTest);
        }

        public async Task Delete(int id)
        {
            var laboratoryTest = await _laboratoryTestRepository.GetByIdAsync(id);
            await _laboratoryTestRepository.DeleteAsync(laboratoryTest);
        }

        public async Task<List<LaboratoryTestViewModel>> GetAllViewModel()
        {
            var laboratoryTestList = await _laboratoryTestRepository.GetAllAsync();

            return laboratoryTestList.Select(laboratoryTest => new LaboratoryTestViewModel
            {
                Id = laboratoryTest.Id,
                Name = laboratoryTest.Name,
            }).ToList();
        }

        public async Task<LaboratoryTestViewModel> GetById(int id)
        {
            var laboratoryTest = await _laboratoryTestRepository.GetByIdAsync(id);

            LaboratoryTestViewModel laboratoryTestVM = new();
            laboratoryTestVM.Id = laboratoryTest.Id;
            laboratoryTestVM.Name = laboratoryTest.Name;

            return laboratoryTestVM;
        }

        public async Task<SaveLaboratoryTestViewModel> GetByIdSaveViewModel(int id)
        {
            var laboratoryTest = await _laboratoryTestRepository.GetByIdAsync(id);

            SaveLaboratoryTestViewModel laboratoryTestVM = new();
            laboratoryTestVM.Id = laboratoryTest.Id;
            laboratoryTestVM.Name = laboratoryTest.Name;
            laboratoryTestVM.Created = laboratoryTest.Created;
            laboratoryTestVM.CreatedBy = laboratoryTest.CreatedBy;

            return laboratoryTestVM;
        }
    }
}
