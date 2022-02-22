using AutoMapper;
using CrimeService.Models;
using CrimeService.Services;
using EventBus.Messaging.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;

namespace CrimeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CrimeController: ControllerBase
    {
        private readonly ICrimeRepository _repository;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly IMapper _mapper;

        public CrimeController(ICrimeRepository repository, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _repository = repository;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CrimeReadDTO>>> GetAllCrimes()
        {
            var crimes = await _repository.GetAsyncCrimes();
            return Ok(_mapper.Map<IEnumerable<CrimeReadDTO>>(crimes));
        }

        [HttpGet("{id}", Name = "GetCrime")]
        public async Task<ActionResult<CrimeReadDTO>> GetCrimeById(string id)
        {
            var crime = await _repository.GetAsyncCrime(id);
            if (crime != null)
            {
                return Ok(_mapper.Map<CrimeReadDTO>(crime));
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CrimeCreateDTO>> PostCrime(CrimeCreateDTO crimeDto)
        {
            
            var crime = _mapper.Map<Crime>(crimeDto);
            
            await _repository.CreateAsyncCrime(crime);

            try
            {
                var eventMessage = _mapper.Map<NewCrimeEvent>(crime);
                await _publishEndpoint.Publish(eventMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send asynchronously: {ex.Message}");
            }

            return CreatedAtRoute("GetCrime", new { id = crime.Id }, crime);
        }
    }
}
