using System;
using System.Collections.Generic;
using LinuxLudo.Core.Models;

namespace LinuxLudo.API.Domain.Repositories
{
    public interface IGameHubRepository
    {
        void AddGame(OpenGame game);
        void AddPlayer(OpenGame game, string username);
        IEnumerable<OpenGame> FetchAllGames();
        OpenGame FetchGameById(Guid id);
    }
}