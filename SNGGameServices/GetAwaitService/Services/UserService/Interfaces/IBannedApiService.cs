using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;

namespace GetAwaitService.Services.UserService.Interfaces
{
    public interface IBannedApiService
    {
        Task<IEnumerable<BannedDTO>?> GetAllAsync();
        Task<BannedDTO?> GetByIdAsync(Guid id);
        Task<BannedDTO?> CreateAsync(BannedDTO dto);
        Task<bool> UpdateAsync(Guid id, BannedDTO dto);
        Task<bool> DeleteAsync(Guid id);
        public Task<IEnumerable<BannedDTO>?> GetBannedsByUserId(Guid id);
    }
}
