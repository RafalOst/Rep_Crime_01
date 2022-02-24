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
        private readonly IPublishEndpoint _publishEndpoint;

        public NewCrimeConsumer(IMapper mapper, IPoliceRepository policeRepository, IPublishEndpoint publishEndpoint)
        {
            _mapper = mapper;
            _policeRepository = policeRepository;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<NewCrimeEvent> context)
        {
            var crime = context.Message;
            Console.WriteLine($"Crime {crime.Id} consumed successfully. Adding crime to the crimes...");

            await AssignCrime(crime);
        }

        private async Task AssignCrime(NewCrimeEvent crime)
        {
            var crimeModel = _mapper.Map<Crime>(crime);
            var policeOfficer = await _policeRepository.GetPoliceOfficerWithLowerCrimesTaskAsync();
            crimeModel.AssignedLawEnforcmentId = policeOfficer.Id;

            await _policeRepository.AddCrime(crimeModel);
            await _policeRepository.SaveChanges();

            policeOfficer.Crimes.Add(crimeModel);
            await _policeRepository.SaveChanges();

            try
            {
                var eventMessage = new UpdateCrimeEvent
                {
                    CrimeId = crimeModel.Id,
                    AssignedLawEnforcmentId = crimeModel.AssignedLawEnforcmentId,
                    CrimeReportStatus = crimeModel.CrimeReportStatus
                };
                await _publishEndpoint.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }
        }
    }
}
