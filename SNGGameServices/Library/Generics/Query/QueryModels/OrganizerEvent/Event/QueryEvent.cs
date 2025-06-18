using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.Query.QueryModels.OrganizerEvent.Event;

public class QueryEvent
{
    public List<Guid>? EventId { get; set; }

    [StringLength(255)]
    public string? Title { get; set; }

    [StringLength(255)]
    public string? Address { get; set; }

    [StringLength(255)]
    public string? Country { get; set; } 

    [StringLength(255)]
    public string? Region { get; set; }

    [StringLength(255)]
    public string? City { get; set; }

    [Url(ErrorMessage = "Invalid URL format for GeoUrl")]
    [MaxLength(1024, ErrorMessage = "GeoUrl cannot be longer than 1024 characters")]
    public string? GeoUrl { get; set; }

    public string? Status { get; set; }

    public decimal? PriceMin { get; set; }
    public decimal? PriceMax { get; set; }
}
