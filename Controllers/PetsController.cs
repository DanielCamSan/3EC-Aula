using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FirstExam.Models.dtos;
using FirstExam.Services;
using System.Threading.Tasks;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;

        public PetsController(IPetService petService)
        {
            _petService = petService;
        } 


        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pet>> >GetAll()
        {
            var pets = await _petService.GetAll();
            return Ok(pets);
        }

        [HttpGet("{id:guid}")]
        public async Task <ActionResult<Pet>> GetOne(Guid id)
        {
            var pet = await _petService.GetById(id);
            return pet is null ? NotFound(new { error = "Pet not found", status = 404 }) : Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var pet = await _petService.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = pet.Id },pet);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Pet>> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _petService.Update(id, dto);
            if (updated == null)
                return NotFound(new { error = "Pet not found", status = 404 });
            
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _petService.Delete(id);
            return removed ? NoContent() : NotFound(new { error = "Pet not found", status = 404 });
            
        }
    }
}
