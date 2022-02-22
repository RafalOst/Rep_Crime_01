using CommonItems;
using Microsoft.AspNetCore.Mvc;
using PoliceService.Models;
using PoliceService.Services;

namespace PoliceService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PoliceController : ControllerBase
    {
        private readonly IPoliceRepository _policeRepository;

        public PoliceController(IPoliceRepository policeRepository) =>
            _policeRepository = policeRepository;

        [HttpGet]
        public async Task<ActionResult<List<PoliceOfficer>>> GetPoliceOfficers()
        {
            var policeOfficers = await _policeRepository.GetAllPoliceOfficersAsync();
            return Ok(policeOfficers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PoliceOfficer>> GetPoliceOfficer(int id)
        {
            var policeOfficer = await _policeRepository.GetPoliceOfficerByIdAsync(id);

            if (policeOfficer != null)
            {
                return Ok(policeOfficer);
            }
            return NotFound();           
        }

        [HttpPut("{crimeId:length(24)}")]
        public async Task<IActionResult> UpdateCrimeStatus(string crimeId, CrimeReportStatus newStatus)
        {
            _policeRepository.UpdateCrimeStatusAsync(crimeId, newStatus);
            return NoContent();
        }

        ////to tests
        //[HttpGet]
        //[Route("policeOficerWithLowerTask")]
        //public async Task<ActionResult<PoliceOfficer>> GetPoliceOfficerWithLowerTasks()
        //{
        //    var policeOfficer = await _policeRepository.GetPoliceOfficerWithLowerCrimesTaskAsync();
        //    return Ok(policeOfficer);
        //}

        //[HttpGet]
        //[Route("Crimes")]
        //public async Task<ActionResult<List<Crime>>> GetAllCrimes()
        //{
        //    var crimes = await _policeRepository.GetAllCrimesAsync();
        //    return Ok(crimes);
        //}
    }
}