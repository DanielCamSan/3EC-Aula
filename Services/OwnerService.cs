using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Repositories;

namespace FirstExam.Services
{
    public class OwnerService
    {
    }
}


namespace apiwithdb.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repo;
        public AuthorService(IAuthorRepository repo) { _repo = repo; }

        public async Task<IEnumerable<AuthorListDto>> GetAll()
        {
            var authors = await _repo.GetAllWithBooks();
            return authors
                .OrderBy(a => a.Name)
                .Select(a => new AuthorListDto(
                    a.Id,
                    a.Name,
                    a.Books?.Count ?? 0
                ));
        }

        public async Task<AuthorDetailDto?> GetById(Guid id)
        {
            var a = await _repo.GetByIdWithBooks(id);
            if (a is null) return null;

            var books = (a.Books ?? new List<Book>())
                        .OrderBy(b => b.Title)
                        .Select(b => new SimpleBookDto(b.Id, b.Title, b.Year))
                        .ToList();

            return new AuthorDetailDto(a.Id, a.Name, books);
        }

        public async Task<AuthorDetailDto> Create(CreateAuthorDto dto)
        {
            var name = dto.Name.Trim();
            if (await _repo.ExistsByName(name))
                throw new InvalidOperationException("Author name already exists.");

            var author = new Author { Id = Guid.NewGuid(), Name = name };
            await _repo.Add(author);

            return new AuthorDetailDto(author.Id, author.Name, new List<SimpleBookDto>());
        }

        public async Task<AuthorDetailDto?> Update(Guid id, UpdateAuthorDto dto)
        {
            var current = await _repo.GetByIdWithBooks(id);
            if (current is null) return null;

            var name = dto.Name.Trim();
            if (await _repo.ExistsByNameExcludingId(name, id))
                throw new InvalidOperationException("Another author already uses that name.");

            current.Name = name;
            await _repo.Update(current);

            var books = (current.Books ?? new List<Book>())
                        .OrderBy(b => b.Title)
                        .Select(b => new SimpleBookDto(b.Id, b.Title, b.Year))
                        .ToList();

            return new AuthorDetailDto(current.Id, current.Name, books);
        }

        public async Task<bool> Delete(Guid id)
        {
            // Protegemos el borrado si tiene libros
            if (await _repo.HasBooks(id)) return false;
            return await _repo.Delete(id);
        }
    }
}