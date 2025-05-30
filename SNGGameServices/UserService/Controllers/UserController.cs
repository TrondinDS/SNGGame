using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Mvc;
using UserService.DB.Models;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            this.userService = userService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех клиентов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetAllUser()
        {
            var users = await userService.GetAllAsync();
            var customerDTOs = mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(customerDTOs);
        }

        /// <summary>
        /// Получение клиента по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDTO>> GetUserById(Guid id)
        {
            var user = await userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = mapper.Map<UserDTO>(user);
            return Ok(userDTO);
        }

        /// <summary>
        /// Создание нового клиента
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateUser(UserCreateDTO userCreateDTO)
        {
            var user = mapper.Map<UserDTO>(userCreateDTO);
            await userService.AddAsync(user);
            var userResultDTO = mapper.Map<UserDTO>(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, userResultDTO);
        }

        /// <summary>
        /// Обновление клиента
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, UserDTO userDto)
        {
            if (id != userDto.Id)
            {
                return BadRequest();
            }

            var existingUser = await userService.GetByIdAsync(id);
            if (existingUser == null)
            {
                return NotFound();
            }
            await userService.UpdateAsync(userDto);

            return Ok(userDto);
        }

        /// <summary>
        /// Удаление клиента
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            await userService.DeleteAsync(id);
            return NoContent();
        }
    }
}