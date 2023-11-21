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
    public class LaboratoryTestRepository : GenericRepository<LaboratoryTest>, ILaboratoryTestRepository
    {
        private readonly ApplicationContext _dbContext;

        public LaboratoryTestRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<LaboratoryTest>> GetAllAsync()
        {
            return await _dbContext.Set<LaboratoryTest>()
                .ToListAsync();
        }

        public async Task<LaboratoryTest> GetByIdAsync(int id)
        {
            return await _dbContext.Set<LaboratoryTest>()
                .FirstOrDefaultAsync(LT => LT.Id == id);
        }
    }
}
