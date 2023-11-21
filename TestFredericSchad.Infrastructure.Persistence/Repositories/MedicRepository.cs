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
    public class MedicRepository : GenericRepository<Medic>, IMedicRepository
    {
        private readonly ApplicationContext _dbContext;

        public MedicRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Medic>> GetAllAsync()
        {
            return await _dbContext.Set<Medic>()
                .ToListAsync();
        }

        public async Task<Medic> GetByIdAsync(int id)
        {
            return await _dbContext.Set<Medic>()
                .FirstOrDefaultAsync(medic => medic.Id == id);
        }
    }
}
