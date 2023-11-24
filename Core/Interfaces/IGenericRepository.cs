using Core.Entities;
using eCommerce.Core.Spesification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T> GetByIdAsnyc(int id);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetEntityWithSpec(ISpesification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpesification<T> spec);
    }
}
