using PatientManager.Core.Application.ViewModels.Users;

namespace PatientManager.Core.Application.Interfaces.Services
{
    public interface IUserService : IGenericService<SaveUserViewModel, UserViewModel>
    {
        Task<UserViewModel> Login(LoginViewModel userLogin);
        Task<bool> UserNameExists(string userName);  
    }
}
