using AdministratumService.DB.DTO.ChatFeedback;
using AdministratumService.DB.Models;
using AdministratumService.Repository;
using AdministratumService.Repository.Interfaces;
using AutoMapper;
using Library.GenericService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Npgsql.PostgresTypes;

namespace AdministratumService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ChatFeedbackController : Controller
    {
        private readonly CrudGenericService<ChatFeedback, int, ChatFeedbackRepository> service;
        private readonly IMapper mapper;

        public ChatFeedbackController(CrudGenericService<ChatFeedback, int, ChatFeedbackRepository> chatFeedbackService, IMapper mapper)
        {
            this.service = chatFeedbackService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<GetByIdChatFeedbackDTO>> Create(CreateChatFeedbackDTO DTO)
        {
            var model = mapper.Map<ChatFeedback>(DTO);
            await service.AddAsync(model);
            var res = mapper.Map<GetByIdChatFeedbackDTO>(model);
            return Ok(res);
        }
    }
}