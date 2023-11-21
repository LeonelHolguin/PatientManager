using PatientManager.Core.Domain.Entities;

namespace PatientManager.Core.Application.Interfaces.Repositories
{
    public interface IAppointmentRepository : IGenericRepository<Appointment>
    {
        Task<List<Appointment>> GetAllAsync();
        Task<Appointment> GetByIdAsync(int id);
    }
}
