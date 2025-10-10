using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using FirstExam.Models;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        // --- "DB" en memoria para el examen/demo ---
        private static readonly List<Appointment> _db = new()
        {
            new Appointment {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000A1"),
                PetId = Guid.Parse("00000000-0000-0000-0000-0000000000AA"),
                ScheduledAt = DateTime.UtcNow.AddDays(1),
                Reason = "vacunación", Status = "scheduled", Notes = "Primera dosis"
            },
            new Appointment {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000B1"),
                PetId = Guid.Parse("00000000-0000-0000-0000-0000000000BB"),
                ScheduledAt = DateTime.UtcNow.AddHours(8),
                Reason = "control", Status = "scheduled"
            },
            new Appointment {
                Id = Guid.Parse("00000000-0000-0000-0000-0000000000C1"),
                PetId = Guid.Parse("00000000-0000-0000-0000-0000000000BB"),
                ScheduledAt = DateTime.UtcNow.AddDays(-2),
                Reason = "curación", Status = "completed"
            }
        };

        // --- Helpers ---
        private static (int page, int limit) Normalize(int page, int limit)
        {
            page = Math.Max(1, page);
            limit = Math.Clamp(limit <= 0 ? 10 : limit, 1, 100);
            return (page, limit);
        }

        private static IOrderedEnumerable<Appointment> ApplySort(IEnumerable<Appointment> q, string sort, string order)
        {
            bool desc = string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase);
            sort = (sort ?? "scheduledAt").ToLowerInvariant();

            Func<Appointment, object> key = sort switch
            {
                "reason" => a => a.Reason,
                "status" => a => a.Status,
                "petid" => a => a.PetId,
                "id" => a => a.Id,
                _ => a => a.ScheduledAt
            };

            return desc ? q.OrderByDescending(key) : q.OrderBy(key);
        }

        // --- GET /api/v1/appointments
        // Soporta: page, limit, sort, order, status, from, to, search
        [HttpGet]
        public IActionResult GetAll(
            [FromQuery] int page = 1,
            [FromQuery] int limit = 10,
            [FromQuery] string? sort = "scheduledAt",
            [FromQuery] string? order = "asc",
            [FromQuery] string? status = null,
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null,
            [FromQuery] string? search = null
        )
        {
            (page, limit) = Normalize(page, limit);

            IEnumerable<Appointment> q = _db;

            if (!string.IsNullOrWhiteSpace(status))
                q = q.Where(a => a.Status.Equals(status, StringComparison.OrdinalIgnoreCase));

            if (from.HasValue) q = q.Where(a => a.ScheduledAt >= from.Value);
            if (to.HasValue) q = q.Where(a => a.ScheduledAt <= to.Value);

            if (!string.IsNullOrWhiteSpace(search))
                q = q.Where(a => a.Reason.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                 (a.Notes != null && a.Notes.Contains(search, StringComparison.OrdinalIgnoreCase)));

            var total = q.Count();
            var ordered = ApplySort(q, sort ?? "scheduledAt", order ?? "asc");
            var items = ordered.Skip((page - 1) * limit).Take(limit).ToList();

            return Ok(new
            {
                data = items,
                meta = new
                {
                    page,
                    limit,
                    total,
                    sort = sort ?? "scheduledAt",
                    order = order ?? "asc"
                }
            });
        }

        // --- GET /api/v1/appointments/{id}
        [HttpGet("{id:guid}")]
        public IActionResult GetOne(Guid id)
        {
            var a = _db.FirstOrDefault(x => x.Id == id);
            return a == null
                ? NotFound(new { error = "Appointment not found", status = 404 })
                : Ok(new { data = a });
        }

        // --- POST /api/v1/appointments
        [HttpPost]
        public IActionResult Create([FromBody] Appointment input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid data", details = ModelState });

            // Evitar solapamiento básico por Pet en misma hora exacta
            bool clash = _db.Any(a => a.PetId == input.PetId && a.ScheduledAt == input.ScheduledAt && a.Status != "cancelled");
            if (clash)
                return Conflict(new { error = "Time slot already booked for this pet", status = 409 });

            input.Id = input.Id == Guid.Empty ? Guid.NewGuid() : input.Id;
            input.Status = string.IsNullOrWhiteSpace(input.Status) ? "scheduled" : input.Status;

            _db.Add(input);
            return CreatedAtAction(nameof(GetOne), new { id = input.Id }, new { data = input });
        }

        // --- PUT /api/v1/appointments/{id}
        [HttpPut("{id:guid}")]
        public IActionResult Update(Guid id, [FromBody] Appointment input)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid data", details = ModelState });

            var a = _db.FirstOrDefault(x => x.Id == id);
            if (a == null) return NotFound(new { error = "Appointment not found", status = 404 });

            // Revisa choque horario si cambia fecha/hora o pet
            bool clash = _db.Any(x => x.Id != id && x.PetId == input.PetId && x.ScheduledAt == input.ScheduledAt && x.Status != "cancelled");
            if (clash)
                return Conflict(new { error = "Time slot already booked for this pet", status = 409 });

            a.PetId = input.PetId;
            a.ScheduledAt = input.ScheduledAt;
            a.Reason = input.Reason;
            a.Status = string.IsNullOrWhiteSpace(input.Status) ? a.Status : input.Status;
            a.Notes = input.Notes;

            return Ok(new { data = a });
        }

        // --- PATCH /api/v1/appointments/{id}/status
        [HttpPatch("{id:guid}/status")]
        public IActionResult ChangeStatus(Guid id, [FromBody] dynamic body)
        {
            var a = _db.FirstOrDefault(x => x.Id == id);
            if (a == null) return NotFound(new { error = "Appointment not found", status = 404 });

            string? status = body?.status;
            if (string.IsNullOrWhiteSpace(status))
                return BadRequest(new { error = "Missing 'status' field" });

            a.Status = status!;
            return Ok(new { data = a });
        }

        // --- DELETE /api/v1/appointments/{id}
        [HttpDelete("{id:guid}")]
        public IActionResult Delete(Guid id)
        {
            var removed = _db.RemoveAll(x => x.Id == id);
            return removed == 0
                ? NotFound(new { error = "Appointment not found", status = 404 })
                : NoContent();
        }
    }
}
