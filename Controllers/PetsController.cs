using FirstExam.DTOs;
using FirstExam.Models;
using FirstExam.Services;
using Microsoft.AspNetCore.Mvc;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService service;

        public PetsController(IPetService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var pets = await service.GetAllAsync();
            return Ok(pets);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var pet = await service.GetByIdAsync(id);
            return pet is null ? NotFound(new { error = "Pet not found" }) : Ok(pet);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var pet = await service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetOne), new { id = pet.Id }, pet);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var pet = await service.UpdateAsync(id, dto);
            return pet is null ? NotFound(new { error = "Pet not found" }) : Ok(pet);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await service.DeleteAsync(id);
            return result ? NoContent() : NotFound(new { error = "Pet not found" });
        }
    }
}
