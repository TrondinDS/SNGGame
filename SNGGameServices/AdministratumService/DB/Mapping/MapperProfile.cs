using AdministratumService.DB.DTO.ChatFeedback;
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
                CreateMap<ChatFeedback, DeleteChatFeedbackDTO>().ReverseMap();
                
                CreateMap<ChatFeedback, GetByIdChatFeedbackDTO>().ReverseMap();
            }
        }
    }
}
