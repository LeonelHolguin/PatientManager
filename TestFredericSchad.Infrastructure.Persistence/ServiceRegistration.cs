using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PatientManager.Core.Application.Interfaces.Repositories;
using PatientManager.Infrastructure.Persistence.Contexts;
using PatientManager.Infrastructure.Persistence.Repositories;

namespace PatientManager.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            #region Contexts
            var connectionString = config.GetConnectionString("DefaultConnection");
            var InMemoryDatabase = config.GetValue<bool>("InMemoryDatabase ");

            if (InMemoryDatabase)
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("MemoryDatabase"));
            }
            else
            {

                services.AddDbContext<ApplicationContext>(options =>
                                     options.UseSqlite(connectionString,
                                         m => {
                                             m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName);
                                         }));
            }
            #endregion

            #region Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMedicRepository, MedicRepository>();
            services.AddTransient<ILaboratoryTestRepository, LaboratoryTestRepository>();
            services.AddTransient<ILaboratoryTestResultRepository, LaboratoryTestResultRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<IAppointmentRepository, AppointmentRepository>();
            #endregion
        }
    }
}
