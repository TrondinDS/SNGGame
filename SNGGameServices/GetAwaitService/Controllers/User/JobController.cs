using GetAwaitService.Services.UserService.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Job;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace GetAwaitService.Controllers.User
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobApiService _jobService;

        public JobController(IJobApiService jobService)
        {
            _jobService = jobService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJob()
        {
            var jobs = await _jobService.GetAllJobAsync();
            return jobs != null ? Ok(jobs) : StatusCode(500, "Ошибка при получении списка должностей");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobById(Guid id)
        {
            var job = await _jobService.GetJobByIdAsync(id);
            return job != null ? Ok(job) : NotFound();
        } 
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetJobsByUserId(Guid id)
        {
            var job = await _jobService.GetJobsByUserIdAsync(id);
            return job != null ? Ok(job) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob([FromBody] JobCreateDTO jobDto)
        {
            var userIdClaim = User.FindFirst("userId")?.Value;
            if (string.IsNullOrEmpty(userIdClaim) || !Guid.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("User ID not found in claims.");
            }

            var createdJob = await _jobService.CreateJobAsync(jobDto);
            return createdJob != null
                ? CreatedAtAction(nameof(GetJobById), new { id = createdJob.Id }, createdJob)
                : StatusCode(500, "Ошибка при создании должности");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJob(Guid id, [FromBody] JobDTO jobDto)
        {
            if (id != jobDto.Id)
                return BadRequest("ID в запросе не совпадает с ID в данных");

            var result = await _jobService.UpdateJobAsync(id, jobDto);
            return result ? Ok() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJob(Guid id)
        {
            var result = await _jobService.DeleteJobAsync(id);
            return result ? NoContent() : NotFound();
        }
    }
}