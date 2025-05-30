using Library.Generics.DB.DTO.DTOModelServices.UserService.Job;

namespace GetAwaitService.Services.UserService.Interfaces
{
    public interface IJobApiService
    {
        Task<IEnumerable<JobDTO>?> GetAllJobAsync();
        Task<JobDTO?> GetJobByIdAsync(Guid id);
        Task<JobDTO?> CreateJobAsync(JobCreateDTO jobDto);
        Task<bool> UpdateJobAsync(Guid id, JobDTO jobDto);
        Task<bool> DeleteJobAsync(Guid id);

        Task<IEnumerable<JobDTO?>> GetJobsByUserIdAsync(Guid id);
    }
}
