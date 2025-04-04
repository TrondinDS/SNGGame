using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Job;
using Microsoft.AspNetCore.Mvc;
using StudioGameService.DB.Model;
using UserService.Services.Interfaces;

namespace UserService.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        private readonly IJobService jobService;
        private readonly IMapper mapper;

        public JobController(IJobService jobService, IMapper mapper)
        {
            this.jobService = jobService;
            this.mapper = mapper;
        }

        /// <summary>
        /// Получение всех работников
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<JobDTO>>> GetAllJob()
        {
            var jobs = await jobService.GetAllAsync();
            var jobsDTO = mapper.Map<IEnumerable<JobDTO>>(jobs);
            return Ok(jobsDTO);
        }

        /// <summary>
        /// Получение работника по ID
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<JobDTO>> GetJobById(int id)
        {
            var job = await jobService.GetByIdAsync(id);
            if (job == null)
            {
                return NotFound();
            }
            var jobDTO = mapper.Map<JobDTO>(job);
            return Ok(jobDTO);
        }

        /// <summary>
        /// Создание нового работника
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> CreateJob(JobDTO jobDTO)
        {
            var job = mapper.Map<Job>(jobDTO);
            await jobService.AddAsync(job);
            var jobResultDTO = mapper.Map<JobDTO>(job);
            return CreatedAtAction(nameof(GetJobById), new { id = job.Id }, jobResultDTO);
        }

        /// <summary>
        /// Обновление работника
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJob(int id, JobDTO jobDto)
        {
            if (id != jobDto.Id)
            {
                return BadRequest();
            }

            var existingJob = await jobService.GetByIdAsync(id);
            if (existingJob == null)
            {
                return NotFound();
            }

            var job = mapper.Map<Job>(jobDto);
            await jobService.UpdateAsync(job);
            return Ok(job);
        }

        /// <summary>
        /// Удаление работника
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteJob(int id)
        {
            await jobService.DeleteAsync(id);
            return NoContent();
        }
    }
}
