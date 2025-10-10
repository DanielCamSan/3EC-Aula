
using FirstExam.Data;

namespace FirstExam.Repositories
{
    public class PetRepository : IPetRepository
    {
        private readonly AppDbContext _context;
        public PetRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task Add(Pet pet)
        {
            await _context.Pets.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public Task Delete(Guid id)
        {
            var book = await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
            if (book != null)
            {
                _context.Pets.Remove(book);
                await _context.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Pet>> GetAll()
        {
            return await _context.Pets.AsNoTracking()
        .Include(b => b.Author).ToListAsync();
        }

        public Task<Pet?> GetById(Guid id)
        {
            return await _context.Pets.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
