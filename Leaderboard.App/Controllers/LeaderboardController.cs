using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Leaderboard.App.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Leaderboard.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeaderboardController : ControllerBase
    {

        private readonly ILogger<LeaderboardController> _logger;
        private AmazonDynamoDBClient _client;        

        public LeaderboardController(ILogger<LeaderboardController> logger, AmazonDynamoDBClient client)
        {
            _logger = logger;
            _client = client;
        }

        [HttpGet("{game}")]
        public async Task<ActionResult<IEnumerable<LeaderboardEntry>>> Get(string game) {
            Console.WriteLine("Leaderboard: GET");
            
            var request = new QueryRequest
            {
                TableName = "Leaderboard",
                KeyConditionExpression = "Game = :v_Game",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":v_Game", new AttributeValue { S =  game }}}
            };
            var response = await _client.QueryAsync(request);
            
            if (response.Items.Count == 0)
            {
                return NotFound();
            }
            return Ok(response.Items);
        }

        [HttpPut("{game, name, rank}")]
        public ActionResult Put(string game, string name, int rank)
        {
            //Put it in dynamo
            
            
            return Ok();
        }
    }
}