using FirstExam.Models;        
using FirstExam.Models.DTO;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;


namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _svc;
        public AppointmentsController(IAppointmentService svc) { _svc = svc; }

        private static (int page, int limit) NormalizePage(int? page, int? limit)
        {
            var p = page.GetValueOrDefault(1); if (p < 1) p = 1;
            var l = limit.GetValueOrDefault(10); if (l < 1) l = 1; if (l > 100) l = 100;
            return (p, l);
        }

        private static IEnumerable<T> OrderByProp<T>(IEnumerable<T> src, string? sort, string? order)
        {
            if (string.IsNullOrWhiteSpace(sort)) return src;
            var prop = typeof(T).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop is null) return src;
            bool desc = string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase);
            return desc ? src.OrderByDescending(x => prop.GetValue(x)) : src.OrderBy(x => prop.GetValue(x));
        }

        private static object Wrap(object? data, int? page = null, int? limit = null, int? total = null)
            => new { data, meta = new { page, limit, total, count = data is IEnumerable<object> e ? e.Count() : data is null ? 0 : 1 } };

        // GET /api/v1/appointments?Page=1&Limit=10&Sort=ScheduledAt&Order=asc&q=control
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? Page, [FromQuery] int? Limit, [FromQuery] string? Sort, [FromQuery] string? Order, [FromQuery] string? q)
        {
            var all = await _svc.GetAll();

            IEnumerable<Appointment> query = all;

            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(a =>
                    (a.Reason?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (a.Status?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (a.Notes?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            query = OrderByProp(query, Sort, Order);

            var (p, l) = NormalizePage(Page, Limit);
            var total = query.Count();
            var data = query.Skip((p - 1) * l).Take(l).ToList();

            return Ok(Wrap(data, p, l, total));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var found = await _svc.GetById(id);
            if (found is null) return NotFound(Wrap(new { error = "Appointment not found", status = 404 }));
            return Ok(Wrap(found));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await _svc.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = created.Id }, Wrap(created));
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var ok = await _svc.Update(id, dto);
            if (!ok) return NotFound(Wrap(new { error = "Appointment not found", status = 404 }));
            var updated = await _svc.GetById(id);
            return Ok(Wrap(updated));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var ok = await _svc.Delete(id);
            if (!ok) return NotFound(Wrap(new { error = "Appointment not found", status = 404 }));
            return NoContent();
        }
    }
}
