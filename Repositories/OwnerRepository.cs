using System;
//using FirstExam.Data;
using FirstExam.Models;
//using Microsoft.EntityFrameworkCore;

namespace FirstExam.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _db;
        public AuthorRepository(AppDbContext db) { _db = db; }

        public Task<List<Author>> GetAllWithBooks() =>
            _db.Authors.AsNoTracking().Include(a => a.Books).ToListAsync();

        public Task<Author?> GetByIdWithBooks(Guid id) =>
            _db.Authors.AsNoTracking().Include(a => a.Books)
               .FirstOrDefaultAsync(a => a.Id == id);

        public Task<bool> ExistsByName(string name) =>
            _db.Authors.AnyAsync(a => a.Name.ToLower() == name.ToLower());

        public Task<bool> ExistsByNameExcludingId(string name, Guid excludeId) =>
            _db.Authors.AnyAsync(a => a.Id != excludeId && a.Name.ToLower() == name.ToLower());

        public async Task Add(Author author)
        {
            await _db.Authors.AddAsync(author);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Author author)
        {
            _db.Authors.Update(author);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> Delete(Guid id)
        {
            var entity = await _db.Authors.FirstOrDefaultAsync(a => a.Id == id);
            if (entity is null) return false;

            _db.Authors.Remove(entity);
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

        public Task<bool> HasBooks(Guid id) =>
            _db.Books.AnyAsync(b => b.AuthorId == id);
        public Task<bool> ExistsById(Guid id)
           => _db.Authors.AnyAsync(a => a.Id == id);
    }
}


