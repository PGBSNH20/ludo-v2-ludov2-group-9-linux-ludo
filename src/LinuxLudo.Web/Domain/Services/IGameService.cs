namespace LinuxLudo.Web.Domain.Services
{
    public interface IGameService
    {
        public bool CanPlay(string username);
    }
}