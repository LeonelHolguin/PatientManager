using Microsoft.EntityFrameworkCore;
using PatientManager.Core.Application.Helpers;
using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Application.ViewModels.Users;
using PatientManager.Core.Domain.Entities;
using PatientManager.Infrastructure.Persistence.Contexts;


namespace PatientManager.Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _dbContext;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<User> AddAsync(User userToSave)
        {
            userToSave.Password = PasswordEncryption.ComputeShad256Hash(userToSave.Password);
            return await base.AddAsync(userToSave);
        }

        public override async Task UpdateAsync(User userToSave)
        {
            userToSave.Password = PasswordEncryption.ComputeShad256Hash(userToSave.Password);
            await base.UpdateAsync(userToSave);
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _dbContext.Set<User>()
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _dbContext.Set<User>()
                .AsNoTracking()
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> LoginAsync(LoginViewModel userLogin)
        {
            string passwordEncrypt = PasswordEncryption.ComputeShad256Hash(userLogin.Password);

            User user = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(user => user.UserName == userLogin.UserName && user.Password == passwordEncrypt);

            return user;
        }

        public async Task<bool> UserNameExistsAsync(string userName)
        {
            return await _dbContext.Set<User>().AnyAsync(u => u.UserName == userName);
        }
    }
}
