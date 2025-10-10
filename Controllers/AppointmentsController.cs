using FirstExam.Models;
using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        // Helper para normalizar los parámetros de paginación
        private static (int page, int limit) NormalizePage(int? page, int? limit)
        {
            var p = page.GetValueOrDefault(1); if (p < 1) p = 1;
            var l = limit.GetValueOrDefault(10); if (l < 1) l = 1; if (l > 100) l = 100;
            return (p, l);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? Page, [FromQuery] int? Limit, [FromQuery] string? Sort, [FromQuery] string? Order, [FromQuery] string? q)
        {
            var (p, l) = NormalizePage(Page, Limit);
            var query = await _service.GetAllAsync(q, Sort, Order);

            var total = query.Count();
            var data = query.Skip((p - 1) * l).Take(l).ToList();

            return Ok(new { data, meta = new { Page = p, Limit = l, total } });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Appointment>> GetOne(Guid id)
        {
            var appointment = await _service.GetByIdAsync(id);
            return appointment is null ? NotFound(new { error = "Appointment not found", status = 404 }) : Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var appointment = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOne), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Appointment>> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.UpdateAsync(id, dto);

            return updated is null ? NotFound(new { error = "Appointment not found", status = 404 }) : Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.DeleteAsync(id);
            return success ? NoContent() : NotFound(new { error = "Appointment not found", status = 404 });
        }
    }
}