using Microsoft.EntityFrameworkCore;
using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Domain.Entities;
using PatientManager.Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Infrastructure.Persistence.Repositories
{
    public class PatientRepository : GenericRepository<Patient>, IPatientRepository
    {
        private readonly ApplicationContext _dbContext;

        public PatientRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }


        public async Task<List<Patient>> GetAllAsync()
        {
            return await _dbContext.Set<Patient>()
                .ToListAsync();
        }

        public async Task<Patient> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Patient>()
                .FirstOrDefaultAsync(patient => patient.Id == id);
        }
    }
}
