using System;
using System.Threading.Tasks;

namespace LinuxLudo.API.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        IPlayerStatsRepository PlayerStats { get; }
        Task<int> CommitAsync();
    }
}