using FirstExam.Models;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Runtime.CompilerServices;
using FirstExam.Models.Dtos;
using FirstExam.Services;
namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[Controller]")]
    public class OwnersController : Controller
    {
        private readonly IOwnerService _service;
        public OwnersController(IOwnerService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var owners = await _service.GetAll();
            return Ok(owners);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var owner = await _service.GetById(id);
            if (owner == null) return NotFound();
            return Ok(owner);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var owner = await _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = owner.Id }, owner);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message, status=409 });
            }
        }
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            try
            {
                var updated = await _service.Update(id, dto);
                return updated is null
                    ? NotFound(new { error = "Owner not found", status = 404 })
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
                : NotFound(new { error = "Owner not found or has related appointments", status = 404 });
            
        }

    }
}
