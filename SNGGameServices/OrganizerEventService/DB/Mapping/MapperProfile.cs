using OrganizerEventService.DB.DTO.Event;
using OrganizerEventService.DB.DTO.Organizer;
using OrganizerEventService.DB.Models;
using AutoMapper;

namespace AdministratumService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            {
                CreateMap<Event, CreateEventDTO>().ReverseMap();
                CreateMap<Event, DeleteEventDTO>().ReverseMap();
                CreateMap<Event, EventIdDTO>().ReverseMap();
                CreateMap<Event, GetByIdEventDTO>().ReverseMap();
                CreateMap<Event, UpdateEventDTO>().ReverseMap();
            }

            {
                CreateMap<Organizer, CreateOrganizerDTO>().ReverseMap();
                CreateMap<Organizer, DeleteOrganizerDTO>().ReverseMap();
                CreateMap<Organizer, GetByIdOrganizerDTO>().ReverseMap();
                CreateMap<Organizer, OrganizerIdDTO>().ReverseMap();
                CreateMap<Organizer, UpdateOrganizerDTO>().ReverseMap();
            }
        }
    }
}
