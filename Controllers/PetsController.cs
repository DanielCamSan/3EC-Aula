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

        public static (int page, int limit) NormalizePage(int? page, int? limit)
        {
            var p = page.GetValueOrDefault(1); if (p < 1) p = 1;
            var l = limit.GetValueOrDefault(10); if (l < 1) l = 1; if (l > 100) l = 100;
            return (p, l);
        }

        public static IEnumerable<T> OrderByProp<T>(IEnumerable<T> src, string? sort, string? order)
        {
            if (string.IsNullOrWhiteSpace(sort)) return src;
            var prop = typeof(T).GetProperty(sort, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (prop is null) return src;

            return string.Equals(order, "desc", StringComparison.OrdinalIgnoreCase) ? src.OrderByDescending(x => prop.GetValue(x)) : src.OrderBy(x => prop.GetValue(x));
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
