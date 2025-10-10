
using FirstExam.Models;
using FirstExam.Data;
using Microsoft.EntityFrameworkCore;
namespace FirstExam.Repositories
{
    public class PetRepository: IPetRepository
    {
            private readonly AppDbContext _context;

            public PetRepository(AppDbContext context)
            {
                _context = context;
            }
            public async Task Add(Pet pet)
            {
                await _context.Books.AddAsync(pet);
            }

            public async Task Delete(Guid id)
            {
                var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    _context.Books.Remove(book);
                }
            }

            public async Task<IEnumerable<Pet>> GetAll()
            {
                return await _context.Books.ToListAsync();
            }

            public async Task<Pet?> GetById(Guid id)
            {
                return await _context.Books.FirstOrDefaultAsync(x => x.Id == id);
            }
        }
}
