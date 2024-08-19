﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NTierManagement.DAL.Abstract
{
    public interface IBaseRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task<T> GetByIdAsync(int id);

        Task SaveChangesAsync();
    }
}
