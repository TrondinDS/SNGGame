using AdministratumService.DB.DTO.ChatFeedback;
using AdministratumService.DB.DTO.ComplainTicket;
using AdministratumService.DB.Models;
using AutoMapper;

namespace AdministratumService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            {
                CreateMap<ChatFeedback, CreateChatFeedbackDTO>().ReverseMap();
                CreateMap<ChatFeedback, ChatFeedbackIdDTO>().ReverseMap();
                CreateMap<ChatFeedback, UpdateChatFeedbackDTO>().ReverseMap();
                CreateMap<ChatFeedback, GetByIdChatFeedbackDTO>().ReverseMap();
                CreateMap<ChatFeedback, DeleteChatFeedbackDTO>().ReverseMap();
            }

            {
                CreateMap<ComplainTicket, ComplainTicketIdDTO>().ReverseMap();
				CreateMap<ComplainTicket, CreateComplainTicketDTO>().ReverseMap();
				CreateMap<ComplainTicket, DeleteComplainTicketDTO>().ReverseMap();
                CreateMap<ComplainTicket, GetByIdComplainTicketDTO>().ReverseMap();
				CreateMap<ComplainTicket, UpdateComplainTicketDTO>().ReverseMap();
            }
        }
    }
}
