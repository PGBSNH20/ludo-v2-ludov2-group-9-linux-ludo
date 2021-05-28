using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Resources;
using LinuxLudo.API.Domain.Response;
using LinuxLudo.API.Domain.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Controllers
{
    [Authorize]
    [Route("/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly IMapper _mapper;

        public GamesController(IGameService gameService, IMapper mapper)
        {
            _gameService = gameService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateGameResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse(ModelState.Values.First().Errors.First().ErrorMessage, 500, null).Respond());

            var game = _mapper.Map<CreateGameResource, Game>(resource);
            var res = await _gameService.CreateGameAsync(game);
            if (res.Name == null)
                return BadRequest(new ErrorResponse("Game with the name already exists", 500, null).Respond());
            return Created("", new SuccessResponse("Game Created", 201).Respond(res));
        }

        [HttpGet]
        public async Task<ActionResult> GetAllAsync()
        {
            var res = await _gameService.GetAllGamesAsync();
            if (!res.Any())
                return NotFound(new ErrorResponse("No games found", 204, null).Respond());

            return Ok(new SuccessResponse("Games found", 200).Respond(res));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] string id)
        {
            var res = await _gameService.GetGameByIdAsync(id);
            if (res == null)
                return NotFound(new ErrorResponse($"Game with {id} not found", 404, null).Respond());

            return Ok(new SuccessResponse("Game found", 200).Respond(res));
        }
    }
}