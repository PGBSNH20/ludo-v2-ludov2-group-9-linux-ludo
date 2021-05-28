using AutoMapper;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Models.Auth;
using LinuxLudo.API.Domain.Resources;
using LinuxLudo.API.Domain.Resources.Auth;

namespace LinuxLudo.API.Mappings
{
    public class ResourceToModelMapper : Profile
    {
        public ResourceToModelMapper()
        {
            CreateMap<SignUpResource, User>();
            CreateMap<SignInResource, User>();
            CreateMap<CreateGameResource, Game>();
        }
    }
}