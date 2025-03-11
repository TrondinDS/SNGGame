using StudioGameService.DB.Model;
using UserService.Repository.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class JobService : IJobService
    {
        protected readonly IJobRepository jobRepository;

        public JobService(IJobRepository jobRepository)
        {
            this.jobRepository = jobRepository;
        }

        public async Task AddAsync(Job job)
        {
            await jobRepository.AddAsync(job);
            await jobRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await jobRepository.GetByIdAsync(id);
            if (user != null)
            {
                jobRepository.DeleteAsync(user);
                await jobRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Job>> GetAllAsync()
        {
            return jobRepository.GetAllAsync();
        }

        public async Task<Job> GetByIdAsync(int id)
        {
            return await jobRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Job job)
        {
            jobRepository.UpdateAsync(job);
            await jobRepository.SaveChangesAsync();
        }
    }
}
