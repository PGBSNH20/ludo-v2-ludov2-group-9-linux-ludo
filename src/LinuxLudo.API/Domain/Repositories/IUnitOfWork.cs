using System;
using System.Threading.Tasks;

namespace LinuxLudo.API.Domain.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGameRepository Games { get; }
        IGameResultRepository GameResult { get; }
        Task<int> CommitAsync();
    }
}