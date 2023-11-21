using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Users;
using PatientManager.Core.Domain.Entities;

namespace PatientManager.Core.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        { 
            _userRepository = userRepository;
        }

        public async Task<UserViewModel> Login(LoginViewModel userLogin)
        {
            User user = await _userRepository.LoginAsync(userLogin);

            if(user == null)
            {
                return null;
            }

            UserViewModel userVM = new();

            userVM.Id = user.Id;
            userVM.Name = user.Name;
            userVM.LastName = user.LastName;
            userVM.Email = user.Email;
            userVM.Password = user.Password;
            userVM.Role = user.Role;

            return userVM;
        }

        public async Task<SaveUserViewModel> Add(SaveUserViewModel userToSave)
        {
            User user = new();

            user.Id = userToSave.Id;
            user.Name = userToSave.Name;
            user.LastName = userToSave.LastName;
            user.Email = userToSave.Email;
            user.UserName = userToSave.UserName;
            user.Password = userToSave.Password;
            user.Role = userToSave.Role;

            user = await _userRepository.AddAsync(user);

            SaveUserViewModel viewModel = new();

            viewModel.Id = user.Id;
            viewModel.Name = user.Name;
            viewModel.LastName = user.LastName;
            viewModel.Email = user.Email;
            viewModel.UserName = user.UserName;
            viewModel.Password = user.Password;
            viewModel.Role = user.Role;

            return viewModel;
        }

        public async Task Update(SaveUserViewModel userToSave)
        {
            User user = new();

            user.Id = userToSave.Id;
            user.Name = userToSave.Name;
            user.LastName = userToSave.LastName;
            user.Email = userToSave.Email;
            user.UserName = userToSave.UserName;
            user.Password = userToSave.Password;
            user.Role = userToSave.Role;
            user.Created = (DateTime)userToSave.Created;
            user.CreatedBy = userToSave.CreatedBy;

            await _userRepository.UpdateAsync(user);
        }

        public async Task Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<List<UserViewModel>> GetAllViewModel()
        {
            var userList = await _userRepository.GetAllAsync();

            return userList.Select(user => new UserViewModel 
            { 
                Id = user.Id,
                Name = user.Name,
                LastName = user.LastName,
                Email = user.Email,
                UserName = user.UserName,
                Password = user.Password,
                Role = user.Role

            }).ToList();
        }

        public async Task<UserViewModel> GetById(int id)
        {
            var user = await  _userRepository.GetByIdAsync(id);

            UserViewModel userVM = new();
            userVM.Id = user.Id;
            userVM.Name = user.Name;
            userVM.LastName = user.LastName;
            userVM.Email = user.Email;
            userVM.UserName = user.UserName;
            userVM.Password = user.Password;
            userVM.Role = user.Role;

            return userVM;
        }

        public async Task<SaveUserViewModel> GetByIdSaveViewModel(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            SaveUserViewModel userVM = new();
            userVM.Id = user.Id;
            userVM.Name = user.Name;
            userVM.LastName = user.LastName;
            userVM.Email = user.Email;
            userVM.UserName = user.UserName;
            userVM.Role = user.Role;
            userVM.Created = user.Created;
            userVM.CreatedBy = user.CreatedBy;

            return userVM;
        }

        public async Task<bool> UserNameExists(string userName)
        {
            return await _userRepository.UserNameExistsAsync(userName);
        }
    }
}
