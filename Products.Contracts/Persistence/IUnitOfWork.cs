﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
