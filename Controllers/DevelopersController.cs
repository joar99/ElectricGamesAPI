using GameApiTwo.Models;
using GameApiTwo.Services;
using GamesApi.Models;
using GamesApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlTypes;

namespace GameApiTwo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DevelopersController : ControllerBase
    {

        private readonly DeveloperService _developerService;

        public DevelopersController(DeveloperService developerService) =>
            _developerService = developerService;

        [HttpGet]
        public async Task<List<Developer>> Get() =>

            await _developerService.GetAsync();



        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Developer>> Get(string id)
        {
            var developer = await _developerService.GetAsync(id);

            if (developer is null)
            {
                return NotFound();
            }

            return Ok(developer);
        }

        [HttpGet("name/{query}")]
        public ActionResult<List<Developer>> GetDevelopersByName(string query)
        {
            List<Developer> allDevs = _developerService.Get();

            List<Developer> devsByQuery = allDevs.Where(c => c.Name.ToLower().Contains(query.ToLower()) |

            c.Location.ToLower().Contains(query.ToLower())).ToList();

            if (devsByQuery == null)
            {
                return NoContent();
            }

            return Ok(devsByQuery);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Developer newDeveloper)
        {
            await _developerService.CreateAsync(newDeveloper);

            return CreatedAtAction(nameof(Get), new { id = newDeveloper.Id }, newDeveloper);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Developer updatedDeveloper)
        {
            var developer = await _developerService.GetAsync(id);

            if (developer is null)
            {
                return NotFound();
            }

            updatedDeveloper.Id = developer.Id;

            await _developerService.UpdateAsync(id, updatedDeveloper);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var developer = await _developerService.GetAsync(id);

            if (developer is null)
            {
                return NotFound();
            }

            await _developerService.RemoveAsync(id);

            return NoContent();
        }
    }
}
