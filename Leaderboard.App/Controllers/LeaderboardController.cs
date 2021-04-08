using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Leaderboard.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {

        private readonly ILogger<LeaderboardController> _logger;

        public LeaderboardController(ILogger<LeaderboardController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<LeaderboardEntry> Get()
        {
            Console.WriteLine("Leaderboard: GET");
            return Enumerable.Range(1, 5).Select(index => new LeaderboardEntry()
                {
                    Player = "TestPlayer" + index,
                    Game = "SampleGame",
                    Rank = 300
                })
                .ToArray();
        }
    }
}