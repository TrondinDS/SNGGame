using AdministratumService.DB.DTO.ChatFeedback;
using AdministratumService.DB.DTO.ComplainTicket;
using AdministratumService.DB.DTO.Message;
using AdministratumService.DB.Models;
using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;

namespace AdministratumService.DB.Mapping
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            {
                CreateMap<ChatFeedback, ChatFeedbackDTO>().ReverseMap();
                CreateMap<ChatFeedback, CreateChatFeedbackDTO>().ReverseMap();
                CreateMap<ChatFeedback, ChatFeedbackIdDTO>().ReverseMap();
                CreateMap<ChatFeedback, UpdateChatFeedbackDTO>().ReverseMap();
                CreateMap<ChatFeedback, GetByIdChatFeedbackDTO>().ReverseMap();
                CreateMap<ChatFeedback, DeleteChatFeedbackDTO>().ReverseMap();
            }

            {
                CreateMap<ComplainTicket, ComplainTicketDTO>().ReverseMap();
                CreateMap<ComplainTicket, ComplainTicketIdDTO>().ReverseMap();
				CreateMap<ComplainTicket, CreateComplainTicketDTO>().ReverseMap();
				CreateMap<ComplainTicket, DeleteComplainTicketDTO>().ReverseMap();
                CreateMap<ComplainTicket, GetByIdComplainTicketDTO>().ReverseMap();
				CreateMap<ComplainTicket, UpdateComplainTicketDTO>().ReverseMap();
            }

			{
                CreateMap<Message, MessageDTO>().ReverseMap();
				CreateMap<Message, CreateMessageDTO>().ReverseMap();
				CreateMap<Message, DeleteMessageDTO>().ReverseMap();
				CreateMap<Message, GetByIdMessageDTO>().ReverseMap();
				CreateMap<Message, MessageIdDTO>().ReverseMap();
				CreateMap<Message, UpdateMessageDTO>().ReverseMap();
			}
        }
    }
}
