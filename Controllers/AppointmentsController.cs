using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;
using FirstExam.Models.Dtos;
using FirstExam.Service;
using static Appointment;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;
        public AppointmentController (IAppointmentService service)
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
            var book = await _service.GetById(id);
            return book == null
                ? NotFound(new { error = "Book not found", status = 404 })
                : Ok(book);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var book = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = book.Id }, book);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.Delete(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Book not found", status = 404 });
        }
    }   
}
  