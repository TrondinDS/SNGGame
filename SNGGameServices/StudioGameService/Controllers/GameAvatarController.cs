using Library.Services;
using Microsoft.AspNetCore.Mvc;
using Library.Generics.DB.DTO;

namespace StudioGameService.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class GameAvatarController : Controller
    {
        private readonly Mongo mongoService;

        const string imgsDatabase = "ImagesDatabase";
        const string avasCollection = "GameCollection";

        public GameAvatarController(Mongo mongoService)
        {
            this.mongoService = mongoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ImgDTO dto)
        {
            await mongoService
                .Database(imgsDatabase)
                .Collection(avasCollection)
                .Insert(dto.Id, dto.ImageFile);
            return Ok("Avatar uploaded successfully.");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUserId(Guid id)
        {
            var avatar = await mongoService
                .Database(imgsDatabase)
                .Collection(avasCollection)
                .GetImgById(id);
            return Ok(File(avatar.Bytes, avatar.ContentType));
        }

        [HttpPut]
        public async Task<IActionResult> Update(ImgDTO dto)
        {
            await mongoService
                .Database(imgsDatabase)
                .Collection(avasCollection)
                .Insert(dto.Id, dto.ImageFile);
            return Ok("Avatar updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await mongoService.Database(imgsDatabase).Collection(avasCollection).Delete(id);
            return Ok("Avatar deleted successfully.");
        }
    }
}
