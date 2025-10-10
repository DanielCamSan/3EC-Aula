using Microsoft.AspNetCore.Mvc;
using apiwithdb.Services;
using apiwithdb.Models.dtos;
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
            var items = await _service.GetAll();
            return Ok(items);
        }
        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetOne(Guid id)
        {
            var pet = await _service.GetById(id);
            return pet == null
                ? NotFound(new { error = "Pet not found", sttus = 404 })
                : Ok(pet);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreatePetDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var pet = await _service.Create(dto);
            return CreatedAtAction(nameof(GetOne), new { id = pet.Id }, pet);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var success = await _service.Delete(id);
            return success
                ? NoContent()
                : NotFound(new { error = "Pet not found", status = 404 });
        }
    }
}
