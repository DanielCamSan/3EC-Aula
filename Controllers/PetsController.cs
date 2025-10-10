using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FirstExam.Services;
using System.Threading.Tasks;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _service;
        public PetsController(IPetService service)
        {
            _service = service;
        }

        public static (int page, int limit) NormalizePage(int? page, int? limit)
        {
            var p = page.GetValueOrDefault(1); if (p < 1) p = 1;
            var l = limit.GetValueOrDefault(10); if (l < 1) l = 1; if (l > 100) l = 100;
            return (p, l);
        }

        public static IEnumerable<T> OrderByProp<T>(IEnumerable<T> src, string? sort, string? order)
        {
            if (string.IsNullOrWhiteSpace(sort)) return src;
            var prop = typeof(T).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop is null) return src;

            return string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase) ? src.OrderByDescending(x => prop.GetValue(x)) : src.OrderBy(x => prop.GetValue(x));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? Page, [FromQuery] int? Limit, [FromQuery] string? Sort, [FromQuery] string? Order, [FromQuery] string? Q)
        {
            var (p, l) = NormalizePage(Page, Limit);
            IEnumerable<Pet> query = await _service.GetAll();
            if (!string.IsNullOrWhiteSpace(Q))
            {
                query = query.Where(a => a.Name.Contains(Q, StringComparison.OrdinalIgnoreCase) ||
                a.Breed.Contains(Q, StringComparison.OrdinalIgnoreCase) ||
                a.Species.Contains(Q, StringComparison.OrdinalIgnoreCase));
            }
            query = OrderByProp(query, Sort, Order);
            var total = query.Count();
            var data = query.Skip((p - 1) * l).Take(l).ToArray();
            return Ok(new { data, meta = new { page = p, limit = l, total } });

        }

        [HttpGet("{id:guid}")]
        public async  Task<ActionResult<Pet>> GetOne(Guid id)
        {
            var pet = await _service.GetById(id);
            return pet is null ? NotFound(new { error = "Pet not found", status = 404 }) : Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var pet = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = pet.Id },pet);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Pet>> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.Update(id, dto);
            return Ok(updated);

        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _service.Delete(id);
            return !removed ?
                NotFound(new { error = "Pet not found", status = 404 }) :
            NoContent();
        }
    }
}
