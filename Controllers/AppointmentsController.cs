using FirstExam.Models.dtos;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;
using static Appointment;

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
            return appointment == null
                ? NotFound(new { error = "Appointment not found", status = 404 })
                : Ok(appointment);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var appointment = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = appointment.Id }, appointment);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.Delete(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Appointment not found", status = 404 });
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var success = await _service.Update(id, dto);
            return success
                ? NoContent()
                : NotFound(new { error = "Appointment not found", status = 404 });
        } 
    }   
}
  