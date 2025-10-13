using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class OwnersController : Controller
    {
        private readonly IOwnerService _service;
        public OwnersController(IOwnerService service)
        {
            _service = service;
        }
        private static (int page, int limit) NormalizePage(int? page, int? limit)
        {
            var p = page.GetValueOrDefault(1); if (p < 1) p = 1;
            var l = limit.GetValueOrDefault(10); if (l < 1) l = 1; if (l > 100) l = 100;
            return (p, l);

        }
        private static IEnumerable<T> OrderbyProp<T>(IEnumerable<T> src, string? sort, string? order)
        {
            if (string.IsNullOrEmpty(sort)) return src;
            var prop = typeof(T).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) return src;
            return string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase) ? src.OrderByDescending(x => prop.GetValue(x)) : src.OrderBy(x => prop.GetValue(x));

        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            return Ok(await _service.GetAll());
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Owner>> GetOne(Guid id)
        {
            var owner = await _service.GetById(id);
            return owner is null ? NotFound(new { error = "owner not found ", status = 404 }) : Ok(owner);

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var owner = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = owner.Id }, owner);
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
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
                NotFound(new { error = "Owner not found", status = 404 }) :
                NoContent();
        }

    }
}