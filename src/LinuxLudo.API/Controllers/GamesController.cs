using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Resources;
using LinuxLudo.API.Domain.Response;
using LinuxLudo.API.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace LinuxLudo.API.Controllers
{
    [Route("/api/[controller]")]
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
        public async Task<IActionResult> Create([FromBody] CreateGameResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(new ErrorResponse(ModelState.Values.First().Errors.First().ErrorMessage, 500, null).Respond());

            var game = _mapper.Map<CreateGameResource, Game>(resource);
            var res = await _gameService.CreateGameAsync(game);
            return Created("", new SuccessResponse("Game Created", 201).Respond(res));
        }
    }
}