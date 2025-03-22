using OrganizerEventService.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrganizerEventService.DB.DTO.Event
{
    public class UpdateEventDTO
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
        public string Title { get; set; }

        [MaxLength(255, ErrorMessage = "Address cannot be longer than 255 characters")]
        public string Address { get; set; }

        [MaxLength(100, ErrorMessage = "Country cannot be longer than 100 characters")]
        public string Country { get; set; }

        [MaxLength(100, ErrorMessage = "Region cannot be longer than 100 characters")]
        public string Region { get; set; }

        [MaxLength(100, ErrorMessage = "City cannot be longer than 100 characters")]
        public string City { get; set; }

        [Url(ErrorMessage = "Invalid URL format for GeoUrl")]
        [MaxLength(1024, ErrorMessage = "GeoUrl cannot be longer than 1024 characters")]
        public string GeoUrl { get; set; }

        public string Status { get; set; }

        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
    }
}
