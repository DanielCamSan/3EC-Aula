using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

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

        private static (int page, int limit) NormalizePage(int? page, int? limit)
        {
            var p = page.GetValueOrDefault(1); if (p < 1) p = 1;
            var l = limit.GetValueOrDefault(10); if (l < 1) l = 1; if (l > 100) l = 100;
            return (p, l);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? Page, [FromQuery] int? Limit, [FromQuery] string? Sort, [FromQuery] string? Order, [FromQuery] string? Q)
        {
            var (p, l) = NormalizePage(Page, Limit);
            var query = await _service.GetAllAsync(Q, Sort, Order);

            var total = query.Count();
            var data = query.Skip((p - 1) * l).Take(l).ToList();

            return Ok(new { data, meta = new { page = p, limit = l, total } });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Pet>> GetOne(Guid id)
        {
            var pet = await _service.GetByIdAsync(id);
            return pet is null ? NotFound(new { error = "Pet not found", status = 404 }) : Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var pet = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOne), new { id = pet.Id }, pet);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Pet>> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.UpdateAsync(id, dto);
            return updated is null ? NotFound(new { error = "Pet not found", status = 404 }) : Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound(new { error = "Pet not found", status = 404 });
        }
    }
}