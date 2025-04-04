using TechFood.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TechFood.Domain.Repositories
{
    public interface IPaymentTypeRepository
    {
        Task<IEnumerable<PaymentType>> GetAllAsync();
    }
}
