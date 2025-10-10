using System;
using FirstExam.Data;
using FirstExam.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _db;
        public OwnerRepository(AppDbContext db) { _db = db; }


        public async Task Add(Owner owner)
        {
            await _db.Owners.AddAsync(owner);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Owner owner)
        {
            _db.Owners.Update(owner);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _db.Owners.FirstOrDefaultAsync(a => a.Id == id);
            if (entity is null) return false;

            _db.Owners.Remove(entity);
            try
            {
                await _db.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                // Probable violación por FK si tiene libros (DeleteBehavior.Restrict)
                return false;
            }
        }

        public Task<bool> ExistsById(Guid id)
           => _db.Owners.AnyAsync(a => a.Id == id);
    }
}


