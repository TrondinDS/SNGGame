using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;

namespace FrontService.Services.Interfaces
{
    public interface IBannedApiService
    {
        Task<IEnumerable<BannedDTO>?> GetAllBannedAsync();
        Task<IEnumerable<BannedDTO>?> GetBannedsByUserIdAsync(Guid userId);
        Task<BannedDTO?> GetBannedByIdAsync(Guid id);
        Task<BannedDTO?> CreateBannedAsync(BannedCreateDTO createDto);
        Task<bool> UpdateBannedAsync(BannedDTO bannedDto);
        Task<bool> DeleteBannedAsync(Guid id);
    }
}
