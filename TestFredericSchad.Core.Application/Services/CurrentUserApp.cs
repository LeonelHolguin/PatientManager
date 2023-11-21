using Microsoft.AspNetCore.Http;
using PatientManager.Core.Application.Helpers;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.ViewModels.Users;


namespace PatientManager.Core.Application.Services
{
    public class CurrentUserApp : ICurrentUserApp
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public CurrentUserApp(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }

        public string GetCurrentUserName()
        {
            UserViewModel userViewModel = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            return userViewModel.Name;
        }
    }
}
