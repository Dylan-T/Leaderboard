using System;
using Amazon.DynamoDBv2.Model;

namespace Leaderboard.App.Models
{
    public class LeaderboardEntry
    {

        public string Player { get; set; } = string.Empty;

        public string Game { get; set; } = string.Empty;
        
        public int Rank { get; set; }
    }
}