﻿using System.Threading.Tasks;

namespace TechFood.Domain.UoW
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();

        Task RollbackAsync();
    }
}
