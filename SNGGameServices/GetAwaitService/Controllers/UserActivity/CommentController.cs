using AutoMapper;
using GetAwaitService.Services.UserActivityService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.UserActivity
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentApiService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentApiService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllComments()
        {
            var comments = await _commentService.GetAllAsync();
            return comments != null ? Ok(comments) : StatusCode(500, "Ошибка при получении списка комментариев.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(Guid id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            return comment != null ? Ok(comment) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentCreateDTO commentDtoC)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
                return BadRequest("User ID not found in claims.");

            var commentDto = _mapper.Map<CommentDTO>(commentDtoC);
            commentDto.UserId = userId;


            var created = await _commentService.CreateAsync(commentDto);
            return created != null
                ? CreatedAtAction(nameof(GetCommentById), new { id = created.Id }, created)
                : StatusCode(500, "Ошибка при создании комментария.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(Guid id, [FromBody] CommentDTO commentDto)
        {
            if (id != commentDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных.");

            var updated = await _commentService.UpdateAsync(id, commentDto);
            return updated ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(Guid id)
        {
            var deleted = await _commentService.DeleteAsync(id);
            return deleted ? NoContent() : NotFound();
        }
    }
}