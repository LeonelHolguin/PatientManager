using PatientManager.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Core.Application.Interfaces.Repositories
{
    public interface ILaboratoryTestRepository : IGenericRepository<LaboratoryTest>
    {
        Task<List<LaboratoryTest>> GetAllAsync();
        Task<LaboratoryTest> GetByIdAsync(int id);
    }
}
