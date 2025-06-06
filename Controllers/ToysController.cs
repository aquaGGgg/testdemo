using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ToyStoreApi.DTOs;
using ToyStoreApi.Services.Interfaces;

namespace ToyStoreApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToysController : ControllerBase
    {
        private readonly IToyService _toyService;

        public ToysController(IToyService toyService)
        {
            _toyService = toyService;
        }

        // GET: api/Toys
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ToyDto>>> GetAll()
        {
            var toys = await _toyService.GetAllAsync();
            return Ok(toys);
        }

        // GET: api/Toys/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ToyDto>> GetById(int id)
        {
            var toy = await _toyService.GetByIdAsync(id);
            if (toy == null)
                return NotFound();
            return Ok(toy);
        }

        // POST: api/Toys
        [HttpPost]
        public async Task<ActionResult<ToyDto>> Create([FromBody] ToyDto toyDto)
        {
            var created = await _toyService.CreateAsync(toyDto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT: api/Toys/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ToyDto toyDto)
        {
            if (id != toyDto.Id)
                return BadRequest("ID не совпадает с телом запроса.");

            var result = await _toyService.UpdateAsync(id, toyDto);
            if (!result)
                return NotFound();

            return NoContent();
        }

        // DELETE: api/Toys/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _toyService.DeleteAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }
    }
}
