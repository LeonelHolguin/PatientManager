
namespace PatientManager.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<Entity> where Entity : class
    {
        Task<Entity> AddAsync(Entity entity);

        Task UpdateAsync(Entity entity);

        Task DeleteAsync(Entity entity);

    }
}
