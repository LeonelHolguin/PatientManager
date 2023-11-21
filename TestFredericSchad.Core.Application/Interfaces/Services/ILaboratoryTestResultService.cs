using PatientManager.Core.Application.ViewModels.LaboratoryTestResults;

namespace PatientManager.Core.Application.Interfaces.Services
{
    public interface ILaboratoryTestResultService : IGenericService<SaveLaboratoryTestResultViewModel, LaboratoryTestResultViewModel>
    {
        Task<List<LaboratoryTestResultViewModel>> GetAllByIdentityCardPatient(string identityCardPatient);
        Task BulkDeleteByAppointment(int id);
    }
}
