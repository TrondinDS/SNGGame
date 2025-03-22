using OrganizerEventService.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrganizerEventService.DB.DTO.Event
{
    public class CreateEventDTO
    {
        [Required(ErrorMessage = "Title is required")]
        [MaxLength(255, ErrorMessage = "Title cannot be longer than 255 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Address is required")]
        [MaxLength(255, ErrorMessage = "Address cannot be longer than 255 characters")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Country is required")]
        [MaxLength(100, ErrorMessage = "Country cannot be longer than 100 characters")]
        public string Country { get; set; }

        [Required(ErrorMessage = "Region is required")]
        [MaxLength(100, ErrorMessage = "Region cannot be longer than 100 characters")]
        public string Region { get; set; }

        [Required(ErrorMessage = "City is required")]
        [MaxLength(100, ErrorMessage = "City cannot be longer than 100 characters")]
        public string City { get; set; }

        [Url(ErrorMessage = "Invalid URL format for GeoUrl")]
        [MaxLength(1024, ErrorMessage = "GeoUrl cannot be longer than 1024 characters")]
        public string GeoUrl { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public int Status { get; set; }

        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }

        [Required(ErrorMessage = "OrganizerEventId is required")]
        public int OrganizerEventId { get; set; }
    }
}
