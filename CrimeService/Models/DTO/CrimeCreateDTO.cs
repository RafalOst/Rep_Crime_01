using CommonItems;
using System.ComponentModel.DataAnnotations;

namespace CrimeService.Models
{
    public class CrimeCreateDTO
    {
        public CrimeEventType CrimeType { get; set; }

        [MinLength(10)]
        [MaxLength(500)]
        public string Description { get; set; }

        [MinLength(10)]
        [MaxLength(250)]
        public string PlaceOfEvent { get; set; }

        [MaxLength(150)]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }
    }
}
