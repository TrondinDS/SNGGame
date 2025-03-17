using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using UserActivityService.DB.Models;
using UserActivityService.Services.Interfaces;
using UserActivityService.DB.DTO.Comment;
using UserActivityService.Services;

namespace UserActivityService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService commentService;
        private readonly IMapper mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            this.commentService = commentService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех комментариев
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllComments()
        {
            var comments = await commentService.GetAllAsync();
            var commentsDTO = mapper.Map<IEnumerable<CommentDTO>>(comments);
            return Ok(commentsDTO);
        }

        /// <summary>
        /// Получение комментария по ID
        /// </summary>
        /// <param name="id">Идентификатор комментария</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDTO>> GetCommentById(int id)
        {
            var comment = await commentService.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            var commentDTO = mapper.Map<CommentDTO>(comment);
            return Ok(commentDTO);
        }

        /// <summary>
        /// Создание нового комментария
        /// </summary>
        /// <param name="commentDTO">Данные для создания нового комментария</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateComment(CommentDTO commentDTO)
        {
            var comment = mapper.Map<Comment>(commentDTO);
            await commentService.AddAsync(comment);
            var commentResultDTO = mapper.Map<CommentDTO>(comment);
            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, commentResultDTO);
        }

        /// <summary>
        /// Обновление комментария
        /// </summary>
        /// <param name="id">Идентификатор комментария</param>
        /// <param name="commentDTO">Обновленные данные комментария</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateComment(int id, CommentDTO commentDTO)
        {
            if (id != commentDTO.Id)
            {
                return BadRequest();
            }

            var existingComment = await commentService.GetByIdAsync(id);
            if (existingComment == null)
            {
                return NotFound();
            }

            var comment = mapper.Map<Comment>(commentDTO);
            await commentService.UpdateAsync(comment);
            return Ok(comment);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        /// <param name="id">Идентификатор комментария</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteComment(int id)
        {
            await commentService.DeleteAsync(id);
            return NoContent();
        }
    }
}