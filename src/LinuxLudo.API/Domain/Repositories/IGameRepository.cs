using System.Threading.Tasks;
using LinuxLudo.API.Domain.Models;

namespace LinuxLudo.API.Domain.Repositories
{
    public interface IGameRepository : IRepository<Game>
    {
        Task<Game> FindByName(string name);
    }
}