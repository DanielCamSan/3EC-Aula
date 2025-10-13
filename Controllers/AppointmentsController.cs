using FirstExam.Services;
using FirstExam.Models;
using FirstExam.Models.dtos;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using static Appointment;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;
        public AppointmentsController (IAppointmentService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {           
            IEnumerable<Appointment> items = await _service.GetAll();          
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Appointment>> GetOne(Guid id)
        {
            var appointment = await _service.GetById(id);
            return appointment is null? NotFound(new { error = "Appointment not found", status = 404}): Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var appt = await _service.Create(dto);
            var res = new AppointmentDto
            {
                Id = appt.Id,
                ScheduledAt = appt.ScheduledAt,
                Reason = appt.Reason,
                Status = appt.Status,
                Notes = appt.Notes,
                PetId = appt.PetId,
                OwnerId = appt.OwnerId
            };
            return CreatedAtAction(nameof(GetOne), new { id = res.Id }, res);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Appointment>> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.Update(id,dto);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Appointment>> Delete(Guid id)
        {
            var removed = await _service.Delete(id);
            return !removed ? NotFound(new { error = "Appointment not found", status =404 }): NoContent();
        }
    }  
}
  