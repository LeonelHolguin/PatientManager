using Microsoft.Extensions.DependencyInjection;
using PatientManager.Core.Application.Interfaces.Services;
using PatientManager.Core.Application.Services;

namespace PatientManager.Core.Application
{
    //Decorator with Extension Methods
   public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ICurrentUserApp, CurrentUserApp>();
            services.AddTransient<IMedicService, MedicService>();
            services.AddTransient<ILaboratoryTestService, LaboratoryTestService>();
            services.AddTransient<IPatientService, PatientService>();
            services.AddTransient<ILaboratoryTestResultService, LaboratoryTestResultService>();
            services.AddTransient<IAppointmentService, AppointmentService>();
            #endregion
        }
    }
}
