using AutoMapper;
using CrimeService.Services;
using EventBus.Messaging.Events;
using MassTransit;

namespace PoliceService.EventBusConsumer
{
    public class UpdateCrimeConsumer : IConsumer<UpdateCrimeEvent>
    {
        private readonly IMapper _mapper;
        private readonly ICrimeRepository _crimeRepository;

        public UpdateCrimeConsumer(IMapper mapper, ICrimeRepository crimeRepository)
        {
            _mapper = mapper;
            _crimeRepository = crimeRepository;
        }

        public async Task Consume(ConsumeContext<UpdateCrimeEvent> context)
        {
            var crimeToUpdate = context.Message;
            Console.WriteLine($"Crime {crimeToUpdate} consumed successfully. Adding crime to the crimes...");

            await UpdateCrime(crimeToUpdate);
        }

        private async Task UpdateCrime(UpdateCrimeEvent crimeToUpdate)
        {
            var crime = await _crimeRepository.GetAsyncCrime(crimeToUpdate.CrimeId);
            crime.AssignedLawEnforcmentId = crimeToUpdate.AssignedLawEnforcmentId;
            crime.CrimeReportStatus = crimeToUpdate.CrimeReportStatus;

            _crimeRepository.UpdateAsyncCrime(crime);
        }
    }
}
