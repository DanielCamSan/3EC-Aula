using FirstExam.Services;
using FirstExam.Models;
using FirstExam.Models.dtos;
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
        private static (int page, int limit) NormalizePage(int? page, int? limit)
        {
            var p = page.GetValueOrDefault(1); if (p < 1) p = 1;
            var l = limit.GetValueOrDefault(10); if (l < 1) l = 1; if (l > 100) l = 100;
            return (p, l);
        }
        private static IEnumerable<T> OrderByProp<T>(IEnumerable<T> src, string? sort, string? order)
        {
            if (string.IsNullOrEmpty(sort)) return src;
            var prop = typeof(T).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) return src;

            return string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase) ? src.OrderByDescending(x => prop.GetValue(x)) : src.OrderBy(x => prop.GetValue(x));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? Page, [FromQuery] int? Limit, [FromQuery] string? Sort, [FromQuery] string? Order, [FromQuery] string? q)
        {
            var (p, l) = NormalizePage(Page, Limit);
            IEnumerable<Appointment> query = await _service.GetAll();
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(a => a.Reason.Contains(q, StringComparison.OrdinalIgnoreCase));
            }
            query = OrderByProp(query, Sort, Order);
            var total = query.Count();
            var data = query.Skip((p - 1) * l).Take(l).ToList();
            return Ok(new { data, meta = new { Page = p, Limit = l, total } });
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Appointment>> GetOne(Guid id)
        {
            var appointment = await _service.GetById(id);
            return appointment is null ? NotFound(new { error = "Appointment not found", status = 404 }) : Ok(appointment);
        }

        [HttpPost]
        public async Task<ActionResult<Appointment>> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var appointment = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = appointment.Id }, appointment);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Appointment>> Update(Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.Update(id, dto);
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Appointment>> Delete(Guid id)
        {
            var removed = await _service.Delete(id);
            return !removed ? NotFound(new { error = "Appointment not found", status = 404 }) : NoContent();
        }
    }
}
