using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using UserActivityService.DB.Models;
using UserActivityService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;

namespace UserActivityService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicService topicService;
        private readonly IMapper mapper;

        public TopicController(ITopicService topicService, IMapper mapper)
        {
            this.topicService = topicService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех тем
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetAllTopics()
        {
            var topics = await topicService.GetAllAsync();
            var topicsDTO = mapper.Map<IEnumerable<TopicDTO>>(topics);
            return Ok(topicsDTO);
        }

        /// <summary>
        /// Получение темы по ID
        /// </summary>
        /// <param name="id">Идентификатор темы</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDTO>> GetTopicById(Guid id)
        {
            var topic = await topicService.GetByIdAsync(id);
            if (topic == null)
            {
                return NotFound();
            }
            var topicDTO = mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }

        /// <summary>
        /// Создание новой темы
        /// </summary>
        /// <param name="topicDTO">Данные для создания новой темы</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateTopic(TopicDTO topicDTO)
        {
            var topic = mapper.Map<Topic>(topicDTO);
            await topicService.AddAsync(topic);
            var topicResultDTO = mapper.Map<TopicDTO>(topic);
            return CreatedAtAction(nameof(GetTopicById), new { id = topic.Id }, topicResultDTO);
        }

        /// <summary>
        /// Обновление темы
        /// </summary>
        /// <param name="id">Идентификатор темы</param>
        /// <param name="topicDTO">Обновленные данные темы</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTopic(Guid id, TopicDTO topicDTO)
        {
            if (id != topicDTO.Id)
            {
                return BadRequest();
            }

            var existingTopic = await topicService.GetByIdAsync(id);
            if (existingTopic == null)
            {
                return NotFound();
            }

            var topic = mapper.Map<Topic>(topicDTO);
            await topicService.UpdateAsync(topic);
            return Ok(topic);
        }

        /// <summary>
        /// Удаление темы
        /// </summary>
        /// <param name="id">Идентификатор темы</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTopic(Guid id)
        {
            await topicService.DeleteAsync(id);
            return NoContent();
        }
    }
}
