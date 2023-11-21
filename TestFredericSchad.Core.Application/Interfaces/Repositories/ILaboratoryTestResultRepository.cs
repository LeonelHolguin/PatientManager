using PatientManager.Core.Domain.Entities;

namespace PatientManager.Core.Application.Interfaces.Repositories
{
    public interface ILaboratoryTestResultRepository : IGenericRepository<LaboratoryTestResult>
    {
        Task<List<LaboratoryTestResult>> GetAllAsync();
        Task<LaboratoryTestResult> GetByIdAsync(int id);
        Task BulkDeleteByAppointmentAsync(int id);
    }
}
