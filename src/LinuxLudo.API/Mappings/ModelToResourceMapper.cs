using AutoMapper;
using LinuxLudo.API.Domain.Models.Auth;
using LinuxLudo.API.Domain.Resources.Auth;

namespace LinuxLudo.API.Mappings
{
    public class ModelToResourceMapper : Profile
    {
        public ModelToResourceMapper()
        {
            CreateMap<User, SignUpResource>();
        }
    }
}