using PatientManager.Core.Application.ViewModels.Users;
using PatientManager.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientManager.Core.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {

        Task<List<User>> GetAllAsync();

        Task<bool> UserNameExistsAsync(string userName);

        Task<User> GetByIdAsync(int id);

        Task<User> LoginAsync(LoginViewModel userLogin);

    }
}
