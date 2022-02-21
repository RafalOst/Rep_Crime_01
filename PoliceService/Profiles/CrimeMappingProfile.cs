using AutoMapper;
using EventBus.Messaging.Events;
using PoliceService.Models;

namespace PoliceService.Profiles
{
    public class CrimeMappingProfile : Profile
    {
        public CrimeMappingProfile()
        {
            CreateMap<Crime, NewCrimeEvent>().ReverseMap();
        }
    }
}
