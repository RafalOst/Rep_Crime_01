using AutoMapper;
using CrimeService.Models;
using EventBus.Messaging.Events;

namespace CrimeService.Profiles
{
    public class CrimeMappingProfile : Profile
    {
        public CrimeMappingProfile()
        {
            CreateMap<Crime, NewCrimeEvent>().ReverseMap();
        }
    }
}
