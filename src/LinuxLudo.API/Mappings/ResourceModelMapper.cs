using AutoMapper;
using LinuxLudo.API.Domain.Models;
using LinuxLudo.API.Domain.Resources;

namespace LinuxLudo.API.Mappings
{
    public class ResourceModelMapper : Profile
    {
        public ResourceModelMapper()
        {
            CreateMap<CreateGameResource, Game>();
        }
    }
}