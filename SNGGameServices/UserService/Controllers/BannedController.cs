using AutoMapper;
using BannedService.DB.Models;
using Microsoft.AspNetCore.Mvc;
using UserService.DB.DTO.Banned;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Route("/api/[controller]/[action]")]
    [ApiController]
    public class BannedController : ControllerBase
    {
        private readonly IBannedService bannedService;
        private readonly IMapper mapper;

        public BannedController(IBannedService bannedService, IMapper mapper)
        {
            this.bannedService = bannedService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех банов
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BannedDTO>>> GetAllBanned()
        {
            var banneds = await bannedService.GetAllAsync();
            var bannedsDTO = mapper.Map<IEnumerable<BannedDTO>>(banneds);
            return Ok(bannedsDTO);
        }

        /// <summary>
        /// Получение бана по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<BannedDTO>> GetBannedById(int id)
        {
            var banned = await bannedService.GetByIdAsync(id);
            if (banned == null)
            {
                return NotFound();
            }
            var bannedDTO = mapper.Map<BannedDTO>(banned);
            return Ok(bannedDTO);
        }

        /// <summary>
        /// Создание нового бана
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateBanned(BannedDTO bannedDTO)
        {
            var banned = mapper.Map<Banned>(bannedDTO);
            await bannedService.AddAsync(banned);
            var bannedResultDTO = mapper.Map<BannedDTO>(banned);
            return CreatedAtAction(nameof(GetBannedById), new { id = banned.Id }, bannedResultDTO);
        }

        /// <summary>
        /// Обновление бана
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateBanned(int id, BannedDTO bannedDto)
        {
            if (id != bannedDto.Id)
            {
                return BadRequest();
            }

            var existingBanned = await bannedService.GetByIdAsync(id);
            if (existingBanned == null)
            {
                return NotFound();
            }

            var banned = mapper.Map<Banned>(bannedDto);
            await bannedService.UpdateAsync(banned);
            return Ok(banned);
        }

        /// <summary>
        /// Удаление бана
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteBanned(int id)
        {
            await bannedService.DeleteAsync(id);
            return NoContent();
        }
    }
}
