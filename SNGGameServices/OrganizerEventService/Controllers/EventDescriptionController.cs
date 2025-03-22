using Library.Generics.DB.DTO;
using Library.Services;
using Microsoft.AspNetCore.Mvc;

namespace StudioGameService.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class EventDescriptionController : Controller
    {
        private readonly Mongo mongoService;

        const string contentDatabase = "ImagesDatabase";
        const string contentCollection = "ContentCollection";

        public EventDescriptionController(Mongo mongoService)
        {
            this.mongoService = mongoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ContentDTO dto)
        {
            await mongoService
                .Database(contentDatabase)
                .Collection(contentCollection)
                .Insert(dto.Id, dto.Value);
            return Ok("Content uploaded successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(int id)
        {
            var content = await mongoService
                .Database(contentDatabase)
                .Collection(contentCollection)
                .GetContentById(id);
            return Ok(content.Value);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ContentDTO dto)
        {
            await mongoService
                .Database(contentDatabase)
                .Collection(contentCollection)
                .Insert(dto.Id, dto.Value);
            return Ok("Content updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await mongoService.Database(contentDatabase).Collection(contentCollection).Delete(id);
            return Ok("Content deleted successfully.");
        }
    }
}
