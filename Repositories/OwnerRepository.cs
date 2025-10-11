using FirstExam.Models;
using FirstExam.Data;
using Microsoft.EntityFrameworkCore;
namespace FirstExam.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly AppDbContext _context;
        public OwnerRepository(AppDbContext context)
        {
            _context = context;
        }
        public Task<List<Owner>> GetAllWithAppointments()
        {
            return _context.Owners.AsNoTracking().Include(o => o.Appointments).ToListAsync();
        }
        public Task<Owner?> GetByIdWithOwner(Guid id)
        {
            return _context.Owners.AsNoTracking().Include(o => o.Appointments).FirstOrDefaultAsync(o => o.Id == id);
        }
        public Task<bool> GetByName(string name)
        {
            return _context.Owners.AnyAsync(o => o.FullName.ToLower() == name.ToLower());
        }
        public Task<bool> ExistsByNameExcludingId(string name, Guid excludeId)
        {
            return _context.Owners.AnyAsync(o => o.Id != excludeId && o.FullName.ToLower() == name.ToLower());
        }
        public Task<bool> HasAppointments(Guid ownerId)
        {
            return _context.Owners.AnyAsync(o => o.Id == ownerId);
        }
    public async Task<IEnumerable<Owner>> GetAll()
        {
            return await _context.Owners.ToListAsync();
        }
        public async Task<Owner?> GetById(Guid id)
        {
            return await _context.Owners.FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task Add(Owner owner)
        {
            await _context.Owners.AddAsync(owner);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Owner owner)
        {
            _context.Owners.Update(owner);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> Delete(Guid id)
        {
            var entity = await _context.Owners.FirstOrDefaultAsync(o => o.Id == id);
            if (entity is null) return false;
            _context.Owners.Remove(entity);
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateException)
            {
                return false;
            }
        }
    }
}