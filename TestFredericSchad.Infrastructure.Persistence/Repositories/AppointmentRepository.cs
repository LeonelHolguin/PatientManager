using Microsoft.EntityFrameworkCore;
using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Domain.Entities;
using PatientManager.Infrastructure.Persistence.Contexts;


namespace PatientManager.Infrastructure.Persistence.Repositories
{
    public class AppointmentRepository : GenericRepository<Appointment>, IAppointmentRepository
    {
        private readonly ApplicationContext _dbContext;

        public AppointmentRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Appointment>> GetAllAsync()
        {
            return await _dbContext.Set<Appointment>()
                .Include(appointment => appointment.Patient)
                .Include(appointment => appointment.Medic)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Appointment> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Appointment>()
                .Include(appointment => appointment.Patient)
                .Include(appointment => appointment.Medic)
                .AsNoTracking()
                .FirstOrDefaultAsync(appointment => appointment.Id == id);
        }
    }
}
