using System.Threading.Tasks;
using TechFood.Domain.Entities;

namespace TechFood.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByIdAsync(int id);
    }
}
