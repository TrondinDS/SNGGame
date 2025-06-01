using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;
using Library.Generics.DB.DTO.DTOModelServices.UserService.UserSubscription;
using GetAwaitService.Services.UserService.Interfaces;
using System.Linq;
using AutoMapper;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserSubscriptionController : ControllerBase
    {
        private readonly IUserSubscriptionApiService _subscriptionService;
        private readonly IMapper _mapper;

        public UserSubscriptionController(IUserSubscriptionApiService subscriptionService, IMapper mapper)
        {
            _subscriptionService = subscriptionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUserSubscription()
        {
            var subscriptions = await _subscriptionService.GetAllAsync();
            return subscriptions != null ? Ok(subscriptions) : StatusCode(500, "Ошибка при получении подписок пользователей");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserSubscriptionById(Guid id)
        {
            var subscription = await _subscriptionService.GetByIdAsync(id);
            return subscription != null ? Ok(subscription) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserSubscription([FromBody] UserSubscriptionCreateDTOFront dtoF)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var createDto = _mapper.Map<UserSubscriptionCreateDTO>(dtoF);
            createDto.UserId = userId;

            var created = await _subscriptionService.CreateAsync(createDto);
            return created != null
                ? CreatedAtAction(nameof(GetUserSubscriptionById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании подписки пользователя");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUserSubscription(Guid id, [FromBody] UserSubscriptionDTO dto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            if (userId != id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var updated = await _subscriptionService.UpdateAsync(id, dto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserSubscription(Guid id)
        {
            var deleted = await _subscriptionService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}