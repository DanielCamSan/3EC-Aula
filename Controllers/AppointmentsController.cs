using FirstExam.Models.DTO;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static Appointment;
namespace FirstExam.Controllers

{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        public static readonly List<Appointment> appointments = new()
        {
            new Appointment { Id = Guid.NewGuid(), PetId = Guid.NewGuid(), ScheduledAt = DateTime.Now.AddHours(4), Reason = "Vacunación", Status="scheduled", Notes = "Primera dosis" },
            new Appointment { Id = Guid.NewGuid(), PetId = Guid.NewGuid(), ScheduledAt = DateTime.Now.AddDays(1), Reason = "Control",     Status="scheduled", Notes = "Traer carnet" }
        };

        // --- Helpers: paginación/orden/meta/wrap ---
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
            => new
            {
                data,
                meta = new
                {
                    page,
                    limit,
                    total,
                    count = data switch
                    {
                        IEnumerable<object> e => e.Count(),
                        System.Collections.IEnumerable ie => ie.Cast<object>().Count(),
                        null => 0,
                        _ => 1
                    }
                }
            };

        // --- GET /api/v1/appointments?Page=1&Limit=10&Sort=ScheduledAt&Order=asc&q=vacuna ---
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] int? Page,
            [FromQuery] int? Limit,
            [FromQuery] string? Sort,
            [FromQuery] string? Order,
            [FromQuery] string? q)
        {
            var (p, l) = NormalizePage(Page, Limit);

            IEnumerable<Appointment> query = appointments;

            // Filtro “q” por Reason, Status y Notes
            if (!string.IsNullOrWhiteSpace(q))
            {
                query = query.Where(a =>
                    (a.Reason?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (a.Status?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (a.Notes?.Contains(q, StringComparison.OrdinalIgnoreCase) ?? false));
            }

            // Orden dinámico por cualquier propiedad pública (Id, PetId, ScheduledAt, Reason, Status, Notes)
            query = OrderByProp(query, Sort, Order);

            var total = query.Count();
            var data = query.Skip((p - 1) * l).Take(l).ToList();

            return Ok(Wrap(data, p, l, total));
        }

        // --- GET /api/v1/appointments/{id} ---
        [HttpGet("{id:guid}")]
        public IActionResult GetOne([FromRoute] Guid id)
        {
            var appointment = appointments.FirstOrDefault(a => a.Id == id);
            if (appointment is null)
                return NotFound(Wrap(new { error = "Appointment not found", status = 404 }));

            return Ok(Wrap(appointment));
        }

        // --- POST /api/v1/appointments ---
        [HttpPost]
        public IActionResult Create([FromBody] CreateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var appointment = new Appointment
            {
                Id = Guid.NewGuid(),
                PetId = dto.PetId,                                            // usar el enviado
                ScheduledAt = dto.ScheduledAt,                               // usar el enviado
                Reason = dto.Reason?.Trim() ?? string.Empty,
                Status = dto.Status?.Trim() ?? "scheduled",
                Notes = dto.Notes?.Trim()
            };

            appointments.Add(appointment);

            return CreatedAtAction(nameof(GetOne), new { id = appointment.Id }, Wrap(appointment));
        }

        // --- PUT /api/v1/appointments/{id} ---
        [HttpPut("{id:guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateAppointmentDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var idx = appointments.FindIndex(a => a.Id == id);
            if (idx == -1)
                return NotFound(Wrap(new { error = "Appointment not found", status = 404 }));

            // Actualizamos respetando lo enviado
            var current = appointments[idx];
            current.PetId = dto.PetId;
            current.ScheduledAt = dto.ScheduledAt;
            current.Reason = dto.Reason?.Trim() ?? current.Reason;
            current.Status = dto.Status?.Trim() ?? current.Status;
            current.Notes = dto.Notes?.Trim();

            appointments[idx] = current;

            return Ok(Wrap(current));
        }

        // --- DELETE /api/v1/appointments/{id} ---
        [HttpDelete("{id:guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var removed = appointments.RemoveAll(a => a.Id == id);
            if (removed == 0)
                return NotFound(Wrap(new { error = "Appointment not found", status = 404 }));

            return NoContent();
        }
    }
}