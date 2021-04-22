using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime.Internal.Util;
using FluentAssertions;
using Leaderboard.App.Controllers;
using Leaderboard.App.Models;
using Machine.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Arg = Moq.It;
using ILogger = Amazon.Runtime.Internal.Util.ILogger;
using It = Machine.Specifications.It;

namespace Leaderboard.Unit.Test
{
    public class ControllerTests
    {
        static Mock<AmazonDynamoDBClient> client;
        static ILogger<LeaderboardController> logger;
        static LeaderboardController controller;

        private Establish context = () =>
        {
            client = new Mock<AmazonDynamoDBClient>();
            logger = Mock.Of<ILogger<LeaderboardController>>();
            controller = new LeaderboardController(logger, client.Object);

        };
        
        public class when_using_get_method
        {
            public class when_get_succeeds
            {
                private static Exception exception;
                static ActionResult result;

                private Establish context = () =>
                {
                    client.Setup(v => v.QueryAsync(Arg.IsAny<QueryRequest>(), default)).ReturnsAsync(new QueryResponse { HttpStatusCode = HttpStatusCode.Accepted});
                };
                
                Because of = async () =>
                {
                    Exception exception = Catch.Exception( () => result = controller.Get("table-tennis").Await());
                };

                private It should_not_throw_an_exception = () =>
                {
                    exception.Should().BeNull();
                };
            }
        }
    }
}