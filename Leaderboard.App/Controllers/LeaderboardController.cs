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
    [Route("api/leaderboard")]
    public class LeaderboardController : ControllerBase
    {

        private readonly ILogger<LeaderboardController> _logger;
        private AmazonDynamoDBClient _client;        

        public LeaderboardController(ILogger<LeaderboardController> logger)
        {
            _logger = logger;
            Environment.SetEnvironmentVariable("AWS_PROFILE","private");
            _client = new AmazonDynamoDBClient();
        }

        [HttpGet]
        public async Task<ActionResult> Get(string game) {
            Console.WriteLine("Leaderboard: GET");
            
            var request = new QueryRequest
            {
                TableName = "leaderboard",
                KeyConditionExpression = "Game = :v_Game",
                ExpressionAttributeValues = new Dictionary<string, AttributeValue> {
                    {":v_Game", new AttributeValue { S =  game }}}
            };
            var response = await _client.QueryAsync(request);
            if (response.Items.Count == 0)
            {
                return NotFound();
            }
            
            List<LeaderboardEntry> output = new List<LeaderboardEntry>();
            
            foreach (var item in response.Items)
            {
                output.Add(new LeaderboardEntry()
                {
                    Game = item["Game"].S,
                    Player = item["Name"].S,
                    Rank = int.Parse(item["Rank"].N)
                });
            }
            
            return new JsonResult(output);
        }

        // [HttpPut("{game, name, rank}")]
        // public ActionResult Put(string game, string name, int rank)
        // {
        //     //Put it in dynamo
        //     
        //     
        //     return Ok();
        // }
    }
}