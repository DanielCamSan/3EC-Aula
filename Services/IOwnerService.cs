using apiwithdb.Models;
using apiwithdb.Models.dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace apiwithdb.Services
{
    public interface IOwnerService
    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task<Owner> Create(CreateOwnerDto dto);
        Task<bool> Delete(Guid id);
        Task<Owner> Update(Guid id, UpdateOwnerDto dto);
    }
}
