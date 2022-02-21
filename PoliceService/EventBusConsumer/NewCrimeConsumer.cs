using AutoMapper;
using EventBus.Messaging.Events;
using MassTransit;
using PoliceService.Models;
using PoliceService.Services;

namespace PoliceService.EventBusConsumer
{
    public class NewCrimeConsumer : IConsumer<NewCrimeEvent>
    {
        private readonly IMapper _mapper;
        private readonly IPoliceRepository _policeRepository;

        public NewCrimeConsumer(IMapper mapper, IPoliceRepository policeRepository)
        {
            _mapper = mapper;
            _policeRepository = policeRepository;
        }

        public async Task Consume(ConsumeContext<NewCrimeEvent> context)
        {
            var crime = context.Message;
            Console.WriteLine($"Crime {crime.Id} consumed successfully. Adding crime to the crimes...");

            await UpdateCrimes(crime);
        }

        private async Task UpdateCrimes(NewCrimeEvent crime)
        {
            var crimeModel = _mapper.Map<Crime>(crime);
            var policeOfficer = await _policeRepository.GetPoliceOfficerWithLowerCrimesTaskAsync();
            crimeModel.AssignedLawEnforcmentId = policeOfficer.Id;

            await _policeRepository.AddCrime(crimeModel);
            await _policeRepository.SaveChanges();

            policeOfficer.Crimes.Add(crimeModel);
            await _policeRepository.SaveChanges();
        }
    }
}
