using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Domain.Entities;
using TechFood.Domain.Repositories;
using TechFood.Infra.Data.Contexts;

namespace TechFood.Infra.Data.Repositories
{
    public class PaymentTypeRepository : IPaymentTypeRepository
    {
        private readonly DbSet<PaymentType> _paymentTypes;

        public PaymentTypeRepository(TechFoodContext dbContext)
        {
            _paymentTypes = dbContext.PaymentTypes;
        }

        public async Task<IEnumerable<PaymentType>> GetAllAsync()
        {
            return await _paymentTypes.ToListAsync();
        }
    }
}
