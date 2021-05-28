using System;
using System.Collections.Generic;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.Core.Models;

namespace LinuxLudo.API.Domain.Repositories
{
    public interface IGameHubRepository
    {
        void AddGame(OpenGame game);
        void RemoveGame(OpenGame game);

        void AddPlayer(OpenGame game, string username);
        void RemovePlayer(OpenGame game, string username);

        void ConnectUser(ConnectedUser user);
        void DisconnectUser(ConnectedUser user);

        IEnumerable<OpenGame> FetchAllGames();
        ConnectedUser FetchUserById(string connectionId);
        OpenGame FetchGameById(Guid id);
    }
}