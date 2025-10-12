using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using FirstExam.Services;
using FirstExam.Models.DTO; 

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] string? sort = "scheduledAt",
            [FromQuery] string? order = "asc",
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10)
        {
            var items = _service.List(sort, order, page, limit, out var total).ToList();
            return Ok(new
            {
                data = items,
                meta = new { page, limit, total, count = items.Count }
            });
        }

        // GET /api/appointments/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetOne(Guid id)
        {
            var appt = _service.Get(id);
            return appt is null
                ? NotFound(new { error = "Appointment not found", status = 404 })
                : Ok(appt);
        }

        // POST /api/appointments
        [HttpPost]
        public IActionResult Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var created = _service.Create(new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason?.Trim() ?? string.Empty,
                Status = dto.Status?.Trim() ?? "scheduled",
                Notes = dto.Notes?.Trim()
            });

            return CreatedAtAction(nameof(GetOne), new { id = created.Id }, created);
        }

        // PUT /api/appointments/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var ok = _service.Update(id, new Appointment
            {
                Id = id,
                PetId = dto.PetId,
                ScheduledAt = dto.ScheduledAt,
                Reason = dto.Reason?.Trim() ?? string.Empty,
                Status = dto.Status?.Trim() ?? "scheduled",
                Notes = dto.Notes?.Trim()
            });

            if (!ok) return NotFound(new { error = "Appointment not found", status = 404 });

            var updated = _service.Get(id)!;
            return Ok(updated);
        }

        // DELETE /api/appointments/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var ok = _service.Delete(id);
            return ok
                ? NoContent()
                : NotFound(new { error = "Appointment not found", status = 404 });
        }
    }
}
