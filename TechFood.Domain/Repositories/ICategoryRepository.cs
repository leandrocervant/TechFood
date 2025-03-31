using TechFood.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechFood.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllAsync();
    }
}
