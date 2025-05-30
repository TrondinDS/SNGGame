using GetAwaitService.Services.UserActivityService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.UserActivity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly ITopicApiService _topicService;

        public TopicController(ITopicApiService topicService)
        {
            _topicService = topicService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTopics()
        {
            var topics = await _topicService.GetAllAsync();
            return topics != null ? Ok(topics) : StatusCode(500, "Ошибка при получении списка тем.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopicById(Guid id)
        {
            var topic = await _topicService.GetByIdAsync(id);
            return topic != null ? Ok(topic) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateTopic([FromBody] TopicDTO topicDto)
        {
            var created = await _topicService.CreateAsync(topicDto);
            return created != null
                ? CreatedAtAction(nameof(GetTopicById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании темы.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTopic(Guid id, [FromBody] TopicDTO topicDto)
        {
            if (id != topicDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var updated = await _topicService.UpdateAsync(id, topicDto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopic(Guid id)
        {
            var deleted = await _topicService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}