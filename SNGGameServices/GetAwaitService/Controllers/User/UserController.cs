using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserApiService _userService;

        public UserController(IUserApiService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUser()
        {
            var users = await _userService.GetAllUsersAsync();
            return users != null ? Ok(users) : StatusCode(500, "Ошибка при получении пользователей");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return user != null ? Ok(user) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDTO userDto)
        {
            var createdUser = await _userService.CreateUserAsync(userDto);

            return createdUser != null
                ? CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser)
                : StatusCode(500, "Ошибка при создании пользователя");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserDTO userDto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID not found in claims.");
            }
            if (userId != userDto.Id)
            {
                return BadRequest("Отказ доступа");
            }

            var success = await _userService.UpdateUserAsync(userId, userDto);
            return success ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var success = await _userService.DeleteUserAsync(id);
            return success ? NoContent() : NotFound();
        }
    }
}