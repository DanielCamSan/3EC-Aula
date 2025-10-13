using Microsoft.AspNetCore.Mvc;
using Microsoft.JSInterop.Infrastructure;
using System.Reflection;
using System.Runtime.CompilerServices;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FirstExam.Services;
using System.Threading.Tasks;

namespace FirstExam.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _service;
        public PetsController(IPetService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {          
            IEnumerable<Pet> items = await _service.GetAll();         
            return Ok(items);
        }

        [HttpGet("{id:guid}")]
        public async  Task<ActionResult<Pet>> GetOne(Guid id)
        {
            var pet = await _service.GetById(id);
            return pet is null ? NotFound(new { error = "Pet not found", status = 404 }) : Ok(pet);
        }

        [HttpPost]
        public async Task<ActionResult<Pet>> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var pet = await _service.Create(dto);

            var response = new PetDto
            {               
                Name = pet.Name,               
            };

            return CreatedAtAction(nameof(GetOne), new { id = pet.Id }, response);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Pet>> Update(Guid id, [FromBody] UpdatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.Update(id, dto);
            return Ok(updated);

        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var removed = await _service.Delete(id);
            return !removed ?
                NotFound(new { error = "Pet not found", status = 404 }) :
            NoContent();
        }
    }
}
