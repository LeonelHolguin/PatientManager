using PatientManager.Core.Application.ViewModels.Users;
using PatientManager.Core.Application.Helpers;
using WebApp.PatientManager.Constants;


namespace WebApp.PatientManager.Middlewares
{
    public class ValidateUserSession
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public ValidateUserSession(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }


        public bool HasUser()
        {
            UserViewModel userVM = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            if (userVM == null)
                return false;
            else
            {

                return true;    
            }

        }


        public bool IsAdministrator()
        {
            UserViewModel userVM = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            return userVM.Role == Roles.Administrator;
        }


        public bool IsAssistant()
        {
            UserViewModel userVM = _contextAccessor.HttpContext.Session.Get<UserViewModel>("user");

            return userVM.Role == Roles.Assistant;
        }
    }
}
