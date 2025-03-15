using AutoMapper;
using Library.Services;
using Microsoft.AspNetCore.Mvc;
using UserService.DB.DTO.User;

namespace UserService.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class UserAvatarController : Controller
    {
        private readonly Mongo mongoService;

        const string imgsDatabase = "ImagesDatabase";
        const string avasCollection = "AvatarsCollection";

        public UserAvatarController(Mongo mongoService)
        {
            this.mongoService = mongoService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserAvatarDTO userAvatarDTO)
        {
            await mongoService
                .Database(imgsDatabase)
                .Collection(avasCollection)
                .Insert(userAvatarDTO.Id, userAvatarDTO.ImageFile);
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
        public async Task<IActionResult> Update(UserAvatarDTO userAvatarDTO)
        {
            await mongoService
                .Database(imgsDatabase)
                .Collection(avasCollection)
                .Insert(userAvatarDTO.Id, userAvatarDTO.ImageFile);
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
