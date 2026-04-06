using Microsoft.AspNetCore.Mvc;
using TravelDestinationApi.Models;
using TravelDestinationApi.Repository;

namespace TravelDestinationApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DestinationsController : ControllerBase
    {
        private readonly IDestinationRepository _repository;

        public DestinationsController(IDestinationRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var destinations = await _repository.GetAllAsync();
            return Ok(destinations);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var destination = await _repository.GetByIdAsync(id);
            return Ok(destination);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Destination destination)
        {
            await _repository.AddAsync(destination);
            return CreatedAtAction(nameof(GetById), new { id = destination.Id }, destination);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Destination destination)
        {
            if (id != destination.Id)
            {
                return BadRequest(new { Message = "ID mismatch." });
            }

            await _repository.UpdateAsync(destination);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }
    }
}