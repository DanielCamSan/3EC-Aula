using System.Reflection;
using System.Runtime.CompilerServices;
using FirstExam.Models;
using FirstExam.Models.DTO;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetOne(Guid id)
        {
            var owner = await _service.GetById(id);
            return owner == null
                ? NotFound(new { error = "Owner not found", status = 404 })
                : Ok(owner);
        }

      
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var owner = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = owner.Id }, owner);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateOwnerDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var updated = await _service.Update(id, dto);
            return updated == null
                ? NotFound(new { error = "Owner not found", status = 404 })
                : Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.Delete(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Owner not found", status = 404 });
        }

    }
}
