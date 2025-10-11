using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
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

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? Page, [FromQuery] int? Limit, [FromQuery] string? Sort, [FromQuery] string? Order, [FromQuery] string? Q)
        {
            var (p, l) = NormalizePage(Page, Limit);
            var query = await _service.GetAllAsync(Q, Sort, Order);

            var total = query.Count();
            var data = query.Skip((p - 1) * l).Take(l).ToList();

            return Ok(new { data, meta = new { Page = p, Limit = l, total } });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Owner>> GetOne(Guid id)
        {
            var owner = await _service.GetByIdAsync(id);
            return owner is null ? NotFound(new { error = "Owner not found", status = 404 }) : Ok(owner);
        }

        [HttpPost]
        public async Task<ActionResult<Owner>> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var owner = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOne), new { id = owner.Id }, owner);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Owner>> Update(Guid id, [FromBody] UpdateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.UpdateAsync(id, dto);
            return updated is null ? NotFound(new { error = "Owner not found", status = 404 }) : Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound(new { error = "Owner not found", status = 404 });
        }
    }
}