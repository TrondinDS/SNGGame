using AutoMapper;
using Library.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserService.DB.DTO.User;
using UserService.DB.Models;

namespace UserService.Controllers
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class UserAvatarController : Controller
    {
        private readonly Mongo mongoService;
        private readonly IMapper mapper;

        public UserAvatarController(Mongo mongoService, IMapper mapper)
        {
            this.mongoService = mongoService;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task Create(UserCreateDTO userDTO)
        {
        }

        [HttpGet]
        public async Task Read(UserCreateDTO userDTO)
        {
        }

        [HttpPut("{id}")]
        public async Task Update(UserCreateDTO userDTO)
        {
        }

        [HttpDelete("{id}")]
        public async Task Delete(UserCreateDTO userDTO)
        {
        }
    }
}
