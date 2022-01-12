using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        // returns all games from chess.com and lichess of given user
        [HttpGet("{chesscomusr}/{lichessusr}/games")]
        public async Task<IActionResult> GetAllGamesAsync(string chesscomusr, string lichessusr)
        {
            Console.WriteLine("In GetAllGamesAsync");

            var games = await _gameService.GetAllAsync(chesscomusr, lichessusr);

            if(games == null)
            {
                return NotFound();
            }

            return Ok(games);
        }

    }
}
