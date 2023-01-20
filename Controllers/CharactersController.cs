using Microsoft.AspNetCore.Mvc;
using GamesApi.Services;
using GamesApi.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GamesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CharactersController : ControllerBase
    {

        private readonly CharacterService _characterService;

        public CharactersController(CharacterService characterService) =>
            _characterService = characterService;

        [HttpGet]
        public async Task<List<Character>> Get() =>

            await _characterService.GetAsync();



        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Character>> Get(string id)
        {
            var character = await _characterService.GetAsync(id);

            if (character is null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        [HttpGet("name/{query}")]
        public ActionResult<List<Character>> GetCharacterByName(string query)
        {
            List<Character> allChars = _characterService.Get();
            
            List<Character> charsByQuery = allChars.Where(c => c.Name.ToLower().Contains(query.ToLower()) |

            c.Game.ToLower().Contains(query.ToLower()) |

            c.Weapon.ToLower().Contains(query.ToLower())).ToList();


            if (charsByQuery == null)
            {
                return NoContent();
            }

            return Ok(charsByQuery);
        }

        [HttpGet("randomizefour")]
        public async Task<ActionResult<Character>> GetRandomCharacters()
        {
            var characters = await _characterService.GetAsync();

            var random = new Random();

            var randChars = characters.OrderBy(x => random.Next()).Take(4);

            if (randChars == null)
            {
                return NoContent();
            }
            return Ok(randChars);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Character newCharacter)
        {
            await _characterService.CreateAsync(newCharacter);

            return CreatedAtAction(nameof(Get), new { id = newCharacter.Id }, newCharacter);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Character updatedCharacter)
        {
            var character = await _characterService.GetAsync(id);

            if (character is null)
            {
                return NotFound();
            }

            updatedCharacter.Id = character.Id;

            await _characterService.UpdateAsync(id, updatedCharacter);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var character = await _characterService.GetAsync(id);

            if (character is null)
            {
                return NotFound();
            }

            await _characterService.RemoveAsync(id);

            return NoContent();
        }

    }
}
