using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;
        public AppointmentsController(IAppointmentService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAll();
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var appointment = await _service.GetById(id);
            return appointment is null
                ? NotFound(new { error = "Appointment not found", status = 404 })
                : Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var appointment = await _service.Create(dto);
                return CreatedAtAction(nameof(GetOne), new { id = ((dynamic)appointment).Id }, appointment);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message, status = 409 });
            }
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var updated = await _service.Update(id, dto);
                return updated is null
                    ? NotFound(new { error = "Appointment not found", status = 404 })
                    : Ok(updated);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { error = ex.Message, status = 409 });
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.Delete(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Appointment not found or already completed", status = 404 });
        }

        // CAMBIO: Endpoint renombrado de pet a owner
        [HttpGet("owner/{ownerId:guid}")]
        public async Task<IActionResult> GetByOwnerId(Guid ownerId)
        {
            var appointments = await _service.GetByOwnerId(ownerId);
            return Ok(appointments);
        }

        [HttpPatch("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
        {
            try
            {
                var success = await _service.UpdateStatus(id, status);
                return success
                    ? NoContent()
                    : NotFound(new { error = "Appointment not found", status = 404 });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message, status = 400 });
            }
        }
    }
}