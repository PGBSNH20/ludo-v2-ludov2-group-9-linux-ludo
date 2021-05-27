using System.Threading.Tasks;
using LinuxLudo.API.Database.Repositories;
using LinuxLudo.API.Hubs;
using NUnit.Framework;
using Xunit;
using LinuxLudo.Core.Models;
using MessagePack;
using LinuxLudo.Core;
using Moq;
using Microsoft.AspNetCore.SignalR;
using System;

namespace LinuxLudo.Test.Game
{
    public class ActiveGameTest
    {
        private readonly GameHub testHub;
        private readonly GameHubRepository testRepo;
        public ActiveGameTest()
        {
            testRepo = new GameHubRepository();
            testHub = new(testRepo);
        }

        [Fact]
        [Description("Verifies that the hub the correct token upon request")]
        public async Task Hub_MoveToken()
        {
            
        }
    }
}