using OrganizerEventService.DB.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OrganizerEventService.DB.DTO.Event
{
    public class GetByIdEventDTO
    {
        public string Title { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string GeoUrl { get; set; }
        public string Status { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public int OrganizerEventId { get; set; }
    }
}
