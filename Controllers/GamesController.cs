using Microsoft.AspNetCore.Mvc;
using GamesApi.Services;
using GamesApi.Models;
using System.Xml.Linq;
using System.Data.SqlTypes;

namespace GamesApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : ControllerBase
    {

        private readonly GamesService _gamesService;

        public GamesController(GamesService gamesService) =>
            _gamesService = gamesService;

        [HttpGet]
        public async Task<List<Game>> Get() =>

            await _gamesService.GetAsync();

       

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Game>> Get(string id)
        {
            var game = await _gamesService.GetAsync(id);

            if (game is null)
            {
                return NotFound();
            }

            return Ok(game);
        }

        [HttpGet("title/{query}")]
        public ActionResult<List<Game>> GetGameByTitle(string query)
        {

            List<Game> allGames = _gamesService.Get();

            List<Game> gamesByQuery = allGames.Where(c => c.Title.ToLower().Contains(query.ToLower()) |

            c.ReleaseDate.ToLower().Contains(query.ToLower()) |

            c.Platform.ToLower().Contains(query.ToLower()) |
            
            c.Developer.ToLower().Contains(query.ToLower()) |

            c.ESRB.ToLower().Contains(query.ToLower())).ToList();

            if (gamesByQuery == null)
            {
                return NoContent();
            }

            return Ok(gamesByQuery);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Game newGame)
        {
            await _gamesService.CreateAsync(newGame);

            return CreatedAtAction(nameof(Get), new { id = newGame.Id }, newGame);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Game updatedGame)
        {
            var game = await _gamesService.GetAsync(id);

            if (game is null)
            {
                return NotFound();
            }

            updatedGame.Id = game.Id;

            await _gamesService.UpdateAsync(id, updatedGame);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var game = await _gamesService.GetAsync(id);

            if (game is null)
            {
                return NotFound();
            }

            await _gamesService.RemoveAsync(id);

            return NoContent();
        }

    }
}