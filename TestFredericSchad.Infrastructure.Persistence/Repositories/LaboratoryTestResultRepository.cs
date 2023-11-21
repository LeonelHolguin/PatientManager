

using Microsoft.EntityFrameworkCore;
using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Domain.Entities;
using PatientManager.Infrastructure.Persistence.Contexts;
using System.Reflection.Emit;

namespace PatientManager.Infrastructure.Persistence.Repositories
{
    public class LaboratoryTestResultRepository : GenericRepository<LaboratoryTestResult>, ILaboratoryTestResultRepository
    {
        private readonly ApplicationContext _dbContext;

        public LaboratoryTestResultRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LaboratoryTestResult>> GetAllAsync()
        {
            return await _dbContext.Set<LaboratoryTestResult>()
                .Include(laboratoryTestResult => laboratoryTestResult.Patient)
                .Include(laboratoryTestResult => laboratoryTestResult.LaboratoryTest)
                .Include(laboratoryTestResult => laboratoryTestResult.Appointment)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<LaboratoryTestResult> GetByIdAsync(int id)
        {
            return await _dbContext.Set<LaboratoryTestResult>()
                .Include(laboratoryTestResult => laboratoryTestResult.Patient)
                .Include(laboratoryTestResult => laboratoryTestResult.LaboratoryTest)
                .Include(laboratoryTestResult => laboratoryTestResult.Appointment)
                .FirstOrDefaultAsync(LT => LT.Id == id);
        }

        public async Task BulkDeleteByAppointmentAsync(int id)
        {
            var entities =  await _dbContext.Set<LaboratoryTestResult>()
                .Where(LT => LT.AppointmentId == id)
                .AsNoTracking()
                .ToListAsync();

            _dbContext.Set<LaboratoryTestResult>().RemoveRange(entities);
            await _dbContext.SaveChangesAsync();
        }


    }
}
