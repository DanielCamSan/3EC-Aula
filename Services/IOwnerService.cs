using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FirstExam.Models;
using FirstExam.Models.dtos;
namespace FirstExam.Services
{
    public interface IOwnerService

    {
        Task<IEnumerable<Owner>> GetAll();
        Task<Owner?> GetById(Guid id);
        Task<Owner> Create(Models.CreateOwnerDto dto);
        Task<Owner> Update(Guid id, Models.UpdateOwnerDto dto);
        Task<bool> Delete(Guid id);
        }
    }