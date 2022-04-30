using AutoMapper;
using Stargate.API.Data.Entities;
using Stargate.API.ViewModels;

namespace Stargate.API.Data.Mappers
{
    public class FileMappingProfile : Profile
    {
        public FileMappingProfile()
        {
           CreateMap<File,FileViewModel>();
        }
    }
}