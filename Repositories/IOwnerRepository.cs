using apiwithdb.Models;
using apiwithdb.Models.dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiwithdb.Repositories
{
    public interface IOwnerRepository
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task Add(Owner owner);
        Task Delete(Guid id);
        Task Update(Owner owner);
    }
}
