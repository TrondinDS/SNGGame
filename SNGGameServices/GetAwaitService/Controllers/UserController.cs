using GetAwaitService.RabbitMQ.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRabbitMqService _rabbitMq;

        public UserController(IRabbitMqService rabbitMq)
        {
            _rabbitMq = rabbitMq;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] string message)
        {
            await _rabbitMq.SendMessageAsync("testQueue", message);
            return Ok("Message sent");
        }
    }
}
